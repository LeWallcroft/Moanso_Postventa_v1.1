using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class Bahia
    {
        public int BahiaId { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public bool Activo { get; set; }

        // Propiedades de visualización
        public string EstadoDisplay => Activo ? "Activa" : "Inactiva";
        public string InformacionCompleta => $"{Nombre} - {Tipo}";
    }
}
