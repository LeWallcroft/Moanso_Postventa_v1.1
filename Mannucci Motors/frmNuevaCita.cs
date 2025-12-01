using CapaDominio;
using CapaLogica;
using CapaLogicaNegocio;
using Dominio;
using System;
using System.Collections;
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

        private int _vehiculoSeleccionadoID;
        private int _clienteSeleccionadoID;
        private int _bahiaSeleccionadaID;
        private DateTime _fechaSeleccionada;
        private int _idBahiaSeleccionada;

        private int _idServicioSeleccionado;
        private string _nombreServicioSeleccionado; 
        private decimal _precioServicio;            
        private string _observacionesCita;
        private int _duracionServicio;

        private TimeSpan _horaInicioSeleccionada;

        private List<BahiaHorarioDTO> _listaBahiasMemoria;

        public frmNuevaCita()
        {
            InitializeComponent();
            ConfiguracionInicialCalendario();
            CargarServicios();
            btnContinuarCalendario.Enabled = false;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtDireccion.Enabled = false;
            txtTelefono.Enabled = false;
            txtEmail.Enabled = false;

            txtPrecio.Enabled = false;
            txtDuracion.Enabled = false;
            txtDescripcion.Enabled = false;

            tbNuevaCita.Appearance = TabAppearance.FlatButtons;
            tbNuevaCita.ItemSize = new Size(0, 1);
            tbNuevaCita.SizeMode = TabSizeMode.Fixed;
        }

        private void ConfiguracionInicialCalendario()
        {
            // 1. Configurar calendario
            mcFecha.MinDate = DateTime.Today; // No permitir fechas pasadas
            mcFecha.MaxSelectionCount = 1;    // Solo un día a la vez

            // 2. Estado inicial de controles
            btnContinuarDetalle.Enabled = false;

            // 3. Cargar datos de HOY automáticamente al abrir
            _fechaSeleccionada = DateTime.Today;
            CargarDisponibilidadBahias(_fechaSeleccionada);
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            string dni = txtDNIBusqueda.Text.Trim();

            if (string.IsNullOrEmpty(dni))
            {
                MessageBox.Show("Por favor ingrese un DNI.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                CN_Cliente objNegocio = new CN_Cliente();

                // 1. Buscar Cliente
                Cliente cliente = objNegocio.BuscarClientePorDNI(dni);

                if (cliente != null)
                {
                    // Llenar cajas de texto
                    txtNombre.Text = cliente.Nombre;
                    txtApellido.Text = cliente.Apellido;
                    txtEmail.Text = cliente.Email;
                    txtTelefono.Text = cliente.Telefono;
                    txtDireccion.Text = cliente.Direccion;

                    // Guardar el ID del cliente (útil para luego registrar citas)
                    txtDNIBusqueda.Tag = cliente.ClienteID;

                    // 2. Buscar Vehículos (Usando el método que ya tenías en CD)
                    List<Vehiculo> misVehiculos = objNegocio.ListarVehiculosCliente(cliente.ClienteID);

                    // 3. Llenar el Grid
                    dgvVehiculosRegistrados.DataSource = null; // Limpiar
                    dgvVehiculosRegistrados.DataSource = misVehiculos;

                    // 4. Formatear el Grid (Ocultar IDs y ordenar columnas)
                    FormatearGridVehiculos();
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Método auxiliar para limpiar si no se encuentra
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtDNIBusqueda.Tag = null; // Limpiar ID guardado
            dgvVehiculosRegistrados.DataSource = null;
        }

        private void FormatearGridVehiculos()
        {
            if (dgvVehiculosRegistrados.Rows.Count > 0)
            {
                // OCULTAR COLUMNAS QUE NO QUIERES VER (Como los IDs)
                dgvVehiculosRegistrados.Columns["VehiculoID"].Visible = false;
                dgvVehiculosRegistrados.Columns["ClienteID"].Visible = false;
                dgvVehiculosRegistrados.Columns["ModeloID"].Visible = false;
                dgvVehiculosRegistrados.Columns["ClienteDNI"].Visible = false;
                dgvVehiculosRegistrados.Columns["ClienteNombre"].Visible = false;
                dgvVehiculosRegistrados.Columns["ClienteApellido"].Visible = false;
                dgvVehiculosRegistrados.Columns["ClienteNombreCompleto"].Visible = false;
                dgvVehiculosRegistrados.Columns["Activo"].Visible = false;

                dgvVehiculosRegistrados.Columns["Placa"].HeaderText = "Placa";
                dgvVehiculosRegistrados.Columns["Placa"].DisplayIndex = 0;

                dgvVehiculosRegistrados.Columns["Marca"].HeaderText = "Marca";
                dgvVehiculosRegistrados.Columns["Marca"].DisplayIndex = 1;

                dgvVehiculosRegistrados.Columns["Modelo"].HeaderText = "Modelo";
                dgvVehiculosRegistrados.Columns["Modelo"].DisplayIndex = 2;

                dgvVehiculosRegistrados.Columns["Color"].HeaderText = "Color";
                dgvVehiculosRegistrados.Columns["Color"].DisplayIndex = 3;

                dgvVehiculosRegistrados.Columns["Anio"].HeaderText = "Año";
                dgvVehiculosRegistrados.Columns["Anio"].DisplayIndex = 4;

                dgvVehiculosRegistrados.Columns["Kilometraje"].HeaderText = "KM";
                dgvVehiculosRegistrados.Columns["Kilometraje"].DisplayIndex = 5;

                // Ajustar el resto según tu preferencia...
                dgvVehiculosRegistrados.Columns["VIN"].HeaderText = "Nro VIN";
                dgvVehiculosRegistrados.Columns["TipoVehiculo"].HeaderText = "Tipo";
                dgvVehiculosRegistrados.Columns["FechaRegistro"].HeaderText = "Registrado";

                // Ajuste automático de ancho
                dgvVehiculosRegistrados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void dgvVehiculosRegistrados_SelectionChanged(object sender, EventArgs e)
        {
            // Solo habilitar si hay al menos una fila seleccionada
            if (dgvVehiculosRegistrados.SelectedRows.Count > 0)
            {
                btnContinuarCalendario.Enabled = true;
            }
            else
            {
                btnContinuarCalendario.Enabled = false;
            }
        }

        private void btnContinuarCalendario_Click(object sender, EventArgs e)
        {
            // 1. Verificar que haya selección (doble seguridad)
            if (dgvVehiculosRegistrados.SelectedRows.Count == 0) return;

            // 2. Obtener el ID del vehículo de la fila seleccionada
            // Usamos DataBoundItem si usaste DataSource, o Cells["nombre"] si llenaste manual.
            // Como ocultaste la columna "VehiculoID" pero está ahí, podemos leerla:
            int idVehiculoSeleccionado = Convert.ToInt32(dgvVehiculosRegistrados.CurrentRow.Cells["VehiculoID"].Value);

            // Obtener datos visuales para el mensaje (opcional, se ve bonito)
            string placa = dgvVehiculosRegistrados.CurrentRow.Cells["Placa"].Value.ToString();
            string modelo = dgvVehiculosRegistrados.CurrentRow.Cells["Modelo"].Value.ToString();

            try
            {
                CN_Garantia objNegocioGarantia = new CN_Garantia();

                // 3. Consultar a Base de Datos
                bool tieneGarantia = objNegocioGarantia.VehiculoTieneGarantia(idVehiculoSeleccionado);

                if (tieneGarantia)
                {
                    // --- ÉXITO: TIENE GARANTÍA ---

                    // Opcional: Guardar el vehículo seleccionado en una variable global o Tag para el siguiente paso
                    btnContinuarCalendario.Tag = idVehiculoSeleccionado;
                    _vehiculoSeleccionadoID = idVehiculoSeleccionado;

                    MessageBox.Show($"El vehículo {modelo} con placa {placa} tiene garantía ACTIVA.\nProcediendo al siguiente paso...",
                                    "Garantía Validada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    tbNuevaCita.SelectedTab = tabPage2;
                }
                else
                {
                    // --- ERROR: NO TIENE GARANTÍA ---
                    MessageBox.Show($"El vehículo seleccionado ({placa}) NO tiene una garantía vigente o ha expirado.\n\nNo se puede continuar con este tipo de atención.",
                                    "Garantía Inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al validar la garantía: " + ex.Message);
            }
        }

        private void mcFecha_DateChanged(object sender, DateRangeEventArgs e)
        {
            _fechaSeleccionada = mcFecha.SelectionStart;
            CargarDisponibilidadBahias(_fechaSeleccionada);
        }

        private void CargarDisponibilidadBahias(DateTime fecha)
        {
            try
            {
                CN_Bahia objNegocio = new CN_Bahia();

                // Usamos el nuevo método que trae horarios
                _listaBahiasMemoria = objNegocio.ObtenerHorariosDisponibles(fecha);

                CargarComboFiltro();
                ActualizarGrid(_listaBahiasMemoria);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar disponibilidad: " + ex.Message);
            }
        }

        private void CargarComboFiltro()
        {
            cmbBahias.SelectedIndexChanged -= cmbBahias_SelectedIndexChanged;

            // Filtramos por NombreBahia
            var nombres = _listaBahiasMemoria.Select(b => b.NombreBahia).Distinct().ToList();
            nombres.Insert(0, "Todas las Bahías");
            cmbBahias.DataSource = nombres;

            cmbBahias.SelectedIndexChanged += cmbBahias_SelectedIndexChanged;
        }

        private void ActualizarGrid(List<BahiaHorarioDTO> lista)
        {
            dgvBahias.DataSource = null;
            dgvBahias.DataSource = lista;
            FormatearGridBahias();
            btnContinuarDetalle.Enabled = false;
        }

        private void FormatearGridBahias()
        {
            if (dgvBahias.Rows.Count > 0)
            {
                dgvBahias.Columns["BahiaID"].Visible = false;
                dgvBahias.Columns["HoraInicio"].Visible = false; 
                dgvBahias.Columns["HoraFin"].Visible = false;

                dgvBahias.Columns["NombreBahia"].HeaderText = "Bahía";
                dgvBahias.Columns["HorarioTexto"].HeaderText = "Horario"; 
                dgvBahias.Columns["CapacidadTotal"].HeaderText = "Cap. Total";
                dgvBahias.Columns["CuposDisponibles"].HeaderText = "Cupos";

                dgvBahias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void dgvBahias_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBahias.SelectedRows.Count > 0)
            {
                // Obtenemos el objeto seleccionado
                BahiaHorarioDTO seleccion = (BahiaHorarioDTO)dgvBahias.CurrentRow.DataBoundItem;

                // VALIDACIÓN: ¿Hay cupo en ese horario específico?
                if (seleccion.CuposDisponibles > 0)
                {
                    btnContinuarDetalle.Enabled = true;
                }
                else
                {
                    btnContinuarDetalle.Enabled = false;
                }
            }
            else
            {
                btnContinuarDetalle.Enabled = false;
            }
        }

        private void cmbBahias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_listaBahiasMemoria == null) return;
            string seleccion = cmbBahias.SelectedItem.ToString();

            if (seleccion == "Todas las Bahías")
                ActualizarGrid(_listaBahiasMemoria);
            else
                ActualizarGrid(_listaBahiasMemoria.Where(b => b.NombreBahia == seleccion).ToList());
        }

        private void btnContinuarDetalle_Click(object sender, EventArgs e)
        {
            if (dgvBahias.CurrentRow == null) return;

            // CORRECCIÓN: Castear a BahiaHorarioDTO
            BahiaHorarioDTO seleccion = (BahiaHorarioDTO)dgvBahias.CurrentRow.DataBoundItem;

            if (seleccion.CuposDisponibles <= 0)
            {
                MessageBox.Show("No se puede seleccionar un horario sin cupos disponibles.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // GUARDAR DATOS EN MEMORIA DEL FORMULARIO
            _idBahiaSeleccionada = seleccion.BahiaID;
            _horaInicioSeleccionada = seleccion.HoraInicio; // ¡Importante guardar la hora!

            // Mensaje de confirmación visual
            MessageBox.Show($"Seleccionado: {seleccion.NombreBahia}\nHorario: {seleccion.HorarioTexto}", "Confirmación");

            // AVANZAR AL SIGUIENTE TAB
            tbNuevaCita.SelectedTab = tabPage3;
        }

        private void btnAtrasBuscarCliente_Click(object sender, EventArgs e)
        {
            tbNuevaCita.SelectedTab = tabPage1;

            _idBahiaSeleccionada = 0;
            btnContinuarDetalle.Enabled = false;
        }

        private void dgvBahias_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvBahias.Columns[e.ColumnIndex].Name == "CuposDisponibles")
            {
                if (e.Value != null && int.TryParse(e.Value.ToString(), out int cupos))
                {
                    if (cupos == 0)
                    {
                        // Si hay 0 cupos, pintar la fila o celda de rojo suave
                        dgvBahias.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
                        dgvBahias.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Red;
                    }
                }
            }
        }

        private void cmbServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServicios.SelectedIndex != -1 && cmbServicios.SelectedItem is Servicio servicioSelected)
            {
                // Llenar los TextBoxes (Read Only)
                txtPrecio.Text = servicioSelected.Precio.ToString("C2"); // Formato moneda
                txtDuracion.Text = servicioSelected.DuracionEstimada.ToString() + " min";
                txtDescripcion.Text = servicioSelected.Descripcion;

                // Guardar en variables temporales por si acaso
                _idServicioSeleccionado = servicioSelected.ServicioID;
                _precioServicio = servicioSelected.Precio;
                _nombreServicioSeleccionado = servicioSelected.Nombre;

                _duracionServicio = servicioSelected.DuracionEstimada;

                // Habilitar botón continuar
                btnContinuarResumen.Enabled = true;
            }
            else
            {
                LimpiarDetallesServicio();
            }
        }

        private void CargarServicios()
        {
            try
            {
                CN_Servicios objNegocio = new CN_Servicios();
                List<Servicio> listaServicios = objNegocio.ListarServicios();

                // Configuración para que el ComboBox entienda qué mostrar y qué valor guardar
                cmbServicios.DataSource = listaServicios;
                cmbServicios.DisplayMember = "Nombre";      // Lo que ve el usuario
                cmbServicios.ValueMember = "ServicioID";    // El valor interno (ID)

                // Limpiamos selección inicial para obligar al usuario a elegir
                cmbServicios.SelectedIndex = -1;
                LimpiarDetallesServicio();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar servicios: " + ex.Message);
            }
        }

        private void LimpiarDetallesServicio()
        {
            txtPrecio.Text = "";
            txtDuracion.Text = "";
            txtDescripcion.Text = "";
            btnContinuarResumen.Enabled = false; // Deshabilitar hasta que seleccione algo
        }

        private void btnContinuarResumen_Click(object sender, EventArgs e)
        {
            // 1. Validar que se haya seleccionado un servicio
            if (cmbServicios.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un servicio para continuar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Guardar las observaciones (Este campo es opcional, pero lo guardamos)
            _observacionesCita = txtObservaciones.Text.Trim();

            MessageBox.Show("Datos del servicio guardados.", "Paso 3 Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 4. Ir a la pestaña RESUMEN
            CargarDatosResumen();
            tbNuevaCita.SelectedTab = tabPage4;
        }

        private void btnAtrasCalendario_Click(object sender, EventArgs e)
        {
            tbNuevaCita.SelectedTab = tabPage2;
        }

        private void CargarDatosResumen()
        {
            DataTable dtResumen = new DataTable();
            dtResumen.Columns.Add("Concepto");
            dtResumen.Columns.Add("Valor");

            dtResumen.Rows.Add("Cliente", $"{txtNombre.Text} {txtApellido.Text}");
            dtResumen.Rows.Add("Vehículo", dgvVehiculosRegistrados.CurrentRow.Cells["Placa"].Value.ToString());

            // Combinamos fecha y hora para mostrar
            DateTime fechaHoraCita = _fechaSeleccionada.Date + _horaInicioSeleccionada;
            dtResumen.Rows.Add("Fecha y Hora", fechaHoraCita.ToString("dd/MM/yyyy HH:mm"));

            // Recuperar nombre de bahía del grid anterior (o buscar en la lista memoria)
            string nombreBahia = _listaBahiasMemoria.FirstOrDefault(b => b.BahiaID == _idBahiaSeleccionada)?.NombreBahia ?? "N/A";
            dtResumen.Rows.Add("Bahía", nombreBahia);

            dtResumen.Rows.Add("Servicio", _nombreServicioSeleccionado);
            dtResumen.Rows.Add("Total a Pagar", _precioServicio.ToString("C2"));

            dgvResumen.DataSource = dtResumen;
            dgvResumen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResumen.ColumnHeadersVisible = false;
        }

        private void btnRegistrarCita_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Confirmar registro de cita?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            try
            {
                CN_Cita objCita = new CN_Cita();
                string mensaje;
                int usuarioLogueadoId = 1; // Aquí iría tu variable de sesión real

                // COMBINAR FECHA + HORA PARA LA BD
                // _fechaSeleccionada tiene la fecha (00:00:00)
                // _horaInicioSeleccionada tiene la hora (ej: 08:00:00)
                DateTime fechaFinalBD = _fechaSeleccionada.Date + _horaInicioSeleccionada;

                bool exito = objCita.RegistrarNuevaCita(
                    _vehiculoSeleccionadoID, // Ya lo guardamos en el paso 1
                    usuarioLogueadoId,
                    fechaFinalBD,            // Enviamos la fecha con hora exacta
                    _duracionServicio,
                    _observacionesCita,
                    _idServicioSeleccionado,
                    _precioServicio,
                    out mensaje
                );

                if (exito)
                {
                    MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al registrar: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error crítico: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "¿Está seguro de que desea cancelar el proceso?\nSe perderán todos los datos ingresados.",
            "Cancelar Cita",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
    );

            // Si dice que sí, cerramos el formulario
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
