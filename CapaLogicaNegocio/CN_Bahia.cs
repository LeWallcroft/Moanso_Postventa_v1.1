using CapaDatos;
using CapaDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class CN_Bahia
    {
        private CD_Bahia cdBahia = new CD_Bahia();

        public List<Bahia> ListarBahiasActivas()
        {
            // Se pueden añadir reglas de negocio (validaciones, etc.) antes de llamar a la Capa Datos
            return cdBahia.ListarActivas();
        }
    }
}
