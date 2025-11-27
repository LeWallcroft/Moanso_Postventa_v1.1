using System;

namespace Dominio
{
    public class Cliente
    {
        public int ClienteID { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}