using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    namespace Dominio.Entidades
    {
        public class EntidadTecnico
        {
            public int TecnicoID { get; set; }
            public int? UsuariosID { get; set; }
            public string Especialidad { get; set; }
            public DateTime FechaContratacion { get; set; }
            public decimal? Salario { get; set; }
            public bool Disponible { get; set; }
            public bool Activo { get; set; }

            // Propiedades de navegación
            public virtual EntidadUsuario Usuario { get; set; }
            public virtual ICollection<EntidadOrdentrabajo> OrdenesTrabajo { get; set; }
            public virtual ICollection<EntidadControlCalidad> ControlesCalidad { get; set; }
        }
    }
}
