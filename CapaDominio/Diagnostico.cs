using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio{
    
        public class Diagnostico
        {
            public int DiagnosticoId { get; set; }
            public int CitaId { get; set; }
            public string Hallazgos { get; set; }
            public string Recomendaciones { get; set; }
            public int TecnicoId { get; set; }
            public DateTime Fecha { get; set; }
        }

        public class CitaDetalle
        {
            public int CitaId { get; set; }
            public DateTime FechaCita { get; set; }
            public TimeSpan HoraCita { get; set; }
            public string EstadoCita { get; set; }
            public int ClienteId { get; set; }
            public string ClienteNombre { get; set; }
            public string DNI { get; set; }
            public int VehiculoId { get; set; }
            public string Placa { get; set; }
            public string VIN { get; set; }
            public int Anio { get; set; }
            public string Marca { get; set; }
            public string Modelo { get; set; }
            public int ServicioId { get; set; }
            public string Servicio { get; set; }
            public string TipoServicio { get; set; }
            public int BahiaId { get; set; }
            public string Bahia { get; set; }
            public int TecnicoId { get; set; }
            public string Tecnico { get; set; }
            public int OTId { get; set; }
            public string NumeroOT { get; set; }
            public string EstadoOT { get; set; }
            public DateTime FechaApertura { get; set; }
            public TimeSpan? HoraInicioReal { get; set; }
            public int DuracionEstimadaMin { get; set; }
            public string Email { get; set; }           // Nueva propiedad
            public string Telefono { get; set; }        // Nueva propiedad
            public string Direccion { get; set; }       // Nueva propiedad
            public DateTime FechaRegistro { get; set; } // Nueva propiedad

    }


    
}
