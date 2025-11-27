using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaDominio;

namespace CapaDatos
{
    public class CD_Reportes
    {
        private CD_Conexion conexion = new CD_Conexion();

        // 1. REPORTE DE CLIENTES - usando vw_ClientesCompletos
        public List<ReporteCliente> ObtenerReporteClientes(ReporteFiltros filtros)
        {
            var lista = new List<ReporteCliente>();

            using (var conn = conexion.AbrirConexion())
            {
                string query = @"
                    SELECT 
                        ClienteID, DNI, Nombre, Apellido, Email, 
                        Telefono, Direccion, FechaRegistro, Activo, 
                        TotalVehiculos, Rol
                    FROM vw_ClientesCompletos 
                    WHERE 1=1";

                // Aplicar filtros dinámicos
                if (!string.IsNullOrEmpty(filtros.ClienteDNI))
                    query += " AND DNI LIKE @ClienteDNI";

                if (filtros.FechaInicio.HasValue)
                    query += " AND FechaRegistro >= @FechaInicio";

                if (filtros.FechaFin.HasValue)
                    query += " AND FechaRegistro <= @FechaFin";

                // Ordenamiento por defecto
                query += " ORDER BY FechaRegistro DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    // Parámetros
                    if (!string.IsNullOrEmpty(filtros.ClienteDNI))
                        cmd.Parameters.AddWithValue("@ClienteDNI", "%" + filtros.ClienteDNI + "%");

                    if (filtros.FechaInicio.HasValue)
                        cmd.Parameters.AddWithValue("@FechaInicio", filtros.FechaInicio.Value);

                    if (filtros.FechaFin.HasValue)
                        cmd.Parameters.AddWithValue("@FechaFin", filtros.FechaFin.Value);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new ReporteCliente
                            {
                                ClienteID = Convert.ToInt32(reader["ClienteID"]),
                                DNI = reader["DNI"]?.ToString() ?? "",
                                Nombre = reader["Nombre"]?.ToString() ?? "",
                                Apellido = reader["Apellido"]?.ToString() ?? "",
                                Email = reader["Email"]?.ToString() ?? "",
                                Telefono = reader["Telefono"]?.ToString() ?? "",
                                Direccion = reader["Direccion"]?.ToString() ?? "",
                                FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                                Activo = Convert.ToBoolean(reader["Activo"]),
                                TotalVehiculos = Convert.ToInt32(reader["TotalVehiculos"]),
                                Rol = reader["Rol"]?.ToString() ?? ""
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // 2. REPORTE DE CITAS - usando vw_CitasCompletas
        public List<ReporteCita> ObtenerReporteCitas(ReporteFiltros filtros)
        {
            var lista = new List<ReporteCita>();

            using (var conn = conexion.AbrirConexion())
            {
                string query = @"
                    SELECT 
                        CitaID, FechaCita, DuracionEstimada, Prioridad, Observaciones,
                        EstadoCita, Placa, VIN, Color, Modelo, Marca,
                        ClienteDNI, ClienteNombre, ClienteApellido, ClienteTelefono,
                        Bahia, Recepcionista
                    FROM vw_CitasCompletas 
                    WHERE 1=1";

                // Filtros
                if (filtros.FechaInicio.HasValue)
                    query += " AND FechaCita >= @FechaInicio";

                if (filtros.FechaFin.HasValue)
                    query += " AND FechaCita <= @FechaFin";

                if (!string.IsNullOrEmpty(filtros.Estado))
                    query += " AND EstadoCita = @Estado";

                if (!string.IsNullOrEmpty(filtros.ClienteDNI))
                    query += " AND ClienteDNI LIKE @ClienteDNI";

                // Ordenamiento
                query += " ORDER BY FechaCita DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    if (filtros.FechaInicio.HasValue)
                        cmd.Parameters.AddWithValue("@FechaInicio", filtros.FechaInicio.Value);

                    if (filtros.FechaFin.HasValue)
                        cmd.Parameters.AddWithValue("@FechaFin", filtros.FechaFin.Value);

                    if (!string.IsNullOrEmpty(filtros.Estado))
                        cmd.Parameters.AddWithValue("@Estado", filtros.Estado);

                    if (!string.IsNullOrEmpty(filtros.ClienteDNI))
                        cmd.Parameters.AddWithValue("@ClienteDNI", "%" + filtros.ClienteDNI + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new ReporteCita
                            {
                                CitaID = Convert.ToInt32(reader["CitaID"]),
                                FechaCita = Convert.ToDateTime(reader["FechaCita"]),
                                DuracionEstimada = Convert.ToInt32(reader["DuracionEstimada"]),
                                Prioridad = Convert.ToInt32(reader["Prioridad"]),
                                Observaciones = reader["Observaciones"]?.ToString() ?? "",
                                EstadoCita = reader["EstadoCita"]?.ToString() ?? "",
                                Placa = reader["Placa"]?.ToString() ?? "",
                                VIN = reader["VIN"]?.ToString() ?? "",
                                Color = reader["Color"]?.ToString() ?? "",
                                Modelo = reader["Modelo"]?.ToString() ?? "",
                                Marca = reader["Marca"]?.ToString() ?? "",
                                ClienteDNI = reader["ClienteDNI"]?.ToString() ?? "",
                                ClienteNombre = reader["ClienteNombre"]?.ToString() ?? "",
                                ClienteApellido = reader["ClienteApellido"]?.ToString() ?? "",
                                ClienteTelefono = reader["ClienteTelefono"]?.ToString() ?? "",
                                Bahia = reader["Bahia"]?.ToString() ?? "Sin asignar",
                                Recepcionista = reader["Recepcionista"]?.ToString() ?? "Sin asignar"
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // 3. REPORTE DE ÓRDENES DE TRABAJO - CORREGIDO CON CONVERSIONES SEGURAS
        public List<ReporteOrdenTrabajo> ObtenerReporteOrdenesTrabajo(ReporteFiltros filtros)
        {
            var lista = new List<ReporteOrdenTrabajo>();

            using (var conn = conexion.AbrirConexion())
            {
                string query = @"
                    SELECT 
                        OrdentrabajoID, FechaInicio, FechaFin, Prioridad, Descripcion,
                        EstadoOT, TecnicoID, TecnicoNombre, TecnicoApellido, Especialidad,
                        Placa, VIN, ClienteDNI, ClienteNombre, ClienteApellido, HorasTranscurridas
                    FROM vw_OrdenesTrabajoActivas 
                    WHERE 1=1";

                // Filtros
                if (filtros.FechaInicio.HasValue)
                    query += " AND FechaInicio >= @FechaInicio";

                if (filtros.FechaFin.HasValue)
                    query += " AND FechaInicio <= @FechaFin";

                if (!string.IsNullOrEmpty(filtros.Estado))
                    query += " AND EstadoOT = @Estado";

                if (!string.IsNullOrEmpty(filtros.TecnicoNombre))
                    query += " AND (TecnicoNombre LIKE @TecnicoNombre OR TecnicoApellido LIKE @TecnicoNombre)";

                // Ordenamiento
                query += " ORDER BY FechaInicio DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    if (filtros.FechaInicio.HasValue)
                        cmd.Parameters.AddWithValue("@FechaInicio", filtros.FechaInicio.Value);

                    if (filtros.FechaFin.HasValue)
                        cmd.Parameters.AddWithValue("@FechaFin", filtros.FechaFin.Value);

                    if (!string.IsNullOrEmpty(filtros.Estado))
                        cmd.Parameters.AddWithValue("@Estado", filtros.Estado);

                    if (!string.IsNullOrEmpty(filtros.TecnicoNombre))
                        cmd.Parameters.AddWithValue("@TecnicoNombre", "%" + filtros.TecnicoNombre + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new ReporteOrdenTrabajo
                            {
                                OrdentrabajoID = Convert.ToInt32(reader["OrdentrabajoID"]),
                                Descripcion = reader["Descripcion"]?.ToString() ?? "",
                                EstadoOT = reader["EstadoOT"]?.ToString() ?? "",
                                TecnicoNombre = reader["TecnicoNombre"]?.ToString() ?? "",
                                TecnicoApellido = reader["TecnicoApellido"]?.ToString() ?? "",
                                Especialidad = reader["Especialidad"]?.ToString() ?? "",
                                Placa = reader["Placa"]?.ToString() ?? "",
                                VIN = reader["VIN"]?.ToString() ?? "",
                                ClienteDNI = reader["ClienteDNI"]?.ToString() ?? "",
                                ClienteNombre = reader["ClienteNombre"]?.ToString() ?? "",
                                ClienteApellido = reader["ClienteApellido"]?.ToString() ?? ""
                            };

                            // Manejo seguro de campos que pueden ser NULL
                            if (reader["FechaInicio"] != DBNull.Value)
                                item.FechaInicio = Convert.ToDateTime(reader["FechaInicio"]);

                            if (reader["FechaFin"] != DBNull.Value)
                                item.FechaFin = Convert.ToDateTime(reader["FechaFin"]);

                            // Campos numéricos con conversión segura
                            item.Prioridad = reader["Prioridad"] != DBNull.Value ? Convert.ToInt32(reader["Prioridad"]) : 1;
                            item.TecnicoID = reader["TecnicoID"] != DBNull.Value ? Convert.ToInt32(reader["TecnicoID"]) : 0;
                            item.HorasTranscurridas = reader["HorasTranscurridas"] != DBNull.Value ? Convert.ToInt32(reader["HorasTranscurridas"]) : 0;

                            lista.Add(item);
                        }
                    }
                }
            }
            return lista;
        }

        // 4. REPORTE DE DISPONIBILIDAD - usando vw_DisponibilidadBahias
        public List<ReporteDisponibilidad> ObtenerReporteDisponibilidad(ReporteFiltros filtros)
        {
            var lista = new List<ReporteDisponibilidad>();

            using (var conn = conexion.AbrirConexion())
            {
                string query = @"
                    SELECT 
                        CalendariocapacidadID, Fecha, HoraInicio, HoraFin,
                        CapacidadDisponible, CapacidadTotal, Bahia, 
                        EstadoBahia, EstadoDisponibilidad
                    FROM vw_DisponibilidadBahias 
                    WHERE 1=1";

                // Filtros
                if (filtros.FechaInicio.HasValue)
                    query += " AND Fecha >= @FechaInicio";

                if (filtros.FechaFin.HasValue)
                    query += " AND Fecha <= @FechaFin";

                if (!string.IsNullOrEmpty(filtros.Estado))
                    query += " AND EstadoDisponibilidad = @Estado";

                // Ordenamiento
                query += " ORDER BY Fecha, HoraInicio, Bahia";

                using (var cmd = new SqlCommand(query, conn))
                {
                    if (filtros.FechaInicio.HasValue)
                        cmd.Parameters.AddWithValue("@FechaInicio", filtros.FechaInicio.Value);

                    if (filtros.FechaFin.HasValue)
                        cmd.Parameters.AddWithValue("@FechaFin", filtros.FechaFin.Value);

                    if (!string.IsNullOrEmpty(filtros.Estado))
                        cmd.Parameters.AddWithValue("@Estado", filtros.Estado);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new ReporteDisponibilidad
                            {
                                CalendariocapacidadID = Convert.ToInt32(reader["CalendariocapacidadID"]),
                                Fecha = Convert.ToDateTime(reader["Fecha"]),
                                HoraInicio = (TimeSpan)reader["HoraInicio"],
                                HoraFin = (TimeSpan)reader["HoraFin"],
                                CapacidadDisponible = Convert.ToInt32(reader["CapacidadDisponible"]),
                                CapacidadTotal = Convert.ToInt32(reader["CapacidadTotal"]),
                                Bahia = reader["Bahia"]?.ToString() ?? "",
                                EstadoBahia = reader["EstadoBahia"]?.ToString() ?? "",
                                EstadoDisponibilidad = reader["EstadoDisponibilidad"]?.ToString() ?? ""
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // 5. MÉTODO GENERAL PARA OBTENER REPORTE SEGÚN TIPO
        public object ObtenerReportePorTipo(ReporteFiltros filtros)
        {
            switch (filtros.TipoReporte?.ToUpper())
            {
                case "CLIENTES":
                    return ObtenerReporteClientes(filtros);
                case "CITAS":
                    return ObtenerReporteCitas(filtros);
                case "ORDENES":
                    return ObtenerReporteOrdenesTrabajo(filtros);
                case "DISPONIBILIDAD":
                    return ObtenerReporteDisponibilidad(filtros);
                default:
                    throw new ArgumentException("Tipo de reporte no válido");
            }
        }
    }
}