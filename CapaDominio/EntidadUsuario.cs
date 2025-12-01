using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class EntidadUsuario
    {
        public int UsuariosID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Rol { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? UltimoLogin { get; set; }
        public bool Activo { get; set; }

        // Propiedades de navegación
        public virtual ICollection<EntidadEntrega> EntregasRealizadas { get; set; }
        public virtual ICollection<EntidadDocumentoEntrega> DocumentosGenerados { get; set; }

        // Métodos de dominio
        public string NombreCompleto => $"{Nombre} {Apellido}";

        public bool EsTecnico()
        {
            return Rol?.ToUpper() == "TECNICO";
        }

        public bool EsAdministrador()
        {
            return Rol?.ToUpper() == "ADMINISTRADOR";
        }

        public bool EsRecepcion()
        {
            return Rol?.ToUpper() == "RECEPCION";
        }
    }
}
