using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio.DTOs
{
    public class FiltroServicios
    {
        public string Nombre { get; set; }
        public int? CategoriaID { get; set; }
        public int? TipoServicioID { get; set; }
        public bool? Activo { get; set; }
        public bool? RequiereRepuestos { get; set; }
        public decimal? PrecioMinimo { get; set; }
        public decimal? PrecioMaximo { get; set; }
    }
}
