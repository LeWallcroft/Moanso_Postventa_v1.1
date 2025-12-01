using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class EntidadOrdentrabajo
    {
        public int OrdentrabajoID { get; set; }
        public int? CitaID { get; set; }
        public int? EstadootID { get; set; }
        public int? UsuariosID { get; set; }
        public int? TecnicoID { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Observaciones { get; set; }
        public int Prioridad { get; set; }
        public int? KilometrajeEntrada { get; set; }
        public int? KilometrajeSalida { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Propiedades de navegación
        public virtual EntidadEstadoOT EstadoOT { get; set; }
        public virtual Tecnico Tecnico { get; set; }
        public virtual EntidadUsuario Usuario { get; set; }
        public virtual ICollection<OtActividad> Actividades { get; set; }
        public virtual Cita Cita { get; set; }
        public virtual EntidadEntrega Entrega { get; set; }

        // Constructor
        public EntidadOrdentrabajo()
        {
            Actividades = new HashSet<OtActividad>();
            FechaCreacion = DateTime.Now;
            Prioridad = 1;
        }

        // Métodos de dominio específicos para entrega
        public bool EstaListaParaEntrega()
        {
            // Verificar si está en estado "Para Control Calidad" o "Aprobado Control"
            var estadoNombre = EstadoOT?.Nombre?.ToUpper();
            return (estadoNombre == "PARA CONTROL CALIDAD" ||
                   estadoNombre == "APROBADO CONTROL" ||
                   estadoNombre == "LISTO PARA ENTREGA") &&
                   FechaFin.HasValue;
        }

        public void MarcarComoEntregada()
        {
            if (EstadoOT != null)
            {
                // Buscar estado "ENTREGADO" o "COMPLETADA"
                // Esto dependerá de cómo tengas configurados los estados
                FechaFin = DateTime.Now;
            }
        }

        public void ReabrirOT(string motivo)
        {
            Observaciones += $"\n[REAPERTURA {DateTime.Now:dd/MM/yyyy HH:mm}] {motivo}";
            // El cambio de estado se haría en la capa de servicio
        }
    }
}
