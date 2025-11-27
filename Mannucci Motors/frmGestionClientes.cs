using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CapaLogica;
using Dominio;

namespace Mannucci_Motors
{
    public partial class frmGestionClientes : Form
    {
        private CN_Cliente cnCliente = new CN_Cliente();
        private CN_Vehiculo cnVehiculo = new CN_Vehiculo();
        private Cliente clienteActual = null;

        // Colores de la paleta
        private Color colorRojo = Color.FromArgb(192, 0, 0);
        private Color colorGris = Color.FromArgb(64, 64, 64);
        private Color colorBlanco = Color.White;
        private Color colorGrisClaro = Color.FromArgb(240, 240, 240);

        public frmGestionClientes()
        {
            InitializeComponent();
            ConfigurarEventos();
            ConfigurarDataGrid();
            CargarMarcas();
            DeshabilitarControlesCliente();
            DeshabilitarControlesVehiculo();
        }

        private void ConfigurarEventos()
        {
            this.btnBuscarCliente.Click += new EventHandler(btnBuscarCliente_Click);
            this.btnLimpiarCliente.Click += new EventHandler(btnLimpiarCliente_Click);
            this.btnRegistrarVehiculo.Click += new EventHandler(btnRegistrarVehiculo_Click);
            this.cbMarca.SelectedIndexChanged += new EventHandler(cbMarca_SelectedIndexChanged);

            // Validaciones de entrada
            this.txtDNIBusqueda.KeyPress += new KeyPressEventHandler(txtDNI_KeyPress);
            this.txtDNI.KeyPress += new KeyPressEventHandler(txtDNI_KeyPress);
            this.txtAnio.KeyPress += new KeyPressEventHandler(txtNumerico_KeyPress);
            this.txtKilometraje.KeyPress += new KeyPressEventHandler(txtNumerico_KeyPress);
        }

        private void ConfigurarDataGrid()
        {
            dgvVehiculos.Columns.Clear();

            // Configurar columnas del DataGridView
            dgvVehiculos.Columns.Add("Placa", "Placa");
            dgvVehiculos.Columns.Add("Marca", "Marca");
            dgvVehiculos.Columns.Add("Modelo", "Modelo");
            dgvVehiculos.Columns.Add("Color", "Color");
            dgvVehiculos.Columns.Add("Año", "Año");
            dgvVehiculos.Columns.Add("Combustible", "Combustible");
            dgvVehiculos.Columns.Add("FechaRegistro", "Fecha Registro");

            // Estilo del DataGrid
            dgvVehiculos.BackgroundColor = colorBlanco;
            dgvVehiculos.BorderStyle = BorderStyle.Fixed3D;
            dgvVehiculos.AlternatingRowsDefaultCellStyle.BackColor = colorGrisClaro;
            dgvVehiculos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarMarcas()
        {
            try
            {
                var marcas = cnVehiculo.ListarMarcas();
                cbMarca.DataSource = marcas;
                cbMarca.DisplayMember = "Nombre";
                cbMarca.ValueMember = "MarcaID";
                cbMarca.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar marcas: " + ex.Message);
            }
        }

        private void CargarModelos(int marcaID)
        {
            try
            {
                var modelos = cnVehiculo.ListarModelosPorMarca(marcaID);
                cbModelo.DataSource = modelos;
                cbModelo.DisplayMember = "NombreCompleto";
                cbModelo.ValueMember = "ModeloID";
                cbModelo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar modelos: " + ex.Message);
            }
        }

        // ========== EVENTOS DE CLIENTE ==========

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            string dni = txtDNIBusqueda.Text.Trim();

            if (string.IsNullOrEmpty(dni))
            {
                MostrarAdvertencia("Ingrese un DNI para buscar");
                txtDNIBusqueda.Focus();
                return;
            }

            try
            {
                clienteActual = cnCliente.BuscarClientePorDNI(dni);

                if (clienteActual != null)
                {
                    // Cliente encontrado
                    MostrarDatosCliente(clienteActual);
                    CargarVehiculosCliente(clienteActual.ClienteID);
                    HabilitarControlesVehiculo();
                    lblInfoCliente.Text = $"Cliente encontrado: {clienteActual.NombreCompleto}";
                    lblInfoCliente.ForeColor = colorGris;
                }
                else
                {
                    // Cliente no encontrado - permitir registro
                    LimpiarDatosCliente();
                    txtDNI.Text = dni; // Pre-cargar DNI
                    HabilitarControlesCliente();
                    lblInfoCliente.Text = "Cliente no encontrado. Complete los datos para registrar uno nuevo.";
                    lblInfoCliente.ForeColor = colorRojo;
                    DeshabilitarControlesVehiculo();
                    txtNombre.Focus();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnLimpiarCliente_Click(object sender, EventArgs e)
        {
            LimpiarDatosCliente();
            txtDNIBusqueda.Clear();
            txtDNIBusqueda.Focus();
            lblInfoCliente.Text = "Ingrese el DNI del cliente para buscar o registrar uno nuevo";
            lblInfoCliente.ForeColor = colorRojo;
            DeshabilitarControlesVehiculo();
            clienteActual = null;
            dgvVehiculos.Rows.Clear();
        }

        // ========== EVENTOS DE VEHÍCULO ==========

        private void cbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMarca.SelectedValue != null && int.TryParse(cbMarca.SelectedValue.ToString(), out int marcaID))
            {
                CargarModelos(marcaID);
            }
            else
            {
                cbModelo.DataSource = null;
                cbModelo.Items.Clear();
            }
        }

        private void btnRegistrarVehiculo_Click(object sender, EventArgs e)
        {
            if (clienteActual == null)
            {
                MostrarAdvertencia("Primero debe buscar o registrar un cliente");
                return;
            }

            if (!ValidarDatosVehiculo())
                return;

            try
            {
                Vehiculo nuevoVehiculo = new Vehiculo
                {
                    ClienteID = clienteActual.ClienteID,
                    ModeloID = (int)cbModelo.SelectedValue,
                    Placa = txtPlaca.Text.Trim().ToUpper(),
                    VIN = string.IsNullOrWhiteSpace(txtVIN.Text) ? null : txtVIN.Text.Trim().ToUpper(),
                    Color = string.IsNullOrWhiteSpace(txtColor.Text) ? null : txtColor.Text.Trim(),
                    Anio = string.IsNullOrWhiteSpace(txtAnio.Text) ? (int?)null : int.Parse(txtAnio.Text),
                    Kilometraje = string.IsNullOrWhiteSpace(txtKilometraje.Text) ? (int?)null : int.Parse(txtKilometraje.Text),
                    Combustible = cbCombustible.SelectedItem?.ToString(),
                    Transmision = cbTransmision.SelectedItem?.ToString()
                };

                string resultado = cnVehiculo.RegistrarVehiculo(nuevoVehiculo);

                if (resultado.Contains("éxito"))
                {
                    MostrarExito(resultado);
                    LimpiarDatosVehiculo();
                    CargarVehiculosCliente(clienteActual.ClienteID);
                }
                else
                {
                    MostrarError(resultado);
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al registrar vehículo: " + ex.Message);
            }
        }

        // ========== MÉTODOS AUXILIARES ==========

        private bool ValidarDatosCliente()
        {
            if (string.IsNullOrWhiteSpace(txtDNI.Text))
            {
                MostrarAdvertencia("El DNI es obligatorio");
                txtDNI.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarAdvertencia("El nombre es obligatorio");
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MostrarAdvertencia("El apellido es obligatorio");
                txtApellido.Focus();
                return false;
            }

            return true;
        }

        private bool ValidarDatosVehiculo()
        {
            if (cbMarca.SelectedValue == null)
            {
                MostrarAdvertencia("Seleccione una marca");
                cbMarca.Focus();
                return false;
            }

            if (cbModelo.SelectedValue == null)
            {
                MostrarAdvertencia("Seleccione un modelo");
                cbModelo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPlaca.Text))
            {
                MostrarAdvertencia("La placa es obligatoria");
                txtPlaca.Focus();
                return false;
            }

            return true;
        }

        private void MostrarDatosCliente(Cliente cliente)
        {
            txtDNI.Text = cliente.DNI;
            txtNombre.Text = cliente.Nombre;
            txtApellido.Text = cliente.Apellido;
            txtEmail.Text = cliente.Email ?? "";
            txtTelefono.Text = cliente.Telefono ?? "";
            txtDireccion.Text = cliente.Direccion ?? "";
            DeshabilitarControlesCliente();
        }

        private void LimpiarDatosCliente()
        {
            txtDNI.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            DeshabilitarControlesCliente();
        }

        private void LimpiarDatosVehiculo()
        {
            cbMarca.SelectedIndex = -1;
            cbModelo.DataSource = null;
            cbModelo.Items.Clear();
            txtPlaca.Clear();
            txtVIN.Clear();
            txtColor.Clear();
            txtAnio.Clear();
            txtKilometraje.Clear();
            cbCombustible.SelectedIndex = -1;
            cbTransmision.SelectedIndex = -1;
        }

        private void CargarVehiculosCliente(int clienteID)
        {
            try
            {
                var vehiculos = cnCliente.ListarVehiculosCliente(clienteID);
                dgvVehiculos.Rows.Clear();

                foreach (var vehiculo in vehiculos)
                {
                    dgvVehiculos.Rows.Add(
                        vehiculo.Placa,
                        vehiculo.Marca,
                        vehiculo.Modelo,
                        vehiculo.Color ?? "",
                        vehiculo.Anio?.ToString() ?? "",
                        vehiculo.Combustible ?? "",
                        vehiculo.FechaRegistro.ToString("dd/MM/yyyy")
                    );
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar vehículos: " + ex.Message);
            }
        }

        private void HabilitarControlesCliente()
        {
            txtDNI.Enabled = true;
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtEmail.Enabled = true;
            txtTelefono.Enabled = true;
            txtDireccion.Enabled = true;
            btnRegistrarCliente.Enabled = true;
            btnLimpiarCliente.Enabled = true;
        }

        private void DeshabilitarControlesCliente()
        {
            txtDNI.Enabled = false;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtEmail.Enabled = false;
            txtTelefono.Enabled = false;
            txtDireccion.Enabled = false;
            btnRegistrarCliente.Enabled = false;
            btnLimpiarCliente.Enabled = false;
        }

        private void HabilitarControlesVehiculo()
        {
            cbMarca.Enabled = true;
            cbModelo.Enabled = true;
            txtPlaca.Enabled = true;
            txtVIN.Enabled = true;
            txtColor.Enabled = true;
            txtAnio.Enabled = true;
            txtKilometraje.Enabled = true;
            cbCombustible.Enabled = true;
            cbTransmision.Enabled = true;
            btnRegistrarVehiculo.Enabled = true;
        }

        private void DeshabilitarControlesVehiculo()
        {
            cbMarca.Enabled = false;
            cbModelo.Enabled = false;
            txtPlaca.Enabled = false;
            txtVIN.Enabled = false;
            txtColor.Enabled = false;
            txtAnio.Enabled = false;
            txtKilometraje.Enabled = false;
            cbCombustible.Enabled = false;
            cbTransmision.Enabled = false;
            btnRegistrarVehiculo.Enabled = false;
        }

        // ========== MÉTODOS DE MENSAJES ==========

        private void MostrarExito(string mensaje)
        {
            MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MostrarAdvertencia(string mensaje)
        {
            MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // ========== VALIDACIONES DE ENTRADA ==========

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNumerico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            if (!ValidarDatosCliente())
                return;

            try
            {
                Cliente nuevoCliente = new Cliente
                {
                    DNI = txtDNI.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                    Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                    Direccion = string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim()
                };

                string resultado = cnCliente.RegistrarCliente(nuevoCliente);

                if (resultado.Contains("éxito"))
                {
                    MostrarExito(resultado);
                    // Buscar el cliente recién registrado para obtener su ID
                    clienteActual = cnCliente.BuscarClientePorDNI(nuevoCliente.DNI);
                    if (clienteActual != null)
                    {
                        HabilitarControlesVehiculo();
                        lblInfoCliente.Text = $"Cliente registrado: {clienteActual.NombreCompleto}";
                        lblInfoCliente.ForeColor = colorGris;
                        CargarVehiculosCliente(clienteActual.ClienteID);
                    }
                }
                else
                {
                    MostrarError(resultado);
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al registrar cliente: " + ex.Message);
            }
        }
    }
}