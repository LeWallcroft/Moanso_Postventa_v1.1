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

        public Usuario Login(string username, string password)
        {
            Usuario usuarioLogueado = null;

            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Usuario_Login", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuarioLogueado = new Usuario
                        {
                            UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                            Username = reader["Username"].ToString(),
                            Rol = reader["Rol"].ToString(),
                            Activo = Convert.ToBoolean(reader["Activo"])
                        };
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
                            UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                            Username = reader["Username"].ToString(),
                            Rol = reader["Rol"].ToString(),
                            Activo = Convert.ToBoolean(reader["Activo"]),
                            UltimoAcceso = reader["UltimoAcceso"] != DBNull.Value ?
                                         Convert.ToDateTime(reader["UltimoAcceso"]) : (DateTime?)null
                        });
                    }
                }
            }
            return lista;
        }

        public bool RegistrarUsuario(Usuario usuario, out string mensaje)
        {
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_Usuario_Registrar", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Username", usuario.Username);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@Rol", usuario.Rol);

                    SqlParameter outputMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    outputMensaje.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputMensaje);

                    cmd.ExecuteNonQuery();

                    mensaje = outputMensaje.Value.ToString();
                    return mensaje.ToLower().Contains("éxito");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }

        public bool EditarUsuario(Usuario usuario, out string mensaje)
        {
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_Usuario_Editar", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UsuarioId", usuario.UsuarioId);
                    cmd.Parameters.AddWithValue("@Username", usuario.Username);
                    cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                    cmd.Parameters.AddWithValue("@Activo", usuario.Activo);

                    if (!string.IsNullOrEmpty(usuario.Password))
                    {
                        cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    }

                    SqlParameter outputMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    outputMensaje.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputMensaje);

                    cmd.ExecuteNonQuery();

                    mensaje = outputMensaje.Value.ToString();
                    return mensaje.ToLower().Contains("éxito");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }

        public bool EliminarUsuario(int usuarioId, out string mensaje)
        {
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_Usuario_Eliminar", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

                    SqlParameter outputMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    outputMensaje.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputMensaje);

                    cmd.ExecuteNonQuery();

                    mensaje = outputMensaje.Value.ToString();
                    return mensaje.ToLower().Contains("éxito");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }
    }
}