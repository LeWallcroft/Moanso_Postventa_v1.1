using CapaDominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Tecnico
    {
        private CD_Conexion conexion = new CD_Conexion();

        public List<Tecnico> ListarTodos()
        {
            List<Tecnico> lista = new List<Tecnico>();
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Tecnicos_ListarTodos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(CrearTecnicoDesdeReader(reader));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar técnicos: " + ex.Message);
                }
            }
            return lista;
        }

        // ✅ NUEVO MÉTODO: Listar técnicos activos
        public List<Tecnico> ListarActivos()
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
                            lista.Add(CrearTecnicoDesdeReader(reader));
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

        // ✅ NUEVO MÉTODO: Listar usuarios disponibles para técnicos
        public List<Usuario> ListarUsuariosParaTecnicos()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Usuarios_ListarParaTecnicos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Usuario
                            {
                                UsuariosID = Convert.ToInt32(reader["UsuariosID"]),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Email = reader["Email"].ToString(),
                                Rol = reader["Rol"].ToString(),
                                Activo = Convert.ToBoolean(reader["Activo"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar usuarios para técnicos: " + ex.Message);
                }
            }
            return lista;
        }

        // ✅ NUEVO MÉTODO: Listar TODOS los usuarios técnicos activos (sin filtrar por asignación)
        public List<Usuario> ListarTodosUsuariosTecnicos()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection con = conexion.AbrirConexion())
            {
                // Consulta directa que lista TODOS los usuarios con rol Tecnico activos
                string query = @"
                    SELECT 
                        u.UsuariosID,
                        u.Nombre,
                        u.Apellido,
                        u.Email,
                        u.Rol,
                        u.Activo
                    FROM Usuarios u
                    WHERE u.Rol = 'Tecnico' 
                        AND u.Activo = 1
                    ORDER BY u.Nombre, u.Apellido";

                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Usuario
                            {
                                UsuariosID = Convert.ToInt32(reader["UsuariosID"]),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Email = reader["Email"].ToString(),
                                Rol = reader["Rol"].ToString(),
                                Activo = Convert.ToBoolean(reader["Activo"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar todos los usuarios técnicos: " + ex.Message);
                }
            }
            return lista;
        }

        public bool CrearTecnico(int? usuarioId, string especialidad, DateTime fechaContratacion,
                                decimal salario, bool disponible, out string mensaje)
        {
            mensaje = string.Empty;
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Tecnico_Crear", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // ✅ CORRECCIÓN: Manejar correctamente el parámetro UsuariosID
                if (usuarioId.HasValue && usuarioId.Value > 0)
                    cmd.Parameters.AddWithValue("@UsuariosID", usuarioId.Value);
                else
                    cmd.Parameters.AddWithValue("@UsuariosID", DBNull.Value);

                cmd.Parameters.AddWithValue("@Especialidad", especialidad);
                cmd.Parameters.AddWithValue("@FechaContratacion", fechaContratacion);
                cmd.Parameters.AddWithValue("@Salario", salario);
                cmd.Parameters.AddWithValue("@Disponible", disponible);

                SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);

                try
                {
                    cmd.ExecuteNonQuery();
                    mensaje = mensajeParam.Value.ToString();

                    // ✅ CORRECCIÓN: Retornar boolean según el mensaje
                    return !mensaje.Contains("Error") && !mensaje.Contains("existe");
                }
                catch (Exception ex)
                {
                    mensaje = "Error al crear técnico: " + ex.Message;
                    return false;
                }
            }
        }

        public bool ActualizarTecnico(int tecnicoId, int? usuarioId, string especialidad,
                                     DateTime fechaContratacion, decimal salario, bool disponible,
                                     bool activo, out string mensaje)
        {
            mensaje = string.Empty;
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Tecnico_Actualizar", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TecnicoID", tecnicoId);

                // ✅ CORRECCIÓN: Manejar correctamente el parámetro UsuariosID
                if (usuarioId.HasValue && usuarioId.Value > 0)
                    cmd.Parameters.AddWithValue("@UsuariosID", usuarioId.Value);
                else
                    cmd.Parameters.AddWithValue("@UsuariosID", DBNull.Value);

                cmd.Parameters.AddWithValue("@Especialidad", especialidad);
                cmd.Parameters.AddWithValue("@FechaContratacion", fechaContratacion);
                cmd.Parameters.AddWithValue("@Salario", salario);
                cmd.Parameters.AddWithValue("@Disponible", disponible);
                cmd.Parameters.AddWithValue("@Activo", activo);

                SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);

                try
                {
                    cmd.ExecuteNonQuery();
                    mensaje = mensajeParam.Value.ToString();

                    // ✅ CORRECCIÓN: Retornar boolean según el mensaje
                    return !mensaje.Contains("Error") && !mensaje.Contains("existe");
                }
                catch (Exception ex)
                {
                    mensaje = "Error al actualizar técnico: " + ex.Message;
                    return false;
                }
            }
        }

        public bool InactivarTecnico(int tecnicoId, out string mensaje)
        {
            mensaje = string.Empty;
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Tecnico_Inactivar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TecnicoID", tecnicoId);

                SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);

                try
                {
                    cmd.ExecuteNonQuery();
                    mensaje = mensajeParam.Value.ToString();

                    // ✅ CORRECCIÓN: Misma lógica de retorno que ActivarTecnico
                    return !mensaje.Contains("Error");
                }
                catch (Exception ex)
                {
                    mensaje = "Error al inactivar técnico: " + ex.Message;
                    return false;
                }
            }
        }

        public bool ActivarTecnico(int tecnicoId, out string mensaje)
        {
            mensaje = string.Empty;
            using (SqlConnection con = conexion.AbrirConexion())
            {
                // ✅ CORRECCIÓN: Usar stored procedure en lugar de consulta directa
                SqlCommand cmd = new SqlCommand("sp_Tecnico_Activar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TecnicoID", tecnicoId);

                SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);

                try
                {
                    cmd.ExecuteNonQuery();
                    mensaje = mensajeParam.Value.ToString();

                    // ✅ CORRECCIÓN: Retornar true si fue exitoso
                    return !mensaje.Contains("Error");
                }
                catch (Exception ex)
                {
                    mensaje = "Error al activar técnico: " + ex.Message;
                    return false;
                }
            }
        }

        // ✅ NUEVO MÉTODO: Obtener técnico por ID
        public Tecnico ObtenerTecnicoPorId(int tecnicoId)
        {
            using (SqlConnection con = conexion.AbrirConexion())
            {
                string query = @"
                    SELECT t.TecnicoID, t.UsuariosID, t.Especialidad, t.FechaContratacion, 
                           t.Salario, t.Disponible, t.Activo,
                           u.Nombre, u.Apellido, u.Email, u.Rol
                    FROM Tecnico t
                    LEFT JOIN Usuarios u ON t.UsuariosID = u.UsuariosID
                    WHERE t.TecnicoID = @TecnicoID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TecnicoID", tecnicoId);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return CrearTecnicoDesdeReader(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener técnico: " + ex.Message);
                }
            }
            return null;
        }

        // ✅ NUEVO MÉTODO: Verificar si usuario ya es técnico
        public bool UsuarioEsTecnico(int usuarioId)
        {
            using (SqlConnection con = conexion.AbrirConexion())
            {
                string query = "SELECT COUNT(1) FROM Tecnico WHERE UsuariosID = @UsuariosID AND Activo = 1";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UsuariosID", usuarioId);

                try
                {
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al verificar si usuario es técnico: " + ex.Message);
                }
            }
        }

        private Tecnico CrearTecnicoDesdeReader(SqlDataReader reader)
        {
            try
            {
                var tecnico = new Tecnico
                {
                    TecnicoID = Convert.ToInt32(reader["TecnicoID"]),
                    UsuariosId = reader["UsuariosID"] != DBNull.Value ? Convert.ToInt32(reader["UsuariosID"]) : (int?)null,
                    Especialidad = reader["Especialidad"].ToString(),
                    FechaContratacion = Convert.ToDateTime(reader["FechaContratacion"]),
                    Salario = Convert.ToDecimal(reader["Salario"]),
                    Disponible = Convert.ToBoolean(reader["Disponible"]),
                    Activo = Convert.ToBoolean(reader["Activo"])
                };

                // ✅ CORRECCIÓN DEFINITIVA: Usar SOLO las columnas que existen
                tecnico.Usuario = new Usuario
                {
                    UsuariosID = tecnico.UsuariosId ?? 0,
                    Nombre = reader["Nombre"] != DBNull.Value ? reader["Nombre"].ToString() : string.Empty,
                    Apellido = reader["Apellido"] != DBNull.Value ? reader["Apellido"].ToString() : string.Empty,
                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty,
                    Rol = "Tecnico", // ✅ VALOR FIJO porque todos son técnicos
                    Activo = true // ✅ VALOR FIJO porque el SP ya filtra por activos
                };

                return tecnico;
            }
            catch (Exception ex)
            {
                // ✅ MEJOR MENSAJE DE ERROR para debugging
                throw new Exception($"Error al crear técnico desde reader. Columnas disponibles: {GetColumnNames(reader)}. Error: {ex.Message}");
            }
        }

        // ✅ MÉTODO AUXILIAR para debugging
        private string GetColumnNames(SqlDataReader reader)
        {
            var columns = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                columns.Add(reader.GetName(i));
            }
            return string.Join(", ", columns);
        }
    }
}