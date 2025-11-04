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
    public class CD_Servicio
    {
        private CD_Conexion conexion = new CD_Conexion();

        // Método: Listar servicios activos (Usa sp_Servicios_ListarActivos)
        public List<Servicio> ListarServiciosActivos()
        {
            List<Servicio> lista = new List<Servicio>();

            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Servicios_ListarActivos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Servicio()
                            {
                                ServicioId = Convert.ToInt32(reader["ServicioId"]),
                                Nombre = reader["Nombre"].ToString()
                                // El SP solo devuelve ID y Nombre, suficiente para el combo
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar servicios activos: " + ex.Message);
                }
            }
            return lista;
        }
    }
}
