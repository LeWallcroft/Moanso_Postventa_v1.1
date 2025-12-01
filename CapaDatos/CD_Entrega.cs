using CapaDominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Entrega
    {
        private readonly CD_Conexion _conexion;

        public CD_Entrega()
        {
            _conexion = new CD_Conexion();
        }

        #region Métodos para obtener datos

        public DatosEntregaDTO ObtenerDatosEntrega(int ordentrabajoID)
        {
            Console.WriteLine($"🔍 Iniciando ObtenerDatosEntrega para OT: {ordentrabajoID}");

            var datosEntrega = new DatosEntregaDTO();

            try
            {
                using (var connection = _conexion.AbrirConexion())
                {
                    Console.WriteLine($"✅ Conexión abierta para OT: {ordentrabajoID}");

                    // 1. Obtener datos principales de la OT, control de calidad, cliente y vehículo
                    using (var command = new SqlCommand("sp_Entrega_ObtenerDatos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine($"✅ Datos encontrados para OT: {ordentrabajoID}");

                                try
                                {
                                    // DEBUG: Mostrar información de las columnas
                                    Console.WriteLine("=== DEBUG: Información de columnas ===");
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Console.WriteLine($"Col {i}: {reader.GetName(i)} = '{reader[i]}' (Tipo: {reader.GetFieldType(i)?.Name})");
                                    }
                                    Console.WriteLine("=== FIN DEBUG ===");

                                    // Mapear datos básicos
                                    datosEntrega.OrdentrabajoID = ordentrabajoID;
                                    datosEntrega.FechaInicio = SafeConvertToDateTime(reader["FechaInicio"]);
                                    datosEntrega.FechaFin = SafeConvertToDateTime(reader["FechaFin"]);
                                    datosEntrega.ObservacionesOT = SafeConvertToString(reader["ObservacionesOT"]);
                                    datosEntrega.KilometrajeEntrada = SafeConvertToInt32(reader["KilometrajeEntrada"]);
                                    datosEntrega.KilometrajeSalida = SafeConvertToInt32(reader["KilometrajeSalida"]);

                                    // Estado OT
                                    datosEntrega.EstadoOT = SafeConvertToString(reader["EstadoOTNombre"], "SIN ESTADO");
                                    datosEntrega.ColorEstadoOT = SafeConvertToString(reader["EstadoOTColor"], "#CCCCCC");
                                    datosEntrega.EstadoOTID = SafeConvertToInt32(reader["EstadootID"]);

                                    // Control de Calidad
                                    datosEntrega.ControlcalidadID = SafeConvertToNullableInt32(reader["ControlcalidadID"]);
                                    datosEntrega.TecnicoID = SafeConvertToNullableInt32(reader["TecnicoID"]);
                                    datosEntrega.NombreTecnico = SafeConvertToString(reader["NombreTecnico"], "NO ASIGNADO");
                                    datosEntrega.FechaControl = SafeConvertToDateTime(reader["FechaControl"]);
                                    datosEntrega.ObservacionesControl = SafeConvertToString(reader["ObservacionesControl"]);
                                    datosEntrega.Resultado = SafeConvertToString(reader["Resultado"], "PENDIENTE");
                                    datosEntrega.Calificacion = SafeConvertToNullableInt32(reader["Calificacion"]);

                                    // Cliente
                                    datosEntrega.ClienteID = SafeConvertToInt32(reader["ClienteID"]);
                                    datosEntrega.NombreCliente = SafeConvertToString(reader["NombreCliente"], "CLIENTE NO ENCONTRADO");
                                    datosEntrega.DNI = SafeConvertToString(reader["DNI"]);
                                    datosEntrega.Email = SafeConvertToString(reader["Email"]);
                                    datosEntrega.Telefono = SafeConvertToString(reader["Telefono"]);

                                    // Vehículo
                                    datosEntrega.VehiculoID = SafeConvertToInt32(reader["VehiculoID"]);
                                    datosEntrega.Placa = SafeConvertToString(reader["Placa"], "SIN PLACA");
                                    datosEntrega.Color = SafeConvertToString(reader["Color"]);
                                    datosEntrega.Anio = SafeConvertToNullableInt32(reader["Anio"]);
                                    datosEntrega.Modelo = SafeConvertToString(reader["Modelo"], "SIN MODELO");
                                    datosEntrega.Marca = SafeConvertToString(reader["Marca"], "SIN MARCA");
                                    datosEntrega.KilometrajeActual = SafeConvertToNullableInt32(reader["KilometrajeActual"]);
                                    datosEntrega.CombustibleVehiculo = SafeConvertToString(reader["Combustible"]);
                                    datosEntrega.Transmision = SafeConvertToString(reader["Transmision"]);

                                    // Datos de la Entrega
                                    datosEntrega.EntregavehiculoID = SafeConvertToNullableInt32(reader["EntregavehiculoID"]);
                                    datosEntrega.FechaEntrega = SafeConvertToDateTime(reader["FechaEntrega"]);
                                    datosEntrega.KilometrajeEntrega = SafeConvertToNullableInt32(reader["KilometrajeEntrega"]);
                                    datosEntrega.CombustibleEntrega = SafeConvertToString(reader["CombustibleEntrega"]);
                                    datosEntrega.ObservacionesEntrega = SafeConvertToString(reader["ObservacionesEntrega"]);
                                    datosEntrega.FirmaCliente = SafeConvertToString(reader["FirmaCliente"]);
                                    datosEntrega.UsuariosEntregaID = SafeConvertToNullableInt32(reader["UsuariosEntregaID"]);
                                    datosEntrega.NombreUsuarioEntrega = SafeConvertToString(reader["NombreUsuarioEntrega"]);
                                    datosEntrega.AprobacionCliente = SafeConvertToNullableBoolean(reader["AprobacionCliente"]);
                                    datosEntrega.FechaAprobacion = SafeConvertToDateTime(reader["FechaAprobacion"]);
                                    datosEntrega.ObservacionesCliente = SafeConvertToString(reader["ObservacionesCliente"]);

                                    Console.WriteLine($"✅ Datos mapeados exitosamente para OT: {ordentrabajoID}");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"🔥 ERROR CRÍTICO en mapeo de datos: {ex.Message}");
                                    Console.WriteLine($"🔥 StackTrace: {ex.StackTrace}");
                                    throw new Exception($"Error al mapear datos de entrega para OT {ordentrabajoID}: {ex.Message}", ex);
                                }
                            }
                            else
                            {
                                throw new Exception($"No se encontraron datos para la orden de trabajo #{ordentrabajoID}");
                            }
                        }
                    }

                    Console.WriteLine($"✅ Datos principales obtenidos para OT: {ordentrabajoID}");

                    // 2. Obtener detalles del control de calidad
                    if (datosEntrega.ControlcalidadID.HasValue)
                    {
                        Console.WriteLine($"✅ Obteniendo detalles control calidad ID: {datosEntrega.ControlcalidadID.Value}");
                        datosEntrega.DetallesControl = ObtenerDetallesControlCalidad(datosEntrega.ControlcalidadID.Value, connection);
                    }
                    else
                    {
                        Console.WriteLine($"✅ Obteniendo detalles por OT: {ordentrabajoID}");
                        datosEntrega.DetallesControl = ObtenerDetallesControlCalidadPorOT(ordentrabajoID, connection);
                    }

                    Console.WriteLine($"✅ Detalles control obtenidos: {datosEntrega.DetallesControl?.Count ?? 0} registros");

                    // 3. Obtener documentos si ya existe entrega
                    if (datosEntrega.EntregavehiculoID.HasValue)
                    {
                        Console.WriteLine($"✅ Obteniendo documentos entrega ID: {datosEntrega.EntregavehiculoID.Value}");
                        datosEntrega.Documentos = ObtenerDocumentosEntrega(datosEntrega.EntregavehiculoID.Value, connection);
                    }

                    // 4. Verificar si está lista para entrega
                    Console.WriteLine($"✅ Verificando si OT está lista para entrega: {ordentrabajoID}");
                    datosEntrega.EstaListaParaEntrega = VerificarOTListaParaEntrega(ordentrabajoID, connection);

                    Console.WriteLine($"✅✅✅ ObtenerDatosEntrega COMPLETADO EXITOSAMENTE para OT: {ordentrabajoID}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥🔥🔥 ERROR GLOBAL en ObtenerDatosEntrega: {ex.Message}");
                Console.WriteLine($"🔥🔥🔥 Inner Exception: {ex.InnerException?.Message}");
                Console.WriteLine($"🔥🔥🔥 StackTrace: {ex.StackTrace}");
                throw new Exception($"Error al obtener datos de entrega para OT {ordentrabajoID}: {ex.Message}", ex);
            }

            return datosEntrega;
        }

        #region Métodos Helper para conversiones seguras

        private int SafeConvertToInt32(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                Console.WriteLine($"DEBUG: SafeConvertToInt32 - Valor es NULL/DBNull, retornando 0");
                return 0;
            }

            try
            {
                string stringValue = value.ToString();

                // MANEJO ESPECIAL PARA VALORES DE COLOR COMO "primary"
                if (stringValue.Trim().Equals("primary", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"DEBUG: SafeConvertToInt32 - Valor '{stringValue}' detectado como 'primary', retornando 0");
                    return 0;
                }

                // MANEJO ESPECIAL PARA VALORES DE COLOR COMO "success", "warning", etc.
                if (stringValue.Trim().Equals("success", StringComparison.OrdinalIgnoreCase) ||
                    stringValue.Trim().Equals("warning", StringComparison.OrdinalIgnoreCase) ||
                    stringValue.Trim().Equals("danger", StringComparison.OrdinalIgnoreCase) ||
                    stringValue.Trim().Equals("info", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"DEBUG: SafeConvertToInt32 - Valor '{stringValue}' detectado como color, retornando 0");
                    return 0;
                }

                if (int.TryParse(stringValue, out int result))
                {
                    Console.WriteLine($"DEBUG: SafeConvertToInt32 - '{stringValue}' parseado exitosamente a {result}");
                    return result;
                }
                else
                {
                    Console.WriteLine($"ADVERTENCIA: No se puede convertir '{stringValue}' a Int32. Valor por defecto: 0");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR en SafeConvertToInt32: Valor='{value}', Error: {ex.Message}");
                return 0;
            }
        }

        private int? SafeConvertToNullableInt32(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                Console.WriteLine($"DEBUG: SafeConvertToNullableInt32 - Valor es NULL/DBNull, retornando null");
                return null;
            }

            try
            {
                string stringValue = value.ToString();

                // MANEJO ESPECIAL PARA VALORES DE COLOR
                if (stringValue.Trim().Equals("primary", StringComparison.OrdinalIgnoreCase) ||
                    stringValue.Trim().Equals("success", StringComparison.OrdinalIgnoreCase) ||
                    stringValue.Trim().Equals("warning", StringComparison.OrdinalIgnoreCase) ||
                    stringValue.Trim().Equals("danger", StringComparison.OrdinalIgnoreCase) ||
                    stringValue.Trim().Equals("info", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"DEBUG: SafeConvertToNullableInt32 - Valor '{stringValue}' detectado como color, retornando null");
                    return null;
                }

                if (int.TryParse(stringValue, out int result))
                {
                    Console.WriteLine($"DEBUG: SafeConvertToNullableInt32 - '{stringValue}' parseado exitosamente a {result}");
                    return result;
                }
                else
                {
                    Console.WriteLine($"ADVERTENCIA: No se puede convertir '{stringValue}' a Int32?. Valor por defecto: null");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR en SafeConvertToNullableInt32: Valor='{value}', Error: {ex.Message}");
                return null;
            }
        }

        private string SafeConvertToString(object value, string defaultValue = "")
        {
            if (value == null || value == DBNull.Value)
            {
                Console.WriteLine($"DEBUG: SafeConvertToString - Valor es NULL/DBNull, retornando '{defaultValue}'");
                return defaultValue;
            }

            try
            {
                string result = value.ToString();
                Console.WriteLine($"DEBUG: SafeConvertToString - Convertido '{result}'");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR en SafeConvertToString: Valor='{value}', Error: {ex.Message}");
                return defaultValue;
            }
        }

        private DateTime? SafeConvertToDateTime(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                Console.WriteLine($"DEBUG: SafeConvertToDateTime - Valor es NULL/DBNull, retornando null");
                return null;
            }

            try
            {
                if (value is string stringValue)
                {
                    if (DateTime.TryParse(stringValue, out DateTime result))
                    {
                        Console.WriteLine($"DEBUG: SafeConvertToDateTime - '{stringValue}' parseado exitosamente a {result}");
                        return result;
                    }
                    else
                    {
                        Console.WriteLine($"ADVERTENCIA: No se puede convertir '{stringValue}' a DateTime. Valor por defecto: null");
                        return null;
                    }
                }

                DateTime resultConverted = Convert.ToDateTime(value);
                Console.WriteLine($"DEBUG: SafeConvertToDateTime - Convertido exitosamente a {resultConverted}");
                return resultConverted;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR en SafeConvertToDateTime: Valor='{value}', Error: {ex.Message}");
                return null;
            }
        }

        private bool? SafeConvertToNullableBoolean(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                Console.WriteLine($"DEBUG: SafeConvertToNullableBoolean - Valor es NULL/DBNull, retornando null");
                return null;
            }

            try
            {
                if (value is string stringValue)
                {
                    if (bool.TryParse(stringValue, out bool result))
                    {
                        Console.WriteLine($"DEBUG: SafeConvertToNullableBoolean - '{stringValue}' parseado exitosamente a {result}");
                        return result;
                    }
                    else if (stringValue == "1" || stringValue.ToUpper() == "TRUE" || stringValue.ToUpper() == "SI")
                    {
                        Console.WriteLine($"DEBUG: SafeConvertToNullableBoolean - '{stringValue}' interpretado como true");
                        return true;
                    }
                    else if (stringValue == "0" || stringValue.ToUpper() == "FALSE" || stringValue.ToUpper() == "NO")
                    {
                        Console.WriteLine($"DEBUG: SafeConvertToNullableBoolean - '{stringValue}' interpretado como false");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine($"ADVERTENCIA: No se puede convertir '{stringValue}' a bool?. Valor por defecto: null");
                        return null;
                    }
                }

                bool resultConverted = Convert.ToBoolean(value);
                Console.WriteLine($"DEBUG: SafeConvertToNullableBoolean - Convertido exitosamente a {resultConverted}");
                return resultConverted;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR en SafeConvertToNullableBoolean: Valor='{value}', Error: {ex.Message}");
                return null;
            }
        }

        #endregion


        private List<DetalleControlDTO> ObtenerDetallesControlCalidad(int controlcalidadID, SqlConnection connection)
        {
            var detalles = new List<DetalleControlDTO>();

            try
            {
                using (var command = new SqlCommand("sp_Entrega_ObtenerDetallesControl", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ControlcalidadID", controlcalidadID);
                    command.Parameters.AddWithValue("@OrdentrabajoID", DBNull.Value);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            detalles.Add(new DetalleControlDTO
                            {
                                ControlcalidaddetalleID = SafeConvertToInt32(reader["ControlcalidaddetalleID"]),
                                Descripcion = SafeConvertToString(reader["Descripcion"]),
                                Estado = SafeConvertToString(reader["Estado"], "PENDIENTE"),
                                Observaciones = SafeConvertToString(reader["Observaciones"]),
                                FechaVerificacion = SafeConvertToDateTime(reader["FechaVerificacion"]) ?? DateTime.Now,
                                ActividadDescripcion = SafeConvertToString(reader["ActividadDescripcion"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR en ObtenerDetallesControlCalidad: {ex.Message}");
            }

            return detalles;
        }

        private List<DetalleControlDTO> ObtenerDetallesControlCalidadPorOT(int ordentrabajoID, SqlConnection connection)
        {
            var detalles = new List<DetalleControlDTO>();

            try
            {
                using (var command = new SqlCommand("sp_Entrega_ObtenerDetallesControl", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ControlcalidadID", DBNull.Value);
                    command.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            detalles.Add(new DetalleControlDTO
                            {
                                ControlcalidaddetalleID = SafeConvertToInt32(reader["ControlcalidaddetalleID"]),
                                Descripcion = SafeConvertToString(reader["Descripcion"]),
                                Estado = SafeConvertToString(reader["Estado"], "PENDIENTE"),
                                Observaciones = SafeConvertToString(reader["Observaciones"]),
                                FechaVerificacion = SafeConvertToDateTime(reader["FechaVerificacion"]) ?? DateTime.Now,
                                ActividadDescripcion = SafeConvertToString(reader["ActividadDescripcion"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR en ObtenerDetallesControlCalidadPorOT: {ex.Message}");
            }

            return detalles;
        }

        private List<DocumentoEntregaDTO> ObtenerDocumentosEntrega(int entregavehiculoID, SqlConnection connection)
        {
            var documentos = new List<DocumentoEntregaDTO>();

            try
            {
                using (var command = new SqlCommand("sp_Entrega_ListarDocumentos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EntregavehiculoID", entregavehiculoID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            documentos.Add(new DocumentoEntregaDTO
                            {
                                DocumentoentregaID = SafeConvertToInt32(reader["DocumentoentregaID"]),
                                TipoDocumento = SafeConvertToString(reader["TipoDocumento"]),
                                Descripcion = SafeConvertToString(reader["Descripcion"]),
                                ArchivoUrl = SafeConvertToString(reader["ArchivoUrl"]),
                                FechaCreacion = SafeConvertToDateTime(reader["FechaCreacion"]) ?? DateTime.Now,
                                NombreUsuario = SafeConvertToString(reader["NombreUsuario"], "SIN USUARIO")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR en ObtenerDocumentosEntrega: {ex.Message}");
            }

            return documentos;
        }

        private bool VerificarOTListaParaEntrega(int ordentrabajoID, SqlConnection connection)
        {
            try
            {
                using (var command = new SqlCommand("sp_Entrega_VerificarEstadoOT", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);

                    var param = new SqlParameter("@EstaListaParaEntrega", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(param);

                    command.ExecuteNonQuery();

                    return param.Value != DBNull.Value && Convert.ToBoolean(param.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR en VerificarOTListaParaEntrega: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Métodos para aprobación/rechazo

        public string RegistrarAprobacionCliente(SolicitudAprobacionDTO solicitud)
        {
            string mensaje = "";

            using (var connection = _conexion.AbrirConexion())
            {
                try
                {
                    using (var command = new SqlCommand("sp_Entrega_AprobarCliente", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EntregavehiculoID", solicitud.EntregavehiculoID);
                        command.Parameters.AddWithValue("@AprobacionCliente", solicitud.Aprobacion);
                        command.Parameters.AddWithValue("@ObservacionesCliente",
                            string.IsNullOrEmpty(solicitud.Observaciones) ? DBNull.Value : (object)solicitud.Observaciones);
                        command.Parameters.AddWithValue("@FirmaCliente",
                            string.IsNullOrEmpty(solicitud.FirmaCliente) ? DBNull.Value : (object)solicitud.FirmaCliente);

                        var paramMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(paramMensaje);

                        command.ExecuteNonQuery();

                        mensaje = paramMensaje.Value != DBNull.Value ? paramMensaje.Value.ToString() : "Procesado sin mensaje";
                    }
                }
                catch (Exception ex)
                {
                    mensaje = $"Error al registrar aprobación: {ex.Message}";
                }
            }

            return mensaje;
        }

        public string CrearEntrega(int ordentrabajoID, int usuarioID, string observaciones = null)
        {
            string mensaje = "";

            using (var connection = _conexion.AbrirConexion())
            {
                try
                {
                    using (var command = new SqlCommand("sp_GenerarOrdenEntrega", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);
                        command.Parameters.AddWithValue("@UsuarioEntregaID", usuarioID);
                        command.Parameters.AddWithValue("@Kilometraje", DBNull.Value);
                        command.Parameters.AddWithValue("@Combustible", DBNull.Value);
                        command.Parameters.AddWithValue("@Observaciones",
                            string.IsNullOrEmpty(observaciones) ? DBNull.Value : (object)observaciones);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var entregavehiculoID = reader["EntregavehiculoID"];
                                mensaje = SafeConvertToString(reader["Mensaje"], "Entrega creada exitosamente");
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    mensaje = $"Error al crear entrega: {ex.Message}";
                }
                catch (Exception ex)
                {
                    mensaje = $"Error inesperado al crear entrega: {ex.Message}";
                }
            }

            return mensaje;
        }

        #endregion

        #region Métodos para documentos

        public string AgregarDocumento(DocumentoEntregaRequestDTO documentoRequest)
        {
            string mensaje = "";

            using (var connection = _conexion.AbrirConexion())
            {
                try
                {
                    using (var command = new SqlCommand("sp_Entrega_GenerarDocumentos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EntregavehiculoID", documentoRequest.EntregavehiculoID);
                        command.Parameters.AddWithValue("@TipoDocumento", documentoRequest.TipoDocumento);
                        command.Parameters.AddWithValue("@Descripcion",
                            string.IsNullOrEmpty(documentoRequest.Descripcion) ? DBNull.Value : (object)documentoRequest.Descripcion);
                        command.Parameters.AddWithValue("@ArchivoUrl", documentoRequest.NombreArchivo);
                        command.Parameters.AddWithValue("@UsuariosID", documentoRequest.UsuarioID);

                        var paramMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(paramMensaje);

                        command.ExecuteNonQuery();

                        mensaje = paramMensaje.Value != DBNull.Value ? paramMensaje.Value.ToString() : "Documento agregado exitosamente";

                        // Si el documento se guardó exitosamente, guardamos el archivo físicamente
                        if (mensaje.Contains("exitosamente") && !string.IsNullOrEmpty(documentoRequest.ArchivoBase64))
                        {
                            GuardarArchivoFisico(documentoRequest.ArchivoBase64, documentoRequest.NombreArchivo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    mensaje = $"Error al agregar documento: {ex.Message}";
                }
            }

            return mensaje;
        }

        private void GuardarArchivoFisico(string archivoBase64, string nombreArchivo)
        {
            try
            {
                // Convertir base64 a bytes
                byte[] fileBytes = Convert.FromBase64String(archivoBase64);

                // Definir ruta de almacenamiento
                string carpetaDocumentos = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "DocumentosEntrega"
                );

                // Crear carpeta si no existe
                if (!System.IO.Directory.Exists(carpetaDocumentos))
                {
                    System.IO.Directory.CreateDirectory(carpetaDocumentos);
                }

                // Guardar archivo
                string rutaCompleta = System.IO.Path.Combine(carpetaDocumentos, nombreArchivo);
                System.IO.File.WriteAllBytes(rutaCompleta, fileBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar archivo: {ex.Message}");
            }
        }

        public string ObtenerRutaArchivo(string nombreArchivo)
        {
            try
            {
                string carpetaDocumentos = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "DocumentosEntrega"
                );

                string rutaCompleta = System.IO.Path.Combine(carpetaDocumentos, nombreArchivo);

                return System.IO.File.Exists(rutaCompleta) ? rutaCompleta : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener ruta archivo: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Métodos para reapertura de OT

        public string ReabrirOrdenTrabajo(int ordentrabajoID, string motivo, int usuarioID)
        {
            string mensaje = "";

            using (var connection = _conexion.AbrirConexion())
            {
                try
                {
                    // Cambiar estado
                    using (var command = new SqlCommand("sp_Ordentrabajo_CambiarEstado", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);
                        command.Parameters.AddWithValue("@EstadootID", 2); // Estado 2 = EN PROCESO

                        command.ExecuteNonQuery();
                    }

                    // Actualizar observaciones
                    using (var command = new SqlCommand("sp_ActualizarObservaciones", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);
                        command.Parameters.AddWithValue("@Observaciones",
                            $"[REAPERTURA {DateTime.Now:dd/MM/yyyy HH:mm}] Por rechazo en entrega. Motivo: {motivo}");

                        command.ExecuteNonQuery();
                    }

                    mensaje = "Orden de trabajo reabierta exitosamente. La OT ahora está en estado EN PROCESO.";
                }
                catch (Exception ex)
                {
                    mensaje = $"Error al reabrir orden de trabajo: {ex.Message}";
                }
            }

            return mensaje;
        }

        #endregion

        #region Métodos de búsqueda y listado

        public List<OrdentrabajoResumenDTO> BuscarOrdenesParaEntrega(string filtro = "")
        {
            var ordenes = new List<OrdentrabajoResumenDTO>();

            using (var connection = _conexion.AbrirConexion())
            {
                try
                {
                    Console.WriteLine($"🔍 CD_Entrega - Buscando órdenes para entrega con filtro: '{filtro}'");

                    // QUERY CORREGIDA: Mostrar TODOS los estados relevantes para entregas
                    var query = @"
                SELECT DISTINCT
                    ot.OrdentrabajoID,
                    ot.FechaCreacion,
                    ot.EstadootID,
                    eot.Nombre AS EstadoNombre,
                    eot.Color AS EstadoColor,
                    v.Placa,
                    c.Nombre + ' ' + c.Apellido AS NombreCliente,
                    m.Nombre AS Modelo,
                    ma.Nombre AS Marca,
                    CASE 
                        -- Si ya tiene entrega creada
                        WHEN ev.EntregavehiculoID IS NOT NULL THEN 
                            CASE 
                                WHEN ev.AprobacionCliente = 1 THEN 'ENTREGA APROBADA'
                                WHEN ev.AprobacionCliente = 0 THEN 'ENTREGA RECHAZADA'
                                ELSE 'ENTREGA PENDIENTE'
                            END
                        -- Si NO tiene entrega pero tiene control de calidad
                        WHEN cc.Resultado = 'APROBADO' THEN 'LISTA PARA ENTREGA'
                        WHEN cc.Resultado = 'RECHAZADO' THEN 'CONTROL RECHAZADO'
                        WHEN cc.Resultado IS NULL THEN 'SIN CONTROL CALIDAD'
                        ELSE 'EN CONTROL CALIDAD'
                    END AS EstadoEntrega,
                    -- Datos adicionales para diagnóstico
                    cc.Resultado AS ResultadoControl,
                    ev.EntregavehiculoID,
                    ev.AprobacionCliente
                FROM Ordentrabajo ot
                INNER JOIN Estadoot eot ON ot.EstadootID = eot.EstadootID
                INNER JOIN Cita ci ON ot.CitaID = ci.CitaID
                INNER JOIN Vehiculo v ON ci.VehiculoID = v.VehiculoID
                INNER JOIN Cliente c ON v.ClienteID = c.ClienteID
                INNER JOIN Modelo m ON v.ModeloID = m.ModeloID
                INNER JOIN Marca ma ON m.MarcaID = ma.MarcaID
                LEFT JOIN Controlcalidad cc ON EXISTS (
                    SELECT 1 FROM Controlcalidaddetalle ccd
                    INNER JOIN Otactividad oa ON ccd.OtactividadID = oa.OtactividadID
                    WHERE ccd.ControlcalidadID = cc.ControlcalidadID
                    AND oa.OrdentrabajoID = ot.OrdentrabajoID
                )
                LEFT JOIN Entregavehiculo ev ON ev.OrdentrabajoID = ot.OrdentrabajoID
                -- MOSTRAR TODAS LAS OTs (quitamos el filtro restrictivo)
                WHERE ot.EstadootID IN (1, 2, 3, 4, 5, 6)  -- Todos los estados posibles
                -- Opcional: Filtrar solo OTs que estén avanzadas (completadas o en proceso)
                -- WHERE ot.EstadootID IN (3, 4, 5, 6)  -- EN PROGRESO, APROBADO, LISTA, ENTREGADO
            ";

                    if (!string.IsNullOrEmpty(filtro))
                    {
                        query += @" AND (
                    v.Placa LIKE @Filtro OR 
                    c.Nombre LIKE @Filtro OR 
                    c.Apellido LIKE @Filtro OR 
                    c.DNI LIKE @Filtro OR
                    CONVERT(VARCHAR, ot.OrdentrabajoID) LIKE @Filtro OR
                    m.Nombre LIKE @Filtro OR
                    ma.Nombre LIKE @Filtro
                )";
                    }

                    query += " ORDER BY ot.EstadootID, ot.FechaCreacion DESC";

                    Console.WriteLine($"🔍 CD_Entrega - Query ejecutada");

                    using (var command = new SqlCommand(query, connection))
                    {
                        if (!string.IsNullOrEmpty(filtro))
                        {
                            command.Parameters.AddWithValue("@Filtro", $"%{filtro}%");
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            int count = 0;
                            Console.WriteLine($"📋 RESULTADOS DE BÚSQUEDA:");
                            Console.WriteLine($"{"OT",-8} {"Placa",-10} {"Cliente",-20} {"Estado OT",-15} {"Estado Entrega",-20} {"Control",-10} {"Entrega ID",-10}");
                            Console.WriteLine(new string('-', 93));

                            while (reader.Read())
                            {
                                count++;
                                var orden = new OrdentrabajoResumenDTO
                                {
                                    OrdentrabajoID = SafeConvertToInt32(reader["OrdentrabajoID"]),
                                    FechaCreacion = SafeConvertToDateTime(reader["FechaCreacion"]) ?? DateTime.Now,
                                    EstadoOT = SafeConvertToString(reader["EstadoNombre"]),
                                    ColorEstadoOT = SafeConvertToString(reader["EstadoColor"]),
                                    Placa = SafeConvertToString(reader["Placa"]),
                                    NombreCliente = SafeConvertToString(reader["NombreCliente"]),
                                    Modelo = SafeConvertToString(reader["Modelo"]),
                                    Marca = SafeConvertToString(reader["Marca"]),
                                    EstadoEntrega = SafeConvertToString(reader["EstadoEntrega"])
                                };

                                // Datos para diagnóstico
                                string resultadoControl = SafeConvertToString(reader["ResultadoControl"], "SIN CONTROL");
                                int? entregaID = SafeConvertToNullableInt32(reader["EntregavehiculoID"]);

                                Console.WriteLine($"{orden.NumeroOT,-8} {orden.Placa,-10} {orden.NombreCliente,-20} {orden.EstadoOT,-15} {orden.EstadoEntrega,-20} {resultadoControl,-10} {(entregaID.HasValue ? entregaID.Value.ToString() : "NO"),-10}");

                                ordenes.Add(orden);
                            }
                            Console.WriteLine(new string('-', 93));
                            Console.WriteLine($"✅ CD_Entrega - Encontradas {count} órdenes");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"🔥 CD_Entrega - ERROR en BuscarOrdenesParaEntrega: {ex.Message}");
                    Console.WriteLine($"🔥 StackTrace: {ex.StackTrace}");
                }
            }

            return ordenes;
        }

        #endregion

        #region DTOs auxiliares

        public class OrdentrabajoResumenDTO
        {
            public int OrdentrabajoID { get; set; }
            public DateTime FechaCreacion { get; set; }
            public string EstadoOT { get; set; }
            public string ColorEstadoOT { get; set; }
            public string Placa { get; set; }
            public string NombreCliente { get; set; }
            public string Modelo { get; set; }
            public string Marca { get; set; }
            public string EstadoEntrega { get; set; }

            public string NumeroOT => $"OT-{OrdentrabajoID:00000}";
            public string VehiculoCompleto => $"{Marca} {Modelo}";
        }

        #endregion
    }
}