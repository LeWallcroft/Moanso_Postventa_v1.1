using CapaDatos;
using CapaDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class CN_Tecnico
    {
        private CD_Tecnico cdTecnico = new CD_Tecnico();

        // Método para listar técnicos activos
        public List<Tecnico> ListarTecnicos()
        {
            return cdTecnico.ListarTecnicosActivos();
        }
    }
}
