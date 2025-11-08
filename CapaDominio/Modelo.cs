using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class Modelo
    {
        public int ModeloId { get; set; }
        public int MarcaId { get; set; } // FK
        public string Nombre { get; set; }
    }
}
