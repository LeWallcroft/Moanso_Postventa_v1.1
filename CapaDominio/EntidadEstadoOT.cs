using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class EntidadEstadoOT
    {
        public int EstadootID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
        public bool Activo { get; set; }

        // Propiedades de navegación
        public virtual ICollection<EntidadOrdentrabajo> OrdenesTrabajo { get; set; }
    }
}
