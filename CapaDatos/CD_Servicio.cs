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
        public List<Servicio> ListarServiciosActivos()
        {
            List<Servicio> lista = new List<Servicio>();

            CD_Conexion conexionManager = new CD_Conexion();

            using (SqlConnection conexion = conexionManager.AbrirConexion())
            {
                try
                {
                    string query = @"SELECT ServicioId, Codigo, Nombre, DuracionMin, 
                                            PrecioBase, Activo, Tipo 
                                     FROM Servicios 
                                     WHERE Activo = 1";

                    SqlCommand cmd = new SqlCommand(query, conexion);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Servicio()
                            {
                                ServicioId = Convert.ToInt32(reader["ServicioId"]),
                                Codigo = reader["Codigo"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                DuracionMin = Convert.ToInt32(reader["DuracionMin"]),
                                PrecioBase = Convert.ToDecimal(reader["PrecioBase"]),
                                Activo = Convert.ToBoolean(reader["Activo"]),
                                Tipo = reader["Tipo"] != DBNull.Value ? reader["Tipo"].ToString() : "General"
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en CD_Servicio: " + ex.Message);
                }
            }

            return lista;
        }
    }
}