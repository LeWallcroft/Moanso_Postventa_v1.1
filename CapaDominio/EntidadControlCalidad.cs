using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class EntidadControlCalidad
    {
        public int ControlcalidadID { get; set; }
        public int? TecnicoID { get; set; }
        public DateTime FechaControl { get; set; }
        public string Observaciones { get; set; }
        public string Resultado { get; set; }
        public int? Calificacion { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Propiedades de navegación
        public virtual ICollection<EntidadControlCalidadDetalle> Detalles { get; set; }
        public virtual Tecnico Tecnico { get; set; }

        // Constructor
        public EntidadControlCalidad()
        {
            Detalles = new HashSet<EntidadControlCalidadDetalle>();
            FechaControl = DateTime.Now;
            FechaCreacion = DateTime.Now;
        }

        // Métodos de dominio
        public bool EstaAprobado()
        {
            return Resultado?.ToUpper() == "APROBADO";
        }

        public bool EstaRechazado()
        {
            return Resultado?.ToUpper() == "RECHAZADO";
        }

        public bool EstaPendiente()
        {
            return Resultado?.ToUpper() == "PENDIENTE";
        }

        public bool TieneCalificacionValida()
        {
            return Calificacion.HasValue && Calificacion.Value >= 0 && Calificacion.Value <= 10;
        }

        public bool TodosDetallesCompletados()
        {
            if (Detalles == null || Detalles.Count == 0)
                return false;

            foreach (var detalle in Detalles)
            {
                if (string.IsNullOrEmpty(detalle.Estado) ||
                    detalle.Estado.ToUpper() == "PENDIENTE")
                    return false;
            }
            return true;
        }
    }
}
