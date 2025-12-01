using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class SolicitudAprobacionDTO
    {
        public int EntregavehiculoID { get; set; }
        public bool Aprobacion { get; set; } // true = aprobar, false = rechazar
        public string Observaciones { get; set; }
        public string FirmaCliente { get; set; } // Base64 de la firma
        public int UsuarioID { get; set; } // Usuario que registra la acción
    }
}
