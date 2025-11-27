using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    // Clase para los filtros de reportes
    public class ReporteFiltros
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string TipoReporte { get; set; } // "CLIENTES", "CITAS", "ORDENES", "DISPONIBILIDAD"
        public string Estado { get; set; }
        public string OrdenarPor { get; set; }
        public bool OrdenAscendente { get; set; } = true;
        public string ClienteDNI { get; set; }
        public string TecnicoNombre { get; set; }
    }

    // Entidad para reporte de clientes (basado en vw_ClientesCompletos)
    public class ReporteCliente
    {
        public int ClienteID { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
        public int TotalVehiculos { get; set; }
        public string Rol { get; set; }
    }

    // Entidad para reporte de citas (basado en vw_CitasCompletas)
    public class ReporteCita
    {
        public int CitaID { get; set; }
        public DateTime FechaCita { get; set; }
        public int DuracionEstimada { get; set; }
        public int Prioridad { get; set; }
        public string Observaciones { get; set; }
        public string EstadoCita { get; set; }
        public string Placa { get; set; }
        public string VIN { get; set; }
        public string Color { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string ClienteDNI { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteApellido { get; set; }
        public string ClienteTelefono { get; set; }
        public string Bahia { get; set; }
        public string Recepcionista { get; set; }
    }

    // Entidad para reporte de órdenes de trabajo (basado en vw_OrdenesTrabajoActivas)
    public class ReporteOrdenTrabajo
    {
        public int OrdentrabajoID { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Prioridad { get; set; }
        public string Descripcion { get; set; }
        public string EstadoOT { get; set; }
        public int TecnicoID { get; set; }
        public string TecnicoNombre { get; set; }
        public string TecnicoApellido { get; set; }
        public string Especialidad { get; set; }
        public string Placa { get; set; }
        public string VIN { get; set; }
        public string ClienteDNI { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteApellido { get; set; }
        public int HorasTranscurridas { get; set; }
    }

    // Entidad para reporte de disponibilidad (basado en vw_DisponibilidadBahias)
    public class ReporteDisponibilidad
    {
        public int CalendariocapacidadID { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int CapacidadDisponible { get; set; }
        public int CapacidadTotal { get; set; }
        public string Bahia { get; set; }
        public string EstadoBahia { get; set; }
        public string EstadoDisponibilidad { get; set; }
    }

    // Clase para estadísticas/resumen de reportes
    public class ReporteResumen
    {
        public string Titulo { get; set; }
        public int TotalRegistros { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public Dictionary<string, int> Estadisticas { get; set; }

        public ReporteResumen()
        {
            Estadisticas = new Dictionary<string, int>();
            FechaGeneracion = DateTime.Now;
        }
    }
}