using CapaDominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Usuario
    {
        private CD_Conexion conexion = new CD_Conexion();

        // CORREGIDO: Usar procedimiento almacenado para login
        public Usuario Login(string email, string password)
        {
            Usuario usuarioLogueado = null;

            using (SqlConnection con = conexion.AbrirConexion())
            {
                // CORREGIDO: Usar stored procedure en lugar de consulta directa
                SqlCommand cmd = new SqlCommand("sp_Usuario_Login", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PasswordHash", password); // Enviar contraseña directamente

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuarioLogueado = new Usuario
                        {
                            UsuariosID = Convert.ToInt32(reader["UsuariosID"]),
                            Nombre = reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            Email = reader["Email"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Rol = reader["Rol"].ToString(),
                            FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                            UltimoLogin = reader["UltimoLogin"] != DBNull.Value ?
                                        Convert.ToDateTime(reader["UltimoLogin"]) : (DateTime?)null,
                            Activo = Convert.ToBoolean(reader["Activo"])
                        };

                        // Actualizar último login
                        ActualizarUltimoLogin(usuarioLogueado.UsuariosID);
                    }
                }
            }
            return usuarioLogueado;
        }

        // CORREGIDO: Usar stored procedure para listar usuarios
        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection con = conexion.AbrirConexion())
            {
                // CORREGIDO: Usar stored procedure
                SqlCommand cmd = new SqlCommand("sp_Usuario_Listar", con);
                cmd.CommandType = CommandType.StoredProcedure;

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
                            FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                            UltimoLogin = reader["UltimoLogin"] != DBNull.Value ?
                                        Convert.ToDateTime(reader["UltimoLogin"]) : (DateTime?)null,
                            Activo = Convert.ToBoolean(reader["Activo"])
                        });
                    }
                }
            }
            return lista;
        }

        // CORREGIDO: Usar stored procedure para registrar usuario
        public bool RegistrarUsuario(Usuario usuario, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                {
                    // CORREGIDO: Usar stored procedure
                    SqlCommand cmd = new SqlCommand("sp_Usuario_Registrar", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", usuario.PasswordHash); // Contraseña directa
                    cmd.Parameters.AddWithValue("@Rol", usuario.Rol);

                    // CORREGIDO: Agregar parámetro de salida
                    SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    mensajeParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(mensajeParam);

                    cmd.ExecuteNonQuery();

                    // Obtener mensaje del stored procedure
                    mensaje = mensajeParam.Value.ToString();

                    return !mensaje.Contains("Error") && !mensaje.Contains("existe");
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}";
                return false;
            }
        }

        // CORREGIDO: Usar stored procedure para editar usuario
        public bool EditarUsuario(Usuario usuario, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                {
                    // CORREGIDO: Usar stored procedure
                    SqlCommand cmd = new SqlCommand("sp_Usuario_Editar", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UsuariosID", usuario.UsuariosID);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                    cmd.Parameters.AddWithValue("@Activo", usuario.Activo);

                    // CORREGIDO: Solo enviar password si no es la placeholder
                    if (usuario.PasswordHash != "Dejar en blanco para no cambiar" && !string.IsNullOrEmpty(usuario.PasswordHash))
                    {
                        cmd.Parameters.AddWithValue("@PasswordHash", usuario.PasswordHash);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PasswordHash", DBNull.Value);
                    }

                    // CORREGIDO: Agregar parámetro de salida
                    SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    mensajeParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(mensajeParam);

                    cmd.ExecuteNonQuery();

                    // Obtener mensaje del stored procedure
                    mensaje = mensajeParam.Value.ToString();

                    return !mensaje.Contains("Error") && !mensaje.Contains("existe");
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}";
                return false;
            }
        }

        // CORREGIDO: Usar stored procedure para eliminar usuario
        public bool EliminarUsuario(int usuariosID, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                {
                    // CORREGIDO: Usar stored procedure
                    SqlCommand cmd = new SqlCommand("sp_Usuario_Eliminar", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UsuariosID", usuariosID);

                    // CORREGIDO: Agregar parámetro de salida
                    SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    mensajeParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(mensajeParam);

                    cmd.ExecuteNonQuery();

                    // Obtener mensaje del stored procedure
                    mensaje = mensajeParam.Value.ToString();

                    return !mensaje.Contains("Error") && !mensaje.Contains("existe");
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}";
                return false;
            }
        }

        // MÉTODOS NUEVOS ADICIONALES
        public Usuario ObtenerUsuarioPorId(int usuariosID)
        {
            Usuario usuario = null;

            using (SqlConnection con = conexion.AbrirConexion())
            {
                string query = @"
                    SELECT UsuariosID, Nombre, Apellido, Email, Rol, 
                           FechaRegistro, UltimoLogin, Activo
                    FROM Usuarios 
                    WHERE UsuariosID = @UsuariosID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UsuariosID", usuariosID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            UsuariosID = Convert.ToInt32(reader["UsuariosID"]),
                            Nombre = reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            Email = reader["Email"].ToString(),
                            Rol = reader["Rol"].ToString(),
                            FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                            UltimoLogin = reader["UltimoLogin"] != DBNull.Value ?
                                        Convert.ToDateTime(reader["UltimoLogin"]) : (DateTime?)null,
                            Activo = Convert.ToBoolean(reader["Activo"])
                        };
                    }
                }
            }
            return usuario;
        }

        public bool CambiarPassword(int usuariosID, string nuevaPassword, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                {
                    // CORREGIDO: Usar stored procedure para cambiar contraseña
                    SqlCommand cmd = new SqlCommand("sp_Usuario_Editar", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UsuariosID", usuariosID);
                    cmd.Parameters.AddWithValue("@Nombre", DBNull.Value); // No cambiar nombre
                    cmd.Parameters.AddWithValue("@Apellido", DBNull.Value); // No cambiar apellido
                    cmd.Parameters.AddWithValue("@Email", DBNull.Value); // No cambiar email
                    cmd.Parameters.AddWithValue("@Rol", DBNull.Value); // No cambiar rol
                    cmd.Parameters.AddWithValue("@Activo", DBNull.Value); // No cambiar estado
                    cmd.Parameters.AddWithValue("@PasswordHash", nuevaPassword); // CORREGIDO: Contraseña directa

                    // CORREGIDO: Agregar parámetro de salida
                    SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    mensajeParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(mensajeParam);

                    cmd.ExecuteNonQuery();

                    // Obtener mensaje del stored procedure
                    mensaje = mensajeParam.Value.ToString();

                    return !mensaje.Contains("Error");
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}";
                return false;
            }
        }

        public List<Usuario> ListarUsuariosPorRol(string rol)
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection con = conexion.AbrirConexion())
            {
                string query = @"
                    SELECT UsuariosID, Nombre, Apellido, Email, Rol, 
                           FechaRegistro, UltimoLogin, Activo
                    FROM Usuarios 
                    WHERE Rol = @Rol AND Activo = 1
                    ORDER BY Nombre, Apellido";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Rol", rol);

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
                            FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                            UltimoLogin = reader["UltimoLogin"] != DBNull.Value ?
                                        Convert.ToDateTime(reader["UltimoLogin"]) : (DateTime?)null,
                            Activo = Convert.ToBoolean(reader["Activo"])
                        });
                    }
                }
            }
            return lista;
        }

        // MÉTODOS PRIVADOS DE AYUDA
        private void ActualizarUltimoLogin(int usuariosID)
        {
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                {
                    string query = @"
                        UPDATE Usuarios 
                        SET UltimoLogin = GETDATE() 
                        WHERE UsuariosID = @UsuariosID";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UsuariosID", usuariosID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                // Silenciosamente falla la actualización del último login
            }
        }
        // AGREGAR este método si no existe (para crear objetos Usuario desde DataReader)
        private Usuario CrearUsuarioDesdeReader(SqlDataReader reader)
        {
            return new Usuario
            {
                UsuariosID = Convert.ToInt32(reader["UsuariosID"]),
                Nombre = reader["Nombre"].ToString(),
                Apellido = reader["Apellido"].ToString(),
                Email = reader["Email"].ToString(),
                PasswordHash = reader["PasswordHash"].ToString(),
                Rol = reader["Rol"].ToString(),
                FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                UltimoLogin = reader["UltimoLogin"] != DBNull.Value ?
                            Convert.ToDateTime(reader["UltimoLogin"]) : (DateTime?)null,
                Activo = Convert.ToBoolean(reader["Activo"])
            };
        }

    }
}