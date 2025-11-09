using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio;
using CapaDatos;
using System.Data.SqlClient;
using System.Data;
namespace CapaLogicaNegocio
{
    public class CN_OrdenTrabajo
    {
        CD_OrdenTrabajo cot = new CD_OrdenTrabajo();
        public DataTable Listar_Cita2()
        {
            // Se podrían añadir validaciones de fecha aquí
            return cot.ListarCitas();
        }

    }
}
