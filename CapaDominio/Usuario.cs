using System;

namespace CapaDominio
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }
        public DateTime? UltimoAcceso { get; set; }
    }
}