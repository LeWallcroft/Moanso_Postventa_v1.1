using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class OrdenTrabajo
    {
        // Campos principales (tabla Ordentrabajo)
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

        // Campos de apoyo para el listado / UI
        public string TipoServicio { get; set; }          // Tipo servicio (columna)
        public string NombreCliente { get; set; }         // Cliente
        public string Placa { get; set; }                 // Placa
        public string DescripcionVehiculo { get; set; }   // Marca + Modelo + Año
        public string NombreTecnico { get; set; }         // Técnico/operario
        public string EstadoOT { get; set; }              // Estado


        // Campos extra para detalle de cliente
        public string DocumentoCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public string EmailCliente { get; set; }
        public string DireccionCliente { get; set; }

        // Campos extra para detalle de vehículo
        public string ModeloVehiculo { get; set; }   // Para txtModelo
        public string Vin { get; set; }              // Para txtVin
        public string Color { get; set; }            // Para txtColor
        public string AnioVehiculo { get; set; }     // Para txtAnio
        public string TipoCombustible { get; set; }  // Para txtCombustible
        public string Transmision { get; set; }      // Para txtTransmision
        public string NumeroRegistro { get; set; }   // Para txtRegistro
        public string EstadoVehiculo { get; set; }   // Para txtEstadoVehiculo


    }
     

   
}

