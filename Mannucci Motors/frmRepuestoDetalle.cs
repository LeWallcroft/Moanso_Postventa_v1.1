using CapaDominio;
using CapaDominio.Utilidades;
using CapaLogicaNegocio;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mannucci_Motors
{
    public partial class frmRepuestoDetalle : Form
    {
        private CN_Repuesto _logicaRepuesto;
        private Repuesto _repuesto;
        private bool _esNuevo;
        private int _repuestoId;

        // Eventos para notificar cambios
        public event EventHandler RepuestoCreado;
        public event EventHandler RepuestoActualizado;

        // Constructor para nuevo repuesto
        public frmRepuestoDetalle()
        {
            InitializeComponent();
            _logicaRepuesto = new CN_Repuesto();
            _esNuevo = true;
            _repuestoId = 0;
            lblTitulo.Text = "NUEVO REPUESTO";
        }

        // Constructor para editar repuesto existente
        public frmRepuestoDetalle(int repuestoId)
        {
            InitializeComponent();
            _logicaRepuesto = new CN_Repuesto();
            _esNuevo = false;
            _repuestoId = repuestoId;
            lblTitulo.Text = "EDITAR REPUESTO";
        }

        private async void frmRepuestoDetalle_Load(object sender, EventArgs e)
        {
            if (!_esNuevo)
            {
                await CargarRepuestoAsync();
            }
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            // Configurar valores por defecto para nuevo repuesto
            if (_esNuevo)
            {
                numStock.Value = 0;
                numStockMinimo.Value = 5;
                numPrecio.Value = 0;
                chkActivo.Checked = true;
                chkActivo.Enabled = false; // No se puede desactivar un repuesto nuevo
            }

            // Establecer el foco en el campo nombre
            txtNombre.Focus();
        }

        private async Task CargarRepuestoAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _repuesto = await _logicaRepuesto.ObtenerRepuestoPorIdAsync(_repuestoId);

                if (_repuesto == null)
                {
                    MessageBox.Show("Repuesto no encontrado", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                CargarDatosEnFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar repuesto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void CargarDatosEnFormulario()
        {
            if (_repuesto != null)
            {
                txtNombre.Text = _repuesto.Nombre;
                txtDescripcion.Text = _repuesto.Descripcion ?? string.Empty;
                numPrecio.Value = _repuesto.Precio;
                numStock.Value = _repuesto.Stock;
                numStockMinimo.Value = _repuesto.StockMinimo;
                txtCodigo.Text = _repuesto.Codigo ?? string.Empty;
                txtProveedor.Text = _repuesto.Proveedor ?? string.Empty;
                chkActivo.Checked = _repuesto.Activo;
            }
        }

        private void CargarDatosDesdeFormulario()
        {
            if (_esNuevo)
            {
                _repuesto = new Repuesto();
            }

            _repuesto.Nombre = txtNombre.Text.Trim();
            _repuesto.Descripcion = txtDescripcion.Text.Trim();
            _repuesto.Precio = numPrecio.Value;
            _repuesto.Stock = (int)numStock.Value;
            _repuesto.StockMinimo = (int)numStockMinimo.Value;
            _repuesto.Codigo = txtCodigo.Text.Trim();
            _repuesto.Proveedor = txtProveedor.Text.Trim();
            _repuesto.Activo = chkActivo.Checked;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;

            await GuardarRepuestoAsync();
        }

        private bool ValidarFormulario()
        {
            // Validar nombre
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarError("El nombre del repuesto es obligatorio");
                txtNombre.Focus();
                return false;
            }

            if (txtNombre.Text.Length > 200)
            {
                MostrarError("El nombre no puede tener más de 200 caracteres");
                txtNombre.Focus();
                return false;
            }

            // Validar precio
            if (numPrecio.Value <= 0)
            {
                MostrarError("El precio debe ser mayor a cero");
                numPrecio.Focus();
                return false;
            }

            // Validar stock
            if (numStock.Value < 0)
            {
                MostrarError("El stock no puede ser negativo");
                numStock.Focus();
                return false;
            }

            // Validar stock mínimo
            if (numStockMinimo.Value < 0)
            {
                MostrarError("El stock mínimo no puede ser negativo");
                numStockMinimo.Focus();
                return false;
            }

            // Validar descripción
            if (txtDescripcion.Text.Length > 500)
            {
                MostrarError("La descripción no puede tener más de 500 caracteres");
                txtDescripcion.Focus();
                return false;
            }

            // Validar código
            if (txtCodigo.Text.Length > 100)
            {
                MostrarError("El código no puede tener más de 100 caracteres");
                txtCodigo.Focus();
                return false;
            }

            // Validar proveedor
            if (txtProveedor.Text.Length > 200)
            {
                MostrarError("El proveedor no puede tener más de 200 caracteres");
                txtProveedor.Focus();
                return false;
            }

            return true;
        }

        private async Task GuardarRepuestoAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                btnGuardar.Enabled = false;

                CargarDatosDesdeFormulario();

                ResultadoOperacion resultado;

                if (_esNuevo)
                {
                    resultado = await _logicaRepuesto.CrearRepuestoAsync(_repuesto);
                }
                else
                {
                    resultado = await _logicaRepuesto.ActualizarRepuestoAsync(_repuesto);
                }

                if (resultado.Exitoso)
                {
                    MostrarExito(resultado.Mensaje);

                    // Disparar evento correspondiente
                    if (_esNuevo)
                    {
                        RepuestoCreado?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        RepuestoActualizado?.Invoke(this, EventArgs.Empty);
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MostrarError(resultado.Mensaje);
                    btnGuardar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error al guardar repuesto: {ex.Message}");
                btnGuardar.Enabled = true;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MostrarConfirmacion("¿Está seguro de cancelar? Los cambios no guardados se perderán."))
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        #region Métodos de ayuda para mensajes
        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MostrarExito(string mensaje)
        {
            MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool MostrarConfirmacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
        #endregion

        #region Validación en tiempo real
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtNombre.Text.Length > 200)
            {
                MostrarAdvertencia("Máximo 200 caracteres");
            }
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Length > 500)
            {
                MostrarAdvertencia("Máximo 500 caracteres");
            }
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length > 100)
            {
                MostrarAdvertencia("Máximo 100 caracteres");
            }
        }

        private void txtProveedor_TextChanged(object sender, EventArgs e)
        {
            if (txtProveedor.Text.Length > 200)
            {
                MostrarAdvertencia("Máximo 200 caracteres");
            }
        }

        private void MostrarAdvertencia(string mensaje)
        {
            // Podrías mostrar esto en un label de estado en lugar de MessageBox
            // para no interrumpir al usuario
            Console.WriteLine(mensaje); // Por ahora solo log
        }
        #endregion

        #region Navegación con teclado
        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtDescripcion.Focus();
            }
        }

        private void txtDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                numPrecio.Focus();
            }
        }

        private void numPrecio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                numStock.Focus();
            }
        }
        #endregion
    }
}