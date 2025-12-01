using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class EntidadDocumentoEntrega
    {
        public int DocumentoentregaID { get; set; }
        public int? EntregavehiculoID { get; set; }
        public string TipoDocumento { get; set; }
        public string Descripcion { get; set; }
        public string ArchivoUrl { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? UsuariosID { get; set; }

        // Propiedades de navegación
        public virtual EntidadEntrega Entrega { get; set; }
        public virtual EntidadUsuario Usuario { get; set; }

        // Constructor
        public EntidadDocumentoEntrega()
        {
            FechaCreacion = DateTime.Now;
        }

        // Métodos de dominio
        public bool EsDocumentoValido()
        {
            return !string.IsNullOrEmpty(TipoDocumento) &&
                   !string.IsNullOrEmpty(ArchivoUrl);
        }

        public string ObtenerExtension()
        {
            if (string.IsNullOrEmpty(ArchivoUrl))
                return string.Empty;

            return System.IO.Path.GetExtension(ArchivoUrl).ToLower();
        }

        public bool EsImagen()
        {
            var extension = ObtenerExtension();
            return extension == ".jpg" || extension == ".jpeg" ||
                   extension == ".png" || extension == ".gif";
        }

        public bool EsPDF()
        {
            return ObtenerExtension() == ".pdf";
        }
    }
}
