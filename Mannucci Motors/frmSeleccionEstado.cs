using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDominio;
using CapaLogicaNegocio;


namespace Mannucci_Motors
{
    public partial class frmSeleccionEstado : Form
    {
        private readonly CN_OrdenTrabajo _cnOrdenTrabajo = new CN_OrdenTrabajo();

        public EstadoOT EstadoSeleccionado { get; private set; }

        public frmSeleccionEstado()
        {
            InitializeComponent();
            this.Load += FrmSeleccionEstado_Load;
            btnSeleccionar.Click += btnSeleccionar_Click;
            btnCancelar.Click += btnCancelar_Click;
        }

        private void FrmSeleccionEstado_Load(object sender, EventArgs e)
        {
            try
            {
                var estados = _cnOrdenTrabajo.ListarEstadosOT();
                dgvEstados.AutoGenerateColumns = true;
                dgvEstados.DataSource = estados;
                dgvEstados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvEstados.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estados: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvEstados.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un estado.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            EstadoSeleccionado = dgvEstados.CurrentRow.DataBoundItem as EstadoOT;
            if (EstadoSeleccionado == null)
            {
                MessageBox.Show("No se pudo obtener el estado seleccionado.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
