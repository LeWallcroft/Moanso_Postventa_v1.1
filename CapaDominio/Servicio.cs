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
        public string Nombre { get; set; }
        public int DuracionMin { get; set; } // Necesaria para el nudDuracion
        public decimal PrecioBase { get; set; }
    }
}
