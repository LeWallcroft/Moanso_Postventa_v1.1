using CapaDatos;
using CapaDominio;
using System;
using System.Collections.Generic;

namespace CapaLogicaNegocio
{
    public class CN_Usuario
    {
        private CD_Usuario cdUsuario = new CD_Usuario();

        public Usuario Login(string username, string password)
        {
            return cdUsuario.Login(username, password);
        }

        // MÉTODOS NUEVOS QUE FALTAN:
        public List<Usuario> ListarUsuarios()
        {
            return cdUsuario.ListarUsuarios();
        }

        public bool RegistrarUsuario(Usuario usuario, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(usuario.Username))
            {
                mensaje = "El nombre de usuario no puede estar vacío";
                return false;
            }

            if (string.IsNullOrEmpty(usuario.Password))
            {
                mensaje = "La contraseña no puede estar vacía";
                return false;
            }

            if (string.IsNullOrEmpty(usuario.Rol))
            {
                mensaje = "El rol no puede estar vacío";
                return false;
            }

            return cdUsuario.RegistrarUsuario(usuario, out mensaje);
        }

        public bool EditarUsuario(Usuario usuario, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(usuario.Username))
            {
                mensaje = "El nombre de usuario no puede estar vacío";
                return false;
            }

            if (string.IsNullOrEmpty(usuario.Rol))
            {
                mensaje = "El rol no puede estar vacío";
                return false;
            }

            return cdUsuario.EditarUsuario(usuario, out mensaje);
        }

        public bool EliminarUsuario(int usuarioId, out string mensaje)
        {
            return cdUsuario.EliminarUsuario(usuarioId, out mensaje);
        }
    }
}