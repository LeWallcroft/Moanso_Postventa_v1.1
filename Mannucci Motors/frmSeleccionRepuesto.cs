using CapaDominio;
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
    public partial class frmSeleccionRepuesto : Form
    {
        // Lista de repuestos disponibles para elegir
        public List<Repuesto> RepuestosDisponibles { get; set; } = new List<Repuesto>();
        public Repuesto RepuestoSeleccionado { get; private set; }

        public frmSeleccionRepuesto()
        {
            InitializeComponent();
            this.Load += FrmSeleccionRepuesto_Load;
            btnSeleccionar.Click += btnSeleccionar_Click;
            btnCancelar.Click += btnCancelar_Click;
        }

        private void FrmSeleccionRepuesto_Load(object sender, EventArgs e)
        {
            dgvRepuestos.AutoGenerateColumns = true;
            dgvRepuestos.ReadOnly = true;
            dgvRepuestos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRepuestos.MultiSelect = false;
            dgvRepuestos.DataSource = RepuestosDisponibles;

            if (dgvRepuestos.Columns["RepuestoID"] != null)
                dgvRepuestos.Columns["RepuestoID"].HeaderText = "ID";
            if (dgvRepuestos.Columns["Nombre"] != null)
                dgvRepuestos.Columns["Nombre"].HeaderText = "Repuesto";
            if (dgvRepuestos.Columns["Precio"] != null)
                dgvRepuestos.Columns["Precio"].HeaderText = "Precio";
            if (dgvRepuestos.Columns["Stock"] != null)
                dgvRepuestos.Columns["Stock"].HeaderText = "Stock";

            // ocultar columnas que no quieres
            string[] ocultar = { "Descripcion", "StockMinimo", "Proveedor",
                                 "FechaCreacion", "Activo", "StockBajo", "TieneStock" };
            foreach (var col in ocultar)
            {
                if (dgvRepuestos.Columns[col] != null)
                    dgvRepuestos.Columns[col].Visible = false;
            }
        }
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvRepuestos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un repuesto.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            RepuestoSeleccionado = dgvRepuestos.CurrentRow.DataBoundItem as Repuesto;

            if (RepuestoSeleccionado == null)
            {
                MessageBox.Show("No se pudo obtener el repuesto seleccionado.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
