using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class Servicio
    {
        public int ServicioID { get; set; }
        public int? CategoriaservicioID { get; set; }
        public int? UsuariosID { get; set; }
        public int? TiposervicioID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int DuracionEstimada { get; set; }
        public bool RequiereRepuestos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }

        // Propiedades de navegación (opcionales, para mostrar info relacionada)
        public string CategoriaNombre { get; set; }
        public string TipoNombre { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}