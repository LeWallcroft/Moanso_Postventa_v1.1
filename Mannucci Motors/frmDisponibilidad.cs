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
            this.MaximumSize = new Size(958, 567); // Tamaño fijo
            this.MinimumSize = new Size(958, 567); // Tamaño fijo
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
            // 1. VALIDACIÓN DE SELECCIÓN DE FILA
            if (dgvFranjas.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un slot de disponibilidad (una fila) para crear la cita.", "Validación Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el objeto CapacidadDia de la fila seleccionada
            // Se asume que el DataSource del DGV es una List<CapacidadDia>
            CapacidadDia slotSeleccionado = dgvFranjas.CurrentRow.DataBoundItem as CapacidadDia;

            if (slotSeleccionado == null)
            {
                MessageBox.Show("Error al obtener los datos del slot seleccionado.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. VALIDACIÓN DE CUPOS LIBRES
            if (slotSeleccionado.CuposLibres <= 0)
            {
                MessageBox.Show($"La bahía '{slotSeleccionado.Bahia}' está completamente reservada para el horario {slotSeleccionado.RangoHorario}. Por favor, seleccione otro horario o fecha.", "Slot Ocupado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // 3. NAVEGACIÓN (Si todas las validaciones son exitosas)

            // Obtenemos la fecha seleccionada del calendario para pasarla al nuevo formulario
            DateTime fechaSeleccionada = mcFecha.SelectionStart.Date;

            // Abrir el formulario de creación de cita como modal.
            // Le pasamos el slot de capacidad seleccionado y la fecha.
            // Asumimos que el formulario se llama frmNuevaCita.
            using (frmNuevaCita frm = new frmNuevaCita(slotSeleccionado, fechaSeleccionada))
            {
                DialogResult result = frm.ShowDialog(this);

                // Si la cita se crea correctamente, refrescamos la disponibilidad
                if (result == DialogResult.OK)
                {
                    CargarDisponibilidad(); // Refresca el DataGrid
                }
            }
        }

        // Método que se debe llamar cuando cambia la fecha o la bahía
        private void CargarDisponibilidad()
        {
            try
            {
                // Obtener la fecha seleccionada del calendario (el control MonthCalendar devuelve un rango)
                DateTime fechaSeleccionada = mcFecha.SelectionStart.Date;

                // Obtener el ID de la bahía seleccionada. Se asume cmbBahias.SelectedValue es int
                int? bahiaId = cmbBahia.SelectedValue as int?;

                // Si el valor no es un entero (o es null/0), se consulta para todas (la lógica del SP lo maneja)
                if (bahiaId == 0) bahiaId = null;

                // Llamar a la Capa Lógica
                List<CapacidadDia> disponibilidad = cnCita.ObtenerDisponibilidad(fechaSeleccionada, bahiaId);

                // Asignar los resultados al DataGrid
                dgvFranjas.DataSource = disponibilidad;

                // Ocultar columnas internas y renombrar para la presentación
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
            // Ocultar las columnas que no son relevantes para el usuario
            dgvFranjas.Columns["CapacidadId"].Visible = false;
            dgvFranjas.Columns["HoraInicio"].Visible = false;
            dgvFranjas.Columns["HoraFin"].Visible = false;

            // Renombrar y dar formato a las columnas
            dgvFranjas.Columns["RangoHorario"].HeaderText = "HORA";
            dgvFranjas.Columns["Bahia"].HeaderText = "BAHÍA";
            dgvFranjas.Columns["CapacidadMax"].HeaderText = "CAP. MÁX";
            dgvFranjas.Columns["CapacidadReservada"].HeaderText = "RESERVAS";
            dgvFranjas.Columns["CuposLibres"].HeaderText = "DISPONIBLE";

            // Resaltar la columna de cupos libres
            dgvFranjas.Columns["CuposLibres"].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
        }

        private void CargarBahias()
        {
            try
            {
                // Obtener la lista de bahías activas de la capa de lógica (CN_Bahia.ListarBahiasActivas)
                List<Bahia> listaBahias = cnBahia.ListarBahiasActivas();

                // 1. Añadir la opción "Todas las bahías"
                // Cambiamos el Tipo por el Nombre en el texto, aunque se usará el ID=0 para filtrar.
                listaBahias.Insert(0, new Bahia { BahiaId = 0, Nombre = "Todas las Bahías" });

                // Configurar el ComboBox (cmbBahias)
                cmbBahia.DataSource = listaBahias;

                // 2. CAMBIO CLAVE: Mostrar el NOMBRE en lugar del Tipo
                cmbBahia.DisplayMember = "Nombre";

                // El campo que se guardará internamente (el ID)
                cmbBahia.ValueMember = "BahiaId";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las bahías: " + ex.Message, "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
