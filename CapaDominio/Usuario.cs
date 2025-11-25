using System;
using System.Collections.Generic;

namespace CapaDominio
{
    public class Usuario
    {
        public int UsuariosID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Rol { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? UltimoLogin { get; set; }
        public bool Activo { get; set; }

    }
}