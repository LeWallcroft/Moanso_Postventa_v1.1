using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class FiltroRepuesto
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Proveedor { get; set; }
        public bool? Activo { get; set; }
        public bool? StockBajo { get; set; }
        public int Pagina { get; set; } = 1;
        public int RegistrosPorPagina { get; set; } = 20;
    }
}
