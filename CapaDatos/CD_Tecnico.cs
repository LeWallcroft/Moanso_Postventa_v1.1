using CapaDominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Tecnico
    {
        private CD_Conexion conexion = new CD_Conexion();

        // Método: Listar técnicos activos (Usa sp_Tecnicos_ListarActivos)
        public List<Tecnico> ListarTecnicosActivos()
        {
            List<Tecnico> lista = new List<Tecnico>();

            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Tecnicos_ListarActivos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Tecnico()
                            {
                                TecnicoId = Convert.ToInt32(reader["TecnicoId"]),
                                Nombre = reader["Nombre"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar técnicos activos: " + ex.Message);
                }
            }
            return lista;
        }
    }
}
