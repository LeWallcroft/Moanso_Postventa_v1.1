using CapaDominio;
using CapaDominio.Utilidades;
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
    public partial class frmRepuestos : Form
    {
        private CN_Repuesto _logicaRepuesto;
        private List<Repuesto> _repuestos;

        // Colores de la paleta
        private Color _colorRojo = Color.FromArgb(192, 0, 0);
        private Color _colorGrisOscuro = Color.FromArgb(64, 64, 64);
        private Color _colorGrisClaro = Color.FromArgb(240, 240, 240);
        private Color _colorBlanco = Color.White;

        public frmRepuestos()
        {
            InitializeComponent();
            _logicaRepuesto = new CN_Repuesto();
            _repuestos = new List<Repuesto>();

            // Configurar para MDI
            this.WindowState = FormWindowState.Maximized;
            ConfigurarDataGridView();
        }

        private void frmRepuestos_Load(object sender, EventArgs e)
        {
            CargarRepuestos();
        }

        private void ConfigurarDataGridView()
        {
            // Limpiar columnas existentes
            dgvRepuestos.Columns.Clear();

            // Configurar columnas
            dgvRepuestos.Columns.Add("colId", "ID");
            dgvRepuestos.Columns.Add("colNombre", "Nombre");
            dgvRepuestos.Columns.Add("colDescripcion", "Descripción");
            dgvRepuestos.Columns.Add("colPrecio", "Precio");
            dgvRepuestos.Columns.Add("colStock", "Stock");
            dgvRepuestos.Columns.Add("colStockMinimo", "Stock Mínimo");
            dgvRepuestos.Columns.Add("colCodigo", "Código");
            dgvRepuestos.Columns.Add("colProveedor", "Proveedor");
            dgvRepuestos.Columns.Add("colEstado", "Estado");

            // Configurar ancho de columnas
            dgvRepuestos.Columns["colId"].Width = 50;
            dgvRepuestos.Columns["colNombre"].Width = 150;
            dgvRepuestos.Columns["colDescripcion"].Width = 200;
            dgvRepuestos.Columns["colPrecio"].Width = 80;
            dgvRepuestos.Columns["colStock"].Width = 60;
            dgvRepuestos.Columns["colStockMinimo"].Width = 80;
            dgvRepuestos.Columns["colCodigo"].Width = 100;
            dgvRepuestos.Columns["colProveedor"].Width = 120;
            dgvRepuestos.Columns["colEstado"].Width = 80;

            // Formato de columnas
            dgvRepuestos.Columns["colPrecio"].DefaultCellStyle.Format = "C2";
            dgvRepuestos.Columns["colPrecio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRepuestos.Columns["colStock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRepuestos.Columns["colStockMinimo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private async void CargarRepuestos()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _repuestos = await _logicaRepuesto.ObtenerRepuestosActivosAsync();
                ActualizarGrid();
                ActualizarEstadisticas();
            }
            catch (Exception ex)
            {
                MostrarError($"Error al cargar repuestos: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ActualizarGrid()
        {
            dgvRepuestos.Rows.Clear();

            foreach (var repuesto in _repuestos)
            {
                int rowIndex = dgvRepuestos.Rows.Add();
                DataGridViewRow row = dgvRepuestos.Rows[rowIndex];

                row.Cells["colId"].Value = repuesto.RepuestoID;
                row.Cells["colNombre"].Value = repuesto.Nombre;
                row.Cells["colDescripcion"].Value = repuesto.Descripcion ?? string.Empty;
                row.Cells["colPrecio"].Value = repuesto.Precio;
                row.Cells["colStock"].Value = repuesto.Stock;
                row.Cells["colStockMinimo"].Value = repuesto.StockMinimo;
                row.Cells["colCodigo"].Value = repuesto.Codigo ?? string.Empty;
                row.Cells["colProveedor"].Value = repuesto.Proveedor ?? string.Empty;
                row.Cells["colEstado"].Value = repuesto.Activo ? "Activo" : "Inactivo";

                // Resaltar stock bajo
                if (repuesto.StockBajo && repuesto.Activo)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                    row.DefaultCellStyle.ForeColor = _colorRojo;
                    row.DefaultCellStyle.Font = new Font(dgvRepuestos.Font, FontStyle.Bold);
                }
                else if (!repuesto.Activo)
                {
                    row.DefaultCellStyle.BackColor = _colorGrisClaro;
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
        }

        private async void ActualizarEstadisticas()
        {
            try
            {
                var estadisticas = await _logicaRepuesto.ObtenerEstadisticasAsync();
                if (estadisticas != null)
                {
                    var stats = estadisticas as dynamic;
                    lblTotalRepuestos.Text = $"Total Repuestos: {stats.TotalRepuestosActivos}";
                    lblStockBajo.Text = $"Con Stock Bajo: {stats.TotalRepuestosBajoStock}";
                }
                else
                {
                    lblTotalRepuestos.Text = $"Total Repuestos: {_repuestos.Count}";
                    var stockBajo = _repuestos.Count(r => r.StockBajo && r.Activo);
                    lblStockBajo.Text = $"Con Stock Bajo: {stockBajo}";
                }
            }
            catch (Exception ex)
            {
                lblTotalRepuestos.Text = $"Total Repuestos: {_repuestos.Count}";
                var stockBajo = _repuestos.Count(r => r.StockBajo && r.Activo);
                lblStockBajo.Text = $"Con Stock Bajo: {stockBajo}";
                // Se utiliza 'ex' para evitar CS0168
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await BuscarRepuestos();
        }

        private async Task BuscarRepuestos()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string criterio = txtBuscar.Text.Trim();
                _repuestos = await _logicaRepuesto.BuscarRepuestosAsync(criterio);
                ActualizarGrid();
                ActualizarEstadisticas();
            }
            catch (Exception ex)
            {
                MostrarError($"Error al buscar repuestos: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frmDetalle = new frmRepuestoDetalle();
            frmDetalle.MdiParent = this.MdiParent;
            frmDetalle.RepuestoCreado += (s, args) => CargarRepuestos();
            frmDetalle.Show();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvRepuestos.CurrentRow == null)
            {
                MostrarAdvertencia("Seleccione un repuesto para editar");
                return;
            }

            int repuestoId = Convert.ToInt32(dgvRepuestos.CurrentRow.Cells["colId"].Value);
            var frmDetalle = new frmRepuestoDetalle(repuestoId);
            frmDetalle.MdiParent = this.MdiParent;
            frmDetalle.RepuestoActualizado += (s, args) => CargarRepuestos();
            frmDetalle.Show();
        }

        private async void btnInactivar_Click(object sender, EventArgs e)
        {
            if (dgvRepuestos.CurrentRow == null)
            {
                MostrarAdvertencia("Seleccione un repuesto para inactivar");
                return;
            }

            int repuestoId = Convert.ToInt32(dgvRepuestos.CurrentRow.Cells["colId"].Value);
            string nombre = dgvRepuestos.CurrentRow.Cells["colNombre"].Value.ToString();

            if (MostrarConfirmacion($"¿Está seguro de inactivar el repuesto '{nombre}'?"))
            {
                await InactivarRepuesto(repuestoId);
            }
        }

        private async Task InactivarRepuesto(int repuestoId)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var resultado = await _logicaRepuesto.InactivarRepuestoAsync(repuestoId);

                if (resultado.Exitoso)
                {
                    MostrarExito(resultado.Mensaje);
                    CargarRepuestos();
                }
                else
                {
                    MostrarError(resultado.Mensaje);
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error al inactivar repuesto: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async void btnStockBajo_Click(object sender, EventArgs e)
        {
            await CargarStockBajo();
        }

        private async Task CargarStockBajo()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _repuestos = await _logicaRepuesto.ObtenerRepuestosStockBajoAsync();
                ActualizarGrid();
                ActualizarEstadisticas();

                if (_repuestos.Count == 0)
                {
                    MostrarInformacion("No hay repuestos con stock bajo");
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error al cargar repuestos con stock bajo: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar_Click(sender, e);
                e.Handled = true;
            }
        }

        private void dgvRepuestos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEditar_Click(sender, e);
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

        private void MostrarAdvertencia(string mensaje)
        {
            MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MostrarInformacion(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool MostrarConfirmacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
        #endregion
    }
}