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
    public class CD_Cita
    {
        private CD_Conexion conexion = new CD_Conexion();

        public List<CapacidadDia> ConsultarDisponibilidad(DateTime fecha, int? bahiaId)
        {
            List<CapacidadDia> lista = new List<CapacidadDia>();

            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Calendario_ConsultarDisponibilidad", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetros
                cmd.Parameters.AddWithValue("@Fecha", fecha.Date);
                // Si BahiaId es 0 (Todas), enviamos NULL, si no, enviamos el ID
                cmd.Parameters.AddWithValue("@BahiaId", bahiaId.GetValueOrDefault() == 0 ? (object)DBNull.Value : bahiaId);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new CapacidadDia()
                            {
                                CapacidadId = Convert.ToInt32(reader["CapacidadId"]),
                                Bahia = reader["Bahia"].ToString(),
                                // Mapeo de TIME (SQL) a TimeSpan (C#)
                                HoraInicio = (TimeSpan)reader["HoraInicio"],
                                HoraFin = (TimeSpan)reader["HoraFin"],
                                CapacidadMax = Convert.ToInt32(reader["CapacidadMax"]),
                                CapacidadReservada = Convert.ToInt32(reader["CapacidadReservada"]),
                                CuposLibres = Convert.ToInt32(reader["CuposLibres"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al consultar la disponibilidad: " + ex.Message);
                }
            }
            return lista;
        }

        public bool CrearCita(Cita cita, int capacidadId)
        {
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Cita_Crear", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetros basados en el SP sp_Cita_Crear
                cmd.Parameters.AddWithValue("@CapacidadId", capacidadId);
                cmd.Parameters.AddWithValue("@ClienteId", cita.ClienteId);
                cmd.Parameters.AddWithValue("@VehiculoId", cita.VehiculoId);
                cmd.Parameters.AddWithValue("@ServicioId", cita.ServicioId);
                cmd.Parameters.AddWithValue("@TecnicoId", cita.TecnicoId);
                cmd.Parameters.AddWithValue("@Fecha", cita.FechaCita.Date);

                try
                {
                    cmd.ExecuteNonQuery();
                    return true; // Si no hay excepción, la transacción fue exitosa
                }
                catch (SqlException ex)
                {
                    // El SP usa THROW si falla la validación de cupo o existencia
                    throw new Exception($"Error en la creación de la cita (BD): {ex.Message}");
                }
            }
        }
    }
}
