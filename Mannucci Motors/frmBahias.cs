using CapaLogicaNegocio;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmBahias : Form
    {
        private CN_Bahia cnBahia = new CN_Bahia();
        private int bahiaIdSeleccionada = 0;

        public frmBahias()
        {
            InitializeComponent();
        }

        private void frmBahias_Load(object sender, EventArgs e)
        {
            CargarBahias();
            LimpiarFormulario();
            CargarTipos();
        }

        private void CargarTipos()
        {
            cmbTipo.Items.Clear();
            cmbTipo.Items.AddRange(new string[] { "General", "Lubricacion", "Express" });
        }

        private void CargarBahias()
        {
            try
            {
                // Obtenemos TODAS las bahías (activas e inhabilitadas)
                var bahias = cnBahia.ListarTodasLasBahias();

                // Limpiamos el DataGridView
                dgvBahias.Rows.Clear();

                // Cargamos los datos manualmente
                foreach (var bahia in bahias)
                {
                    // Cambiar color de fondo si está inhabilitada
                    int rowIndex = dgvBahias.Rows.Add(
                        bahia.BahiaId,
                        bahia.Nombre,
                        bahia.Tipo,
                        bahia.EstadoDisplay
                    );

                    // Si la bahía está inhabilitada, cambiar el color de la fila
                    if (!bahia.Activo)
                    {
                        dgvBahias.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                        dgvBahias.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.DarkGray;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar bahías: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            cmbTipo.SelectedIndex = -1;
            btnGuardar.Text = "Crear";
            btnInhabilitar.Enabled = false;
            btnInhabilitar.Text = "INHABILITAR";
            btnInhabilitar.BackColor = Color.Silver;
            bahiaIdSeleccionada = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;

            string nombre = txtNombre.Text.Trim();
            string tipo = cmbTipo.SelectedItem.ToString();
            string mensaje;

            bool resultado;
            if (bahiaIdSeleccionada > 0)
            {
                resultado = cnBahia.ModificarBahia(bahiaIdSeleccionada, nombre, tipo, out mensaje);
            }
            else
            {
                resultado = cnBahia.CrearBahia(nombre, tipo, out mensaje);
            }

            if (resultado)
            {
                MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarBahias();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnInhabilitar_Click(object sender, EventArgs e)
        {
            if (bahiaIdSeleccionada > 0)
            {
                string nombre = txtNombre.Text.Trim();

                // Determinar la acción basándonos en el texto actual del botón
                bool esHabilitar = btnInhabilitar.Text == "HABILITAR";
                string accion = esHabilitar ? "HABILITAR" : "INHABILITAR";

                var confirmacion = MessageBox.Show(
                    $"¿Está seguro de {accion} la bahía '{nombre}'?",
                    $"Confirmar {accion}",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    string mensaje;
                    bool resultado;

                    if (esHabilitar)
                    {
                        resultado = cnBahia.HabilitarBahia(bahiaIdSeleccionada, out mensaje);
                    }
                    else
                    {
                        resultado = cnBahia.InhabilitarBahia(bahiaIdSeleccionada, out mensaje);
                    }

                    if (resultado)
                    {
                        MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarBahias();
                        LimpiarFormulario();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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

            if (cmbTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un tipo de bahía", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipo.Focus();
                return false;
            }

            return true;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnGuardar.PerformClick();
            }
        }

        private void dgvBahias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvBahias.Rows.Count)
            {
                DataGridViewRow row = dgvBahias.Rows[e.RowIndex];

                if (!row.IsNewRow)
                {
                    if (row.Cells["colId"].Value != null &&
                        row.Cells["colNombre"].Value != null &&
                        row.Cells["colTipo"].Value != null &&
                        row.Cells["colEstado"].Value != null)
                    {
                        bahiaIdSeleccionada = Convert.ToInt32(row.Cells["colId"].Value);
                        txtNombre.Text = row.Cells["colNombre"].Value.ToString();
                        cmbTipo.Text = row.Cells["colTipo"].Value.ToString();

                        // Obtener el estado actual de la bahía
                        string estado = row.Cells["colEstado"].Value.ToString();
                        bool estaActiva = estado == "Activa";

                        // ACTUALIZAR EL TEXTO Y COLOR DEL BOTÓN SEGÚN EL ESTADO
                        if (estaActiva)
                        {
                            btnInhabilitar.Text = "INHABILITAR";
                            btnInhabilitar.BackColor = Color.Silver;
                        }
                        else
                        {
                            btnInhabilitar.Text = "HABILITAR";
                            btnInhabilitar.BackColor = Color.LightGreen;
                        }

                        btnGuardar.Text = "Modificar";
                        btnInhabilitar.Enabled = true;
                    }
                }
            }
        }
    }
}