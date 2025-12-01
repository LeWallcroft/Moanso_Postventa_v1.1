using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class EntidadEntrega
    {
        // Propiedades principales
        public int EntregavehiculoID { get; set; }
        public int? ControlcalidadID { get; set; }
        public int? OrdentrabajoID { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int? Kilometraje { get; set; }
        public string Combustible { get; set; }
        public string Observaciones { get; set; }
        public string FirmaCliente { get; set; }
        public int? UsuariosEntregaID { get; set; }

        // Nuevos campos para aprobación del cliente
        public bool? AprobacionCliente { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string ObservacionesCliente { get; set; }

        // Propiedades de navegación
        public virtual EntidadOrdentrabajo Ordentrabajo { get; set; }
        public virtual EntidadControlCalidad ControlCalidad { get; set; }
        public virtual ICollection<EntidadDocumentoEntrega> Documentos { get; set; }
        public virtual EntidadUsuario UsuarioEntrega { get; set; }

        // Constructor
        public EntidadEntrega()
        {
            Documentos = new HashSet<EntidadDocumentoEntrega>();
            FechaEntrega = DateTime.Now;
        }

        // Métodos de dominio
        public void AprobarEntrega(string observaciones = null, string firmaCliente = null)
        {
            AprobacionCliente = true;
            FechaAprobacion = DateTime.Now;
            ObservacionesCliente = observaciones;
            FirmaCliente = firmaCliente;
        }

        public void RechazarEntrega(string observaciones)
        {
            AprobacionCliente = false;
            FechaAprobacion = DateTime.Now;
            ObservacionesCliente = observaciones;
            FirmaCliente = null; // Limpiar firma si rechaza
        }

        public bool EstaAprobada()
        {
            return AprobacionCliente.HasValue && AprobacionCliente.Value;
        }

        public bool EstaRechazada()
        {
            return AprobacionCliente.HasValue && !AprobacionCliente.Value;
        }

        public bool PendienteAprobacion()
        {
            return !AprobacionCliente.HasValue;
        }
    }
}
