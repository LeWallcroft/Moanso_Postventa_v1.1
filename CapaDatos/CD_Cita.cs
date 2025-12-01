using CapaDominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CapaDatos
{
    //error en el parametro de crear cita
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

        public bool RegistrarCita(int vehiculoId, int usuarioId, DateTime fecha, int duracionMinutos, string observaciones, int servicioId, decimal precio, out string mensaje)
        {
            mensaje = string.Empty;
            // 1. Construir el XML para el servicio (El SP lo requiere así)
            // Estructura: <Servicios><Servicio><ServicioID>1</ServicioID><Cantidad>1</Cantidad><PrecioUnitario>100</PrecioUnitario></Servicio></Servicios>
            string xmlServicios = $@"
                <Servicios>
                    <Servicio>
                        <ServicioID>{servicioId}</ServicioID>
                        <Cantidad>1</Cantidad>
                        <PrecioUnitario>{precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}</PrecioUnitario>
                    </Servicio>
                </Servicios>";

            try
            {
                using (SqlConnection con = new CD_Conexion().AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCita", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros obligatorios del SP
                    cmd.Parameters.AddWithValue("@VehiculoID", vehiculoId);
                    cmd.Parameters.AddWithValue("@UsuarioID", usuarioId);
                    cmd.Parameters.AddWithValue("@FechaCita", fecha);
                    cmd.Parameters.AddWithValue("@DuracionEstimada", duracionMinutos);
                    cmd.Parameters.AddWithValue("@Observaciones", observaciones);
                    cmd.Parameters.AddWithValue("@Prioridad", 1); // Prioridad baja por defecto

                    // El parámetro XML
                    cmd.Parameters.Add("@Servicios", SqlDbType.Xml).Value = xmlServicios;

                    // Ejecutar
                    // Nota: El SP hace un SELECT al final, así que usamos ExecuteScalar o Reader
                    // Pero para confirmar éxito, si no lanza error, asumimos que pasó.
                    cmd.ExecuteNonQuery();

                    mensaje = "Cita registrada correctamente.";
                    return true;
                }
            }
            catch (SqlException sqlex)
            {
                // Capturamos errores específicos del SP (como "No hay bahías disponibles")
                mensaje = sqlex.Message;
                return false;
            }
            catch (Exception ex)
            {
                mensaje = "Error en capa datos: " + ex.Message;
                return false;
            }
        }
       

        public bool RegistrarCita(int vehiculoId, int usuarioId, DateTime fecha, int duracionMinutos, string observaciones, int servicioId, decimal precio, out string mensaje)
        {
            mensaje = string.Empty;
            // 1. Construir el XML para el servicio (El SP lo requiere así)
            // Estructura: <Servicios><Servicio><ServicioID>1</ServicioID><Cantidad>1</Cantidad><PrecioUnitario>100</PrecioUnitario></Servicio></Servicios>
            string xmlServicios = $@"
                <Servicios>
                    <Servicio>
                        <ServicioID>{servicioId}</ServicioID>
                        <Cantidad>1</Cantidad>
                        <PrecioUnitario>{precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}</PrecioUnitario>
                    </Servicio>
                </Servicios>";

            try
            {
                using (SqlConnection con = new CD_Conexion().AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCita", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros obligatorios del SP
                    cmd.Parameters.AddWithValue("@VehiculoID", vehiculoId);
                    cmd.Parameters.AddWithValue("@UsuarioID", usuarioId);
                    cmd.Parameters.AddWithValue("@FechaCita", fecha);
                    cmd.Parameters.AddWithValue("@DuracionEstimada", duracionMinutos);
                    cmd.Parameters.AddWithValue("@Observaciones", observaciones);
                    cmd.Parameters.AddWithValue("@Prioridad", 1); // Prioridad baja por defecto

                    // El parámetro XML
                    cmd.Parameters.Add("@Servicios", SqlDbType.Xml).Value = xmlServicios;

                    // Ejecutar
                    // Nota: El SP hace un SELECT al final, así que usamos ExecuteScalar o Reader
                    // Pero para confirmar éxito, si no lanza error, asumimos que pasó.
                    cmd.ExecuteNonQuery();

                    mensaje = "Cita registrada correctamente.";
                    return true;
                }
            }
            catch (SqlException sqlex)
            {
                // Capturamos errores específicos del SP (como "No hay bahías disponibles")
                mensaje = sqlex.Message;
                return false;
            }
            catch (Exception ex)
            {
                mensaje = "Error en capa datos: " + ex.Message;
                return false;
            }
        }

        public int RegistrarCitaYDevolverId(
         int vehiculoId,
         int usuarioId,
         DateTime fechaCita,
         int duracionEstimada,
         string observaciones,
         int prioridad,
         int servicioId,
         decimal precio,
         out string mensaje)
        {
            mensaje = string.Empty;

            // 1. Construir XML de servicios (igual que en RegistrarCita)
            string xmlServicios = $@"
                <Servicios>
                    <Servicio>
                        <ServicioID>{servicioId}</ServicioID>
                        <Cantidad>1</Cantidad>
                        <PrecioUnitario>{precio.ToString("0.00", CultureInfo.InvariantCulture)}</PrecioUnitario>
                    </Servicio>
                </Servicios>";

            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("sp_RegistrarCita", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehiculoID", vehiculoId);
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioId);
                cmd.Parameters.AddWithValue("@FechaCita", fechaCita);
                cmd.Parameters.AddWithValue("@DuracionEstimada", duracionEstimada);
                cmd.Parameters.AddWithValue("@Observaciones", (object)observaciones ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Prioridad", prioridad);

                // ⬅️ AHORA SÍ enviamos el XML de servicios
                cmd.Parameters.Add("@Servicios", SqlDbType.Xml).Value = xmlServicios;

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int citaId = reader["CitaID"] != DBNull.Value
                            ? Convert.ToInt32(reader["CitaID"])
                            : 0;

                        mensaje = reader["Mensaje"]?.ToString();
                        return citaId;
                    }
                }
            }

            return 0;
        }

    }
}
