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
        private CN_Cita cnCita = new CN_Cita(); // Necesaria para la creación final

        // ------------------ VARIABLES DE ESTADO DEL WIZARD ------------------
        private CapacidadDia _slotCapacidad; // Slot de disponibilidad seleccionado en frmDisponibilidad
        private DateTime _fechaCita;         // Fecha seleccionada

        private Cliente _clienteActual;      // Cliente seleccionado/registrado (Paso 2)
        private Vehiculo _vehiculoSeleccionado; // Vehículo seleccionado/registrado (Paso 2)
        private Servicio _servicioSeleccionado; // Servicio seleccionado (Paso 1)
        private Tecnico _tecnicoSeleccionado;  // Técnico seleccionado (Paso 3)

        public frmNuevaCita()
        {
            InitializeComponent();

            // Configuración básica
            nudDuracion.Minimum = 0;
            nudDuracion.Maximum = 480;
            nudDuracion.ReadOnly = true;

            // Inicializar con valores por defecto o nulos
            _fechaCita = DateTime.Today;
            _slotCapacidad = null; // o crear uno por defecto

            this.Text = "Crear Nueva Cita";
            InicializarWizard();
        }

        public frmNuevaCita(CapacidadDia slotSeleccionado, DateTime fecha)
        {
            InitializeComponent();

            // Configuración del NumericUpDown
            nudDuracion.Minimum = 0;
            // Establece el máximo a un valor que nunca excederás (Ej: 8 horas)
            nudDuracion.Maximum = 480;
            nudDuracion.ReadOnly = true;

            _slotCapacidad = slotSeleccionado;
            _fechaCita = fecha;

            // Inicializar el TabControl para empezar en la primera pestaña y desactivar las demás
            this.Text = $"Crear Cita para el {fecha:dd/MM/yyyy} ({slotSeleccionado.RangoHorario})";
            InicializarWizard();
        }

        private void InicializarWizard()
        {
            // Bloquea las pestañas 2, 3 y 4 al inicio
            tabControl1.Selecting += TabControlCita_Selecting;
            tabControl1.SelectedIndex = 0;
            // Configurar nudDuracion como solo lectura visualmente
            nudDuracion.ReadOnly = true;
        }

        // Método para evitar que el usuario salte pestañas con el mouse/teclado
        private void TabControlCita_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // Solo permitir el movimiento si el índice destino es el inmediatamente siguiente
            // y si la validación del paso anterior es exitosa.
            if (e.Action == TabControlAction.Selecting)
            {
                if (e.TabPageIndex > tabControl1.SelectedIndex + 1)
                {
                    e.Cancel = true;
                }
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
            // 1. Mostrar Horario Seleccionado (ReadOnly)
            lblFecha.Text = _fechaCita.ToShortDateString();
            lblHorario.Text = _slotCapacidad.RangoHorario;
            lblBahia.Text = _slotCapacidad.Bahia;

            // La duración del slot
            TimeSpan duracionSlot = _slotCapacidad.HoraFin - _slotCapacidad.HoraInicio;
            nudDuracion.Value = (decimal)duracionSlot.TotalMinutes;
            string tipoBahia = _slotCapacidad.Tipo;

            // 2. Cargar ComboBox de Servicios
            try
            {
                // Usamos la Capa Lógica
                List<Servicio> servicios = cnServicio.ListarServicios(tipoBahia);

                // Añadir un servicio Dummy si la lista está vacía
                if (servicios.Count == 0)
                {
                    servicios.Insert(0, new Servicio { ServicioId = 0, Nombre = $"-- No hay servicios para {tipoBahia} --", DuracionMin = 0, Tipo = tipoBahia });
                }

                // Configuración del ComboBox
                cmbServicio.DataSource = servicios;
                cmbServicio.DisplayMember = "Nombre";
                cmbServicio.ValueMember = "ServicioId";
                cmbServicio.SelectedIndex = -1; // No seleccionar nada por defecto

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar servicios: " + ex.Message, "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. Evento: El usuario selecciona un servicio
        private void cmbServicio_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbServicio.SelectedItem is Servicio servicio)
            {
                // Guarda el servicio seleccionado en la variable de estado
                _servicioSeleccionado = servicio;

                // Mostrar la duración del servicio en el nudDuracion
                if (_servicioSeleccionado.ServicioId > 0)
                {
                    // La duración del servicio seleccionado debe ser menor o igual que la duración del slot (Validación opcional)
                    nudDuracion.Value = _servicioSeleccionado.DuracionMin;
                }
                else
                {
                    nudDuracion.Value = 0;
                }
            }
            else
            {
                _servicioSeleccionado = null;
                nudDuracion.Value = 0;
            }
        }

        // 4. Evento: Botón Continuar (btnContinuar1)
        private void btnContinuar1_Click(object sender, EventArgs e)
        {
            // VALIDACIÓN DEL PASO 1
            if (_servicioSeleccionado == null || _servicioSeleccionado.ServicioId <= 0)
            {
                MessageBox.Show("Debe seleccionar un servicio principal para continuar.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TRANSICIÓN AL PASO 2
            CargarPaso2_BuscarCliente();
            tabControl1.SelectedIndex = 1;
        }

        // -------------------------------------------------------------------
        //                       LÓGICA DEL PASO 2
        // -------------------------------------------------------------------

        private void CargarPaso2_BuscarCliente()
        {
            LimpiarCamposCliente();
            LimpiarCamposVehiculo();
            // 2. Opcional: Llenar combos de Marca/Modelo aquí si se necesita para btnRegistrarVehiculo
        }

        // -------------------------------------------------------------------
        //                       LÓGICA DEL PASO 3
        // -------------------------------------------------------------------

        private void DeterminarEstadoGarantia()
        {
            // Lógica Simple (Idealmente, esta lógica de negocio estaría en CN_Vehiculo)

            if (_vehiculoSeleccionado == null)
            {
                lblGarantia.Text = "No Aplica (Vehículo sin datos)";
                return;
            }

            // Nota: Necesitamos un método en CD_Vehiculo para obtener los datos de garantía por VehiculoId.
            // Como no lo tenemos aún, haremos una simulación basada en el vehículo.

            // **ASUNCIÓN TEMPORAL:** Si el año es 2024 o más, asumimos Vigente
            if (_vehiculoSeleccionado.Anio >= (DateTime.Now.Year - 1))
            {
                lblGarantia.Text = "VIGENTE (Fecha o KM no verificado)";
                lblGarantia.BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                lblGarantia.Text = "VENCIDA (Modelo Antiguo)";
                lblGarantia.BackColor = System.Drawing.Color.LightSalmon;
            }

            // Lógica CORRECTA: 
            // Llamar a CN_Vehiculo.ConsultarGarantia(_vehiculoSeleccionado.VehiculoId)
            // El método devuelve un objeto Garantia o null.
            // Si Garantia.Estado == "Vigente", establecer LightGreen.
        }

        private void CargarPaso3_DetallesAdicionales()
        {
            // 1. Mostrar estado de Garantía (Lógica a implementar en Capa Logica o aquí mismo)
            DeterminarEstadoGarantia();

            // 2. Cargar Técnicos (cmbTecnico)
            try
            {
                List<Tecnico> tecnicos = cnTecnico.ListarTecnicos();

                // Añadir una opción por defecto
                tecnicos.Insert(0, new Tecnico { TecnicoId = 0, Nombre = "-- Seleccionar Técnico --" });

                cmbTecnico.DataSource = tecnicos;
                cmbTecnico.DisplayMember = "Nombre";
                cmbTecnico.ValueMember = "TecnicoId";
                cmbTecnico.SelectedIndex = 0; // Selecciona el ítem por defecto
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar técnicos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // 3. Inicializar campos
            txtKilometrajeVehiculo.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
        }

        private void LimpiarCamposCliente()
        {
            txtNombreCliente.Text = string.Empty;
            txtApellidosCliente.Text = string.Empty;
            txtDniCliente.Text = string.Empty;
            txtTelefonoCliente.Text = string.Empty;
            txtEmailCliente.Text = string.Empty;

            // Limpia el DGV de vehículos
            dgvVehiculosRegistrados.DataSource = null;
        }

        private void LlenarCamposCliente(Cliente cliente)
        {
            // Los campos de registro se llenan con los datos del cliente encontrado
            txtNombreCliente.Text = cliente.Nombres;
            txtApellidosCliente.Text = cliente.Apellidos;
            txtDniCliente.Text = cliente.DNI;
            txtTelefonoCliente.Text = cliente.Telefono;
            txtEmailCliente.Text = cliente.Email;

            // Los campos del cliente deben ser de solo lectura si el cliente existe
            EstablecerCamposClienteReadOnly(true);
        }

        private void EstablecerCamposClienteReadOnly(bool readOnly)
        {
            // Si se encuentra, el DNI de búsqueda (txtBuscarDni) debe ser de solo lectura.
            txtBuscarDni.ReadOnly = readOnly;

            // Campos del registro: si existe, solo puede modificar Tlf/Email
            txtNombreCliente.ReadOnly = readOnly;
            txtApellidosCliente.ReadOnly = readOnly;
            txtDniCliente.ReadOnly = readOnly;

            // Dejar Tlf/Email editables para posibles actualizaciones
            txtTelefonoCliente.ReadOnly = false;
            txtEmailCliente.ReadOnly = false;
        }

        private void LimpiarCamposVehiculo()
        {
            txtMarcaVehiculo.Text = string.Empty;
            txtModeloVehiculo.Text = string.Empty;
            txtPlacaVehiculo.Text = string.Empty;
            txtAnioVehiculo.Text = string.Empty;
            txtVinVehiculo.Text = string.Empty;

            _vehiculoSeleccionado = null;

            // Configuramos los campos del vehículo para ser editables para un posible registro nuevo
            EstablecerCamposVehiculoReadOnly(false);
        }

        private void LlenarCamposVehiculo(Vehiculo vehiculo)
        {
            txtMarcaVehiculo.Text = vehiculo.Marca;
            txtModeloVehiculo.Text = vehiculo.Modelo;
            txtPlacaVehiculo.Text = vehiculo.Placa;
            txtAnioVehiculo.Text = vehiculo.Anio.ToString();
            txtVinVehiculo.Text = vehiculo.VIN;

            _vehiculoSeleccionado = vehiculo;

            // Si se llenan por selección del DGV, son de solo lectura
            EstablecerCamposVehiculoReadOnly(true);
        }

        private void EstablecerCamposVehiculoReadOnly(bool readOnly)
        {
            // Si se selecciona un vehículo del DGV, los campos se bloquean (ReadOnly=true)
            txtMarcaVehiculo.ReadOnly = readOnly;
            txtModeloVehiculo.ReadOnly = readOnly;
            txtPlacaVehiculo.ReadOnly = readOnly;
            txtAnioVehiculo.ReadOnly = readOnly;
            txtVinVehiculo.ReadOnly = readOnly;

            // El botón de registro se habilita si no hay vehículo seleccionado (ReadOnly=false)
            btnRegistrarVehiculo.Enabled = !readOnly;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string dniBuscar = txtBuscarDni.Text.Trim();
            List<Vehiculo> vehiculosEncontrados;

            // Limpiar estado y campos antes de buscar
            _clienteActual = null;
            LimpiarCamposCliente();
            LimpiarCamposVehiculo();

            try
            {
                // 1. Llamar a la capa de lógica para buscar Cliente y Vehículos
                _clienteActual = cnCliente.ConsultarCliente(dniBuscar, out vehiculosEncontrados);

                if (_clienteActual != null)
                {
                    // Cliente EXISTENTE encontrado
                    LlenarCamposCliente(_clienteActual);

                    // 2. Mostrar vehículos en el DGV
                    dgvVehiculosRegistrados.DataSource = vehiculosEncontrados;
                    AjustarDGVVehiculos(); // Método para configurar columnas

                    // 3. Seleccionar el primer vehículo por defecto y cargarlo
                    if (dgvVehiculosRegistrados.Rows.Count > 0)
                    {
                        dgvVehiculosRegistrados.Rows[0].Selected = true;
                        SeleccionarVehiculoDeDGV(dgvVehiculosRegistrados.Rows[0]);
                    }

                    // 4. Feedback
                    MessageBox.Show("Cliente encontrado. Seleccione un vehículo o registre uno nuevo.", "Búsqueda Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Cliente NUEVO
                    LimpiarCamposCliente();
                    txtDniCliente.Text = dniBuscar; // Precargar el DNI que no se encontró
                    EstablecerCamposClienteReadOnly(false); // Permite ingresar los datos nuevos

                    // Feedback
                    MessageBox.Show("Cliente no encontrado. Ingrese sus datos para registrarlo.", "Nuevo Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AjustarDGVVehiculos()
        {
            // Lógica para renombrar y ocultar columnas del DGV de Vehículos (copiar de la respuesta anterior)
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
                LlenarCamposVehiculo(vehiculo);
            }
        }

        private void btnRegistrarVehiculo_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validar que el cliente esté listo para el registro
                // Si _clienteActual es null, significa que los datos se deben tomar de los TXT del Registro.
                if (_clienteActual == null)
                {
                    if (string.IsNullOrWhiteSpace(txtNombreCliente.Text) ||
                        string.IsNullOrWhiteSpace(txtApellidosCliente.Text) ||
                        string.IsNullOrWhiteSpace(txtDniCliente.Text))
                    {
                        MessageBox.Show("Complete los datos de Nombres, Apellidos y DNI para registrar el nuevo cliente.", "Validación de Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // --- REGISTRO DEL NUEVO CLIENTE ---
                    Cliente nuevoCliente = new Cliente
                    {
                        DNI = txtDniCliente.Text.Trim(),
                        Nombres = txtNombreCliente.Text.Trim(),
                        Apellidos = txtApellidosCliente.Text.Trim(),
                        Telefono = txtTelefonoCliente.Text.Trim(),
                        Email = txtEmailCliente.Text.Trim()
                    };

                    // Llamada a la Lógica de Negocio para el INSERT
                    int nuevoClienteId = cnCliente.RegistrarNuevoCliente(nuevoCliente);

                    // Actualizar el estado del formulario con el cliente recién creado
                    _clienteActual = nuevoCliente;
                    _clienteActual.ClienteId = nuevoClienteId;
                    EstablecerCamposClienteReadOnly(true);
                }

                // 2. Validar campos del vehículo
                if (string.IsNullOrWhiteSpace(txtPlacaVehiculo.Text) ||
                    string.IsNullOrWhiteSpace(txtMarcaVehiculo.Text) || // Asumimos que los TXT se usan para registrar Marca/Modelo nuevo
                    string.IsNullOrWhiteSpace(txtModeloVehiculo.Text) ||
                    string.IsNullOrWhiteSpace(txtAnioVehiculo.Text) ||
                    !int.TryParse(txtAnioVehiculo.Text, out int anio))
                {
                    MessageBox.Show("Complete Placa, Marca, Modelo y Año válidos para registrar el vehículo.", "Validación de Vehículo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int marcaIdFicticio = 1;
                int modeloIdFicticio = 1;

                Vehiculo nuevoVehiculo = new Vehiculo
                {
                    ClienteId = _clienteActual.ClienteId,
                    Placa = txtPlacaVehiculo.Text.Trim(),
                    VIN = txtVinVehiculo.Text.Trim(),
                    Anio = anio,
                    MarcaId = marcaIdFicticio,
                    ModeloId = modeloIdFicticio,
                    // Datos extra para el DGV
                    Marca = txtMarcaVehiculo.Text.Trim(),
                    Modelo = txtModeloVehiculo.Text.Trim()
                };

                // Llamada a la Lógica de Negocio para el INSERT
                int nuevoVehiculoId = cnVehiculo.RegistrarNuevoVehiculo(nuevoVehiculo);
                nuevoVehiculo.VehiculoId = nuevoVehiculoId;

                // 3. Actualizar la interfaz y seleccionar el vehículo recién creado

                // Obtener la fuente de datos actual (o crear una si no existe)
                List<Vehiculo> lista = dgvVehiculosRegistrados.DataSource as List<Vehiculo> ?? new List<Vehiculo>();
                lista.Add(nuevoVehiculo);

                // Refrescar el DGV
                dgvVehiculosRegistrados.DataSource = null;
                dgvVehiculosRegistrados.DataSource = lista;
                AjustarDGVVehiculos();

                // Llenar los campos del vehículo y bloquearlos, indicando que este es el auto de la cita
                LlenarCamposVehiculo(nuevoVehiculo);

                MessageBox.Show($"Vehículo con placa {nuevoVehiculo.Placa} registrado para {_clienteActual.NombreCompleto}.", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnContinuar2_Click(object sender, EventArgs e)
        {
            // VALIDACIÓN DEL PASO 2

            if (_clienteActual == null || _clienteActual.ClienteId <= 0)
            {
                MessageBox.Show("Debe registrar un cliente (o buscar uno existente) antes de continuar.", "Validación Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_vehiculoSeleccionado == null || _vehiculoSeleccionado.VehiculoId <= 0)
            {
                MessageBox.Show("Debe seleccionar un vehículo de la lista o registrar uno nuevo para continuar.", "Validación Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TRANSICIÓN AL PASO 3
            CargarPaso3_DetallesAdicionales();
            tabControl1.SelectedIndex = 2;
        }

        private void btnContinuar3_Click(object sender, EventArgs e)
        {
            // 1. Validar Kilometraje
            if (string.IsNullOrWhiteSpace(txtKilometrajeVehiculo.Text) ||
                !int.TryParse(txtKilometrajeVehiculo.Text, out int kilometraje) ||
                kilometraje <= 0)
            {
                MessageBox.Show("Debe ingresar un kilometraje válido (mayor a cero).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validar Selección del Técnico
            if (cmbTecnico.SelectedValue == null || (int)cmbTecnico.SelectedValue == 0)
            {
                MessageBox.Show("Debe asignar un técnico responsable para la cita.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Guardar el estado
            _tecnicoSeleccionado = cmbTecnico.SelectedItem as Tecnico;

            // TRANSICIÓN AL PASO 4
            MostrarResumen(); // Llama al método que llena el dgvResumenCita
            tabControl1.SelectedIndex = 3;
        }

        private void MostrarResumen()
        {
            // Asegurarse de que _tecnicoSeleccionado no sea null (por la validación previa)
            string nombreTecnico = _tecnicoSeleccionado?.Nombre ?? "N/A";

            // 1. Crear la lista de resumen
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

            // 2. Asignar al DGV
            dgvResumenCita.DataSource = resumen;

            // 3. Ajustar presentación (Asumiendo que dgvResumenCita es el nombre de tu control)
            dgvResumenCita.Columns["Concepto"].Width = 200;
            dgvResumenCita.Columns["Valor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvResumenCita.RowHeadersVisible = false;
            dgvResumenCita.AllowUserToAddRows = false;
            dgvResumenCita.ReadOnly = true;
            dgvResumenCita.ClearSelection();
        }

        private void btnRegistrarCita_Click(object sender, EventArgs e)
        {
            // Llenar el objeto Cita con los datos recolectados
            Cita nuevaCita = new Cita
            {
                ClienteId = _clienteActual.ClienteId,
                VehiculoId = _vehiculoSeleccionado.VehiculoId,
                ServicioId = _servicioSeleccionado.ServicioId,
                TecnicoId = _tecnicoSeleccionado.TecnicoId,
                FechaCita = _fechaCita,
                // El SP sp_Cita_Crear calcula HoraInicio/Fin y usa el ID de capacidad
            };

            try
            {
                // Llamar al método final de registro
                bool exito = cnCita.RegistrarCita(nuevaCita, _slotCapacidad.CapacidadId);

                if (exito)
                {
                    MessageBox.Show("Cita registrada exitosamente. La disponibilidad ha sido actualizada.", "Registro Completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Indica a frmDisponibilidad que debe refrescar
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo registrar la cita. Detalle: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarCita_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

}
