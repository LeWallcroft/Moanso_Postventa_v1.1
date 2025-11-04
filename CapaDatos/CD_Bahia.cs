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
    public class CD_Bahia
    {
        private CD_Conexion conexion = new CD_Conexion();

        public List<Bahia> ListarActivas()
        {
            List<Bahia> lista = new List<Bahia>();

            // Usaremos el SP ya creado en la BD: sp_Bahias_ListarActivas
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Bahias_ListarActivas", con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Bahia()
                            {
                                BahiaId = Convert.ToInt32(reader["BahiaId"]),
                                Nombre = reader["Nombre"].ToString(),
                                Tipo = reader["Tipo"].ToString(),
                                // El SP solo devuelve activas, pero se mapean los campos devueltos.
                                Activo = true
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores. En un entorno real, esto se loguearía.
                    throw new Exception("Error al listar bahías activas: " + ex.Message);
                }
            }
            return lista;
        }
    }
}
