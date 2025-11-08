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
    public partial class frmDisponibilidad : Form
    {
        private CN_Bahia cnBahia = new CN_Bahia();
        private CN_Cita cnCita = new CN_Cita();
        private List<Bahia> bahiasActivas = new List<Bahia>();

        public frmDisponibilidad()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoSize = false;
            this.Size = new Size(958, 567);
            this.MaximumSize = new Size(958, 567);
            this.MinimumSize = new Size(958, 567);
            CargarBahias();
            CargarDisponibilidad();
        }

        private void mcFecha_DateChanged(object sender, DateRangeEventArgs e)
        {
            CargarDisponibilidad();
        }

        private void cmbBahia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDisponibilidad();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarBahias();
            CargarDisponibilidad();
        }

        private void btnNuevaCita_Click(object sender, EventArgs e)
        {
            if (dgvFranjas.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un slot de disponibilidad (una fila) para crear la cita.", "Validación Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CapacidadDia slotSeleccionado = dgvFranjas.CurrentRow.DataBoundItem as CapacidadDia;

            if (slotSeleccionado == null)
            {
                MessageBox.Show("Error al obtener los datos del slot seleccionado.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (slotSeleccionado.CuposLibres <= 0)
            {
                MessageBox.Show($"La bahía '{slotSeleccionado.Bahia}' está completamente reservada para el horario {slotSeleccionado.RangoHorario}. Por favor, seleccione otro horario o fecha.", "Slot Ocupado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DateTime fechaSeleccionada = mcFecha.SelectionStart.Date;

            using (frmNuevaCita frm = new frmNuevaCita(slotSeleccionado, fechaSeleccionada))
            {
                DialogResult result = frm.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    CargarDisponibilidad();
                }
            }
        }

        private void CargarDisponibilidad()
        {
            try
            {
                DateTime fechaSeleccionada = mcFecha.SelectionStart.Date;
                int? bahiaId = null;

                // Manejar correctamente la selección del ComboBox
                if (cmbBahia.SelectedValue != null && cmbBahia.SelectedValue is int selectedId && selectedId > 0)
                {
                    bahiaId = selectedId;
                }

                List<CapacidadDia> disponibilidad = cnCita.ObtenerDisponibilidad(fechaSeleccionada, bahiaId);

                // FILTRAR SOLO BAHÍAS ACTIVAS - OPCIÓN 1: Por lista de bahías activas
                var nombresBahiasActivas = bahiasActivas.Select(b => b.Nombre).ToList();
                var disponibilidadFiltrada = disponibilidad.Where(d => nombresBahiasActivas.Contains(d.Bahia)).ToList();

                // OPCIÓN 2: Si hay un patrón en los nombres de bahías inhabilitadas
                // var disponibilidadFiltrada = disponibilidad.Where(d => !d.Bahia.ToLower().Contains("inhabilitada")).ToList();

                // Limpiar el evento temporalmente para evitar conflictos
                dgvFranjas.CellFormatting -= DgvFranjas_CellFormatting;

                dgvFranjas.DataSource = null;
                dgvFranjas.DataSource = disponibilidadFiltrada;

                // Reasignar el evento
                dgvFranjas.CellFormatting += DgvFranjas_CellFormatting;

                AjustarDataGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la disponibilidad: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvFranjas.DataSource = null;
            }
        }

        private void AjustarDataGrid()
        {
            if (dgvFranjas.Columns.Count == 0) return;

            // Ocultar columnas que no queremos mostrar
            var columnasOcultar = new[] { "CapacidadId", "HoraInicio", "HoraFin", "BahiaId", "Fecha", "Tipo" };

            foreach (string columna in columnasOcultar)
            {
                if (dgvFranjas.Columns.Contains(columna))
                    dgvFranjas.Columns[columna].Visible = false;
            }

            // Renombrar columnas
            if (dgvFranjas.Columns.Contains("RangoHorario"))
                dgvFranjas.Columns["RangoHorario"].HeaderText = "HORA";

            if (dgvFranjas.Columns.Contains("Bahia"))
                dgvFranjas.Columns["Bahia"].HeaderText = "BAHÍA";

            if (dgvFranjas.Columns.Contains("CapacidadMax"))
                dgvFranjas.Columns["CapacidadMax"].HeaderText = "CAP. MÁX";

            if (dgvFranjas.Columns.Contains("CapacidadReservada"))
                dgvFranjas.Columns["CapacidadReservada"].HeaderText = "RESERVAS";

            if (dgvFranjas.Columns.Contains("CuposLibres"))
                dgvFranjas.Columns["CuposLibres"].HeaderText = "DISPONIBLE";

            // Ajustar ancho de columnas
            dgvFranjas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            if (dgvFranjas.Columns.Contains("RangoHorario"))
                dgvFranjas.Columns["RangoHorario"].Width = 120;

            if (dgvFranjas.Columns.Contains("Bahia"))
                dgvFranjas.Columns["Bahia"].Width = 150;

            if (dgvFranjas.Columns.Contains("CapacidadMax"))
                dgvFranjas.Columns["CapacidadMax"].Width = 80;

            if (dgvFranjas.Columns.Contains("CapacidadReservada"))
                dgvFranjas.Columns["CapacidadReservada"].Width = 80;

            if (dgvFranjas.Columns.Contains("CuposLibres"))
                dgvFranjas.Columns["CuposLibres"].Width = 100;
        }

        private void DgvFranjas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvFranjas.Rows.Count == 0 || e.RowIndex < 0 || e.RowIndex >= dgvFranjas.Rows.Count)
                return;

            // Solo aplicamos formato a la columna "CuposLibres" (índice 6)
            if (e.ColumnIndex == dgvFranjas.Columns["CuposLibres"].Index)
            {
                DataGridViewRow row = dgvFranjas.Rows[e.RowIndex];

                // Verificar si la celda tiene un valor válido
                if (row.Cells["CuposLibres"].Value != null &&
                    int.TryParse(row.Cells["CuposLibres"].Value.ToString(), out int cuposLibres))
                {
                    // Cambiar color según la disponibilidad
                    if (cuposLibres == 0)
                    {
                        e.CellStyle.BackColor = Color.LightCoral; // Rojo claro cuando no hay disponibilidad
                        e.CellStyle.ForeColor = Color.DarkRed;
                        e.CellStyle.Font = new Font(dgvFranjas.Font, FontStyle.Bold);
                    }
                    else if (cuposLibres <= 2) // Pocos cupos disponibles
                    {
                        e.CellStyle.BackColor = Color.LightYellow; // Amarillo cuando hay pocos cupos
                        e.CellStyle.ForeColor = Color.OrangeRed;
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.LightGreen; // Verde cuando hay buena disponibilidad
                        e.CellStyle.ForeColor = Color.DarkGreen;
                    }
                }
            }

            // También podemos formatar la fila completa si no hay cupos disponibles
            DataGridViewRow currentRow = dgvFranjas.Rows[e.RowIndex];
            if (currentRow.Cells["CuposLibres"].Value != null &&
                int.TryParse(currentRow.Cells["CuposLibres"].Value.ToString(), out int disponibles) &&
                disponibles == 0)
            {
                // Opcional: cambiar el color de toda la fila cuando no hay disponibilidad
                currentRow.DefaultCellStyle.BackColor = Color.LavenderBlush;
                currentRow.DefaultCellStyle.ForeColor = Color.Gray;
            }
        }

        private void CargarBahias()
        {
            try
            {
                // Limpiar el DataSource primero
                cmbBahia.DataSource = null;

                bahiasActivas = cnBahia.ListarBahiasActivas();
                var listaParaCombo = new List<Bahia>(bahiasActivas);
                listaParaCombo.Insert(0, new Bahia { BahiaId = 0, Nombre = "Todas las Bahías" });

                cmbBahia.DataSource = listaParaCombo;
                cmbBahia.DisplayMember = "Nombre";
                cmbBahia.ValueMember = "BahiaId";

                // Forzar actualización
                cmbBahia.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las bahías: " + ex.Message, "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento para deshabilitar el botón de nueva cita si no hay cupos
        private void dgvFranjas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFranjas.CurrentRow != null)
            {
                CapacidadDia slotSeleccionado = dgvFranjas.CurrentRow.DataBoundItem as CapacidadDia;
                if (slotSeleccionado != null && slotSeleccionado.CuposLibres <= 0)
                {
                    btnNuevaCita.Enabled = false;
                    btnNuevaCita.BackColor = Color.LightGray;
                }
                else
                {
                    btnNuevaCita.Enabled = true;
                    btnNuevaCita.BackColor = SystemColors.Control;
                }
            }
        }

        private void frmDisponibilidad_Load(object sender, EventArgs e)
        {

        }

        private void dgvFranjas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}