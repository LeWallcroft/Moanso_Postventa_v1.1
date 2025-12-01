using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class DocumentoEntregaRequestDTO
    {
        public int EntregavehiculoID { get; set; }
        public string TipoDocumento { get; set; }
        public string Descripcion { get; set; }
        public string ArchivoBase64 { get; set; } // Archivo en base64
        public string NombreArchivo { get; set; }
        public int UsuarioID { get; set; }
    }
}
