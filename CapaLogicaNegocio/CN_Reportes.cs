using System;
using System.Collections.Generic;
using System.Linq;
using CapaDatos;
using CapaDominio;

namespace CapaLogicaNegocio
{
    public class CN_Reportes
    {
        private CD_Reportes cdReportes = new CD_Reportes();

        // 1. REPORTE DE CLIENTES
        public List<ReporteCliente> GenerarReporteClientes(ReporteFiltros filtros)
        {
            try
            {
                // Validaciones de negocio
                if (filtros.FechaInicio.HasValue && filtros.FechaFin.HasValue)
                {
                    if (filtros.FechaInicio > filtros.FechaFin)
                        throw new ArgumentException("La fecha de inicio no puede ser mayor a la fecha fin");

                    // Validar que el rango no sea mayor a 1 año
                    if ((filtros.FechaFin.Value - filtros.FechaInicio.Value).TotalDays > 365)
                        throw new ArgumentException("El rango de fechas no puede ser mayor a 1 año");
                }

                // Obtener datos de la capa de datos
                var resultado = cdReportes.ObtenerReporteClientes(filtros);

                // Aplicar lógica de negocio adicional si es necesario
                if (!string.IsNullOrEmpty(filtros.OrdenarPor))
                {
                    resultado = AplicarOrdenamientoClientes(resultado, filtros);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar reporte de clientes: {ex.Message}", ex);
            }
        }

        // 2. REPORTE DE CITAS
        public List<ReporteCita> GenerarReporteCitas(ReporteFiltros filtros)
        {
            try
            {
                // Validaciones específicas para citas
                if (filtros.FechaInicio.HasValue && filtros.FechaFin.HasValue)
                {
                    if (filtros.FechaInicio > filtros.FechaFin)
                        throw new ArgumentException("La fecha de inicio no puede ser mayor a la fecha fin");

                    // Validar rango máximo de 3 meses para citas
                    if ((filtros.FechaFin.Value - filtros.FechaInicio.Value).TotalDays > 90)
                        throw new ArgumentException("El rango de fechas para citas no puede ser mayor a 3 meses");
                }

                var resultado = cdReportes.ObtenerReporteCitas(filtros);

                // Aplicar ordenamiento si se especifica
                if (!string.IsNullOrEmpty(filtros.OrdenarPor))
                {
                    resultado = AplicarOrdenamientoCitas(resultado, filtros);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar reporte de citas: {ex.Message}", ex);
            }
        }

        // 3. REPORTE DE ÓRDENES DE TRABAJO
        public List<ReporteOrdenTrabajo> GenerarReporteOrdenesTrabajo(ReporteFiltros filtros)
        {
            try
            {
                // Validaciones específicas para órdenes de trabajo
                if (filtros.FechaInicio.HasValue && filtros.FechaFin.HasValue)
                {
                    if (filtros.FechaInicio > filtros.FechaFin)
                        throw new ArgumentException("La fecha de inicio no puede ser mayor a la fecha fin");
                }

                var resultado = cdReportes.ObtenerReporteOrdenesTrabajo(filtros);

                // Calcular métricas adicionales
                resultado = CalcularMetricasOrdenesTrabajo(resultado);

                // Aplicar ordenamiento si se especifica
                if (!string.IsNullOrEmpty(filtros.OrdenarPor))
                {
                    resultado = AplicarOrdenamientoOrdenesTrabajo(resultado, filtros);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar reporte de órdenes de trabajo: {ex.Message}", ex);
            }
        }

        // 4. REPORTE DE DISPONIBILIDAD
        public List<ReporteDisponibilidad> GenerarReporteDisponibilidad(ReporteFiltros filtros)
        {
            try
            {
                // Validaciones para disponibilidad
                if (filtros.FechaInicio.HasValue && filtros.FechaFin.HasValue)
                {
                    if (filtros.FechaInicio > filtros.FechaFin)
                        throw new ArgumentException("La fecha de inicio no puede ser mayor a la fecha fin");

                    // Solo permitir máximo 7 días para disponibilidad
                    if ((filtros.FechaFin.Value - filtros.FechaInicio.Value).TotalDays > 7)
                        throw new ArgumentException("El rango de fechas para disponibilidad no puede ser mayor a 7 días");
                }

                var resultado = cdReportes.ObtenerReporteDisponibilidad(filtros);

                // Calcular estadísticas de disponibilidad
                resultado = CalcularEstadisticasDisponibilidad(resultado);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar reporte de disponibilidad: {ex.Message}", ex);
            }
        }

        // 5. MÉTODO GENERAL PARA OBTENER REPORTE
        public object GenerarReporte(ReporteFiltros filtros)
        {
            try
            {
                // Validación básica del tipo de reporte
                if (string.IsNullOrEmpty(filtros.TipoReporte))
                    throw new ArgumentException("El tipo de reporte es requerido");

                switch (filtros.TipoReporte.ToUpper())
                {
                    case "CLIENTES":
                        return GenerarReporteClientes(filtros);
                    case "CITAS":
                        return GenerarReporteCitas(filtros);
                    case "ORDENES":
                        return GenerarReporteOrdenesTrabajo(filtros);
                    case "DISPONIBILIDAD":
                        return GenerarReporteDisponibilidad(filtros);
                    default:
                        throw new ArgumentException($"Tipo de reporte no válido: {filtros.TipoReporte}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar reporte: {ex.Message}", ex);
            }
        }

        // 6. OBTENER ESTADÍSTICAS Y RESUMEN - CORREGIDO
        public ReporteResumen ObtenerResumenReporte(ReporteFiltros filtros)
        {
            try
            {
                var resumen = new ReporteResumen();

                switch (filtros.TipoReporte.ToUpper())
                {
                    case "CLIENTES":
                        var clientes = GenerarReporteClientes(filtros);
                        resumen.Titulo = "Reporte de Clientes";
                        resumen.TotalRegistros = clientes.Count;
                        resumen.Estadisticas.Add("Clientes Activos", clientes.Count(c => c.Activo));
                        resumen.Estadisticas.Add("Clientes Inactivos", clientes.Count(c => !c.Activo));
                        resumen.Estadisticas.Add("Total Vehículos", clientes.Sum(c => c.TotalVehiculos));
                        break;

                    case "CITAS":
                        var citas = GenerarReporteCitas(filtros);
                        resumen.Titulo = "Reporte de Citas";
                        resumen.TotalRegistros = citas.Count;
                        resumen.Estadisticas.Add("Duración Total (min)", citas.Sum(c => c.DuracionEstimada));

                        // CORRECCIÓN: Convertir double a int
                        double duracionPromedio = citas.Count > 0 ? citas.Average(c => c.DuracionEstimada) : 0;
                        resumen.Estadisticas.Add("Duración Promedio (min)", (int)Math.Round(duracionPromedio));
                        break;

                    case "ORDENES":
                        var ordenes = GenerarReporteOrdenesTrabajo(filtros);
                        resumen.Titulo = "Reporte de Órdenes de Trabajo";
                        resumen.TotalRegistros = ordenes.Count;
                        resumen.Estadisticas.Add("Horas Totales", ordenes.Sum(o => o.HorasTranscurridas));

                        // CORRECCIÓN: Convertir double a int
                        double horasPromedio = ordenes.Count > 0 ? ordenes.Average(o => o.HorasTranscurridas) : 0;
                        resumen.Estadisticas.Add("Horas Promedio", (int)Math.Round(horasPromedio));
                        break;

                    case "DISPONIBILIDAD":
                        var disponibilidad = GenerarReporteDisponibilidad(filtros);
                        resumen.Titulo = "Reporte de Disponibilidad";
                        resumen.TotalRegistros = disponibilidad.Count;
                        resumen.Estadisticas.Add("Bloques Disponibles", disponibilidad.Count(d => d.EstadoDisponibilidad == "DISPONIBLE"));
                        resumen.Estadisticas.Add("Bloques Ocupados", disponibilidad.Count(d => d.EstadoDisponibilidad == "OCUPADO"));

                        // Calcular porcentaje de disponibilidad
                        if (disponibilidad.Count > 0)
                        {
                            double porcentajeDisponible = (disponibilidad.Count(d => d.EstadoDisponibilidad == "DISPONIBLE") * 100.0) / disponibilidad.Count;
                            resumen.Estadisticas.Add("Porcentaje Disponible", (int)Math.Round(porcentajeDisponible));
                        }
                        break;
                }

                return resumen;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar resumen del reporte: {ex.Message}", ex);
            }
        }

        // MÉTODOS PRIVADOS PARA ORDENAMIENTO (sin cambios)
        private List<ReporteCliente> AplicarOrdenamientoClientes(List<ReporteCliente> clientes, ReporteFiltros filtros)
        {
            var ordenado = clientes.AsQueryable();

            switch (filtros.OrdenarPor.ToLower())
            {
                case "nombre":
                    ordenado = filtros.OrdenAscendente ? ordenado.OrderBy(c => c.Nombre) : ordenado.OrderByDescending(c => c.Nombre);
                    break;
                case "fecharegistro":
                    ordenado = filtros.OrdenAscendente ? ordenado.OrderBy(c => c.FechaRegistro) : ordenado.OrderByDescending(c => c.FechaRegistro);
                    break;
                case "totalvehiculos":
                    ordenado = filtros.OrdenAscendente ? ordenado.OrderBy(c => c.TotalVehiculos) : ordenado.OrderByDescending(c => c.TotalVehiculos);
                    break;
                default:
                    ordenado = ordenado.OrderByDescending(c => c.FechaRegistro);
                    break;
            }

            return ordenado.ToList();
        }

        private List<ReporteCita> AplicarOrdenamientoCitas(List<ReporteCita> citas, ReporteFiltros filtros)
        {
            var ordenado = citas.AsQueryable();

            switch (filtros.OrdenarPor.ToLower())
            {
                case "fechacita":
                    ordenado = filtros.OrdenAscendente ? ordenado.OrderBy(c => c.FechaCita) : ordenado.OrderByDescending(c => c.FechaCita);
                    break;
                case "duracion":
                    ordenado = filtros.OrdenAscendente ? ordenado.OrderBy(c => c.DuracionEstimada) : ordenado.OrderByDescending(c => c.DuracionEstimada);
                    break;
                case "prioridad":
                    ordenado = filtros.OrdenAscendente ? ordenado.OrderBy(c => c.Prioridad) : ordenado.OrderByDescending(c => c.Prioridad);
                    break;
                case "cliente":
                    ordenado = filtros.OrdenAscendente ? ordenado.OrderBy(c => c.ClienteNombre) : ordenado.OrderByDescending(c => c.ClienteNombre);
                    break;
                default:
                    ordenado = ordenado.OrderByDescending(c => c.FechaCita);
                    break;
            }

            return ordenado.ToList();
        }

        private List<ReporteOrdenTrabajo> AplicarOrdenamientoOrdenesTrabajo(List<ReporteOrdenTrabajo> ordenes, ReporteFiltros filtros)
        {
            var ordenado = ordenes.AsQueryable();

            switch (filtros.OrdenarPor.ToLower())
            {
                case "fechainicio":
                    ordenado = filtros.OrdenAscendente ?
                        ordenado.OrderBy(o => o.FechaInicio ?? DateTime.MaxValue) :
                        ordenado.OrderByDescending(o => o.FechaInicio ?? DateTime.MinValue);
                    break;
                case "prioridad":
                    ordenado = filtros.OrdenAscendente ? ordenado.OrderBy(o => o.Prioridad) : ordenado.OrderByDescending(o => o.Prioridad);
                    break;
                case "tecnico":
                    ordenado = filtros.OrdenAscendente ? ordenado.OrderBy(o => o.TecnicoNombre) : ordenado.OrderByDescending(o => o.TecnicoNombre);
                    break;
                case "horas":
                    ordenado = filtros.OrdenAscendente ? ordenado.OrderBy(o => o.HorasTranscurridas) : ordenado.OrderByDescending(o => o.HorasTranscurridas);
                    break;
                default:
                    ordenado = ordenado.OrderByDescending(o => o.FechaInicio ?? DateTime.MinValue);
                    break;
            }

            return ordenado.ToList();
        }

        private List<ReporteOrdenTrabajo> CalcularMetricasOrdenesTrabajo(List<ReporteOrdenTrabajo> ordenes)
        {
            // Aquí puedes agregar cálculos adicionales de métricas
            // Por ejemplo: eficiencia, tiempos promedios, etc.
            return ordenes;
        }

        private List<ReporteDisponibilidad> CalcularEstadisticasDisponibilidad(List<ReporteDisponibilidad> disponibilidad)
        {
            // Cálculos adicionales para disponibilidad
            // Por ejemplo: porcentajes de utilización, etc.
            return disponibilidad;
        }
    }
}