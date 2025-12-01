using CapaDominio;
using CapaDatos;
using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Mannucci_Motors
{
    public partial class frmEntregas : Form
    {
        // Variables de instancia
        private CN_Entrega _logicaEntrega;
        private CD_Entrega.OrdentrabajoResumenDTO _ordenSeleccionada;
        private DatosEntregaDTO _datosEntregaActual;
        private int _usuarioID; // ID del usuario logueado
        private bool _dibujandoFirma = false;
        private List<Point> _puntosFirma = new List<Point>();
        private Bitmap _bitmapFirma;

        public frmEntregas(int usuarioID)
        {
            InitializeComponent();
            _usuarioID = usuarioID;
            _logicaEntrega = new CN_Entrega();
            InicializarFormulario();
        }

        // Constructor para MDI (sin parámetros)
        public frmEntregas() : this(1) // Por defecto usuario ID 1, ajustar según tu sistema
        {
        }

        private void InicializarFormulario()
        {
            // Configurar colores
            ConfigurarColores();

            // Inicializar bitmap para firma
            _bitmapFirma = new Bitmap(panelFirma.Width, panelFirma.Height);
            using (Graphics g = Graphics.FromImage(_bitmapFirma))
            {
                g.Clear(Color.White);
            }

            // Configurar DataGridViews
            ConfigurarDataGridViews();

            // Cargar datos iniciales
            CargarOrdenesParaEntrega();
        }

        private void ConfigurarColores()
        {
            // Paleta de colores: Rojo (#C00000), Blanco, Gris (#404040)
            Color rojoPrincipal = Color.FromArgb(192, 0, 0);
            Color grisSecundario = Color.FromArgb(64, 64, 64);
            Color blanco = Color.White;

            // Configurar colores de controles
            this.BackColor = blanco;
            panelHeader.BackColor = rojoPrincipal;
            panelContenedor.BackColor = blanco;
            tabPageBusqueda.BackColor = blanco;
            tabPageEntrega.BackColor = blanco;
            panelInfoOT.BackColor = blanco;
            panelAcciones.BackColor = blanco;

            // Configurar botones
            btnBuscar.BackColor = rojoPrincipal;
            btnBuscar.ForeColor = blanco;

            btnCrearEntrega.BackColor = rojoPrincipal;
            btnCrearEntrega.ForeColor = blanco;

            btnAprobar.BackColor = Color.FromArgb(0, 192, 0); // Verde para aprobar
            btnAprobar.ForeColor = blanco;

            btnRechazar.BackColor = rojoPrincipal;
            btnRechazar.ForeColor = blanco;

            btnReabrirOT.BackColor = grisSecundario;
            btnReabrirOT.ForeColor = blanco;

            btnAgregarDocumento.BackColor = rojoPrincipal;
            btnAgregarDocumento.ForeColor = blanco;

            btnEliminarDocumento.BackColor = rojoPrincipal;
            btnEliminarDocumento.ForeColor = blanco;

            btnVerDocumento.BackColor = grisSecundario;
            btnVerDocumento.ForeColor = blanco;

            btnLimpiarFirma.BackColor = grisSecundario;
            btnLimpiarFirma.ForeColor = blanco;

            // Configurar group boxes
            foreach (Control control in this.Controls)
            {
                if (control is GroupBox groupBox)
                {
                    groupBox.ForeColor = rojoPrincipal;
                }
            }
        }

        private void ConfigurarDataGridViews()
        {
            // Configurar dgvOrdenes
            dgvOrdenes.AutoGenerateColumns = false;
            dgvOrdenes.Columns.Clear();

            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "OrdentrabajoID",
                HeaderText = "N° OT",
                DataPropertyName = "NumeroOT",
                Width = 100
            });

            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Placa",
                HeaderText = "Placa",
                DataPropertyName = "Placa",
                Width = 100
            });

            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Cliente",
                HeaderText = "Cliente",
                DataPropertyName = "NombreCliente",
                Width = 200
            });

            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Vehiculo",
                HeaderText = "Vehículo",
                DataPropertyName = "VehiculoCompleto",
                Width = 200
            });

            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "EstadoEntrega",
                Width = 150
            });

            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "FechaCreacion",
                HeaderText = "Fecha Creación",
                DataPropertyName = "FechaCreacion",
                Width = 120
            });

            // Configurar dgvDetallesControl
            dgvDetallesControl.AutoGenerateColumns = false;
            dgvDetallesControl.Columns.Clear();

            dgvDetallesControl.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Actividad",
                HeaderText = "Actividad",
                DataPropertyName = "ActividadDescripcion",
                Width = 200
            });

            dgvDetallesControl.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                Width = 200
            });

            dgvDetallesControl.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 100
            });

            dgvDetallesControl.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Observaciones",
                HeaderText = "Observaciones",
                DataPropertyName = "Observaciones",
                Width = 200
            });
        }

        #region Métodos de Carga de Datos

        private void CargarOrdenesParaEntrega(string filtro = "")
        {
            try
            {
                Console.WriteLine($"🔍 frmEntregas - CargarOrdenesParaEntrega con filtro: '{filtro}'");
                toolStripStatusLabel.Text = "Buscando órdenes para entrega...";
                toolStripProgressBar.Style = ProgressBarStyle.Marquee;

                var resultado = _logicaEntrega.BuscarOrdenesParaEntrega(filtro);

                if (resultado.Exitoso)
                {
                    dgvOrdenes.DataSource = resultado.Datos;
                    toolStripStatusLabel.Text = $"Se encontraron {resultado.Datos.Count} órdenes para entrega";
                    Console.WriteLine($"✅ frmEntregas - Se encontraron {resultado.Datos.Count} órdenes");
                }
                else
                {
                    dgvOrdenes.DataSource = new List<CD_Entrega.OrdentrabajoResumenDTO>();
                    Console.WriteLine($"⚠️ frmEntregas - No se encontraron órdenes: {resultado.Mensaje}");
                    MostrarMensaje(resultado.Mensaje, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 frmEntregas - Error en CargarOrdenesParaEntrega: {ex.Message}");
                MostrarError($"Error al cargar órdenes: {ex.Message}");
            }
            finally
            {
                toolStripProgressBar.Style = ProgressBarStyle.Continuous;
                toolStripProgressBar.Value = 0;
            }
        }

        private void CargarDatosEntrega(int ordentrabajoID)
        {
            try
            {
                Console.WriteLine($"🔍 frmEntregas - Iniciando CargarDatosEntrega para OT: {ordentrabajoID}");
                toolStripStatusLabel.Text = "Cargando datos de la orden de trabajo...";
                toolStripProgressBar.Style = ProgressBarStyle.Marquee;

                var resultado = _logicaEntrega.ObtenerDatosEntrega(ordentrabajoID);

                Console.WriteLine($"📊 frmEntregas - Resultado.Exitoso: {resultado.Exitoso}");
                Console.WriteLine($"📊 frmEntregas - Resultado.Mensaje: {resultado.Mensaje}");
                Console.WriteLine($"📊 frmEntregas - Datos es null: {resultado.Datos == null}");

                if (resultado.Exitoso && resultado.Datos != null)
                {
                    Console.WriteLine($"📊 frmEntregas - Datos obtenidos exitosamente:");
                    Console.WriteLine($"📊 frmEntregas - • NumeroOT: {resultado.Datos.NumeroOT}");
                    Console.WriteLine($"📊 frmEntregas - • NombreCliente: {resultado.Datos.NombreCliente}");
                    Console.WriteLine($"📊 frmEntregas - • EntregaExistente: {resultado.Datos.EntregaExistente}");
                    Console.WriteLine($"📊 frmEntregas - • ClienteAprobo: {resultado.Datos.ClienteAprobo}");
                    Console.WriteLine($"📊 frmEntregas - • ClienteRechazo: {resultado.Datos.ClienteRechazo}");
                    Console.WriteLine($"📊 frmEntregas - • PendienteAprobacion: {resultado.Datos.PendienteAprobacion}");
                    Console.WriteLine($"📊 frmEntregas - • EstaListaParaEntrega: {resultado.Datos.EstaListaParaEntrega}");
                    Console.WriteLine($"📊 frmEntregas - • AprobacionCliente tiene valor: {resultado.Datos.AprobacionCliente.HasValue}");
                    if (resultado.Datos.AprobacionCliente.HasValue)
                    {
                        Console.WriteLine($"📊 frmEntregas - • AprobacionCliente.Value: {resultado.Datos.AprobacionCliente.Value}");
                    }

                    _datosEntregaActual = resultado.Datos;

                    Console.WriteLine($"🔧 frmEntregas - Llamando a ActualizarInterfazConDatos...");
                    ActualizarInterfazConDatos();

                    Console.WriteLine($"✅ frmEntregas - Datos de OT-{ordentrabajoID} cargados exitosamente");
                    toolStripStatusLabel.Text = $"Datos de OT-{ordentrabajoID} cargados exitosamente";
                }
                else
                {
                    Console.WriteLine($"❌ frmEntregas - Error: {resultado.Mensaje}");
                    MostrarMensaje(resultado.Mensaje, MessageBoxIcon.Error);
                    LimpiarFormularioEntrega();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥🔥🔥 frmEntregas - ERROR en CargarDatosEntrega: {ex.Message}");
                Console.WriteLine($"🔥🔥🔥 frmEntregas - StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"🔥🔥🔥 frmEntregas - Inner Exception: {ex.InnerException.Message}");
                }

                MostrarError($"Error al cargar datos: {ex.Message}");
            }
            finally
            {
                toolStripProgressBar.Style = ProgressBarStyle.Continuous;
                toolStripProgressBar.Value = 0;
            }
        }

        private void ActualizarInterfazConDatos()
        {
            try
            {
                Console.WriteLine($"🔧 frmEntregas - Iniciando ActualizarInterfazConDatos...");

                if (_datosEntregaActual == null)
                {
                    Console.WriteLine($"⚠️ frmEntregas - _datosEntregaActual es null");
                    return;
                }

                Console.WriteLine($"🔧 frmEntregas - Actualizando información básica...");

                // Información de la OT
                txtNumeroOT.Text = _datosEntregaActual.NumeroOT;
                txtEstadoOT.Text = _datosEntregaActual.EstadoOT;

                // CORRECCIÓN CRÍTICA: Manejar el valor "primary"
                try
                {
                    string colorEstado = _datosEntregaActual.ColorEstadoOT ?? "#CCCCCC";
                    Console.WriteLine($"🔧 frmEntregas - ColorEstadoOT: '{colorEstado}'");

                    // Convertir nombres de color Bootstrap a códigos hexadecimales
                    if (colorEstado.Equals("primary", StringComparison.OrdinalIgnoreCase))
                    {
                        txtEstadoOT.BackColor = Color.FromArgb(13, 110, 253); // Azul Bootstrap primary
                    }
                    else if (colorEstado.Equals("success", StringComparison.OrdinalIgnoreCase))
                    {
                        txtEstadoOT.BackColor = Color.FromArgb(25, 135, 84); // Verde Bootstrap success
                    }
                    else if (colorEstado.Equals("warning", StringComparison.OrdinalIgnoreCase))
                    {
                        txtEstadoOT.BackColor = Color.FromArgb(255, 193, 7); // Amarillo Bootstrap warning
                    }
                    else if (colorEstado.Equals("danger", StringComparison.OrdinalIgnoreCase))
                    {
                        txtEstadoOT.BackColor = Color.FromArgb(220, 53, 69); // Rojo Bootstrap danger
                    }
                    else if (colorEstado.Equals("info", StringComparison.OrdinalIgnoreCase))
                    {
                        txtEstadoOT.BackColor = Color.FromArgb(13, 202, 240); // Cian Bootstrap info
                    }
                    else if (colorEstado.Equals("secondary", StringComparison.OrdinalIgnoreCase))
                    {
                        txtEstadoOT.BackColor = Color.FromArgb(108, 117, 125); // Gris Bootstrap secondary
                    }
                    else
                    {
                        // Intentar convertir como color hexadecimal normal
                        txtEstadoOT.BackColor = ColorTranslator.FromHtml(colorEstado);
                    }
                    txtEstadoOT.ForeColor = Color.White;
                    Console.WriteLine($"✅ frmEntregas - Color aplicado exitosamente");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ frmEntregas - Error al aplicar color: {ex.Message}");
                    txtEstadoOT.BackColor = Color.FromArgb(13, 110, 253); // Color por defecto (azul primary)
                    txtEstadoOT.ForeColor = Color.White;
                }

                // Información del Cliente
                txtNombreCliente.Text = _datosEntregaActual.NombreCliente;
                txtDNI.Text = _datosEntregaActual.DNI;
                txtEmail.Text = _datosEntregaActual.Email;
                txtTelefono.Text = _datosEntregaActual.Telefono;

                // Información del Vehículo
                txtPlaca.Text = _datosEntregaActual.Placa;
                txtMarca.Text = _datosEntregaActual.Marca;
                txtModelo.Text = _datosEntregaActual.Modelo;
                txtColor.Text = _datosEntregaActual.Color;
                txtAnio.Text = _datosEntregaActual.Anio?.ToString();
                txtCombustible.Text = _datosEntregaActual.CombustibleVehiculo;
                txtTransmision.Text = _datosEntregaActual.Transmision;

                // Control de Calidad
                lblResultadoControl.Text = $"Resultado: {_datosEntregaActual.Resultado ?? "PENDIENTE"}";
                lblCalificacion.Text = $"Calificación: {_datosEntregaActual.Calificacion?.ToString() ?? "N/A"}";

                // Configurar color del resultado
                if (_datosEntregaActual.Resultado?.ToUpper() == "APROBADO")
                    lblResultadoControl.ForeColor = Color.Green;
                else if (_datosEntregaActual.Resultado?.ToUpper() == "RECHAZADO")
                    lblResultadoControl.ForeColor = Color.Red;
                else
                    lblResultadoControl.ForeColor = Color.Orange;

                // Cargar detalles del control
                if (_datosEntregaActual.DetallesControl != null)
                {
                    dgvDetallesControl.DataSource = _datosEntregaActual.DetallesControl;
                    Console.WriteLine($"✅ frmEntregas - Cargados {_datosEntregaActual.DetallesControl.Count} detalles de control");
                }
                else
                {
                    dgvDetallesControl.DataSource = null;
                    Console.WriteLine($"⚠️ frmEntregas - DetallesControl es null");
                }

                Console.WriteLine($"🔧 frmEntregas - Llamando a ActualizarEstadoEntrega...");
                ActualizarEstadoEntrega();

                Console.WriteLine($"🔧 frmEntregas - Llamando a CargarDocumentos...");
                CargarDocumentos();

                Console.WriteLine($"✅ frmEntregas - Interfaz actualizada exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥🔥🔥 frmEntregas - ERROR en ActualizarInterfazConDatos: {ex.Message}");
                Console.WriteLine($"🔥🔥🔥 frmEntregas - StackTrace: {ex.StackTrace}");
                throw new Exception($"Error al actualizar interfaz: {ex.Message}", ex);
            }
        }

        private void ActualizarEstadoEntrega()
        {
            try
            {
                Console.WriteLine($"🔧 frmEntregas - Iniciando ActualizarEstadoEntrega...");

                if (_datosEntregaActual == null)
                {
                    Console.WriteLine($"⚠️ frmEntregas - _datosEntregaActual es null");
                    return;
                }

                // LOG DETALLADO DE TODOS LOS DATOS
                Console.WriteLine($"📊 frmEntregas - Datos completos:");
                Console.WriteLine($"📊 frmEntregas - • OrdentrabajoID: {_datosEntregaActual.OrdentrabajoID}");
                Console.WriteLine($"📊 frmEntregas - • NumeroOT: {_datosEntregaActual.NumeroOT}");
                Console.WriteLine($"📊 frmEntregas - • EstadoOT: {_datosEntregaActual.EstadoOT}");
                Console.WriteLine($"📊 frmEntregas - • EntregaExistente: {_datosEntregaActual.EntregaExistente}");
                Console.WriteLine($"📊 frmEntregas - • ClienteAprobo: {_datosEntregaActual.ClienteAprobo}");
                Console.WriteLine($"📊 frmEntregas - • ClienteRechazo: {_datosEntregaActual.ClienteRechazo}");
                Console.WriteLine($"📊 frmEntregas - • PendienteAprobacion: {_datosEntregaActual.PendienteAprobacion}");
                Console.WriteLine($"📊 frmEntregas - • EstaListaParaEntrega: {_datosEntregaActual.EstaListaParaEntrega}");
                Console.WriteLine($"📊 frmEntregas - • Resultado: {_datosEntregaActual.Resultado}");
                Console.WriteLine($"📊 frmEntregas - • Calificacion: {_datosEntregaActual.Calificacion}");
                Console.WriteLine($"📊 frmEntregas - • DetallesControl count: {_datosEntregaActual.DetallesControl?.Count ?? 0}");

                // 1. ¿La OT está lista para crear entrega?
                // CORRECCIÓN: Verificar condiciones específicas para habilitar "Crear Entrega"
                bool puedeCrearEntrega = _datosEntregaActual.EstaListaParaEntrega &&
                                        !_datosEntregaActual.EntregaExistente &&
                                        _datosEntregaActual.Resultado?.ToUpper() == "APROBADO";

                // Si la calificación es 0, aún debería permitir crear entrega (depende de tu lógica de negocio)
                // Si necesitas calificación > 0, descomenta la siguiente línea:
                // && (_datosEntregaActual.Calificacion ?? 0) > 0;

                btnCrearEntrega.Enabled = puedeCrearEntrega;
                btnCrearEntrega.BackColor = puedeCrearEntrega ?
                    Color.FromArgb(192, 0, 0) : Color.Gray;

                Console.WriteLine($"🔧 frmEntregas - puedeCrearEntrega: {puedeCrearEntrega}");
                Console.WriteLine($"🔧 frmEntregas - btnCrearEntrega.Enabled: {btnCrearEntrega.Enabled}");

                // 2. ¿Hay entrega y está pendiente de aprobación?
                // CORRECCIÓN: Usar lógica corregida para pendiente de aprobación
                bool pendienteAprobacion = _datosEntregaActual.EntregaExistente &&
                                          !_datosEntregaActual.ClienteAprobo &&
                                          !_datosEntregaActual.ClienteRechazo;

                groupBoxAprobacion.Enabled = pendienteAprobacion;
                btnAprobar.Enabled = pendienteAprobacion;
                btnRechazar.Enabled = pendienteAprobacion;
                txtObservacionesCliente.ReadOnly = !pendienteAprobacion;

                Console.WriteLine($"🔧 frmEntregas - pendienteAprobacion: {pendienteAprobacion}");
                Console.WriteLine($"🔧 frmEntregas - groupBoxAprobacion.Enabled: {groupBoxAprobacion.Enabled}");

                // 3. ¿Hay entrega y fue aprobada?
                bool entregaAprobada = _datosEntregaActual.EntregaExistente &&
                                      _datosEntregaActual.ClienteAprobo;

                groupBoxDocumentos.Enabled = entregaAprobada;
                btnAgregarDocumento.Enabled = entregaAprobada;

                Console.WriteLine($"🔧 frmEntregas - entregaAprobada: {entregaAprobada}");
                Console.WriteLine($"🔧 frmEntregas - groupBoxDocumentos.Enabled: {groupBoxDocumentos.Enabled}");

                // 4. ¿Hay entrega y fue rechazada?
                bool entregaRechazada = _datosEntregaActual.EntregaExistente &&
                                       _datosEntregaActual.ClienteRechazo;

                btnReabrirOT.Enabled = entregaRechazada;
                btnReabrirOT.BackColor = entregaRechazada ?
                    Color.FromArgb(64, 64, 64) : Color.Gray;

                Console.WriteLine($"🔧 frmEntregas - entregaRechazada: {entregaRechazada}");
                Console.WriteLine($"🔧 frmEntregas - btnReabrirOT.Enabled: {btnReabrirOT.Enabled}");

                // Mostrar estado de aprobación y firma
                if (_datosEntregaActual.EntregaExistente)
                {
                    Console.WriteLine($"🔧 frmEntregas - Entrega existente, verificando estado...");

                    if (_datosEntregaActual.ClienteAprobo)
                    {
                        Console.WriteLine($"🔧 frmEntregas - Cliente aprobó la entrega");
                        txtObservacionesCliente.Text = _datosEntregaActual.ObservacionesCliente ?? "Entregado sin observaciones";
                        txtObservacionesCliente.ReadOnly = true;
                        MostrarFirmaGuardada();
                    }
                    else if (_datosEntregaActual.ClienteRechazo)
                    {
                        Console.WriteLine($"🔧 frmEntregas - Cliente rechazó la entrega");
                        txtObservacionesCliente.Text = _datosEntregaActual.ObservacionesCliente ?? "Rechazado sin observaciones";
                        txtObservacionesCliente.ReadOnly = true;
                        LimpiarFirma();
                    }
                    else if (pendienteAprobacion)
                    {
                        Console.WriteLine($"🔧 frmEntregas - Pendiente aprobación del cliente");
                        txtObservacionesCliente.Text = _datosEntregaActual.ObservacionesCliente ?? "";
                        txtObservacionesCliente.ReadOnly = false;
                        LimpiarFirma();
                    }
                    else
                    {
                        Console.WriteLine($"⚠️ frmEntregas - Estado de aprobación desconocido");
                        txtObservacionesCliente.Clear();
                        txtObservacionesCliente.ReadOnly = true;
                        LimpiarFirma();
                    }
                }
                else
                {
                    Console.WriteLine($"🔧 frmEntregas - No existe entrega aún");
                    txtObservacionesCliente.Clear();
                    txtObservacionesCliente.ReadOnly = false;
                    LimpiarFirma();
                }

                // Método de depuración (opcional)
                VerificarHabilitacion();

                Console.WriteLine($"✅ frmEntregas - Estado de entrega actualizado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥🔥🔥 frmEntregas - ERROR en ActualizarEstadoEntrega: {ex.Message}");
                Console.WriteLine($"🔥🔥🔥 frmEntregas - StackTrace: {ex.StackTrace}");
                throw new Exception($"Error al actualizar estado de entrega: {ex.Message}", ex);
            }
        }

        // MÉTODO DE DEPURACIÓN - OPCIONAL
        private void VerificarHabilitacion()
        {
            if (_datosEntregaActual == null) return;

            string debugInfo = $"DEBUG - Estado de habilitación para OT-{_datosEntregaActual.NumeroOT}:\n\n";

            // Condiciones para btnCrearEntrega
            debugInfo += $"CREAR ENTREGA:\n";
            debugInfo += $"• EstaListaParaEntrega: {_datosEntregaActual.EstaListaParaEntrega}\n";
            debugInfo += $"• !EntregaExistente: {!_datosEntregaActual.EntregaExistente}\n";
            debugInfo += $"• Resultado == APROBADO: {(_datosEntregaActual.Resultado?.ToUpper() == "APROBADO")}\n";
            debugInfo += $"• Calificación > 0: {((_datosEntregaActual.Calificacion ?? 0) > 0)}\n";
            debugInfo += $"• HABILITADO: {btnCrearEntrega.Enabled}\n\n";

            // Condiciones para Aprobación
            debugInfo += $"APROBACIÓN/RECHAZO:\n";
            debugInfo += $"• EntregaExistente: {_datosEntregaActual.EntregaExistente}\n";
            debugInfo += $"• !ClienteAprobo: {!_datosEntregaActual.ClienteAprobo}\n";
            debugInfo += $"• !ClienteRechazo: {!_datosEntregaActual.ClienteRechazo}\n";
            debugInfo += $"• HABILITADO: {groupBoxAprobacion.Enabled}\n\n";

            // Estado actual
            debugInfo += $"ESTADO ACTUAL:\n";
            debugInfo += $"• Aprobado: {_datosEntregaActual.ClienteAprobo}\n";
            debugInfo += $"• Rechazado: {_datosEntregaActual.ClienteRechazo}\n";
            debugInfo += $"• Pendiente: {(_datosEntregaActual.EntregaExistente && !_datosEntregaActual.ClienteAprobo && !_datosEntregaActual.ClienteRechazo)}";

            Console.WriteLine($"🔍 DEBUG - {debugInfo.Replace("\n", " | ")}");

            // Opcional: Mostrar en ventana emergente para depuración
            // Descomenta la siguiente línea si quieres verlo en un MessageBox
            // MessageBox.Show(debugInfo, "Debug - Estado Habilitación", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CargarDocumentos()
        {
            try
            {
                Console.WriteLine($"🔧 frmEntregas - Iniciando CargarDocumentos...");
                lstDocumentos.Items.Clear();

                if (_datosEntregaActual?.Documentos != null)
                {
                    Console.WriteLine($"🔧 frmEntregas - Hay {_datosEntregaActual.Documentos.Count} documentos");
                    foreach (var documento in _datosEntregaActual.Documentos)
                    {
                        lstDocumentos.Items.Add($"{documento.TipoDocumento} - {documento.Descripcion}");
                    }
                }
                else
                {
                    Console.WriteLine($"⚠️ frmEntregas - Documentos es null o vacío");
                }

                lstDocumentos.Enabled = lstDocumentos.Items.Count > 0;
                btnVerDocumento.Enabled = lstDocumentos.SelectedIndex >= 0;
                btnEliminarDocumento.Enabled = lstDocumentos.SelectedIndex >= 0;

                Console.WriteLine($"✅ frmEntregas - Documentos cargados: {lstDocumentos.Items.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 frmEntregas - Error en CargarDocumentos: {ex.Message}");
                lstDocumentos.Items.Clear();
            }
        }

        private void LimpiarFormularioEntrega()
        {
            try
            {
                Console.WriteLine($"🔧 frmEntregas - Limpiando formulario...");

                // Limpiar todos los campos
                txtNumeroOT.Clear();
                txtEstadoOT.Clear();
                txtEstadoOT.BackColor = Color.White;
                txtEstadoOT.ForeColor = Color.Black;

                txtNombreCliente.Clear();
                txtDNI.Clear();
                txtEmail.Clear();
                txtTelefono.Clear();

                txtPlaca.Clear();
                txtMarca.Clear();
                txtModelo.Clear();
                txtColor.Clear();
                txtAnio.Clear();
                txtCombustible.Clear();
                txtTransmision.Clear();

                lblResultadoControl.Text = "Resultado: -";
                lblCalificacion.Text = "Calificación: -";
                lblResultadoControl.ForeColor = Color.Black;

                dgvDetallesControl.DataSource = null;

                txtObservacionesCliente.Clear();
                txtObservacionesCliente.ReadOnly = false;

                LimpiarFirma();
                lstDocumentos.Items.Clear();

                // Deshabilitar controles
                btnCrearEntrega.Enabled = false;
                btnCrearEntrega.BackColor = Color.Gray;

                groupBoxAprobacion.Enabled = false;
                btnAprobar.Enabled = false;
                btnRechazar.Enabled = false;

                groupBoxDocumentos.Enabled = false;
                btnAgregarDocumento.Enabled = false;

                btnReabrirOT.Enabled = false;
                btnReabrirOT.BackColor = Color.Gray;

                Console.WriteLine($"✅ frmEntregas - Formulario limpiado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 frmEntregas - Error en LimpiarFormularioEntrega: {ex.Message}");
            }
        }

        #endregion

        #region Eventos del Formulario

        private void frmEntregas_Load(object sender, EventArgs e)
        {
            // Centrar formulario si no está en MDI
            if (this.MdiParent == null)
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarOrdenesParaEntrega(txtBusqueda.Text);
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar_Click(sender, e);
                e.Handled = true;
            }
        }

        private void dgvOrdenes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvOrdenes.RowCount)
            {
                var orden = dgvOrdenes.Rows[e.RowIndex].DataBoundItem as CD_Entrega.OrdentrabajoResumenDTO;
                if (orden != null)
                {
                    _ordenSeleccionada = orden;
                    tabControlPrincipal.SelectedTab = tabPageEntrega;
                    Console.WriteLine($"🖱️ frmEntregas - Doble clic en OT: {orden.NumeroOT}");
                    CargarDatosEntrega(orden.OrdentrabajoID);
                }
            }
        }

        private void btnCrearEntrega_Click(object sender, EventArgs e)
        {
            if (_datosEntregaActual == null)
            {
                MostrarMensaje("No hay datos de orden de trabajo cargados", MessageBoxIcon.Warning);
                return;
            }

            if (!_datosEntregaActual.EstaListaParaEntrega)
            {
                MostrarMensaje("Esta orden de trabajo no está lista para entrega", MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(
                $"¿Está seguro de crear una entrega para {_datosEntregaActual.NumeroOT}?\n\n" +
                "Vehículo: " + _datosEntregaActual.Placa + "\n" +
                "Cliente: " + _datosEntregaActual.NombreCliente,
                "Confirmar Creación de Entrega",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Console.WriteLine($"🔄 frmEntregas - Creando entrega para OT: {_datosEntregaActual.OrdentrabajoID}");
                    toolStripStatusLabel.Text = "Creando entrega...";
                    toolStripProgressBar.Style = ProgressBarStyle.Marquee;

                    var resultado = _logicaEntrega.CrearEntrega(
                        _datosEntregaActual.OrdentrabajoID,
                        _usuarioID,
                        "Entrega creada desde sistema");

                    if (resultado.Exitoso)
                    {
                        Console.WriteLine($"✅ frmEntregas - Entrega creada: {resultado.Mensaje}");
                        MostrarMensaje(resultado.Mensaje, MessageBoxIcon.Information);
                        // Recargar datos
                        CargarDatosEntrega(_datosEntregaActual.OrdentrabajoID);
                    }
                    else
                    {
                        Console.WriteLine($"❌ frmEntregas - Error al crear entrega: {resultado.Mensaje}");
                        MostrarMensaje(resultado.Mensaje, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"🔥 frmEntregas - Error al crear entrega: {ex.Message}");
                    MostrarError($"Error al crear entrega: {ex.Message}");
                }
                finally
                {
                    toolStripProgressBar.Style = ProgressBarStyle.Continuous;
                    toolStripProgressBar.Value = 0;
                }
            }
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            if (_datosEntregaActual == null || !_datosEntregaActual.EntregaExistente)
            {
                MostrarMensaje("No hay una entrega creada para esta OT", MessageBoxIcon.Warning);
                return;
            }

            if (_datosEntregaActual.ClienteAprobo || _datosEntregaActual.ClienteRechazo)
            {
                MostrarMensaje("Esta entrega ya fue procesada por el cliente", MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(
                $"¿Confirmar APROBACIÓN de la entrega?\n\n" +
                $"OT: {_datosEntregaActual.NumeroOT}\n" +
                $"Vehículo: {_datosEntregaActual.Placa}\n" +
                $"Cliente: {_datosEntregaActual.NombreCliente}",
                "Confirmar Aprobación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Console.WriteLine($"🔄 frmEntregas - Procesando aprobación para EntregaID: {_datosEntregaActual.EntregavehiculoID}");

                    // Convertir firma a base64
                    string firmaBase64 = ObtenerFirmaBase64();

                    var solicitud = new SolicitudAprobacionDTO
                    {
                        EntregavehiculoID = _datosEntregaActual.EntregavehiculoID.Value,
                        Aprobacion = true,
                        Observaciones = txtObservacionesCliente.Text,
                        FirmaCliente = firmaBase64,
                        UsuarioID = _usuarioID
                    };

                    toolStripStatusLabel.Text = "Procesando aprobación...";
                    toolStripProgressBar.Style = ProgressBarStyle.Marquee;

                    var resultado = _logicaEntrega.RegistrarAprobacionCliente(solicitud);

                    if (resultado.Exitoso)
                    {
                        Console.WriteLine($"✅ frmEntregas - Aprobación registrada: {resultado.Mensaje}");
                        MostrarMensaje(resultado.Mensaje, MessageBoxIcon.Information);
                        // Recargar datos
                        CargarDatosEntrega(_datosEntregaActual.OrdentrabajoID);
                    }
                    else
                    {
                        Console.WriteLine($"❌ frmEntregas - Error en aprobación: {resultado.Mensaje}");
                        MostrarMensaje(resultado.Mensaje, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"🔥 frmEntregas - Error al procesar aprobación: {ex.Message}");
                    MostrarError($"Error al procesar aprobación: {ex.Message}");
                }
                finally
                {
                    toolStripProgressBar.Style = ProgressBarStyle.Continuous;
                    toolStripProgressBar.Value = 0;
                }
            }
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            if (_datosEntregaActual == null || !_datosEntregaActual.EntregaExistente)
            {
                MostrarMensaje("No hay una entrega creada para esta OT", MessageBoxIcon.Warning);
                return;
            }

            if (_datosEntregaActual.ClienteAprobo || _datosEntregaActual.ClienteRechazo)
            {
                MostrarMensaje("Esta entrega ya fue procesada por el cliente", MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtObservacionesCliente.Text))
            {
                MostrarMensaje("Debe especificar las observaciones para el rechazo", MessageBoxIcon.Warning);
                txtObservacionesCliente.Focus();
                return;
            }

            if (MessageBox.Show(
                $"¿Confirmar RECHAZO de la entrega?\n\n" +
                $"OT: {_datosEntregaActual.NumeroOT}\n" +
                $"Observaciones: {txtObservacionesCliente.Text}\n\n" +
                "Esta acción reabrirá la orden de trabajo para ajustes.",
                "Confirmar Rechazo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    Console.WriteLine($"🔄 frmEntregas - Procesando rechazo para EntregaID: {_datosEntregaActual.EntregavehiculoID}");

                    var solicitud = new SolicitudAprobacionDTO
                    {
                        EntregavehiculoID = _datosEntregaActual.EntregavehiculoID.Value,
                        Aprobacion = false,
                        Observaciones = txtObservacionesCliente.Text,
                        FirmaCliente = null,
                        UsuarioID = _usuarioID
                    };

                    toolStripStatusLabel.Text = "Procesando rechazo...";
                    toolStripProgressBar.Style = ProgressBarStyle.Marquee;

                    var resultado = _logicaEntrega.RegistrarAprobacionCliente(solicitud);

                    if (resultado.Exitoso)
                    {
                        Console.WriteLine($"✅ frmEntregas - Rechazo registrado: {resultado.Mensaje}");
                        MostrarMensaje(resultado.Mensaje, MessageBoxIcon.Information);
                        // Recargar datos
                        CargarDatosEntrega(_datosEntregaActual.OrdentrabajoID);
                    }
                    else
                    {
                        Console.WriteLine($"❌ frmEntregas - Error en rechazo: {resultado.Mensaje}");
                        MostrarMensaje(resultado.Mensaje, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"🔥 frmEntregas - Error al procesar rechazo: {ex.Message}");
                    MostrarError($"Error al procesar rechazo: {ex.Message}");
                }
                finally
                {
                    toolStripProgressBar.Style = ProgressBarStyle.Continuous;
                    toolStripProgressBar.Value = 0;
                }
            }
        }

        private void btnReabrirOT_Click(object sender, EventArgs e)
        {
            if (_datosEntregaActual == null)
            {
                MostrarMensaje("No hay datos de orden de trabajo cargados", MessageBoxIcon.Warning);
                return;
            }

            if (!_datosEntregaActual.EntregaExistente || !_datosEntregaActual.ClienteRechazo)
            {
                MostrarMensaje("Solo se puede reabrir una OT que fue rechazada en entrega", MessageBoxIcon.Warning);
                return;
            }

            using (var formMotivo = new Form())
            {
                formMotivo.Text = "Motivo de Reapertura";
                formMotivo.FormBorderStyle = FormBorderStyle.FixedDialog;
                formMotivo.StartPosition = FormStartPosition.CenterParent;
                formMotivo.Width = 400;
                formMotivo.Height = 200;

                var lblMotivo = new Label { Text = "Motivo de reapertura:", Left = 10, Top = 20, Width = 360 };
                var txtMotivo = new TextBox { Left = 10, Top = 50, Width = 360, Multiline = true, Height = 60 };
                var btnAceptar = new Button { Text = "Aceptar", Left = 220, Top = 120, Width = 70 };
                var btnCancelar = new Button { Text = "Cancelar", Left = 300, Top = 120, Width = 70 };

                btnAceptar.Click += (s, ev) => formMotivo.DialogResult = DialogResult.OK;
                btnCancelar.Click += (s, ev) => formMotivo.DialogResult = DialogResult.Cancel;

                formMotivo.Controls.AddRange(new Control[] { lblMotivo, txtMotivo, btnAceptar, btnCancelar });

                if (formMotivo.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrWhiteSpace(txtMotivo.Text))
                    {
                        MostrarMensaje("Debe especificar el motivo de reapertura", MessageBoxIcon.Warning);
                        return;
                    }

                    try
                    {
                        Console.WriteLine($"🔄 frmEntregas - Reabriendo OT: {_datosEntregaActual.OrdentrabajoID}");
                        toolStripStatusLabel.Text = "Reabriendo orden de trabajo...";
                        toolStripProgressBar.Style = ProgressBarStyle.Marquee;

                        var resultado = _logicaEntrega.ReabrirOrdenTrabajo(
                            _datosEntregaActual.OrdentrabajoID,
                            txtMotivo.Text,
                            _usuarioID);

                        if (resultado.Exitoso)
                        {
                            Console.WriteLine($"✅ frmEntregas - OT reabierta: {resultado.Mensaje}");
                            MostrarMensaje(resultado.Mensaje, MessageBoxIcon.Information);
                            // Regresar a la pestaña de búsqueda
                            tabControlPrincipal.SelectedTab = tabPageBusqueda;
                            CargarOrdenesParaEntrega();
                        }
                        else
                        {
                            Console.WriteLine($"❌ frmEntregas - Error al reabrir OT: {resultado.Mensaje}");
                            MostrarMensaje(resultado.Mensaje, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"🔥 frmEntregas - Error al reabrir orden de trabajo: {ex.Message}");
                        MostrarError($"Error al reabrir orden de trabajo: {ex.Message}");
                    }
                    finally
                    {
                        toolStripProgressBar.Style = ProgressBarStyle.Continuous;
                        toolStripProgressBar.Value = 0;
                    }
                }
            }
        }

        private void btnAgregarDocumento_Click(object sender, EventArgs e)
        {
            if (_datosEntregaActual == null || !_datosEntregaActual.EntregaExistente || !_datosEntregaActual.ClienteAprobo)
            {
                MostrarMensaje("Solo se pueden agregar documentos a entregas aprobadas", MessageBoxIcon.Warning);
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Documentos|*.pdf;*.doc;*.docx|Imágenes|*.jpg;*.jpeg;*.png|Todos los archivos|*.*";
                openFileDialog.Title = "Seleccionar documento para entrega";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Console.WriteLine($"🔄 frmEntregas - Agregando documento para EntregaID: {_datosEntregaActual.EntregavehiculoID}");

                        // Leer archivo como base64
                        byte[] fileBytes = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                        string base64String = Convert.ToBase64String(fileBytes);

                        // Solicitar tipo de documento
                        string tipoDocumento = Microsoft.VisualBasic.Interaction.InputBox(
                            "Ingrese el tipo de documento:",
                            "Tipo de Documento",
                            "Comprobante de Entrega");

                        if (string.IsNullOrWhiteSpace(tipoDocumento))
                        {
                            MostrarMensaje("Debe especificar el tipo de documento", MessageBoxIcon.Warning);
                            return;
                        }

                        var documentoRequest = new DocumentoEntregaRequestDTO
                        {
                            EntregavehiculoID = _datosEntregaActual.EntregavehiculoID.Value,
                            TipoDocumento = tipoDocumento,
                            Descripcion = $"Documento: {System.IO.Path.GetFileName(openFileDialog.FileName)}",
                            ArchivoBase64 = base64String,
                            NombreArchivo = System.IO.Path.GetFileName(openFileDialog.FileName),
                            UsuarioID = _usuarioID
                        };

                        toolStripStatusLabel.Text = "Agregando documento...";
                        toolStripProgressBar.Style = ProgressBarStyle.Marquee;

                        var resultado = _logicaEntrega.AgregarDocumento(documentoRequest);

                        if (resultado.Exitoso)
                        {
                            Console.WriteLine($"✅ frmEntregas - Documento agregado: {resultado.Mensaje}");
                            MostrarMensaje(resultado.Mensaje, MessageBoxIcon.Information);
                            // Recargar documentos
                            CargarDocumentos();
                        }
                        else
                        {
                            Console.WriteLine($"❌ frmEntregas - Error al agregar documento: {resultado.Mensaje}");
                            MostrarMensaje(resultado.Mensaje, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"🔥 frmEntregas - Error al agregar documento: {ex.Message}");
                        MostrarError($"Error al agregar documento: {ex.Message}");
                    }
                    finally
                    {
                        toolStripProgressBar.Style = ProgressBarStyle.Continuous;
                        toolStripProgressBar.Value = 0;
                    }
                }
            }
        }

        private void btnVerDocumento_Click(object sender, EventArgs e)
        {
            if (lstDocumentos.SelectedIndex < 0)
            {
                MostrarMensaje("Seleccione un documento de la lista", MessageBoxIcon.Warning);
                return;
            }

            // Aquí implementarías la lógica para ver el documento
            // Depende de cómo almacenes los documentos en tu sistema
            MostrarMensaje("Funcionalidad de visualización de documentos en desarrollo", MessageBoxIcon.Information);
        }

        private void btnEliminarDocumento_Click(object sender, EventArgs e)
        {
            if (lstDocumentos.SelectedIndex < 0)
            {
                MostrarMensaje("Seleccione un documento de la lista", MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(
                "¿Está seguro de eliminar este documento?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Aquí implementarías la lógica para eliminar el documento
                MostrarMensaje("Funcionalidad de eliminación de documentos en desarrollo", MessageBoxIcon.Information);
            }
        }

        private void lstDocumentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnVerDocumento.Enabled = lstDocumentos.SelectedIndex >= 0;
            btnEliminarDocumento.Enabled = lstDocumentos.SelectedIndex >= 0;
        }

        #endregion

        #region Funcionalidad de Firma

        private void panelFirma_MouseDown(object sender, MouseEventArgs e)
        {
            if (!groupBoxAprobacion.Enabled) return;

            _dibujandoFirma = true;
            _puntosFirma.Clear();
            _puntosFirma.Add(e.Location);
        }

        private void panelFirma_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dibujandoFirma || !groupBoxAprobacion.Enabled) return;

            _puntosFirma.Add(e.Location);
            DibujarFirma();
        }

        private void panelFirma_MouseUp(object sender, MouseEventArgs e)
        {
            _dibujandoFirma = false;
        }

        private void panelFirma_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_bitmapFirma, Point.Empty);
        }

        private void DibujarFirma()
        {
            if (_puntosFirma.Count < 2) return;

            using (Graphics g = Graphics.FromImage(_bitmapFirma))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(Color.Black, 2))
                {
                    for (int i = 1; i < _puntosFirma.Count; i++)
                    {
                        g.DrawLine(pen, _puntosFirma[i - 1], _puntosFirma[i]);
                    }
                }
            }

            panelFirma.Invalidate();
        }

        private string ObtenerFirmaBase64()
        {
            if (_puntosFirma.Count == 0) return null;

            using (MemoryStream ms = new MemoryStream())
            {
                _bitmapFirma.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        private void MostrarFirmaGuardada()
        {
            if (_datosEntregaActual?.FirmaCliente != null)
            {
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(_datosEntregaActual.FirmaCliente);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        _bitmapFirma = new Bitmap(Bitmap.FromStream(ms));
                    }
                    panelFirma.Invalidate();
                    Console.WriteLine($"✅ frmEntregas - Firma cargada exitosamente");
                }
                catch
                {
                    Console.WriteLine($"⚠️ frmEntregas - Error al cargar la firma, mostrando panel en blanco");
                    // Si hay error al cargar la firma, mostrar panel en blanco
                    LimpiarFirma();
                }
            }
            else
            {
                Console.WriteLine($"⚠️ frmEntregas - No hay firma guardada");
                LimpiarFirma();
            }
        }

        private void btnLimpiarFirma_Click(object sender, EventArgs e)
        {
            LimpiarFirma();
        }

        private void LimpiarFirma()
        {
            _puntosFirma.Clear();
            using (Graphics g = Graphics.FromImage(_bitmapFirma))
            {
                g.Clear(Color.White);
            }
            panelFirma.Invalidate();
            Console.WriteLine($"✅ frmEntregas - Firma limpiada");
        }

        #endregion

        #region Métodos Auxiliares

        private void MostrarMensaje(string mensaje, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, "Entrega de Vehículos",
                MessageBoxButtons.OK, icono);
        }

        private void MostrarError(string mensaje)
        {
            toolStripStatusLabel.Text = $"Error: {mensaje}";
            MessageBox.Show(mensaje, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        // Declaración de controles (generados automáticamente por el diseñador)
        private Panel panelHeader;
        private Label lblTitulo;
        private Button btnCerrar;
        private Panel panelContenedor;
        private TabControl tabControlPrincipal;
        private TabPage tabPageBusqueda;
        private DataGridView dgvOrdenes;
        private TabPage tabPageEntrega;
        private SplitContainer splitContainerPrincipal;
        private Panel panelInfoOT;
        private GroupBox groupBoxInfoOT;
        private TextBox txtEstadoOT;
        private TextBox txtNumeroOT;
        private Label lblEstado;
        private Label lblNumeroOT;
        private GroupBox groupBoxInfoCliente;
        private TextBox txtTelefono;
        private TextBox txtEmail;
        private TextBox txtDNI;
        private TextBox txtNombreCliente;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label lblNombreCliente;
        private GroupBox groupBoxInfoVehiculo;
        private TextBox txtTransmision;
        private TextBox txtCombustible;
        private TextBox txtAnio;
        private TextBox txtColor;
        private TextBox txtModelo;
        private TextBox txtMarca;
        private TextBox txtPlaca;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private GroupBox groupBoxControlCalidad;
        private Label lblCalificacion;
        private Label lblResultadoControl;
        private DataGridView dgvDetallesControl;
        private Panel panelAcciones;
        private Button btnCrearEntrega;
        private Button btnReabrirOT;
        private GroupBox groupBoxAprobacion;
        private Button btnRechazar;
        private Button btnAprobar;
        private Button btnLimpiarFirma;
        private Label lblFirmaCliente;
        private Panel panelFirma;
        private TextBox txtObservacionesCliente;
        private GroupBox groupBoxDocumentos;
        private ListBox lstDocumentos;
        private Button btnVerDocumento;
        private Button btnEliminarDocumento;
        private Button btnAgregarDocumento;
        private GroupBox groupBoxBusqueda;
        private TextBox txtBusqueda;
        private Button btnBuscar;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private ToolStripProgressBar toolStripProgressBar;
    }
}