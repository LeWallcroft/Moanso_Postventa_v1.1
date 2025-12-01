using CapaDatos;
using CapaDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaLogicaNegocio
{
    public class CN_OrdenTrabajo
    {
        private readonly CD_OrdenTrabajo datos = new CD_OrdenTrabajo();
        private readonly CD_OtActividad datosActividades = new CD_OtActividad();

        /// <summary>
        /// Devuelve el listado de OTs para el formulario.
        /// Aquí podrías aplicar filtros de negocio (estado, fechas, etc).
        /// </summary>
        public List<OrdenTrabajo> ListarOrdenesTrabajo()
        {
            // Aquí podrías agregar reglas, ejemplo:
            // - Sólo mostrar OTs activas
            // - Sólo ciertos estados según el rol, etc.
            return datos.Listar();
        }

        /// <summary>
        /// Obtiene una OT completa por Id para el formulario de edición.
        /// </summary>
        public OrdenTrabajo ObtenerOrdenTrabajo(int ordentrabajoID)
        {
            if (ordentrabajoID <= 0)
                throw new ArgumentException("El Id de la OT no es válido.");

            var ot = datos.ObtenerPorId(ordentrabajoID);

            if (ot == null)
                throw new InvalidOperationException("No se encontró la orden de trabajo.");

            return ot;
        }
        public List<OtActividad> ListarActividadesPorOrden(int ordentrabajoID)
        {
            if (ordentrabajoID <= 0)
                throw new ArgumentException("El Id de la OT no es válido.");

            return datosActividades.ListarPorOrdenTrabajo(ordentrabajoID);
        }

        public void ActualizarActividad(int otactividadID, string estado, int? tiempoReal)
        {
            if (otactividadID <= 0)
                throw new ArgumentException("El Id de la actividad no es válido.");

            if (string.IsNullOrWhiteSpace(estado))
                throw new ArgumentException("El estado no es válido.");

            datosActividades.ActualizarActividad(otactividadID, estado, tiempoReal);
        }
        


        public string ObtenerActividadesOrdenTexto(int ordentrabajoID, bool soloRealizadas)
        {
            var actividades = ListarActividadesPorOrden(ordentrabajoID);

            if (actividades == null || actividades.Count == 0)
                return "No hay actividades registradas para esta orden.";

            var sb = new StringBuilder();

            foreach (var act in actividades)
            {
                bool esRealizada = string.Equals(act.Estado, "COMPLETADA",
                    StringComparison.OrdinalIgnoreCase);

                if (soloRealizadas && !esRealizada)
                    continue;

                string marca = esRealizada ? "☑" : "☐";
                sb.AppendLine($"{marca} {act.Descripcion}");
            }

            if (soloRealizadas && sb.Length == 0)
                return "No hay actividades marcadas como realizadas.";

            return sb.ToString();
        }


        public void AsignarTecnicoAOrden(int ordentrabajoID, int? tecnicoID)
        {
            if (ordentrabajoID <= 0)
                throw new ArgumentException("El Id de la OT no es válido.");

            // Si quieres validar algo del técnico, lo haces aquí
            datos.AsignarTecnico(ordentrabajoID, tecnicoID);

            // Si asignas técnico → estado Asignado
            if (tecnicoID.HasValue)
            {
                // TODO: sustituye 2 por el Id real del estado 'Asignado'
                CambiarEstadoOT(ordentrabajoID, 2);
            }
        }


        public void ActualizarObservaciones(int ordentrabajoID, string observaciones)
        {
            if (ordentrabajoID <= 0)
                throw new ArgumentException("El Id de la OT no es válido.");

            datos.ActualizarObservaciones(ordentrabajoID, observaciones ?? string.Empty);
        }


        public void MarcarInicioTrabajo(int ordentrabajoID)
        {
            if (ordentrabajoID <= 0)
                throw new ArgumentException("El Id de la OT no es válido.");

            datos.MarcarInicioTrabajo(ordentrabajoID);
        }


        public void MarcarActividadesTerminadas(int ordentrabajoID)
        {
            if (ordentrabajoID <= 0)
                throw new ArgumentException("El Id de la OT no es válido.");

            datos.MarcarActividadesTerminadas(ordentrabajoID);
        }


        public void CambiarEstadoOT(int ordentrabajoID, int estadootID)
        {

            if (ordentrabajoID <= 0)
                throw new ArgumentException("Id de OT no válido");
            if (estadootID <= 0)
                throw new ArgumentException("Id de estado no válido");

            datos.CambiarEstadoOT(ordentrabajoID, estadootID);
        }

        public List<EstadoOT> ListarEstadosOT()
        {
            return datos.ListarEstadosOT();
        }

        public OrdenPago ObtenerOrdenPagoPorOrden(int ordentrabajoID)
        {
            if (ordentrabajoID <= 0)
                throw new ArgumentException("El Id de la OT no es válido.");

            return datos.ObtenerOrdenPagoPorOrden(ordentrabajoID);
        }

        public List<RepuestoOT> ListarRepuestosPorOrden(int ordentrabajoID)
        {
            if (ordentrabajoID <= 0)
                throw new ArgumentException("Id de OT no válido");
            return datos.ListarRepuestosPorOrden(ordentrabajoID);
        }

        public void EliminarRepuestoExtra(int otrepuestoID)
        {
            if (otrepuestoID <= 0)
                throw new ArgumentException("Id de detalle no válido");
            datos.EliminarRepuestoExtra(otrepuestoID);
        }

        public void AgregarRepuestoExtra(int ordentrabajoID, int repuestoID, int cantidad)
        {
            if (ordentrabajoID <= 0)
                throw new ArgumentException("Id de OT no válido");
            if (repuestoID <= 0)
                throw new ArgumentException("Id de repuesto no válido");
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero");

            datos.AgregarRepuestoExtra(ordentrabajoID, repuestoID, cantidad);
        }

        public int CrearOrdenTrabajoDesdeCita(int citaId, int usuarioId, int prioridad, int? kilometrajeEntrada)
        {
            if (citaId <= 0)
                throw new ArgumentException("Id de cita no válido.", nameof(citaId));

            if (usuarioId <= 0)
                throw new ArgumentException("Id de usuario no válido.", nameof(usuarioId));

            if (prioridad <= 0)
                prioridad = 1;

            return datos.CrearDesdeCita(citaId, usuarioId, prioridad, kilometrajeEntrada);
        }


        public DateTime? ObtenerFechaControl(int ordentrabajoID)
        {
            return datos.ObtenerFechaControl(ordentrabajoID);
        }

        public DateTime? ObtenerFechaEntrega(int ordentrabajoID)
        {
            return datos.ObtenerFechaEntrega(ordentrabajoID);
        }


        public void RegistrarControlCalidad(
             int ordentrabajoID,
             int usuariosID,
             string resultado,
             string observaciones,
             string xmlChecklist)
        {
            if (ordentrabajoID <= 0)
                throw new ArgumentException("El Id de la OT no es válido.");

            if (usuariosID <= 0)
                throw new ArgumentException("El Id del usuario no es válido.");

            if (string.IsNullOrWhiteSpace(resultado))
                throw new ArgumentException("El resultado del control de calidad no es válido.");

            datos.RegistrarControlCalidad(
                ordentrabajoID,
                usuariosID,
                resultado,
                observaciones ?? string.Empty,
                xmlChecklist
            );
        }

    }
}
