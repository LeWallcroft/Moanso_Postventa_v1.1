using CapaDominio;
using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mannucci_Motors
{
    public partial class frmNuevaCita : Form
    {
        // ------------------ INSTANCIAS DE CAPA LÓGICA ------------------
        private CN_Servicio cnServicio = new CN_Servicio();
        private CN_Cliente cnCliente = new CN_Cliente();
        private CN_Tecnico cnTecnico = new CN_Tecnico();
        private CN_Vehiculo cnVehiculo = new CN_Vehiculo();
        private CN_Cita cnCita = new CN_Cita();

        // ------------------ VARIABLES DE ESTADO DEL WIZARD ------------------
        private CapacidadDia _slotCapacidad;
        private DateTime _fechaCita;
        private Cliente _clienteActual;
        private Vehiculo _vehiculoSeleccionado;
        private Servicio _servicioSeleccionado;
        private Tecnico _tecnicoSeleccionado;

        public frmNuevaCita()
        {
            InitializeComponent();
            ConfigurarControles();
            InicializarWizard();
        }

        public frmNuevaCita(CapacidadDia slotSeleccionado, DateTime fecha)
        {
            InitializeComponent();
            ConfigurarControles();

            _slotCapacidad = slotSeleccionado;
            _fechaCita = fecha;

            this.Text = $"Crear Cita para el {fecha:dd/MM/yyyy} ({slotSeleccionado.RangoHorario})";
            InicializarWizard();
        }

        private void ConfigurarControles()
        {

            this.tabControl1.Appearance = TabAppearance.FlatButtons;

            this.tabControl1.ItemSize = new Size(0, 1);

            this.tabControl1.SizeMode = TabSizeMode.Fixed;

            nudDuracion.Minimum = 0;
            nudDuracion.Maximum = 480;
            nudDuracion.ReadOnly = true;
            _fechaCita = DateTime.Today;

            this.txtBuscarDni.MaxLength = 8;
            this.txtTelefonoCliente.MaxLength = 9;

        }

        private void InicializarWizard()
        {
            tabControl1.Selecting += TabControlCita_Selecting;
            tabControl1.SelectedIndex = 0;
        }

        private void TabControlCita_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex != tabControl1.SelectedIndex)
            {
                e.Cancel = true;
            }
        }

        private void frmNuevaCita_Load(object sender, EventArgs e)
        {
            CargarPaso1_HorarioServicio();
        }

        // -------------------------------------------------------------------
        //                       LÓGICA DEL PASO 1
        // -------------------------------------------------------------------

        private void CargarPaso1_HorarioServicio()
        {
            try
            {
                // 1. Mostrar información básica
                lblFecha.Text = _fechaCita.ToShortDateString();
                lblHorario.Text = _slotCapacidad.RangoHorario;
                lblBahia.Text = _slotCapacidad.Bahia;

                // 2. Calcular duración del slot
                TimeSpan duracionSlot = _slotCapacidad.HoraFin - _slotCapacidad.HoraInicio;
                nudDuracion.Value = (decimal)duracionSlot.TotalMinutes;

                // 3. Obtener tipo de bahía y cargar servicios
                string tipoBahia = _slotCapacidad.Tipo ?? "General";

                // DEBUG
                MessageBox.Show($"Tipo de bahía: {tipoBahia}", "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Information);

                List<Servicio> servicios = cnServicio.ListarServicios(tipoBahia);

                // 4. Configurar ComboBox
                if (servicios.Count == 0)
                {
                    servicios.Insert(0, new Servicio
                    {
                        ServicioId = 0,
                        Nombre = $"-- No hay servicios para {tipoBahia} --",
                        DuracionMin = 0,
                        Tipo = tipoBahia
                    });
                }

                cmbServicio.DataSource = servicios;
                cmbServicio.DisplayMember = "Nombre";
                cmbServicio.ValueMember = "ServicioId";
                cmbServicio.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar servicios: {ex.Message}", "Error de BD",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbServicio_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbServicio.SelectedItem is Servicio servicio)
            {
                _servicioSeleccionado = servicio;
                nudDuracion.Value = _servicioSeleccionado.ServicioId > 0 ? _servicioSeleccionado.DuracionMin : 0;
            }
            else
            {
                _servicioSeleccionado = null;
                nudDuracion.Value = 0;
            }
        }

        private void btnContinuar1_Click(object sender, EventArgs e)
        {
            if (_servicioSeleccionado == null || _servicioSeleccionado.ServicioId <= 0)
            {
                MessageBox.Show("Debe seleccionar un servicio principal para continuar.",
                              "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarPaso2_BuscarCliente();
            this.tabControl1.SelectedIndex = this.tabControl1.SelectedIndex + 1;
        }

        // -------------------------------------------------------------------
        //                       LÓGICA DEL PASO 2
        // -------------------------------------------------------------------

        private void CargarPaso2_BuscarCliente()
        {
            LimpiarCamposCliente();
            LimpiarCamposVehiculo();
        }

        // ... (el resto del código del PASO 2 se mantiene igual)
        // Solo pego las partes críticas para ahorrar espacio

        private void LimpiarCamposCliente()
        {
            txtNombreCliente.Text = string.Empty;
            txtApellidosCliente.Text = string.Empty;
            txtDniCliente.Text = string.Empty;
            txtTelefonoCliente.Text = string.Empty;
            txtEmailCliente.Text = string.Empty;
            dgvVehiculosRegistrados.DataSource = null;
        }

        private void LimpiarCamposVehiculo()
        {
            txtMarcaVehiculo.Text = string.Empty;
            txtModeloVehiculo.Text = string.Empty;
            txtPlacaVehiculo.Text = string.Empty;
            txtAnioVehiculo.Text = string.Empty;
            txtVinVehiculo.Text = string.Empty;
            _vehiculoSeleccionado = null;
            EstablecerCamposVehiculoReadOnly(false);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string dniBuscar = txtBuscarDni.Text.Trim();
            List<Vehiculo> vehiculosEncontrados;

            _clienteActual = null;
            LimpiarCamposCliente();
            LimpiarCamposVehiculo();

            try
            {
                _clienteActual = cnCliente.ConsultarCliente(dniBuscar, out vehiculosEncontrados);

                if (_clienteActual != null)
                {
                    LlenarCamposCliente(_clienteActual);
                    dgvVehiculosRegistrados.DataSource = vehiculosEncontrados;
                    AjustarDGVVehiculos();

                    if (dgvVehiculosRegistrados.Rows.Count > 0)
                    {
                        dgvVehiculosRegistrados.Rows[0].Selected = true;
                        SeleccionarVehiculoDeDGV(dgvVehiculosRegistrados.Rows[0]);
                    }

                    MessageBox.Show("Cliente encontrado. Seleccione un vehículo o registre uno nuevo.",
                                  "Búsqueda Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    LimpiarCamposCliente();
                    txtDniCliente.Text = dniBuscar;
                    EstablecerCamposClienteReadOnly(false);
                    MessageBox.Show("Cliente no encontrado. Ingrese sus datos para registrarlo.",
                                  "Nuevo Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de búsqueda: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AjustarDGVVehiculos()
        {
            if (dgvVehiculosRegistrados.DataSource == null) return;
            dgvVehiculosRegistrados.Columns["VehiculoId"].Visible = false;
            dgvVehiculosRegistrados.Columns["ClienteId"].Visible = false;
            dgvVehiculosRegistrados.Columns["VIN"].Visible = false;
            dgvVehiculosRegistrados.Columns["Marca"].HeaderText = "MARCA";
            dgvVehiculosRegistrados.Columns["Placa"].HeaderText = "PLACA";
            dgvVehiculosRegistrados.Columns["Modelo"].HeaderText = "MODELO";
        }

        private void dgvVehiculosRegistrados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SeleccionarVehiculoDeDGV(dgvVehiculosRegistrados.Rows[e.RowIndex]);
            }
        }

        private void SeleccionarVehiculoDeDGV(DataGridViewRow fila)
        {
            if (fila.DataBoundItem is Vehiculo vehiculo)
            {
                _vehiculoSeleccionado = vehiculo;
                LlenarCamposVehiculo(vehiculo);
            }
        }

        private void LlenarCamposCliente(Cliente cliente)
        {
            txtNombreCliente.Text = cliente.Nombres;
            txtApellidosCliente.Text = cliente.Apellidos;
            txtDniCliente.Text = cliente.DNI;
            txtTelefonoCliente.Text = cliente.Telefono;
            txtEmailCliente.Text = cliente.Email;
            EstablecerCamposClienteReadOnly(true);
        }

        private void LlenarCamposVehiculo(Vehiculo vehiculo)
        {
            txtMarcaVehiculo.Text = vehiculo.Marca;
            txtModeloVehiculo.Text = vehiculo.Modelo;
            txtPlacaVehiculo.Text = vehiculo.Placa;
            txtAnioVehiculo.Text = vehiculo.Anio.ToString();
            txtVinVehiculo.Text = vehiculo.VIN;
            _vehiculoSeleccionado = vehiculo;
            EstablecerCamposVehiculoReadOnly(true);
        }

        private void EstablecerCamposClienteReadOnly(bool readOnly)
        {
            txtBuscarDni.ReadOnly = false;
            txtNombreCliente.ReadOnly = readOnly;
            txtApellidosCliente.ReadOnly = readOnly;
            txtDniCliente.ReadOnly = readOnly;
            txtTelefonoCliente.ReadOnly = false;
            txtEmailCliente.ReadOnly = false;
        }

        private void EstablecerCamposVehiculoReadOnly(bool readOnly)
        {
            txtMarcaVehiculo.ReadOnly = readOnly;
            txtModeloVehiculo.ReadOnly = readOnly;
            txtPlacaVehiculo.ReadOnly = readOnly;
            txtAnioVehiculo.ReadOnly = readOnly;
            txtVinVehiculo.ReadOnly = readOnly;
            btnRegistrarVehiculo.Enabled = !readOnly;
        }

        private void btnRegistrarVehiculo_Click(object sender, EventArgs e)
        {
            try
            {
                CN_Marca cnMarca = new CN_Marca();
                CN_Modelo cnModelo = new CN_Modelo();

                // 1. Obtener/Crear ID de la Marca
                int marcaIdReal = cnMarca.ProcesarMarca(txtMarcaVehiculo.Text.Trim());

                // 2. Obtener/Crear ID del Modelo
                int modeloIdReal = cnModelo.ProcesarModelo(marcaIdReal, txtModeloVehiculo.Text.Trim());

                if (_clienteActual == null)
                {
                    if (string.IsNullOrWhiteSpace(txtNombreCliente.Text) ||
                        string.IsNullOrWhiteSpace(txtApellidosCliente.Text) ||
                        string.IsNullOrWhiteSpace(txtDniCliente.Text))
                    {
                        MessageBox.Show("Complete los datos de Nombres, Apellidos y DNI para registrar el nuevo cliente.",
                                      "Validación de Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    Cliente nuevoCliente = new Cliente
                    {
                        DNI = txtDniCliente.Text.Trim(),
                        Nombres = txtNombreCliente.Text.Trim(),
                        Apellidos = txtApellidosCliente.Text.Trim(),
                        Telefono = txtTelefonoCliente.Text.Trim(),
                        Email = txtEmailCliente.Text.Trim()
                    };

                    int nuevoClienteId = cnCliente.RegistrarNuevoCliente(nuevoCliente);
                    _clienteActual = nuevoCliente;
                    _clienteActual.ClienteId = nuevoClienteId;
                    EstablecerCamposClienteReadOnly(true);
                }

                if (string.IsNullOrWhiteSpace(txtPlacaVehiculo.Text) ||
                    string.IsNullOrWhiteSpace(txtMarcaVehiculo.Text) ||
                    string.IsNullOrWhiteSpace(txtModeloVehiculo.Text) ||
                    string.IsNullOrWhiteSpace(txtAnioVehiculo.Text) ||
                    !int.TryParse(txtAnioVehiculo.Text, out int anio))
                {
                    MessageBox.Show("Complete Placa, Marca, Modelo y Año válidos para registrar el vehículo.",
                                  "Validación de Vehículo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Vehiculo nuevoVehiculo = new Vehiculo
                {
                    ClienteId = _clienteActual.ClienteId,
                    Placa = txtPlacaVehiculo.Text.Trim(),
                    VIN = txtVinVehiculo.Text.Trim(),
                    Anio = anio,
                    MarcaId = marcaIdReal,
                    ModeloId = modeloIdReal,
                    // Datos extra para el DGV
                    Marca = txtMarcaVehiculo.Text.Trim(),
                    Modelo = txtModeloVehiculo.Text.Trim()
                };

                int nuevoVehiculoId = cnVehiculo.RegistrarNuevoVehiculo(nuevoVehiculo);
                nuevoVehiculo.VehiculoId = nuevoVehiculoId;

                List<Vehiculo> lista = dgvVehiculosRegistrados.DataSource as List<Vehiculo> ?? new List<Vehiculo>();
                lista.Add(nuevoVehiculo);

                dgvVehiculosRegistrados.DataSource = null;
                dgvVehiculosRegistrados.DataSource = lista;
                AjustarDGVVehiculos();

                LlenarCamposVehiculo(nuevoVehiculo);

                MessageBox.Show($"Vehículo con placa {nuevoVehiculo.Placa} registrado para {_clienteActual.NombreCompleto}.",
                              "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de registro: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnContinuar2_Click(object sender, EventArgs e)
        {
            if (_clienteActual == null || _clienteActual.ClienteId <= 0)
            {
                MessageBox.Show("Debe registrar un cliente (o buscar uno existente) antes de continuar.",
                              "Validación Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_vehiculoSeleccionado == null || _vehiculoSeleccionado.VehiculoId <= 0)
            {
                MessageBox.Show("Debe seleccionar un vehículo de la lista o registrar uno nuevo para continuar.",
                              "Validación Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarPaso3_DetallesAdicionales();
            this.tabControl1.SelectedIndex = this.tabControl1.SelectedIndex + 1; // Avanza
        }

        private void btnNuevoVehiculo_Click(object sender, EventArgs e)
        {
            LimpiarCamposVehiculo(); 
            EstablecerCamposVehiculoReadOnly(false); 
        }

        // -------------------------------------------------------------------
        //                       LÓGICA DEL PASO 3
        // -------------------------------------------------------------------

        private void CargarPaso3_DetallesAdicionales()
        {
            DeterminarEstadoGarantia();
            CargarTecnicos();
            txtKilometrajeVehiculo.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
        }

        private void DeterminarEstadoGarantia()
        {
            lblGarantia.BackColor = System.Drawing.Color.LightGray; // Resetear color

            if (_vehiculoSeleccionado == null || _vehiculoSeleccionado.VehiculoId <= 0)
            {
                lblGarantia.Text = "No Aplica (Vehículo no seleccionado)";
                return;
            }

            // Llamada a la Capa Lógica
            Garantia garantia = cnVehiculo.ObtenerGarantia(_vehiculoSeleccionado.VehiculoId);

            if (garantia == null)
            {
                lblGarantia.Text = "No Registrada";
            }
            else
            {
                // Mostrar el estado real de la BD
                lblGarantia.Text = $"{garantia.Estado} hasta {garantia.FechaFin:dd/MM/yyyy}";

                if (garantia.Estado == "Vigente")
                {
                    lblGarantia.BackColor = System.Drawing.Color.LightGreen;
                }
                else // Vencida
                {
                    lblGarantia.BackColor = System.Drawing.Color.LightSalmon;
                }
            }
        }

        private void CargarTecnicos()
        {
            try
            {
                List<Tecnico> tecnicos = cnTecnico.ListarTecnicos();
                tecnicos.Insert(0, new Tecnico { TecnicoId = 0, Nombre = "-- Seleccionar Técnico --" });

                cmbTecnico.DataSource = tecnicos;
                cmbTecnico.DisplayMember = "Nombre";
                cmbTecnico.ValueMember = "TecnicoId";
                cmbTecnico.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar técnicos: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnContinuar3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKilometrajeVehiculo.Text) ||
                !int.TryParse(txtKilometrajeVehiculo.Text, out int kilometraje) ||
                kilometraje <= 0)
            {
                MessageBox.Show("Debe ingresar un kilometraje válido (mayor a cero).",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbTecnico.SelectedValue == null || (int)cmbTecnico.SelectedValue == 0)
            {
                MessageBox.Show("Debe asignar un técnico responsable para la cita.",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _tecnicoSeleccionado = cmbTecnico.SelectedItem as Tecnico;
            MostrarResumen();
            tabControl1.SelectedIndex = 3;
        }

        private void MostrarResumen()
        {
            string nombreTecnico = _tecnicoSeleccionado?.Nombre ?? "N/A";

            List<ResumenItem> resumen = new List<ResumenItem>();

            resumen.Add(new ResumenItem { Concepto = "------------------ HORARIO Y SERVICIO ------------------", Valor = "" });
            resumen.Add(new ResumenItem { Concepto = "Fecha", Valor = _fechaCita.ToShortDateString() });
            resumen.Add(new ResumenItem { Concepto = "Horario", Valor = _slotCapacidad.RangoHorario });
            resumen.Add(new ResumenItem { Concepto = "Bahía Asignada", Valor = _slotCapacidad.Bahia });
            resumen.Add(new ResumenItem { Concepto = "Servicio Solicitado", Valor = _servicioSeleccionado.Nombre });

            resumen.Add(new ResumenItem { Concepto = "------------------ CLIENTE Y VEHÍCULO ------------------", Valor = "" });
            resumen.Add(new ResumenItem { Concepto = "Cliente (DNI)", Valor = $"{_clienteActual.NombreCompleto} ({_clienteActual.DNI})" });
            resumen.Add(new ResumenItem { Concepto = "Teléfono", Valor = _clienteActual.Telefono });
            resumen.Add(new ResumenItem { Concepto = "Vehículo (Placa)", Valor = $"{_vehiculoSeleccionado.Marca} {_vehiculoSeleccionado.Modelo} ({_vehiculoSeleccionado.Placa})" });

            resumen.Add(new ResumenItem { Concepto = "------------------ DETALLES DE ATENCIÓN ------------------", Valor = "" });
            resumen.Add(new ResumenItem { Concepto = "Kilometraje Declarado", Valor = txtKilometrajeVehiculo.Text });
            resumen.Add(new ResumenItem { Concepto = "Técnico Principal", Valor = nombreTecnico });
            resumen.Add(new ResumenItem { Concepto = "Observaciones", Valor = string.IsNullOrWhiteSpace(txtObservaciones.Text) ? "Ninguna" : txtObservaciones.Text });

            dgvResumenCita.DataSource = resumen;

            dgvResumenCita.Columns["Concepto"].Width = 200;
            dgvResumenCita.Columns["Valor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvResumenCita.RowHeadersVisible = false;
            dgvResumenCita.AllowUserToAddRows = false;
            dgvResumenCita.ReadOnly = true;
            dgvResumenCita.ClearSelection();
        }

        private void btnRegistrarCita_Click(object sender, EventArgs e)
        {
            Cita nuevaCita = new Cita
            {
                ClienteId = _clienteActual.ClienteId,
                VehiculoId = _vehiculoSeleccionado.VehiculoId,
                ServicioId = _servicioSeleccionado.ServicioId,
                TecnicoId = _tecnicoSeleccionado.TecnicoId,
                FechaCita = _fechaCita,
            };

            try
            {
                bool exito = cnCita.RegistrarCita(nuevaCita, _slotCapacidad.CapacidadId);

                if (exito)
                {
                    MessageBox.Show("Cita registrada exitosamente. La disponibilidad ha sido actualizada.",
                                  "Registro Completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo registrar la cita. Detalle: " + ex.Message,
                              "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarCita_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void btnAtras2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void btnAtras3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }
    }

    // Clase auxiliar para el resumen
    public class ResumenItem
    {
        public string Concepto { get; set; }
        public string Valor { get; set; }
    }
}