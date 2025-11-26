using System;

namespace CapaDominio
{
    public class EstadoBahia
    {
        public int EstadobahiaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
        public bool Activo { get; set; }
    }
}