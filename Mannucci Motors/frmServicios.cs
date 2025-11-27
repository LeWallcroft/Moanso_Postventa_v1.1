using CapaDominio;
using CapaDominio.Utilidades;
using CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmServicios : Form
    {
        private CN_Servicios logicaServicios;
        private List<Servicio> listaServicios;
        private List<CategoriaServicio> listaCategorias;
        private List<TipoServicio> listaTiposServicio;
        private List<Repuesto> listaRepuestos;
        private Servicio servicioActual;
        private bool esNuevo;
        private bool mostrarInactivos = false;

        public frmServicios()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            logicaServicios = new CN_Servicios();
            servicioActual = new Servicio();
            esNuevo = true;

            CargarComboboxes();
            CargarServicios();
            ConfigurarDataGridViews();
            LimpiarFormulario();
        }

        private void CargarComboboxes()
        {
            try
            {
                // Cargar categorías
                listaCategorias = logicaServicios.ListarCategorias();
                cbCategoria.DataSource = listaCategorias;
                cbCategoria.DisplayMember = "Nombre";
                cbCategoria.ValueMember = "CategoriaservicioID";

                // Cargar tipos de servicio
                listaTiposServicio = logicaServicios.ListarTiposServicio();
                cbTipoServicio.DataSource = listaTiposServicio;
                cbTipoServicio.DisplayMember = "Nombre";
                cbTipoServicio.ValueMember = "TiposervicioID";

                // Cargar repuestos
                listaRepuestos = logicaServicios.ListarRepuestos();
            }
            catch (Exception ex)
            {
                MostrarError($"Error al cargar datos: {ex.Message}");
            }
        }

        private void ConfigurarDataGridViews()
        {
            // Configurar DataGridView de servicios
            dgvServicios.AutoGenerateColumns = false;
            dgvServicios.Columns.Clear();

            // Columnas para servicios
            dgvServicios.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "ServicioID",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            });

            dgvServicios.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                Width = 200
            });

            dgvServicios.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "CategoriaNombre",
                HeaderText = "Categoría",
                Width = 150
            });

            dgvServicios.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "TipoNombre",
                HeaderText = "Tipo",
                Width = 150
            });

            dgvServicios.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Precio",
                HeaderText = "Precio",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "C2" }
            });

            dgvServicios.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "DuracionEstimada",
                HeaderText = "Duración (min)",
                Width = 100
            });

            dgvServicios.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                DataPropertyName = "Activo",
                HeaderText = "Activo",
                Width = 60
            });

            // Configurar DataGridView de repuestos del servicio
            dgvRepuestosServicio.AutoGenerateColumns = false;
            dgvRepuestosServicio.Columns.Clear();

            dgvRepuestosServicio.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "RepuestoID",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            });

            dgvRepuestosServicio.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "RepuestoNombre",
                HeaderText = "Repuesto",
                Width = 200
            });

            dgvRepuestosServicio.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Cantidad",
                HeaderText = "Cantidad",
                Width = 80
            });

            dgvRepuestosServicio.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "PrecioUnitario",
                HeaderText = "Precio Unit.",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "C2" }
            });

            dgvRepuestosServicio.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "StockActual",
                HeaderText = "Stock",
                Width = 80
            });

            dgvRepuestosServicio.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Observaciones",
                HeaderText = "Observaciones",
                Width = 200
            });
        }

        private void CargarServicios()
        {
            try
            {
                if (mostrarInactivos)
                {
                    listaServicios = logicaServicios.ListarServiciosInactivos();
                    btnRefresh.Text = "Mostrar Activos";
                }
                else
                {
                    listaServicios = logicaServicios.ListarServicios();
                    btnRefresh.Text = "Mostrar Inactivos";
                }

                dgvServicios.DataSource = listaServicios;
                ActualizarContadores();
                ActualizarEstadoBotones();

            }
            catch (Exception ex)
            {
                MostrarError($"Error al cargar servicios: {ex.Message}");
            }
        }

        private void ActualizarContadores()
        {
            int total = listaServicios?.Count ?? 0;
            int activos = listaServicios?.Count(s => s.Activo) ?? 0;
            int inactivos = total - activos;

            if (mostrarInactivos)
            {
                lblTitulo.Text = $"Servicios Inactivos ({inactivos} servicios)";
            }
            else
            {
                lblTitulo.Text = $"Servicios Activos ({activos} servicios)";
            }
        }

        private void ActualizarEstadoBotones()
        {
            bool haySeleccion = dgvServicios.SelectedRows.Count > 0;
            btnEditar.Enabled = haySeleccion;

            if (haySeleccion && dgvServicios.SelectedRows[0].DataBoundItem is Servicio servicio)
            {
                // En modo activos: mostrar botón desactivar para servicios activos
                // En modo inactivos: mostrar botón activar para servicios inactivos
                btnDesactivar.Enabled = mostrarInactivos ? false : servicio.Activo;
                btnActivar.Enabled = mostrarInactivos ? !servicio.Activo : false;
            }
            else
            {
                btnDesactivar.Enabled = false;
                btnActivar.Enabled = false;
            }
        }

        private void LimpiarFormulario()
        {
            servicioActual = new Servicio();
            esNuevo = true;

            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtDuracion.Text = "";
            chkRequiereRepuestos.Checked = false;
            chkActivo.Checked = true;

            if (cbCategoria.Items.Count > 0)
                cbCategoria.SelectedIndex = 0;
            if (cbTipoServicio.Items.Count > 0)
                cbTipoServicio.SelectedIndex = 0;

            dgvRepuestosServicio.DataSource = null;
            HabilitarControlesDetalle(true);
            ActualizarVisibilidadRepuestos();
        }

        private void CargarDatosServicio()
        {
            if (servicioActual == null) return;

            txtNombre.Text = servicioActual.Nombre;
            txtDescripcion.Text = servicioActual.Descripcion;
            txtPrecio.Text = servicioActual.Precio.ToString("F2");
            txtDuracion.Text = servicioActual.DuracionEstimada.ToString();
            chkRequiereRepuestos.Checked = servicioActual.RequiereRepuestos;
            chkActivo.Checked = servicioActual.Activo;

            // Seleccionar categoría
            if (servicioActual.CategoriaservicioID.HasValue)
            {
                cbCategoria.SelectedValue = servicioActual.CategoriaservicioID.Value;
            }

            // Seleccionar tipo de servicio
            if (servicioActual.TiposervicioID.HasValue)
            {
                cbTipoServicio.SelectedValue = servicioActual.TiposervicioID.Value;
            }

            // Cargar repuestos del servicio
            CargarRepuestosServicio();
            ActualizarVisibilidadRepuestos();
        }

        private void CargarRepuestosServicio()
        {
            try
            {
                if (servicioActual.ServicioID > 0)
                {
                    var repuestos = logicaServicios.ListarRepuestosDeServicio(servicioActual.ServicioID);
                    dgvRepuestosServicio.DataSource = repuestos;
                }
                else
                {
                    dgvRepuestosServicio.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error al cargar repuestos: {ex.Message}");
            }
        }

        private void HabilitarControlesDetalle(bool habilitar)
        {
            txtNombre.Enabled = habilitar;
            txtDescripcion.Enabled = habilitar;
            txtPrecio.Enabled = habilitar;
            txtDuracion.Enabled = habilitar;
            cbCategoria.Enabled = habilitar;
            cbTipoServicio.Enabled = habilitar;
            chkRequiereRepuestos.Enabled = habilitar;
            chkActivo.Enabled = habilitar;

            bool puedeGestionarRepuestos = habilitar && servicioActual.ServicioID > 0 && chkRequiereRepuestos.Checked;
            btnAgregarRepuesto.Enabled = puedeGestionarRepuestos;
            btnQuitarRepuesto.Enabled = puedeGestionarRepuestos && dgvRepuestosServicio.SelectedRows.Count > 0;
        }

        private void ActualizarVisibilidadRepuestos()
        {
            bool mostrarRepuestos = chkRequiereRepuestos.Checked;
            pnlRepuestos.Visible = mostrarRepuestos;

            if (mostrarRepuestos && servicioActual.ServicioID > 0)
            {
                btnAgregarRepuesto.Enabled = true;
                btnQuitarRepuesto.Enabled = dgvRepuestosServicio.SelectedRows.Count > 0;
            }
            else
            {
                btnAgregarRepuesto.Enabled = false;
                btnQuitarRepuesto.Enabled = false;
            }
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarError("El nombre del servicio es obligatorio.");
                txtNombre.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
            {
                MostrarError("El precio debe ser un valor mayor a cero.");
                txtPrecio.Focus();
                return false;
            }

            if (!int.TryParse(txtDuracion.Text, out int duracion) || duracion <= 0)
            {
                MostrarError("La duración debe ser un valor mayor a cero.");
                txtDuracion.Focus();
                return false;
            }

            // Validar nombre único
            if (esNuevo || servicioActual.Nombre != txtNombre.Text)
            {
                if (logicaServicios.ExisteServicioConNombre(txtNombre.Text.Trim(), esNuevo ? 0 : servicioActual.ServicioID))
                {
                    MostrarError("Ya existe un servicio con ese nombre.");
                    txtNombre.Focus();
                    return false;
                }
            }

            return true;
        }

        private void GuardarServicio()
        {
            if (!ValidarFormulario()) return;

            try
            {
                servicioActual.Nombre = txtNombre.Text.Trim();
                servicioActual.Descripcion = txtDescripcion.Text.Trim();
                servicioActual.Precio = decimal.Parse(txtPrecio.Text);
                servicioActual.DuracionEstimada = int.Parse(txtDuracion.Text);
                servicioActual.RequiereRepuestos = chkRequiereRepuestos.Checked;
                servicioActual.Activo = chkActivo.Checked;

                // Asignar IDs de comboboxes
                if (cbCategoria.SelectedValue != null && int.TryParse(cbCategoria.SelectedValue.ToString(), out int catId))
                    servicioActual.CategoriaservicioID = catId;
                else
                    servicioActual.CategoriaservicioID = null;

                if (cbTipoServicio.SelectedValue != null && int.TryParse(cbTipoServicio.SelectedValue.ToString(), out int tipoId))
                    servicioActual.TiposervicioID = tipoId;
                else
                    servicioActual.TiposervicioID = null;

                ResultadoOperacion resultado;

                if (esNuevo)
                {
                    resultado = logicaServicios.InsertarServicio(servicioActual);
                    if (resultado.Exitoso)
                    {
                        MostrarExito("Servicio creado exitosamente.");
                        CargarServicios();
                        tcServicios.SelectedTab = tpListado;
                    }
                    else
                    {
                        MostrarError(resultado.Mensaje);
                    }
                }
                else
                {
                    resultado = logicaServicios.ActualizarServicio(servicioActual);
                    if (resultado.Exitoso)
                    {
                        MostrarExito("Servicio actualizado exitosamente.");
                        CargarServicios();
                        tcServicios.SelectedTab = tpListado;
                    }
                    else
                    {
                        MostrarError(resultado.Mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error al guardar servicio: {ex.Message}");
            }
        }

        #region EVENTOS DE BOTONES

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            tcServicios.SelectedTab = tpDetalle;
            txtNombre.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvServicios.SelectedRows.Count == 0)
            {
                MostrarError("Seleccione un servicio para editar.");
                return;
            }

            var servicio = dgvServicios.SelectedRows[0].DataBoundItem as Servicio;
            if (servicio != null)
            {
                servicioActual = servicio;
                esNuevo = false;
                CargarDatosServicio();
                tcServicios.SelectedTab = tpDetalle;
                txtNombre.Focus();
            }
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            if (dgvServicios.SelectedRows.Count == 0)
            {
                MostrarError("Seleccione un servicio para desactivar.");
                return;
            }

            var servicio = dgvServicios.SelectedRows[0].DataBoundItem as Servicio;
            if (servicio != null && servicio.Activo)
            {
                if (MessageBox.Show(
                    $"¿Está seguro de que desea desactivar el servicio '{servicio.Nombre}'?",
                    "Confirmar Desactivación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var resultado = logicaServicios.DesactivarServicio(servicio.ServicioID);
                        if (resultado.Exitoso)
                        {
                            MostrarExito("Servicio desactivado exitosamente.");
                            CargarServicios();
                        }
                        else
                        {
                            MostrarError(resultado.Mensaje);
                        }
                    }
                    catch (Exception ex)
                    {
                        MostrarError($"Error al desactivar servicio: {ex.Message}");
                    }
                }
            }
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            if (dgvServicios.SelectedRows.Count == 0)
            {
                MostrarError("Seleccione un servicio para activar.");
                return;
            }

            var servicio = dgvServicios.SelectedRows[0].DataBoundItem as Servicio;
            if (servicio != null && !servicio.Activo)
            {
                try
                {
                    var resultado = logicaServicios.ActivarServicio(servicio.ServicioID);
                    if (resultado.Exitoso)
                    {
                        MostrarExito("Servicio activado exitosamente.");
                        CargarServicios();
                    }
                    else
                    {
                        MostrarError(resultado.Mensaje);
                    }
                }
                catch (Exception ex)
                {
                    MostrarError($"Error al activar servicio: {ex.Message}");
                }
            }
        }

        // BOTÓN PARA CAMBIAR ENTRE ACTIVOS E INACTIVOS - CORREGIDO
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            mostrarInactivos = !mostrarInactivos;
            CargarServicios();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarServicios();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BuscarServicios();
                e.Handled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarServicio();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "¿Está seguro de que desea cancelar? Los cambios no guardados se perderán.",
                "Confirmar Cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                tcServicios.SelectedTab = tpListado;
                LimpiarFormulario();
            }
        }

        private void btnAgregarRepuesto_Click(object sender, EventArgs e)
        {
            if (servicioActual.ServicioID <= 0)
            {
                MostrarError("Debe guardar el servicio primero antes de agregar repuestos.");
                return;
            }

            using (var frm = new frmSeleccionarRepuesto(listaRepuestos))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var repuestoSeleccionado = frm.RepuestoSeleccionado;
                    var cantidad = frm.Cantidad;

                    if (repuestoSeleccionado != null && cantidad > 0)
                    {
                        try
                        {
                            var resultado = logicaServicios.AgregarRepuestoAServicio(
                                servicioActual.ServicioID,
                                repuestoSeleccionado.RepuestoID,
                                cantidad);

                            if (resultado.Exitoso)
                            {
                                MostrarExito("Repuesto agregado al servicio.");
                                CargarRepuestosServicio();
                            }
                            else
                            {
                                MostrarError(resultado.Mensaje);
                            }
                        }
                        catch (Exception ex)
                        {
                            MostrarError($"Error al agregar repuesto: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void btnQuitarRepuesto_Click(object sender, EventArgs e)
        {
            if (dgvRepuestosServicio.SelectedRows.Count == 0)
            {
                MostrarError("Seleccione un repuesto para quitar.");
                return;
            }

            var repuestoServicio = dgvRepuestosServicio.SelectedRows[0].DataBoundItem as RepuestoServicio;
            if (repuestoServicio != null)
            {
                if (MessageBox.Show(
                    $"¿Está seguro de que desea quitar el repuesto '{repuestoServicio.RepuestoNombre}'?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var resultado = logicaServicios.QuitarRepuestoDeServicio(
                            servicioActual.ServicioID,
                            repuestoServicio.RepuestoID);

                        if (resultado.Exitoso)
                        {
                            MostrarExito("Repuesto quitado del servicio.");
                            CargarRepuestosServicio();
                        }
                        else
                        {
                            MostrarError(resultado.Mensaje);
                        }
                    }
                    catch (Exception ex)
                    {
                        MostrarError($"Error al quitar repuesto: {ex.Message}");
                    }
                }
            }
        }

        #endregion

        #region MÉTODOS AUXILIARES

        private void BuscarServicios()
        {
            try
            {
                var serviciosFiltrados = logicaServicios.BuscarServicios(txtBuscar.Text.Trim(), mostrarInactivos);
                dgvServicios.DataSource = serviciosFiltrados;

                if (serviciosFiltrados.Count == 0)
                {
                    MostrarInfo("No se encontraron servicios con ese criterio.");
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error al buscar servicios: {ex.Message}");
            }
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MostrarExito(string mensaje)
        {
            MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MostrarInfo(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region EVENTOS DEL FORMULARIO

        private void frmServicios_Load(object sender, EventArgs e)
        {
            tcServicios.SelectedTab = tpListado;
        }

        private void tcServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcServicios.SelectedTab == tpListado)
            {
                CargarServicios();
            }
            else if (tcServicios.SelectedTab == tpDetalle)
            {
                HabilitarControlesDetalle(true);
            }
        }

        private void dgvServicios_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
        }

        private void dgvRepuestosServicio_SelectionChanged(object sender, EventArgs e)
        {
            btnQuitarRepuesto.Enabled = dgvRepuestosServicio.SelectedRows.Count > 0 &&
                                      servicioActual.ServicioID > 0 &&
                                      chkRequiereRepuestos.Checked;
        }

        private void chkRequiereRepuestos_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarVisibilidadRepuestos();
        }

        // Validación de entrada numérica
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Solo permitir un punto decimal
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtDuracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion
    }

    #region FORMULARIO AUXILIAR PARA SELECCIONAR REPUESTO

    public class frmSeleccionarRepuesto : Form
    {
        private ComboBox cbRepuestos;
        private NumericUpDown nudCantidad;
        private Button btnAceptar;
        private Button btnCancelar;
        private Label lblRepuesto;
        private Label lblCantidad;

        public Repuesto RepuestoSeleccionado { get; private set; }
        public int Cantidad { get; private set; }
        private List<Repuesto> repuestos;

        public frmSeleccionarRepuesto(List<Repuesto> repuestosDisponibles)
        {
            repuestos = repuestosDisponibles;
            InitializeComponent();
            CargarRepuestos();
        }

        private void InitializeComponent()
        {
            this.Text = "Seleccionar Repuesto";
            this.Size = new Size(400, 200);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Controles
            lblRepuesto = new Label() { Text = "Repuesto:", Location = new Point(20, 20), AutoSize = true };
            cbRepuestos = new ComboBox() { Location = new Point(100, 17), Size = new Size(250, 23), DropDownStyle = ComboBoxStyle.DropDownList };

            lblCantidad = new Label() { Text = "Cantidad:", Location = new Point(20, 60), AutoSize = true };
            nudCantidad = new NumericUpDown() { Location = new Point(100, 57), Size = new Size(100, 23), Minimum = 1, Maximum = 1000, Value = 1 };

            btnAceptar = new Button() { Text = "Aceptar", Location = new Point(200, 100), Size = new Size(80, 30), DialogResult = DialogResult.OK };
            btnCancelar = new Button() { Text = "Cancelar", Location = new Point(290, 100), Size = new Size(80, 30), DialogResult = DialogResult.Cancel };

            // Agregar controles
            this.Controls.AddRange(new Control[] { lblRepuesto, cbRepuestos, lblCantidad, nudCantidad, btnAceptar, btnCancelar });

            // Eventos
            btnAceptar.Click += (s, e) => { if (Validar()) this.DialogResult = DialogResult.OK; };
        }

        private void CargarRepuestos()
        {
            cbRepuestos.DataSource = repuestos.Where(r => r.Activo && r.Stock > 0).ToList();
            cbRepuestos.DisplayMember = "Nombre";
            cbRepuestos.ValueMember = "RepuestoID";
        }

        private bool Validar()
        {
            if (cbRepuestos.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un repuesto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            RepuestoSeleccionado = cbRepuestos.SelectedItem as Repuesto;
            Cantidad = (int)nudCantidad.Value;

            if (RepuestoSeleccionado.Stock < Cantidad)
            {
                MessageBox.Show($"Stock insuficiente. Stock disponible: {RepuestoSeleccionado.Stock}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }

    #endregion
}