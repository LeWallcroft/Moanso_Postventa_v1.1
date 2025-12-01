using System;
using System.Collections.Generic;
using System.Linq;
using CapaDatos;
using CapaDominio;

namespace CapaLogicaNegocio
{
    public class CN_Entrega
    {
        private readonly CD_Entrega _cdEntrega;
        private readonly CD_OrdenTrabajo _cdOrdenTrabajo;

        public CN_Entrega()
        {
            _cdEntrega = new CD_Entrega();
            _cdOrdenTrabajo = new CD_OrdenTrabajo();
        }

        #region Métodos para gestión de entregas

        /// <summary>
        /// Obtiene todos los datos necesarios para la entrega de una OT
        /// </summary>
        public ResultadoOperacion<DatosEntregaDTO> ObtenerDatosEntrega(int ordentrabajoID)
        {
            try
            {
                Console.WriteLine($"🔄 CN_Entrega - Iniciando ObtenerDatosEntrega para OT: {ordentrabajoID}");

                // Validar entrada
                if (ordentrabajoID <= 0)
                {
                    Console.WriteLine($"❌ CN_Entrega - ID de OT no válido: {ordentrabajoID}");
                    return new ResultadoOperacion<DatosEntregaDTO>
                    {
                        Exitoso = false,
                        Mensaje = "El ID de la orden de trabajo no es válido",
                        Datos = null
                    };
                }

                Console.WriteLine($"✅ CN_Entrega - ID válido, llamando a CD_Entrega.ObtenerDatosEntrega...");

                // Obtener datos directamente de CD_Entrega
                DatosEntregaDTO datosEntrega;
                try
                {
                    datosEntrega = _cdEntrega.ObtenerDatosEntrega(ordentrabajoID);
                    Console.WriteLine($"✅ CN_Entrega - CD_Entrega retornó datos exitosamente");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ CN_Entrega - Error al llamar CD_Entrega: {ex.Message}");
                    return new ResultadoOperacion<DatosEntregaDTO>
                    {
                        Exitoso = false,
                        Mensaje = $"Error al obtener datos de la base de datos: {ex.Message}",
                        Datos = null
                    };
                }

                // Validar que se obtuvieron datos
                if (datosEntrega == null)
                {
                    Console.WriteLine($"❌ CN_Entrega - CD_Entrega retornó null");
                    return new ResultadoOperacion<DatosEntregaDTO>
                    {
                        Exitoso = false,
                        Mensaje = $"No se pudieron obtener datos para la orden de trabajo #{ordentrabajoID}",
                        Datos = null
                    };
                }

                // ********************************************************************
                // ¡¡¡CORRECCIÓN CRÍTICA: CALCULAR EstaListaParaEntrega!!!
                // ********************************************************************
                Console.WriteLine($"🔧 CN_Entrega - Calculando EstaListaParaEntrega...");

                // Condiciones para que una OT esté lista para entrega:
                bool estaListaParaEntrega =
                    datosEntrega.EstadoOT?.ToUpper() == "COMPLETADA" &&  // Estado debe ser COMPLETADA
                    datosEntrega.Resultado?.ToUpper() == "APROBADO" &&    // Control de calidad APROBADO
                    !datosEntrega.EntregaExistente;                       // No debe tener entrega previa
                                                                          // Nota: La calificación es opcional, si necesitas > 0, agrega:
                                                                          // && (datosEntrega.Calificacion ?? 0) > 0;

                datosEntrega.EstaListaParaEntrega = estaListaParaEntrega;

                Console.WriteLine($"📊 CN_Entrega - Cálculo EstaListaParaEntrega:");
                Console.WriteLine($"   • EstadoOT == 'COMPLETADA': {datosEntrega.EstadoOT?.ToUpper() == "COMPLETADA"}");
                Console.WriteLine($"   • Resultado == 'APROBADO': {datosEntrega.Resultado?.ToUpper() == "APROBADO"}");
                Console.WriteLine($"   • !EntregaExistente: {!datosEntrega.EntregaExistente}");
                Console.WriteLine($"   • Calificacion >= 0: {((datosEntrega.Calificacion ?? 0) >= 0)}");
                Console.WriteLine($"   • EstaListaParaEntrega = {estaListaParaEntrega}");

                Console.WriteLine($"✅ CN_Entrega - Datos validados:");
                Console.WriteLine($"   • OT ID: {datosEntrega.OrdentrabajoID}");
                Console.WriteLine($"   • Número OT: {datosEntrega.NumeroOT}");
                Console.WriteLine($"   • Cliente: {datosEntrega.NombreCliente}");
                Console.WriteLine($"   • Vehículo ID: {datosEntrega.VehiculoID}");
                Console.WriteLine($"   • Estado: {datosEntrega.EstadoOT}");
                Console.WriteLine($"   • Resultado: {datosEntrega.Resultado}");
                Console.WriteLine($"   • Calificación: {datosEntrega.Calificacion}");
                Console.WriteLine($"   • EstaListaParaEntrega: {datosEntrega.EstaListaParaEntrega}");
                Console.WriteLine($"   • Entrega existente: {datosEntrega.EntregaExistente}");
                Console.WriteLine($"   • Cliente aprobó: {datosEntrega.ClienteAprobo}");
                Console.WriteLine($"   • Cliente rechazó: {datosEntrega.ClienteRechazo}");
                Console.WriteLine($"   • Pendiente aprobación: {datosEntrega.PendienteAprobacion}");
                Console.WriteLine($"   • DetallesControl count: {datosEntrega.DetallesControl?.Count ?? 0}");

                // Validar que la OT tenga vehículo y cliente
                if (datosEntrega.VehiculoID == 0)
                {
                    Console.WriteLine($"⚠️ CN_Entrega - Vehículo ID es 0");
                }

                if (datosEntrega.ClienteID == 0)
                {
                    Console.WriteLine($"⚠️ CN_Entrega - Cliente ID es 0");
                }

                Console.WriteLine($"✅✅✅ CN_Entrega - Retornando datos exitosamente para OT: {ordentrabajoID}");

                return new ResultadoOperacion<DatosEntregaDTO>
                {
                    Exitoso = true,
                    Mensaje = "Datos obtenidos exitosamente",
                    Datos = datosEntrega
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥🔥🔥 CN_Entrega - ERROR GLOBAL en ObtenerDatosEntrega: {ex.Message}");
                Console.WriteLine($"🔥🔥🔥 StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"🔥🔥🔥 Inner Exception: {ex.InnerException.Message}");
                }

                return new ResultadoOperacion<DatosEntregaDTO>
                {
                    Exitoso = false,
                    Mensaje = $"Error crítico al obtener datos: {ex.Message}",
                    Datos = null
                };
            }
        }

        /// <summary>
        /// Crea una nueva entrega para una OT
        /// </summary>
        public ResultadoOperacion<int> CrearEntrega(int ordentrabajoID, int usuarioID, string observaciones = null)
        {
            try
            {
                Console.WriteLine($"🔄 CN_Entrega - CrearEntrega para OT: {ordentrabajoID}");

                // Validar entradas
                if (ordentrabajoID <= 0)
                {
                    return new ResultadoOperacion<int>
                    {
                        Exitoso = false,
                        Mensaje = "El ID de la orden de trabajo no es válido",
                        Datos = 0
                    };
                }

                if (usuarioID <= 0)
                {
                    return new ResultadoOperacion<int>
                    {
                        Exitoso = false,
                        Mensaje = "El ID de usuario no es válido",
                        Datos = 0
                    };
                }

                // Primero obtener datos para verificar estado
                var resultadoDatos = ObtenerDatosEntrega(ordentrabajoID);
                if (!resultadoDatos.Exitoso || resultadoDatos.Datos == null)
                {
                    return new ResultadoOperacion<int>
                    {
                        Exitoso = false,
                        Mensaje = resultadoDatos.Mensaje,
                        Datos = 0
                    };
                }

                var datosEntrega = resultadoDatos.Datos;

                // Verificar si la OT está lista para entrega
                if (!datosEntrega.EstaListaParaEntrega)
                {
                    Console.WriteLine($"❌ CN_Entrega - OT no está lista para entrega. Razones:");
                    Console.WriteLine($"❌ • EstadoOT == 'COMPLETADA': {datosEntrega.EstadoOT?.ToUpper() == "COMPLETADA"}");
                    Console.WriteLine($"❌ • Resultado == 'APROBADO': {datosEntrega.Resultado?.ToUpper() == "APROBADO"}");
                    Console.WriteLine($"❌ • !EntregaExistente: {!datosEntrega.EntregaExistente}");

                    return new ResultadoOperacion<int>
                    {
                        Exitoso = false,
                        Mensaje = "La orden de trabajo no está lista para entrega. Verifique:\n" +
                                 "1. Estado debe ser 'COMPLETADA'\n" +
                                 "2. Control de calidad debe estar 'APROBADO'\n" +
                                 "3. No debe existir entrega previa",
                        Datos = 0
                    };
                }

                // Verificar si ya existe una entrega para esta OT
                if (datosEntrega.EntregaExistente)
                {
                    return new ResultadoOperacion<int>
                    {
                        Exitoso = false,
                        Mensaje = "Ya existe una entrega creada para esta orden de trabajo",
                        Datos = datosEntrega.EntregavehiculoID ?? 0
                    };
                }

                // Crear la entrega usando CD_Entrega
                var mensaje = _cdEntrega.CrearEntrega(ordentrabajoID, usuarioID, observaciones);

                if (mensaje.Contains("exitosamente") || mensaje.Contains("creada") || !mensaje.Contains("Error"))
                {
                    return new ResultadoOperacion<int>
                    {
                        Exitoso = true,
                        Mensaje = mensaje,
                        Datos = 1 // ID temporal, deberías obtener el real de CD_Entrega
                    };
                }
                else
                {
                    return new ResultadoOperacion<int>
                    {
                        Exitoso = false,
                        Mensaje = mensaje,
                        Datos = 0
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 CN_Entrega - Error en CrearEntrega: {ex.Message}");

                return new ResultadoOperacion<int>
                {
                    Exitoso = false,
                    Mensaje = $"Error al crear entrega: {ex.Message}",
                    Datos = 0
                };
            }
        }

        #endregion

        #region Métodos para aprobación/rechazo del cliente

        /// <summary>
        /// Registra la aprobación o rechazo del cliente
        /// </summary>
        public ResultadoOperacion<bool> RegistrarAprobacionCliente(SolicitudAprobacionDTO solicitud)
        {
            try
            {
                Console.WriteLine($"🔄 CN_Entrega - RegistrarAprobacionCliente para EntregaID: {solicitud.EntregavehiculoID}");

                // Validar entrada
                var errores = ValidarSolicitudAprobacion(solicitud);
                if (errores.Any())
                {
                    return new ResultadoOperacion<bool>
                    {
                        Exitoso = false,
                        Mensaje = string.Join("; ", errores),
                        Datos = false
                    };
                }

                // Registrar la aprobación/rechazo usando CD_Entrega
                var mensaje = _cdEntrega.RegistrarAprobacionCliente(solicitud);

                Console.WriteLine($"📝 CN_Entrega - Mensaje de CD_Entrega: {mensaje}");

                if (mensaje.Contains("exitosamente") || mensaje.Contains("OT marcada") || mensaje.Contains("OT reabierta") || mensaje.Contains("Procesado"))
                {
                    return new ResultadoOperacion<bool>
                    {
                        Exitoso = true,
                        Mensaje = mensaje,
                        Datos = solicitud.Aprobacion
                    };
                }
                else
                {
                    return new ResultadoOperacion<bool>
                    {
                        Exitoso = false,
                        Mensaje = mensaje,
                        Datos = false
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 CN_Entrega - Error en RegistrarAprobacionCliente: {ex.Message}");

                return new ResultadoOperacion<bool>
                {
                    Exitoso = false,
                    Mensaje = $"Error al registrar aprobación: {ex.Message}",
                    Datos = false
                };
            }
        }

        /// <summary>
        /// Valida los datos de la solicitud de aprobación
        /// </summary>
        private List<string> ValidarSolicitudAprobacion(SolicitudAprobacionDTO solicitud)
        {
            var errores = new List<string>();

            if (solicitud.EntregavehiculoID <= 0)
                errores.Add("El ID de entrega no es válido");

            if (solicitud.UsuarioID <= 0)
                errores.Add("El ID de usuario no es válido");

            // Si es rechazo, se requiere observaciones
            if (!solicitud.Aprobacion && string.IsNullOrWhiteSpace(solicitud.Observaciones))
                errores.Add("Se requiere especificar observaciones para el rechazo");

            // Si es aprobación y hay firma, validar formato base64
            if (solicitud.Aprobacion && !string.IsNullOrEmpty(solicitud.FirmaCliente))
            {
                try
                {
                    Convert.FromBase64String(solicitud.FirmaCliente);
                }
                catch
                {
                    errores.Add("La firma del cliente no tiene un formato base64 válido");
                }
            }

            return errores;
        }

        #endregion

        #region Métodos para gestión de documentos

        /// <summary>
        /// Agrega un documento a la entrega (solo si está aprobada)
        /// </summary>
        public ResultadoOperacion<int> AgregarDocumento(DocumentoEntregaRequestDTO documentoRequest)
        {
            try
            {
                Console.WriteLine($"🔄 CN_Entrega - AgregarDocumento para EntregaID: {documentoRequest.EntregavehiculoID}");

                // Validar entrada
                var errores = ValidarDocumentoRequest(documentoRequest);
                if (errores.Any())
                {
                    return new ResultadoOperacion<int>
                    {
                        Exitoso = false,
                        Mensaje = string.Join("; ", errores),
                        Datos = 0
                    };
                }

                // Guardar el documento usando CD_Entrega
                var mensaje = _cdEntrega.AgregarDocumento(documentoRequest);

                Console.WriteLine($"📝 CN_Entrega - Mensaje de CD_Entrega: {mensaje}");

                if (mensaje.Contains("exitosamente") || mensaje.Contains("agregado") || !mensaje.Contains("Error"))
                {
                    return new ResultadoOperacion<int>
                    {
                        Exitoso = true,
                        Mensaje = mensaje,
                        Datos = 1 // ID temporal
                    };
                }
                else
                {
                    return new ResultadoOperacion<int>
                    {
                        Exitoso = false,
                        Mensaje = mensaje,
                        Datos = 0
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 CN_Entrega - Error en AgregarDocumento: {ex.Message}");

                return new ResultadoOperacion<int>
                {
                    Exitoso = false,
                    Mensaje = $"Error al agregar documento: {ex.Message}",
                    Datos = 0
                };
            }
        }

        /// <summary>
        /// Valida los datos del documento
        /// </summary>
        private List<string> ValidarDocumentoRequest(DocumentoEntregaRequestDTO documento)
        {
            var errores = new List<string>();

            if (documento.EntregavehiculoID <= 0)
                errores.Add("El ID de entrega no es válido");

            if (string.IsNullOrWhiteSpace(documento.TipoDocumento))
                errores.Add("El tipo de documento es requerido");

            if (string.IsNullOrWhiteSpace(documento.NombreArchivo))
                errores.Add("El nombre del archivo es requerido");

            if (string.IsNullOrWhiteSpace(documento.ArchivoBase64))
                errores.Add("El contenido del archivo es requerido");

            if (documento.UsuarioID <= 0)
                errores.Add("El ID de usuario no es válido");

            // Validar formato base64
            if (!string.IsNullOrEmpty(documento.ArchivoBase64))
            {
                try
                {
                    Convert.FromBase64String(documento.ArchivoBase64);
                }
                catch
                {
                    errores.Add("El archivo no tiene un formato base64 válido");
                }
            }

            // Validar extensión del archivo
            if (!string.IsNullOrEmpty(documento.NombreArchivo))
            {
                var extensionesPermitidas = new[] { ".pdf", ".jpg", ".jpeg", ".png", ".doc", ".docx" };
                var extension = System.IO.Path.GetExtension(documento.NombreArchivo).ToLower();

                if (!extensionesPermitidas.Contains(extension))
                {
                    errores.Add($"Extensión no permitida: {extension}. Use: {string.Join(", ", extensionesPermitidas)}");
                }
            }

            return errores;
        }

        /// <summary>
        /// Obtiene la ruta física de un archivo
        /// </summary>
        public ResultadoOperacion<string> ObtenerRutaArchivo(string nombreArchivo)
        {
            try
            {
                Console.WriteLine($"🔄 CN_Entrega - ObtenerRutaArchivo: {nombreArchivo}");

                if (string.IsNullOrWhiteSpace(nombreArchivo))
                {
                    return new ResultadoOperacion<string>
                    {
                        Exitoso = false,
                        Mensaje = "El nombre del archivo es requerido",
                        Datos = null
                    };
                }

                var ruta = _cdEntrega.ObtenerRutaArchivo(nombreArchivo);

                if (string.IsNullOrEmpty(ruta))
                {
                    return new ResultadoOperacion<string>
                    {
                        Exitoso = false,
                        Mensaje = "El archivo no existe en el servidor",
                        Datos = null
                    };
                }

                return new ResultadoOperacion<string>
                {
                    Exitoso = true,
                    Mensaje = "Ruta obtenida exitosamente",
                    Datos = ruta
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 CN_Entrega - Error en ObtenerRutaArchivo: {ex.Message}");

                return new ResultadoOperacion<string>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener ruta: {ex.Message}",
                    Datos = null
                };
            }
        }

        #endregion

        #region Métodos para reapertura de OT

        /// <summary>
        /// Reabre una orden de trabajo después de rechazo en entrega
        /// </summary>
        public ResultadoOperacion<bool> ReabrirOrdenTrabajo(int ordentrabajoID, string motivo, int usuarioID)
        {
            try
            {
                Console.WriteLine($"🔄 CN_Entrega - ReabrirOrdenTrabajo para OT: {ordentrabajoID}");

                // Validar entrada
                if (ordentrabajoID <= 0)
                {
                    return new ResultadoOperacion<bool>
                    {
                        Exitoso = false,
                        Mensaje = "El ID de la orden de trabajo no es válido",
                        Datos = false
                    };
                }

                if (string.IsNullOrWhiteSpace(motivo))
                {
                    return new ResultadoOperacion<bool>
                    {
                        Exitoso = false,
                        Mensaje = "Debe especificar el motivo de la reapertura",
                        Datos = false
                    };
                }

                if (usuarioID <= 0)
                {
                    return new ResultadoOperacion<bool>
                    {
                        Exitoso = false,
                        Mensaje = "El ID de usuario no es válido",
                        Datos = false
                    };
                }

                // Usar el método de CD_Entrega
                var mensaje = _cdEntrega.ReabrirOrdenTrabajo(ordentrabajoID, motivo, usuarioID);

                Console.WriteLine($"📝 CN_Entrega - Mensaje de CD_Entrega: {mensaje}");

                if (mensaje.Contains("exitosamente") || mensaje.Contains("reabierta"))
                {
                    return new ResultadoOperacion<bool>
                    {
                        Exitoso = true,
                        Mensaje = mensaje,
                        Datos = true
                    };
                }
                else
                {
                    return new ResultadoOperacion<bool>
                    {
                        Exitoso = false,
                        Mensaje = mensaje,
                        Datos = false
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 CN_Entrega - Error en ReabrirOrdenTrabajo: {ex.Message}");

                return new ResultadoOperacion<bool>
                {
                    Exitoso = false,
                    Mensaje = $"Error al reabrir orden de trabajo: {ex.Message}",
                    Datos = false
                };
            }
        }

        #endregion

        #region Métodos de búsqueda y listado

        /// <summary>
        /// Busca órdenes de trabajo listas para entrega
        /// </summary>
        public ResultadoOperacion<List<CD_Entrega.OrdentrabajoResumenDTO>> BuscarOrdenesParaEntrega(string filtro = "")
        {
            try
            {
                Console.WriteLine($"🔄 CN_Entrega - BuscarOrdenesParaEntrega con filtro: '{filtro}'");

                var ordenes = _cdEntrega.BuscarOrdenesParaEntrega(filtro);

                Console.WriteLine($"✅ CN_Entrega - Encontradas {ordenes.Count} órdenes");

                return new ResultadoOperacion<List<CD_Entrega.OrdentrabajoResumenDTO>>
                {
                    Exitoso = true,
                    Mensaje = ordenes.Count > 0 ? "Órdenes encontradas" : "No se encontraron órdenes para entrega",
                    Datos = ordenes
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 CN_Entrega - Error en BuscarOrdenesParaEntrega: {ex.Message}");

                return new ResultadoOperacion<List<CD_Entrega.OrdentrabajoResumenDTO>>
                {
                    Exitoso = false,
                    Mensaje = $"Error al buscar órdenes: {ex.Message}",
                    Datos = new List<CD_Entrega.OrdentrabajoResumenDTO>()
                };
            }
        }

        #endregion

        #region Clases auxiliares

        /// <summary>
        /// Clase para resultados de operaciones
        /// </summary>
        public class ResultadoOperacion<T>
        {
            public bool Exitoso { get; set; }
            public string Mensaje { get; set; }
            public T Datos { get; set; }
        }

        #endregion
    }
}