using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogicaNegocio;
using CapaDominio;

namespace Mannucci_Motors
{
    public partial class frmReportes : Form
    {
        private CN_Reportes logicaReportes;
        private ReporteFiltros filtrosActuales;

        public frmReportes()
        {
            InitializeComponent();
            logicaReportes = new CN_Reportes();
            filtrosActuales = new ReporteFiltros();
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarCombos();
            LimpiarFiltros();
        }

        private void ConfigurarFormulario()
        {
            // Configurar DataGridView
            dgvReporte.AutoGenerateColumns = false;
            dgvReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReporte.ReadOnly = true;
            dgvReporte.AllowUserToAddRows = false;
            dgvReporte.AllowUserToDeleteRows = false;

            // Configurar fechas por defecto
            dtpFechaInicio.Value = DateTime.Today.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Today;
        }

        private void CargarCombos()
        {
            // Cargar tipos de reporte
            cmbTipoReporte.Items.Clear();
            cmbTipoReporte.Items.AddRange(new object[] {
                "CLIENTES",
                "CITAS",
                "ORDENES",
                "DISPONIBILIDAD"
            });

            // Cargar estados según el tipo de reporte
            ActualizarEstados();
        }

        private void ActualizarEstados()
        {
            cmbEstado.Items.Clear();

            if (cmbTipoReporte.SelectedItem == null) return;

            string tipoReporte = cmbTipoReporte.SelectedItem.ToString();

            switch (tipoReporte)
            {
                case "CITAS":
                    cmbEstado.Items.AddRange(new object[] { "PENDIENTE", "CONFIRMADA", "EN PROGRESO", "COMPLETADA", "CANCELADA" });
                    break;
                case "ORDENES":
                    cmbEstado.Items.AddRange(new object[] { "PENDIENTE", "EN PROGRESO", "ASIGNADA", "COMPLETADA" });
                    break;
                case "DISPONIBILIDAD":
                    cmbEstado.Items.AddRange(new object[] { "DISPONIBLE", "OCUPADO" });
                    break;
                default:
                    // Para CLIENTES no hay estados específicos
                    break;
            }
        }

        private void cmbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarEstados();
            HabilitarControlesSegunTipo();
        }

        private void HabilitarControlesSegunTipo()
        {
            string tipoReporte = cmbTipoReporte.SelectedItem?.ToString() ?? "";

            // Habilitar/deshabilitar controles según el tipo de reporte
            lblEstado.Enabled = (tipoReporte != "CLIENTES");
            cmbEstado.Enabled = (tipoReporte != "CLIENTES");

            lblTecnico.Enabled = (tipoReporte == "ORDENES");
            txtTecnico.Enabled = (tipoReporte == "ORDENES");

            lblClienteDNI.Enabled = (tipoReporte == "CITAS" || tipoReporte == "ORDENES");
            txtClienteDNI.Enabled = (tipoReporte == "CITAS" || tipoReporte == "ORDENES");

            lblFechaInicio.Enabled = (tipoReporte != "DISPONIBILIDAD");
            dtpFechaInicio.Enabled = (tipoReporte != "DISPONIBILIDAD");
            lblFechaFin.Enabled = (tipoReporte != "DISPONIBILIDAD");
            dtpFechaFin.Enabled = (tipoReporte != "DISPONIBILIDAD");
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        private void LimpiarFiltros()
        {
            dtpFechaInicio.Value = DateTime.Today.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Today;
            cmbTipoReporte.SelectedIndex = -1;
            cmbEstado.SelectedIndex = -1;
            txtTecnico.Clear();
            txtClienteDNI.Clear();

            dgvReporte.DataSource = null;
            lblTotalRegistros.Text = "Total Registros: 0";
            lblEstadisticas.Text = "Estadísticas:\nSeleccione un tipo de reporte\ny haga clic en 'Generar Reporte'";
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarFiltros()) return;

                Cursor = Cursors.WaitCursor;
                GenerarReporte();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MostrarError($"Error al generar reporte: {ex.Message}");
            }
        }

        private bool ValidarFiltros()
        {
            if (cmbTipoReporte.SelectedItem == null)
            {
                MostrarError("Por favor seleccione un tipo de reporte");
                cmbTipoReporte.Focus();
                return false;
            }

            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MostrarError("La fecha de inicio no puede ser mayor a la fecha fin");
                dtpFechaInicio.Focus();
                return false;
            }

            return true;
        }

        private void GenerarReporte()
        {
            // Configurar filtros
            filtrosActuales.TipoReporte = cmbTipoReporte.SelectedItem.ToString();
            filtrosActuales.FechaInicio = dtpFechaInicio.Value;
            filtrosActuales.FechaFin = dtpFechaFin.Value;
            filtrosActuales.Estado = cmbEstado.SelectedItem?.ToString();
            filtrosActuales.TecnicoNombre = txtTecnico.Text;
            filtrosActuales.ClienteDNI = txtClienteDNI.Text;

            // Generar reporte
            var resultado = logicaReportes.GenerarReporte(filtrosActuales);

            // Mostrar resultados en DataGridView
            dgvReporte.DataSource = resultado;

            // Configurar columnas según el tipo de reporte
            ConfigurarColumnasDataGridView();

            // Mostrar estadísticas
            MostrarEstadisticas();

            // Actualizar total de registros
            int totalRegistros = 0;
            if (resultado is System.Collections.IList lista)
                totalRegistros = lista.Count;

            lblTotalRegistros.Text = $"Total Registros: {totalRegistros}";
        }

        private void ConfigurarColumnasDataGridView()
        {
            dgvReporte.AutoGenerateColumns = true; // Dejar que se generen automáticamente por ahora
        }

        private void MostrarEstadisticas()
        {
            try
            {
                var resumen = logicaReportes.ObtenerResumenReporte(filtrosActuales);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Reporte: {resumen.Titulo}");
                sb.AppendLine($"Generado: {resumen.FechaGeneracion:dd/MM/yyyy HH:mm}");
                sb.AppendLine();

                foreach (var estadistica in resumen.Estadisticas)
                {
                    sb.AppendLine($"{estadistica.Key}: {estadistica.Value}");
                }

                lblEstadisticas.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                lblEstadisticas.Text = $"Error al cargar estadísticas:\n{ex.Message}";
            }
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReporte.Rows.Count == 0)
                {
                    MostrarError("No hay datos para exportar. Genere un reporte primero.");
                    return;
                }

                // Aquí iría la lógica para exportar a PDF
                // Por ahora mostramos un mensaje
                MostrarMensaje("Funcionalidad de exportación a PDF en desarrollo...");
            }
            catch (Exception ex)
            {
                MostrarError($"Error al exportar PDF: {ex.Message}");
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReporte.Rows.Count == 0)
                {
                    MostrarError("No hay datos para exportar. Genere un reporte primero.");
                    return;
                }

                // Aquí iría la lógica para exportar a Excel
                // Por ahora mostramos un mensaje
                MostrarMensaje("Funcionalidad de exportación a Excel en desarrollo...");
            }
            catch (Exception ex)
            {
                MostrarError($"Error al exportar Excel: {ex.Message}");
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Reportes - Mannucci Motors",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error - Reportes",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}