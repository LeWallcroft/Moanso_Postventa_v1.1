using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class Servicio
    {
        public int ServicioId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioBase { get; set; }
        public int DuracionMin { get; set; }
        public bool Activo { get; set; }
        public string Tipo { get; set; }
    }
}
