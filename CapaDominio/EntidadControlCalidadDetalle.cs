using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class EntidadControlCalidadDetalle
    {
        public int ControlcalidaddetalleID { get; set; }
        public int? ControlcalidadID { get; set; }
        public int? OtactividadID { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaVerificacion { get; set; }

        // Propiedades de navegación
        public virtual EntidadControlCalidad ControlCalidad { get; set; }
        public virtual OtActividad OtActividad { get; set; }

        // Constructor
        public EntidadControlCalidadDetalle()
        {
            FechaVerificacion = DateTime.Now;
            Estado = "PENDIENTE";
        }

        // Métodos de dominio
        public void MarcarComoAprobado(string observaciones = null)
        {
            Estado = "APROBADO";
            Observaciones = observaciones;
            FechaVerificacion = DateTime.Now;
        }

        public void MarcarComoRechazado(string observaciones)
        {
            Estado = "RECHAZADO";
            Observaciones = observaciones;
            FechaVerificacion = DateTime.Now;
        }

        public bool EstaAprobado()
        {
            return Estado?.ToUpper() == "APROBADO";
        }

        public bool EstaCompletado()
        {
            return Estado?.ToUpper() != "PENDIENTE";
        }
    }
}
