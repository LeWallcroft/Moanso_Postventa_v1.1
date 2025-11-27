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

        public Usuario Login(string email, string password)
        {
            Usuario usuarioLogueado = null;

            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Usuario_Login", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PasswordHash", password);

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

                        ActualizarUltimoLogin(usuarioLogueado.UsuariosID);
                    }
                }
            }
            return usuarioLogueado;
        }

        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection con = conexion.AbrirConexion())
            {
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

        public bool RegistrarUsuario(Usuario usuario, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_Usuario_Registrar", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", usuario.PasswordHash);
                    cmd.Parameters.AddWithValue("@Rol", usuario.Rol);

                    SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    mensajeParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(mensajeParam);

                    cmd.ExecuteNonQuery();

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

        public bool EditarUsuario(Usuario usuario, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_Usuario_Editar", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UsuariosID", usuario.UsuariosID);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                    cmd.Parameters.AddWithValue("@Activo", usuario.Activo);

                    if (usuario.PasswordHash != "Dejar en blanco para no cambiar" && !string.IsNullOrEmpty(usuario.PasswordHash))
                    {
                        cmd.Parameters.AddWithValue("@PasswordHash", usuario.PasswordHash);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PasswordHash", DBNull.Value);
                    }

                    SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    mensajeParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(mensajeParam);

                    cmd.ExecuteNonQuery();

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

        // CORREGIDO: Método para cambiar estado del usuario (Habilitar/Inhabilitar)
        public bool CambiarEstadoUsuario(int usuariosID, bool nuevoEstado, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                {
                    // USAR EL NUEVO STORED PROCEDURE ESPECÍFICO
                    SqlCommand cmd = new SqlCommand("sp_Usuario_CambiarEstado", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Solo enviar los parámetros necesarios
                    cmd.Parameters.AddWithValue("@UsuariosID", usuariosID);
                    cmd.Parameters.AddWithValue("@Activo", nuevoEstado);

                    SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    mensajeParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(mensajeParam);

                    cmd.ExecuteNonQuery();

                    mensaje = mensajeParam.Value.ToString();

                    // Personalizar mensaje según la acción
                    if (mensaje.Contains("éxito"))
                    {
                        mensaje = nuevoEstado ?
                            "Usuario habilitado con éxito" :
                            "Usuario inhabilitado con éxito";
                    }

                    return !mensaje.Contains("Error");
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error al cambiar estado: {ex.Message}";
                return false;
            }
        }

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
                // Primero obtener el usuario actual para mantener los otros datos
                Usuario usuarioActual = ObtenerUsuarioPorId(usuariosID);

                if (usuarioActual == null)
                {
                    mensaje = "El usuario no existe";
                    return false;
                }

                using (SqlConnection con = conexion.AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_Usuario_Editar", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UsuariosID", usuariosID);
                    cmd.Parameters.AddWithValue("@Nombre", usuarioActual.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuarioActual.Apellido);
                    cmd.Parameters.AddWithValue("@Email", usuarioActual.Email);
                    cmd.Parameters.AddWithValue("@Rol", usuarioActual.Rol);
                    cmd.Parameters.AddWithValue("@Activo", usuarioActual.Activo);
                    cmd.Parameters.AddWithValue("@PasswordHash", nuevaPassword);

                    SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    mensajeParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(mensajeParam);

                    cmd.ExecuteNonQuery();

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
    }
}