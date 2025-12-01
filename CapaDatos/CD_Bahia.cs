using CapaDominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Bahia
    {
        private CD_Conexion conexion = new CD_Conexion();

        // MÉTODO PRINCIPAL: Listar todas las bahías
        public List<Bahia> ListarTodas()
        {
            List<Bahia> lista = new List<Bahia>();
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Bahias_ListarTodas", con);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(CrearBahiaDesdeReader(reader));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar todas las bahías: " + ex.Message);
                }
            }
            return lista;
        }

        // MÉTODO: Listar bahías con disponibilidad por fecha
        public List<Bahia> ListarBahiasPorFecha(DateTime fecha)
        {
            List<Bahia> lista = new List<Bahia>();

            // Usamos tu misma lógica de conexión
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Bahias_ListarDisponibilidad", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fecha", fecha);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Creamos el objeto manualmente o reusas tu método privado si lo adaptas
                            Bahia bahia = new Bahia();
                            bahia.BahiaId = Convert.ToInt32(reader["BahiaID"]);
                            bahia.Nombre = reader["Nombre"].ToString();
                            bahia.Descripcion = reader["Descripcion"].ToString();
                            bahia.Capacidad = Convert.ToInt32(reader["Capacidad"]);

                            // Llenamos el estado (solo nombre para visualizar)
                            bahia.EstadoBahia = new EstadoBahia { Nombre = reader["EstadoNombre"].ToString() };

                            // Llenamos la nueva propiedad
                            bahia.CuposDisponibles = Convert.ToInt32(reader["CuposDisponiblesDia"]);

                            lista.Add(bahia);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar disponibilidad de bahías: " + ex.Message);
                }
            }
            return lista;
        }

        public List<BahiaHorarioDTO> ListarHorariosPorFecha(DateTime fecha)
        {
            List<BahiaHorarioDTO> lista = new List<BahiaHorarioDTO>();
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Bahias_ListarHorariosDisponibles", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fecha", fecha);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new BahiaHorarioDTO
                        {
                            BahiaID = Convert.ToInt32(reader["BahiaID"]),
                            NombreBahia = reader["NombreBahia"].ToString(),
                            CapacidadTotal = Convert.ToInt32(reader["Capacidad"]),
                            HoraInicio = (TimeSpan)reader["HoraInicio"],
                            HoraFin = (TimeSpan)reader["HoraFin"],
                            CuposDisponibles = Convert.ToInt32(reader["CapacidadDisponible"])
                        });
                    }
                }
            }
            return lista;
        }

        // MÉTODO: Crear bahía completa
        public string CrearBahiaCompleta(string nombre, string descripcion, int capacidad,
                                        int estadoId, int? usuarioId)
        {
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Bahia_CrearCompleta", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                cmd.Parameters.AddWithValue("@Capacidad", capacidad);
                cmd.Parameters.AddWithValue("@EstadobahiaID", estadoId);

                if (usuarioId.HasValue)
                    cmd.Parameters.AddWithValue("@UsuariosID", usuarioId.Value);
                else
                    cmd.Parameters.AddWithValue("@UsuariosID", DBNull.Value);

                SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);

                try
                {
                    cmd.ExecuteNonQuery();
                    return mensajeParam.Value.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al crear bahía: " + ex.Message);
                }
            }
        }

        // MÉTODO: Actualizar bahía completa
        public string ActualizarBahiaCompleta(int bahiaId, string nombre, string descripcion,
                                             int capacidad, int estadoId, int? usuarioId)
        {
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Bahia_ActualizarCompleta", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BahiaID", bahiaId);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                cmd.Parameters.AddWithValue("@Capacidad", capacidad);
                cmd.Parameters.AddWithValue("@EstadobahiaID", estadoId);

                if (usuarioId.HasValue)
                    cmd.Parameters.AddWithValue("@UsuariosID", usuarioId.Value);
                else
                    cmd.Parameters.AddWithValue("@UsuariosID", DBNull.Value);

                SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);

                try
                {
                    cmd.ExecuteNonQuery();
                    return mensajeParam.Value.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar bahía: " + ex.Message);
                }
            }
        }

        // MÉTODO: Inhabilitar bahía
        public string InhabilitarBahia(int bahiaId)
        {
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Bahia_Inhabilitar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BahiaId", bahiaId);
                SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);
                try
                {
                    cmd.ExecuteNonQuery();
                    return mensajeParam.Value.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al inhabilitar bahía: " + ex.Message);
                }
            }
        }

        // MÉTODO: Habilitar bahía
        public string HabilitarBahia(int bahiaId)
        {
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("UPDATE Bahia SET Activo = 1 WHERE BahiaID = @BahiaId", con);
                cmd.Parameters.AddWithValue("@BahiaId", bahiaId);
                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        return "Bahía habilitada con éxito";
                    else
                        return "Error: No se encontró la bahía especificada";
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al habilitar bahía: " + ex.Message);
                }
            }
        }

        // MÉTODO PRIVADO PARA CREAR OBJETO BAHÍA DESDE DATAREADER
        private Bahia CrearBahiaDesdeReader(SqlDataReader reader)
        {
            return new Bahia
            {
                BahiaId = Convert.ToInt32(reader["BahiaID"]),
                EstadobahiaId = Convert.ToInt32(reader["EstadobahiaID"]),
                UsuariosId = reader["UsuariosID"] != DBNull.Value ? Convert.ToInt32(reader["UsuariosID"]) : (int?)null,
                Nombre = reader["Nombre"].ToString(),
                Descripcion = reader["Descripcion"].ToString(),
                Capacidad = Convert.ToInt32(reader["Capacidad"]),
                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                FechaModificacion = reader["FechaModificacion"] != DBNull.Value ?
                                  Convert.ToDateTime(reader["FechaModificacion"]) : (DateTime?)null,
                Activo = Convert.ToBoolean(reader["Activo"]),
                EstadoBahia = new EstadoBahia
                {
                    EstadobahiaId = Convert.ToInt32(reader["EstadobahiaID"]),
                    Nombre = reader["EstadoNombre"].ToString(),
                    Color = reader["EstadoColor"].ToString()
                }
            };
        }
    }
}