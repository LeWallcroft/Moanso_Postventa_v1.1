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
    public partial class frmDiagnosticoTablaCliente : Form
    {
        private CN_Diagnostico diagnosticoBL;
        public int CitaIdSeleccionada { get; private set; }
        public string ClienteSeleccionado { get; private set; }

        public frmDiagnosticoTablaCliente()
        {
            InitializeComponent();
            diagnosticoBL = new CN_Diagnostico();
            CitaIdSeleccionada = 0;
        }


        private void SeleccionarCliente()
        {
            if (dgvTablaCitaRegistrada.CurrentRow != null)
            {
                DataGridViewRow row = dgvTablaCitaRegistrada.CurrentRow;
                CitaIdSeleccionada = Convert.ToInt32(row.Cells["CitaId"].Value);
                ClienteSeleccionado = row.Cells["ClienteNombre"].Value.ToString() + " - " +
                                    row.Cells["Placa"].Value.ToString() + " - " +
                                    row.Cells["Servicio"].Value.ToString();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor seleccione un cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarClientesConCitas()
        {
            try
            {
                DataTable dt = diagnosticoBL.ObtenerClientesConCitas();
                dgvTablaCitaRegistrada.DataSource = dt;

                // Configurar columnas
                dgvTablaCitaRegistrada.Columns["CitaId"].Visible = false;
                dgvTablaCitaRegistrada.Columns["ClienteId"].Visible = false;
                dgvTablaCitaRegistrada.Columns["ClienteNombre"].HeaderText = "Cliente";
                dgvTablaCitaRegistrada.Columns["DNI"].HeaderText = "DNI";
                dgvTablaCitaRegistrada.Columns["Placa"].HeaderText = "Placa";
                dgvTablaCitaRegistrada.Columns["Marca"].HeaderText = "Marca";
                dgvTablaCitaRegistrada.Columns["Modelo"].HeaderText = "Modelo";
                dgvTablaCitaRegistrada.Columns["Servicio"].HeaderText = "Servicio";
                dgvTablaCitaRegistrada.Columns["Fecha"].HeaderText = "Fecha Cita";
                dgvTablaCitaRegistrada.Columns["HoraInicio"].HeaderText = "Hora Inicio";
                dgvTablaCitaRegistrada.Columns["Estado"].HeaderText = "Estado Cita";
                dgvTablaCitaRegistrada.Columns["NumeroOT"].HeaderText = "Orden Trabajo";
                dgvTablaCitaRegistrada.Columns["EstadoOT"].HeaderText = "Estado OT";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDiagnosticoTablaCliente_Load(object sender, EventArgs e)
        {
            CargarClientesConCitas();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            SeleccionarCliente();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgvTablaCitaRegistrada_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SeleccionarCliente();
            }
        }
    }
}
