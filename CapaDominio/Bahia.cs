using System;

namespace CapaDominio
{
    public class Bahia
    {
        public int BahiaId { get; set; }
        public int EstadobahiaId { get; set; }
        public int? UsuariosId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Capacidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool Activo { get; set; }

        // Propiedad de navegación para EstadoBahia
        public EstadoBahia EstadoBahia { get; set; }

        // Propiedades de visualización
        public string EstadoDisplay => Activo ? "Activa" : "Inactiva";
        public string InformacionCompleta => $"{Nombre} - {Descripcion}";
        public string Tipo => ObtenerTipoDesdeDescripcion();

        private string ObtenerTipoDesdeDescripcion()
        {
            if (string.IsNullOrEmpty(Descripcion))
                return "General";

            if (Descripcion.Contains("Lubricacion") || Descripcion.Contains("Lubricación"))
                return "Lubricacion";
            else if (Descripcion.Contains("Express"))
                return "Express";
            else
                return "General";
        }

        // Constructor para inicializar EstadoBahia
        public Bahia()
        {
            EstadoBahia = new EstadoBahia();
        }
    }
}