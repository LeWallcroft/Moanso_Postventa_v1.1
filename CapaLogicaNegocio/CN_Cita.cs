using CapaDatos;
using CapaDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class CN_Cita
    {
        private CD_Cita cdCita = new CD_Cita();

        public List<CapacidadDia> ObtenerDisponibilidad(DateTime fecha, int? bahiaId)
        {
            // Se podrían añadir validaciones de fecha aquí
            return cdCita.ConsultarDisponibilidad(fecha, bahiaId);
        }

        public bool RegistrarCita(Cita cita, int capacidadId)
        {
            // Validaciones finales (se asume que los IDs ya fueron validados en los pasos anteriores)
            if (capacidadId <= 0) throw new Exception("Error interno: Slot de capacidad no válido.");

            return cdCita.CrearCita(cita, capacidadId);
        }
    }
}
