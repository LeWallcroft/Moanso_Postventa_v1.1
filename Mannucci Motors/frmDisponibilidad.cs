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
                int? bahiaId = cmbBahia.SelectedValue as int?;
                if (bahiaId == 0) bahiaId = null;

                List<CapacidadDia> disponibilidad = cnCita.ObtenerDisponibilidad(fechaSeleccionada, bahiaId);

                dgvFranjas.DataSource = disponibilidad;
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
            // Ocultar columnas que no queremos mostrar
            dgvFranjas.Columns["CapacidadId"].Visible = false;
            dgvFranjas.Columns["HoraInicio"].Visible = false;
            dgvFranjas.Columns["HoraFin"].Visible = false;
            dgvFranjas.Columns["BahiaId"].Visible = false; // Ocultamos BahiaId si no quieres mostrarlo
            dgvFranjas.Columns["Fecha"].Visible = false;
            dgvFranjas.Columns["Tipo"].Visible = false;

            // Renombrar columnas
            dgvFranjas.Columns["RangoHorario"].HeaderText = "HORA";
            dgvFranjas.Columns["Bahia"].HeaderText = "BAHÍA";
            dgvFranjas.Columns["CapacidadMax"].HeaderText = "CAP. MÁX";
            dgvFranjas.Columns["CapacidadReservada"].HeaderText = "RESERVAS";
            dgvFranjas.Columns["CuposLibres"].HeaderText = "DISPONIBLE";

            // Configurar el evento para cambiar colores dinámicamente
            dgvFranjas.CellFormatting += DgvFranjas_CellFormatting;

            // Ajustar ancho de columnas
            dgvFranjas.Columns["RangoHorario"].Width = 120;
            dgvFranjas.Columns["Bahia"].Width = 150;
            dgvFranjas.Columns["CapacidadMax"].Width = 80;
            dgvFranjas.Columns["CapacidadReservada"].Width = 80;
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
                List<Bahia> listaBahias = cnBahia.ListarBahiasActivas();
                listaBahias.Insert(0, new Bahia { BahiaId = 0, Nombre = "Todas las Bahías" });

                cmbBahia.DataSource = listaBahias;
                cmbBahia.DisplayMember = "Nombre";
                cmbBahia.ValueMember = "BahiaId";

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
    }
}