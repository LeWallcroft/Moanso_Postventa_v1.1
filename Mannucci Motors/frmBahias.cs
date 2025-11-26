using CapaLogicaNegocio;
using CapaDominio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmBahias : Form
    {
        private CN_Bahia cnBahia = new CN_Bahia();
        private CN_Usuario cnUsuario = new CN_Usuario();
        private int bahiaIdSeleccionada = 0;
        private bool modoEdicion = false;
        private List<Bahia> todasLasBahias = new List<Bahia>();

        public frmBahias()
        {
            InitializeComponent();
        }

        private void frmBahias_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCombos();
                CargarBahias();
                LimpiarFormulario();
                ConfigurarDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el formulario: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCombos()
        {
            try
            {
                // Cargar estados de bahía
                cmbEstado.Items.Clear();
                cmbFiltroEstado.Items.Clear();

                var estados = new List<object>
                {
                    new { Text = "DISPONIBLE", Value = 1 },
                    new { Text = "OCUPADA", Value = 2 },
                    new { Text = "MANTENIMIENTO", Value = 3 },
                    new { Text = "INACTIVA", Value = 4 }
                };

                foreach (var estado in estados)
                {
                    cmbEstado.Items.Add(estado);
                    cmbFiltroEstado.Items.Add(estado);
                }

                cmbEstado.DisplayMember = "Text";
                cmbEstado.ValueMember = "Value";
                cmbFiltroEstado.DisplayMember = "Text";
                cmbFiltroEstado.ValueMember = "Value";

                // Cargar usuarios
                cmbUsuario.Items.Clear();
                var usuarios = cnUsuario.ListarUsuarios();
                cmbUsuario.Items.Add(new { Text = "SIN ASIGNAR", Value = (int?)null });

                foreach (var usuario in usuarios.Where(u => u.Activo))
                {
                    cmbUsuario.Items.Add(new
                    {
                        Text = $"{usuario.Nombre} {usuario.Apellido} ({usuario.Rol})",
                        Value = usuario.UsuariosID
                    });
                }

                cmbUsuario.DisplayMember = "Text";
                cmbUsuario.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar listas: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarBahias()
        {
            try
            {
                todasLasBahias = cnBahia.ListarTodasLasBahias();
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar bahías: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltros()
        {
            try
            {
                var bahiasFiltradas = todasLasBahias.AsEnumerable();

                // Filtrar por estado
                if (cmbFiltroEstado.SelectedItem != null)
                {
                    dynamic estadoSeleccionado = cmbFiltroEstado.SelectedItem;
                    int estadoId = estadoSeleccionado.Value;
                    bahiasFiltradas = bahiasFiltradas.Where(b => b.EstadobahiaId == estadoId);
                }

                // Filtrar por búsqueda de texto
                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    string busqueda = txtBuscar.Text.ToLower();
                    bahiasFiltradas = bahiasFiltradas.Where(b =>
                        b.Nombre.ToLower().Contains(busqueda) ||
                        b.Descripcion.ToLower().Contains(busqueda));
                }

                MostrarBahiasEnGrid(bahiasFiltradas.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar filtros: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarBahiasEnGrid(List<Bahia> bahias)
        {
            dgvBahias.Rows.Clear();

            foreach (var bahia in bahias)
            {
                string nombreUsuario = "SIN ASIGNAR";
                if (bahia.UsuariosId.HasValue)
                {
                    var usuario = cnUsuario.ListarUsuarios()
                        .FirstOrDefault(u => u.UsuariosID == bahia.UsuariosId);
                    if (usuario != null)
                        nombreUsuario = $"{usuario.Nombre} {usuario.Apellido}";
                }

                int rowIndex = dgvBahias.Rows.Add(
                    bahia.BahiaId,
                    bahia.Nombre,
                    bahia.Descripcion,
                    bahia.Capacidad,
                    bahia.EstadoBahia?.Nombre ?? "SIN ESTADO",
                    nombreUsuario,
                    bahia.Activo ? "SÍ" : "NO"
                );

                // Aplicar colores según el estado - SOLUCIÓN MEJORADA
                AplicarColorFila(dgvBahias.Rows[rowIndex], bahia);
            }

            // Prevenir distorsión de colores al seleccionar
            dgvBahias.ClearSelection();
        }

        private void AplicarColorFila(DataGridViewRow fila, Bahia bahia)
        {
            // Configurar estilo de selección para no distorsionar colores
            fila.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            fila.DefaultCellStyle.SelectionForeColor = Color.Black;

            if (!bahia.Activo)
            {
                fila.DefaultCellStyle.BackColor = Color.LightGray;
                fila.DefaultCellStyle.ForeColor = Color.DarkGray;
            }
            else
            {
                string colorHex = ObtenerColorPorEstado(bahia.EstadobahiaId);
                try
                {
                    Color color = ColorTranslator.FromHtml(colorHex);
                    // Usar color más suave para el fondo
                    fila.DefaultCellStyle.BackColor = ControlPaint.LightLight(color);
                    fila.DefaultCellStyle.ForeColor = Color.Black;
                }
                catch
                {
                    fila.DefaultCellStyle.BackColor = Color.White;
                    fila.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private string ObtenerColorPorEstado(int estadoId)
        {
            switch (estadoId)
            {
                case 1: return "#28a745"; // Disponible - Verde
                case 2: return "#dc3545"; // Ocupada - Rojo
                case 3: return "#ffc107"; // Mantenimiento - Amarillo
                case 4: return "#6c757d"; // Inactiva - Gris
                default: return "#ffffff"; // Default - Blanco
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvBahias.AutoGenerateColumns = false;
            dgvBahias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBahias.ReadOnly = true;
            dgvBahias.AllowUserToAddRows = false;

            // Configurar para mejor visualización
            dgvBahias.EnableHeadersVisualStyles = false;
            dgvBahias.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvBahias.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBahias.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            cmbEstado.SelectedIndex = -1;
            cmbUsuario.SelectedIndex = 0; // SIN ASIGNAR
            numCapacidad.Value = 1;

            bahiaIdSeleccionada = 0;
            modoEdicion = false;

            btnGuardar.Text = "GUARDAR";
            btnNuevo.Enabled = true;
            groupBoxDatos.Text = "NUEVA BAHÍA";
        }

        private void CargarDatosEnFormulario(Bahia bahia)
        {
            if (bahia == null) return;

            txtNombre.Text = bahia.Nombre;
            txtDescripcion.Text = bahia.Descripcion;
            numCapacidad.Value = bahia.Capacidad;

            // Seleccionar estado
            foreach (dynamic item in cmbEstado.Items)
            {
                if (item.Value == bahia.EstadobahiaId)
                {
                    cmbEstado.SelectedItem = item;
                    break;
                }
            }

            // Seleccionar usuario
            bool usuarioEncontrado = false;
            foreach (dynamic item in cmbUsuario.Items)
            {
                if (item.Value == bahia.UsuariosId)
                {
                    cmbUsuario.SelectedItem = item;
                    usuarioEncontrado = true;
                    break;
                }
            }

            if (!usuarioEncontrado)
                cmbUsuario.SelectedIndex = 0;

            bahiaIdSeleccionada = bahia.BahiaId;
            modoEdicion = true;
            btnGuardar.Text = "ACTUALIZAR";
            groupBoxDatos.Text = "EDITANDO BAHÍA";
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre de la bahía es requerido", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un estado", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEstado.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("La descripción es requerida", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return false;
            }

            return true;
        }

        // ========== EVENTOS DE BOTONES ==========
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            try
            {
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                int capacidad = (int)numCapacidad.Value;
                int estadoId = ((dynamic)cmbEstado.SelectedItem).Value;
                int? usuarioId = ((dynamic)cmbUsuario.SelectedItem).Value;

                string mensaje;
                bool resultado;

                if (modoEdicion)
                {
                    // Actualizar bahía existente
                    resultado = cnBahia.ActualizarBahiaCompleta(
                        bahiaIdSeleccionada, nombre, descripcion,
                        capacidad, estadoId, usuarioId, out mensaje);
                }
                else
                {
                    // Crear nueva bahía
                    resultado = cnBahia.CrearBahiaCompleta(
                        nombre, descripcion, capacidad,
                        estadoId, usuarioId, out mensaje);
                }

                if (resultado)
                {
                    MessageBox.Show(mensaje, "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarBahias();
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar bahía: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ========== EVENTOS DE FILTROS ==========
        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            cmbFiltroEstado.SelectedIndex = -1;
            txtBuscar.Text = string.Empty;
            AplicarFiltros();
        }

        // ========== EVENTO DE SELECCIÓN EN GRID ==========
        private void dgvBahias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvBahias.Rows.Count)
            {
                DataGridViewRow row = dgvBahias.Rows[e.RowIndex];

                if (row.Cells["colId"].Value != null)
                {
                    int bahiaId = Convert.ToInt32(row.Cells["colId"].Value);
                    var bahia = todasLasBahias.FirstOrDefault(b => b.BahiaId == bahiaId);

                    if (bahia != null)
                    {
                        CargarDatosEnFormulario(bahia);
                    }
                }
            }
        }

        // ========== EVENTOS DE TECLADO ==========
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtDescripcion.Focus();
            }
        }
    }
}