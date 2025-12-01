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
    public partial class frmControlCalidad : Form
    {


        private readonly int _ordentrabajoId;
        private readonly int _usuarioJefeTallerId;
        private readonly CN_OrdenTrabajo _cnOrdenTrabajo = new CN_OrdenTrabajo();

        private OrdenTrabajo _orden;
        private List<RepuestoOT> _repuestos;
        private List<OtActividad> _actividadesOT;
        

        public frmControlCalidad(int ordentrabajoId, int usuarioJefeTallerId)
        {
            InitializeComponent();
            _ordentrabajoId = ordentrabajoId;
            _usuarioJefeTallerId = usuarioJefeTallerId;

            this.Load += FrmControlCalidad_Load;
            btnAprobar.Click += btnAprobar_Click;
            btnRectificar.Click += btnRectificar_Click;
            btnCancelar.Click += btnCancelar_Click;
            dgvChecklist.CellContentClick += dgvChecklist_CellContentClick;
        }

        private void FrmControlCalidad_Load(object sender, EventArgs e)
        {
            try
            {
                // Obtener OT
                _orden = _cnOrdenTrabajo.ObtenerOrdenTrabajo(_ordentrabajoId);

                if (_orden == null)
                {
                    MessageBox.Show("No se encontró la orden de trabajo.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                // Validar estado PARA CONTROL
                if ((_orden.EstadoOT ?? "").ToUpper() != "PARA CONTROL")
                {
                    MessageBox.Show("La OT debe estar en estado PARA CONTROL.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }

                // --------------------------
                // CARGAR ENCABEZADO
                // --------------------------
                txtNroOT.Text = _orden.OrdentrabajoID.ToString();
                txtCliente.Text = _orden.NombreCliente;
                txtPlaca.Text = _orden.Placa;
                txtServicio.Text = _orden.TipoServicio;
                txtEstado.Text = _orden.EstadoOT;

                // --------------------------
                // CARGAR REPUESTOS
                // --------------------------
                _repuestos = _cnOrdenTrabajo.ListarRepuestosPorOrden(_ordentrabajoId);

                if (_repuestos == null || _repuestos.Count == 0)
                {
                    MessageBox.Show("La OT no tiene repuestos registrados.\nNo se puede continuar.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                dgvRepuestos.AutoGenerateColumns = true;
                dgvRepuestos.DataSource = _repuestos;

                txtTotalRepuestos.Text =
                    _repuestos.Sum(r => r.Subtotal).ToString("N2");

                // --------------------------
                // CARGAR ACTIVIDADES → CHECKLIST
                // --------------------------
                _actividadesOT = _cnOrdenTrabajo.ListarActividadesPorOrden(_ordentrabajoId);

                dgvChecklist.Rows.Clear();

                int n = 1;
                foreach (var act in _actividadesOT)
                {
                    dgvChecklist.Rows.Add(
                        n++,
                        act.Descripcion,
                        true,             // por defecto CUMPLE
                        ""                // observación vacía
                    );
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            RegistrarControl("APROBADO");

        }

        private void btnRectificar_Click(object sender, EventArgs e)
        {
            RegistrarControl("RECTIFICAR");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void RegistrarControl(string resultado)
        {
            try
            {
                // Validar repuestos
                if (_repuestos == null || _repuestos.Count == 0)
                {
                    MessageBox.Show("La OT no tiene repuestos registrados.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Si APRUEBA validar que no hayan items sin cumplir
                if (resultado == "APROBADO")
                {
                    bool hayNoCumple = dgvChecklist.Rows.Cast<DataGridViewRow>()
                                      .Any(r => !(bool)r.Cells["colCumple"].Value);

                    if (hayNoCumple)
                    {
                        var r = MessageBox.Show(
                            "Hay ítems que NO cumplen.\n¿Desea aprobar de todas formas?",
                            "Advertencia",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (r != DialogResult.Yes) return;
                    }
                }

                // Generar XML
                string xml = GenerarXMLChecklist();

                // Registrar control
                _cnOrdenTrabajo.RegistrarControlCalidad(
                    _ordentrabajoId,
                    _usuarioJefeTallerId,
                    resultado,
                    rtxtObsControl.Text.Trim(),
                    xml);

                // Cambios de estado según resultado
                if (resultado == "APROBADO")
                {
                    _cnOrdenTrabajo.CambiarEstadoOT(_ordentrabajoId, 5);   // COMPLETADA
                    _cnOrdenTrabajo.CambiarEstadoOrdenPago(_ordentrabajoId, "EMITIDA");
                }
                else if (resultado == "RECTIFICAR")
                {
                    _cnOrdenTrabajo.CambiarEstadoOT(_ordentrabajoId, 3);   // EN PROGRESO
                }

                MessageBox.Show("Control de calidad registrado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar control de calidad: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // =======================
        //   GENERAR XML
        // =======================

        private string GenerarXMLChecklist()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<Checklist>");

            for (int i = 0; i < _actividadesOT.Count; i++)
            {
                var act = _actividadesOT[i];
                var row = dgvChecklist.Rows[i];

                bool cumple = (bool)row.Cells["colCumple"].Value;
                string obs = row.Cells["colObs"].Value?.ToString() ?? "";

                sb.Append("<Item>");
                sb.AppendFormat("<OtactividadID>{0}</OtactividadID>", act.OtactividadID);
                sb.AppendFormat("<Descripcion>{0}</Descripcion>", act.Descripcion);
                sb.AppendFormat("<Estado>{0}</Estado>", cumple ? "CUMPLE" : "NO CUMPLE");
                sb.AppendFormat("<Observacion>{0}</Observacion>", obs);
                sb.Append("</Item>");
            }

            sb.Append("</Checklist>");
            return sb.ToString();
        }

        private void dgvChecklist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // SOLO MANEJAMOS CUMPLE (check/uncheck)
            if (e.ColumnIndex == dgvChecklist.Columns["colCumple"].Index)
            {
                var cell = dgvChecklist.Rows[e.RowIndex].Cells["colCumple"];
                bool valorActual = cell.Value != null && (bool)cell.Value;
                cell.Value = !valorActual; // invertir valor
            }
        }
    }
}
