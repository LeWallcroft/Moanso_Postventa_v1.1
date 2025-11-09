using CapaDominio;
using CapaDatos;
using System;
using System.Data;

namespace CapaLogicaNegocio
{
    public class CN_Diagnostico
    {
        private CD_Diagnostico diagnosticoDAL;

        public CN_Diagnostico()
        {
            diagnosticoDAL = new CD_Diagnostico();
        }

        public DataTable ObtenerClientesConCitas()
        {
            try
            {
                return diagnosticoDAL.ObtenerClientesConCitas();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener clientes con citas: " + ex.Message);
            }
        }

        public CitaDetalle ObtenerDetalleCita(int citaId)
        {
            try
            {
                DataTable dt = diagnosticoDAL.ObtenerDetalleCita(citaId);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    // Crear el objeto con manejo seguro de valores NULL
                    var citaDetalle = new CitaDetalle
                    {
                        CitaId = Convert.ToInt32(row["CitaId"]),
                        FechaCita = Convert.ToDateTime(row["FechaCita"]),
                        EstadoCita = SafeGetString(row, "EstadoCita"),
                        ClienteId = Convert.ToInt32(row["ClienteId"]),
                        ClienteNombre = SafeGetString(row, "ClienteNombre"),
                        DNI = SafeGetString(row, "DNI"),
                        Email = SafeGetString(row, "Email"),                    // Nueva propiedad
                        Telefono = SafeGetString(row, "Telefono"),              // Nueva propiedad
                        Direccion = SafeGetString(row, "Direccion"),            // Nueva propiedad
                        VehiculoId = Convert.ToInt32(row["VehiculoId"]),
                        Placa = SafeGetString(row, "Placa"),
                        VIN = SafeGetString(row, "VIN"),
                        Anio = SafeGetInt(row, "Anio"),
                        Marca = SafeGetString(row, "Marca"),
                        Modelo = SafeGetString(row, "Modelo"),
                        ServicioId = Convert.ToInt32(row["ServicioId"]),
                        Servicio = SafeGetString(row, "Servicio"),
                        TipoServicio = SafeGetString(row, "TipoServicio"),
                        BahiaId = Convert.ToInt32(row["BahiaId"]),
                        Bahia = SafeGetString(row, "Bahia"),
                        TecnicoId = SafeGetInt(row, "TecnicoId"),
                        Tecnico = SafeGetString(row, "Tecnico"),
                        OTId = SafeGetInt(row, "OTId"),
                        NumeroOT = SafeGetString(row, "NumeroOT"),
                        EstadoOT = SafeGetString(row, "EstadoOT"),
                        DuracionEstimadaMin = SafeGetInt(row, "DuracionEstimadaMin")
                    };

                    // Manejar HoraCita (TIME de SQL a TimeSpan de C#)
                    if (row["HoraCita"] != DBNull.Value)
                    {
                        citaDetalle.HoraCita = (TimeSpan)row["HoraCita"];
                    }

                    // Manejar FechaApertura
                    if (row["FechaApertura"] != DBNull.Value)
                    {
                        citaDetalle.FechaApertura = Convert.ToDateTime(row["FechaApertura"]);
                    }
                    else
                    {
                        citaDetalle.FechaApertura = DateTime.MinValue;
                    }

                    // Manejar FechaRegistro
                    if (row["FechaRegistro"] != DBNull.Value)
                    {
                        citaDetalle.FechaRegistro = Convert.ToDateTime(row["FechaRegistro"]);
                    }

                    // CORRECCIÓN: HoraInicioReal es DATETIME2, extraer solo la parte de tiempo
                    if (row["HoraInicioReal"] != DBNull.Value)
                    {
                        DateTime horaInicioReal = Convert.ToDateTime(row["HoraInicioReal"]);
                        citaDetalle.HoraInicioReal = horaInicioReal.TimeOfDay; // Extraer solo la parte Time
                    }

                    return citaDetalle;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener detalle de cita: " + ex.Message);
            }
        }

        // Métodos auxiliares para manejo seguro de valores NULL
        private string SafeGetString(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? row[columnName].ToString() : string.Empty;
        }

        private int SafeGetInt(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? Convert.ToInt32(row[columnName]) : 0;
        }

        public bool CrearDiagnostico(int citaId, string hallazgos, string recomendaciones, int tecnicoId, out string mensaje)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(hallazgos))
                {
                    mensaje = "Los hallazgos son obligatorios";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(recomendaciones))
                {
                    mensaje = "Las recomendaciones son obligatorias";
                    return false;
                }

                if (tecnicoId <= 0)
                {
                    mensaje = "Debe seleccionar un técnico";
                    return false;
                }

                return diagnosticoDAL.CrearDiagnostico(citaId, hallazgos, recomendaciones, tecnicoId, out mensaje);
            }
            catch (Exception ex)
            {
                mensaje = "Error al crear diagnóstico: " + ex.Message;
                return false;
            }
        }

        public bool ValidarCitaParaDiagnostico(int citaId)
        {
            try
            {
                CitaDetalle cita = ObtenerDetalleCita(citaId);
                if (cita == null)
                {
                    return false;
                }

                // Verificar que existe orden de trabajo
                if (cita.OTId == 0 || cita.EstadoOT == "Sin OT")
                {
                    return false;
                }

                // Verificar que no existe diagnóstico previo
                return !diagnosticoDAL.ExisteDiagnostico(citaId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExisteDiagnostico(int citaId)
        {
            try
            {
                return diagnosticoDAL.ExisteDiagnostico(citaId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Diagnostico ObtenerDiagnosticoExistente(int citaId)
        {
            try
            {
                DataTable dt = diagnosticoDAL.ObtenerDiagnosticoPorCita(citaId);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new Diagnostico
                    {
                        DiagnosticoId = Convert.ToInt32(row["DiagnosticoId"]),
                        CitaId = Convert.ToInt32(row["CitaId"]),
                        Hallazgos = SafeGetString(row, "Hallazgos"),
                        Recomendaciones = SafeGetString(row, "Recomendaciones"),
                        TecnicoId = SafeGetInt(row, "TecnicoId"),
                        Fecha = Convert.ToDateTime(row["Fecha"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener diagnóstico existente: " + ex.Message);
            }
        }
    }
}