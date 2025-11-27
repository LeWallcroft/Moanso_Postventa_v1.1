using CapaDatos;
using CapaDominio;
using System;
using System.Collections.Generic;

namespace CapaLogicaNegocio
{
    public class CN_Usuario
    {
        private CD_Usuario cdUsuario = new CD_Usuario();

        // Propiedad para acceder al usuario actual sin crear dependencia circular
        public static Usuario UsuarioActual { get; set; }

        public Usuario Login(string email, string password)
        {
            var usuario = cdUsuario.Login(email, password);
            if (usuario != null)
            {
                UsuarioActual = usuario;
            }
            return usuario;
        }

        public List<Usuario> ListarUsuarios()
        {
            return cdUsuario.ListarUsuarios();
        }

        public bool RegistrarUsuario(Usuario usuario, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(usuario.Nombre))
            {
                mensaje = "El nombre no puede estar vacío";
                return false;
            }

            if (string.IsNullOrEmpty(usuario.Apellido))
            {
                mensaje = "El apellido no puede estar vacío";
                return false;
            }

            if (string.IsNullOrEmpty(usuario.Email))
            {
                mensaje = "El email no puede estar vacío";
                return false;
            }

            if (string.IsNullOrEmpty(usuario.PasswordHash))
            {
                mensaje = "La contraseña no puede estar vacía";
                return false;
            }

            if (string.IsNullOrEmpty(usuario.Rol))
            {
                mensaje = "El rol no puede estar vacío";
                return false;
            }

            if (!IsValidEmail(usuario.Email))
            {
                mensaje = "El formato del email no es válido";
                return false;
            }

            if (!IsValidRol(usuario.Rol))
            {
                mensaje = "El rol especificado no es válido";
                return false;
            }

            return cdUsuario.RegistrarUsuario(usuario, out mensaje);
        }

        public bool EditarUsuario(Usuario usuario, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(usuario.Nombre))
            {
                mensaje = "El nombre no puede estar vacío";
                return false;
            }

            if (string.IsNullOrEmpty(usuario.Apellido))
            {
                mensaje = "El apellido no puede estar vacío";
                return false;
            }

            if (string.IsNullOrEmpty(usuario.Email))
            {
                mensaje = "El email no puede estar vacío";
                return false;
            }

            if (string.IsNullOrEmpty(usuario.Rol))
            {
                mensaje = "El rol no puede estar vacío";
                return false;
            }

            if (!IsValidEmail(usuario.Email))
            {
                mensaje = "El formato del email no es válido";
                return false;
            }

            if (!IsValidRol(usuario.Rol))
            {
                mensaje = "El rol especificado no es válido";
                return false;
            }

            return cdUsuario.EditarUsuario(usuario, out mensaje);
        }

        // NUEVO: Método para cambiar estado del usuario (Habilitar/Inhabilitar)
        public bool CambiarEstadoUsuario(int usuariosID, bool nuevoEstado, out string mensaje)
        {
            mensaje = string.Empty;

            // Validar que el usuario existe
            var usuario = cdUsuario.ObtenerUsuarioPorId(usuariosID);
            if (usuario == null)
            {
                mensaje = "El usuario no existe";
                return false;
            }

            // Validar que no se está intentando cambiar el estado del usuario actual
            if (UsuarioActual != null &&
                UsuarioActual.UsuariosID == usuariosID &&
                !nuevoEstado)
            {
                mensaje = "No puede inhabilitar su propio usuario";
                return false;
            }

            return cdUsuario.CambiarEstadoUsuario(usuariosID, nuevoEstado, out mensaje);
        }

        // ✅ CORRECCIÓN: Agregar este método para obtener técnicos
        public List<Usuario> ListarTecnicos()
        {
            return cdUsuario.ListarUsuariosPorRol("Tecnico");
        }

        // ✅ CORRECCIÓN: Agregar este método para obtener técnicos activos
        public List<Usuario> ListarTecnicosActivos()
        {
            var todosUsuarios = cdUsuario.ListarUsuarios();
            return todosUsuarios.FindAll(u => u.Rol == "Tecnico" && u.Activo);
        }

        // MÉTODOS DE VALIDACIÓN

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // ✅ CORRECCIÓN CRÍTICA: Agregar "Tecnico" a los roles permitidos
        private bool IsValidRol(string rol)
        {
            var rolesPermitidos = new List<string> { "administrador", "asesor", "tecnico" };
            return rolesPermitidos.Contains(rol.ToLower());
        }

        // ✅ MÉTODO ADICIONAL: Obtener usuario por ID
        public Usuario ObtenerUsuarioPorId(int usuariosID)
        {
            return cdUsuario.ObtenerUsuarioPorId(usuariosID);
        }

        // ✅ MÉTODO ADICIONAL: Cambiar contraseña
        public bool CambiarPassword(int usuariosID, string nuevaPassword, out string mensaje)
        {
            if (string.IsNullOrEmpty(nuevaPassword))
            {
                mensaje = "La contraseña no puede estar vacía";
                return false;
            }

            return cdUsuario.CambiarPassword(usuariosID, nuevaPassword, out mensaje);
        }

        // NUEVO: Método para cerrar sesión
        public void CerrarSesion()
        {
            UsuarioActual = null;
        }
    }
}