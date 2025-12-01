using CapaDominio;
using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static Mannucci_Motors.frmLogin;

namespace Mannucci_Motors
{
    public partial class frmOrdenesTrabajo : Form
    {
        private readonly CN_OrdenTrabajo _cnOrdenTrabajo = new CN_OrdenTrabajo();

        private readonly bool _esTecnico;
        private readonly int? _tecnicoActualId;
        public frmOrdenesTrabajo(bool esTecnico, int? tecnicoActualId)
        {
            InitializeComponent();
            _esTecnico = esTecnico;
            _tecnicoActualId = tecnicoActualId;
            this.Load += FrmOrdenesTrabajo_Load;
        }
        public frmOrdenesTrabajo() : this(false, null)
        {
        }
        private void FrmOrdenesTrabajo_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            ConfigurarSegunRol();
            CargarOrdenesTrabajo();
        }

        private void ConfigurarSegunRol()
        {
            if (_esTecnico)
            {
                // El técnico NO puede entrar al detalle de control
                btnEditar.Enabled = false;
                btnEditar.Visible = true;
               
            }
        }
        private void ConfigurarGrid()
        {
            dgvListasOTs.AutoGenerateColumns = false;
            dgvListasOTs.ReadOnly = true;
            dgvListasOTs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvListasOTs.MultiSelect = false;
            dgvListasOTs.AllowUserToAddRows = false;
            dgvListasOTs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvListasOTs.Columns.Clear();

            // Id oculto
            dgvListasOTs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colIdOT",
                HeaderText = "Id OT",
                DataPropertyName = "OrdentrabajoID",
                Visible = false
            });

            dgvListasOTs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNumeroOT",
                HeaderText = "N° OT",
                DataPropertyName = "OrdentrabajoID"
            });

            dgvListasOTs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTipoServicio",
                HeaderText = "Tipo servicio",
                DataPropertyName = "TipoServicio"
            });

            dgvListasOTs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colFechaInicio",
                HeaderText = "Fecha inicio",
                DataPropertyName = "FechaInicio",
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            });

            dgvListasOTs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCliente",
                HeaderText = "Cliente",
                DataPropertyName = "NombreCliente"
            });

            dgvListasOTs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPlaca",
                HeaderText = "Placa",
                DataPropertyName = "Placa"
            });

            dgvListasOTs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colVehiculo",
                HeaderText = "Vehículo",
                DataPropertyName = "DescripcionVehiculo"
            });

            dgvListasOTs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTecnico",
                HeaderText = "Técnico / Operario",
                DataPropertyName = "NombreTecnico"
            });

            dgvListasOTs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colEstado",
                HeaderText = "Estado",
                DataPropertyName = "EstadoOT"
            });
        }

        private void CargarOrdenesTrabajo()
        {
            try
            {
                List<OrdenTrabajo> lista = _cnOrdenTrabajo.ListarOrdenesTrabajo();
                

                dgvListasOTs.DataSource = null;
                dgvListasOTs.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las órdenes de trabajo: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {

            if (dgvListasOTs.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una orden de trabajo.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var otSeleccionada = dgvListasOTs.CurrentRow.DataBoundItem as OrdenTrabajo;
            if (otSeleccionada == null)
            {
                MessageBox.Show("No se pudo obtener la orden seleccionada.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool esJefeTaller =
              Sesion.UsuarioActual.Rol.Equals("JefeTaller", StringComparison.OrdinalIgnoreCase)
              || Sesion.UsuarioActual.Rol.Equals("Admin", StringComparison.OrdinalIgnoreCase);

            // Obtener id de usuario logueado
            int usuarioActualId = Sesion.UsuarioActual.UsuariosID;

            using (var form = new frmOrdenTrabajoControl(
                otSeleccionada.OrdentrabajoID,
                esJefeTaller,
                usuarioActualId))
            {
                form.ShowDialog();
            }


            // Recargar lista por si algo cambió
            CargarOrdenesTrabajo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTarea_Click(object sender, EventArgs e)
        {
            if (dgvListasOTs.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una orden de trabajo.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var otSeleccionada = dgvListasOTs.CurrentRow.DataBoundItem as OrdenTrabajo;
            if (otSeleccionada == null)
            {
                MessageBox.Show("No se pudo obtener la orden seleccionada.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 1) Validar que la OT tenga técnico asignado
            if (otSeleccionada.TecnicoID == null)
            {
                MessageBox.Show("Esta orden aún no tiene técnico asignado.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2) Si el usuario es TÉCNICO, validar que sea el técnico asignado
            if (_esTecnico)
            {
                if (!_tecnicoActualId.HasValue)
                {
                    MessageBox.Show("No se encontró el técnico actual en la sesión.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (otSeleccionada.TecnicoID != _tecnicoActualId.Value)
                {
                    MessageBox.Show("La orden seleccionada está asignada a otro técnico.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // 3)Validar estado de la OT
            // Solo permitir tareas si está ASIGNADA o EN PROGRESO
            string estado = (otSeleccionada.EstadoOT ?? "").ToUpperInvariant();
            if (estado != "ASIGNADA" && estado != "EN PROGRESO")
            {
                MessageBox.Show("Solo se pueden registrar tareas para órdenes ASIGNADAS o EN PROGRESO.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 4) Abrir el formulario de registro de actividades
            using (var frm = new frmRegisroActividad(otSeleccionada.OrdentrabajoID))
            {
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Se actualizaron actividades / estado
                    CargarOrdenesTrabajo();
                }
            }
        }
    }
}
