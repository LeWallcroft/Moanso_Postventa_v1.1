using CapaDatos;
using CapaDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class CN_Servicio
    {
        private CD_Servicio cdServicio = new CD_Servicio();

        // Método para listar servicios activos
        public List<Servicio> ListarServicios()
        {
            // No se requieren validaciones complejas aquí, solo recuperar la lista activa.
            return cdServicio.ListarServiciosActivos();
        }

        // Método auxiliar para obtener Duración, ya que el SP solo da ID y Nombre
        public int ObtenerDuracionMin(int servicioId)
        {
            return 120; // 120 minutos por defecto, hasta que se implemente la búsqueda correcta.
        }
    }
}
