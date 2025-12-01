using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CapaLogicaNegocio;
using CapaDominio;

namespace CapaPresentacion
{
    public partial class frmSeleccionTecnico : Form
    {
        private readonly CN_Tecnico _cnTecnico = new CN_Tecnico();
        private List<Tecnico> _listaTecnicos = new List<Tecnico>();

        public Tecnico TecnicoSeleccionado { get; private set; }

        public frmSeleccionTecnico()
        {
            InitializeComponent();
            this.Load += FrmSeleccionTecnico_Load;            
            btnCancelar.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            dgvTecnicos.DoubleClick += DgvTecnicos_DoubleClick;
        }

        private void FrmSeleccionTecnico_Load(object sender, EventArgs e)
        {
            CargarTecnicos();
            ConfigurarGrid();
        }

        private void CargarTecnicos()
        {
            _listaTecnicos = _cnTecnico.ListarTecnicosActivosDisponibles();
            dgvTecnicos.DataSource = null;
            dgvTecnicos.DataSource = _listaTecnicos;
        }

        private void ConfigurarGrid()
        {
            dgvTecnicos.AutoGenerateColumns = false;
            dgvTecnicos.ReadOnly = true;
            dgvTecnicos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTecnicos.MultiSelect = false;
            dgvTecnicos.AllowUserToAddRows = false;
            dgvTecnicos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvTecnicos.Columns.Clear();

            dgvTecnicos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TecnicoID",
                HeaderText = "Id",
                Visible = false
            });

            dgvTecnicos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NombreCompleto",
                HeaderText = "Técnico"
            });

            dgvTecnicos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Especialidad",
                HeaderText = "Especialidad"
            });

            dgvTecnicos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EstadoCompleto",
                HeaderText = "Estado"
            });
        }
                

        private void DgvTecnicos_DoubleClick(object sender, EventArgs e)
        {
            SeleccionarTecnico();
        }

        private void SeleccionarTecnico()
        {
            if (dgvTecnicos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un técnico.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            TecnicoSeleccionado = dgvTecnicos.CurrentRow.DataBoundItem as Tecnico;
            if (TecnicoSeleccionado == null)
            {
                MessageBox.Show("No se pudo obtener el técnico seleccionado.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;        }

       

        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            SeleccionarTecnico();
        }
    }
}
