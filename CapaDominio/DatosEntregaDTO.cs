using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class DatosEntregaDTO
    {
        // Datos de la OT
        public int OrdentrabajoID { get; set; }
        public string NumeroOT => $"OT-{OrdentrabajoID:00000}";
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string ObservacionesOT { get; set; }
        public int? KilometrajeEntrada { get; set; }
        public int? KilometrajeSalida { get; set; }
        public string EstadoOT { get; set; }
        public string ColorEstadoOT { get; set; }
        public int EstadoOTID { get; set; }

        // Datos del Control de Calidad
        public int? ControlcalidadID { get; set; }
        public int? TecnicoID { get; set; }
        public string NombreTecnico { get; set; }
        public DateTime? FechaControl { get; set; }
        public string ObservacionesControl { get; set; }
        public string Resultado { get; set; }
        public int? Calificacion { get; set; }
        public List<DetalleControlDTO> DetallesControl { get; set; }

        // Datos del Cliente - IMPORTANTE: El campo es DNI, no DocumentoCliente
        public int ClienteID { get; set; }
        public string NombreCliente { get; set; }
        public string DNI { get; set; }  // ← Este es el campo correcto
        public string Email { get; set; }
        public string Telefono { get; set; }

        // Datos del Vehículo
        public int VehiculoID { get; set; }
        public string Placa { get; set; }
        public string Color { get; set; }
        public int? Anio { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int? KilometrajeActual { get; set; }
        public string CombustibleVehiculo { get; set; }
        public string Transmision { get; set; }

        // Datos de la Entrega
        public int? EntregavehiculoID { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int? KilometrajeEntrega { get; set; }
        public string CombustibleEntrega { get; set; }
        public string ObservacionesEntrega { get; set; }
        public string FirmaCliente { get; set; }
        public int? UsuariosEntregaID { get; set; }
        public string NombreUsuarioEntrega { get; set; }
        public bool? AprobacionCliente { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string ObservacionesCliente { get; set; }

        // Documentos asociados
        public List<DocumentoEntregaDTO> Documentos { get; set; }

        // Estado y validaciones
        public bool EstaListaParaEntrega { get; set; }
        public bool TieneControlCalidad => ControlcalidadID.HasValue;
        public bool ControlCalidadAprobado => Resultado?.ToUpper() == "APROBADO";
        public bool EntregaExistente => EntregavehiculoID.HasValue;
        public bool ClienteAprobo => AprobacionCliente.HasValue && AprobacionCliente.Value;
        public bool ClienteRechazo => AprobacionCliente.HasValue && !AprobacionCliente.Value;
        public bool PendienteAprobacion => EntregaExistente && !ClienteAprobo && !ClienteRechazo;

        // Constructor
        public DatosEntregaDTO()
        {
            DetallesControl = new List<DetalleControlDTO>();
            Documentos = new List<DocumentoEntregaDTO>();

            // Inicializar valores por defecto
            EstadoOT = "SIN ESTADO";
            ColorEstadoOT = "#CCCCCC";
            NombreCliente = "CLIENTE NO ENCONTRADO";
            DNI = "";
            Email = "";
            Telefono = "";
            Placa = "SIN PLACA";
            Modelo = "SIN MODELO";
            Marca = "SIN MARCA";
            NombreTecnico = "NO ASIGNADO";
            Resultado = "PENDIENTE";
            NombreUsuarioEntrega = "";
        }

        // Métodos de ayuda
        public string ObtenerEstadoAprobacion()
        {
            if (PendienteAprobacion) return "PENDIENTE";
            return ClienteAprobo ? "APROBADO" : "RECHAZADO";
        }

        public string ObtenerColorEstadoAprobacion()
        {
            if (PendienteAprobacion) return "#FFA500"; // Naranja
            return ClienteAprobo ? "#28a745" : "#dc3545"; // Verde o Rojo
        }
    }

    public class DetalleControlDTO
    {
        public int ControlcalidaddetalleID { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaVerificacion { get; set; }
        public string ActividadDescripcion { get; set; }

        // Propiedades calculadas
        public bool EstaAprobado => Estado?.ToUpper() == "APROBADO";
        public string IconoEstado => EstaAprobado ? "✓" : "✗";
        public string ColorEstado => EstaAprobado ? "text-success" : "text-danger";
    }

    public class DocumentoEntregaDTO
    {
        public int DocumentoentregaID { get; set; }
        public string TipoDocumento { get; set; }
        public string Descripcion { get; set; }
        public string ArchivoUrl { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombreUsuario { get; set; }

        // Propiedades calculadas
        public string NombreArchivo => System.IO.Path.GetFileName(ArchivoUrl);
        public string Extension => System.IO.Path.GetExtension(ArchivoUrl)?.ToLower();
        public bool EsImagen => Extension == ".jpg" || Extension == ".jpeg" ||
                                Extension == ".png" || Extension == ".gif";
        public bool EsPDF => Extension == ".pdf";
        public string IconoTipo => EsPDF ? "📄" : (EsImagen ? "🖼️" : "📎");
    }
}
