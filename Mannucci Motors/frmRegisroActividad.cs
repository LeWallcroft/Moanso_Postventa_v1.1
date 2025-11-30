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
    public partial class frmRegisroActividad : Form
    {
        private readonly int _ordentrabajoId;
        private readonly CN_OrdenTrabajo _cnOrdenTrabajo = new CN_OrdenTrabajo();

        private OrdenTrabajo _ordenActual;
        private List<OtActividad> _actividades;
        private Dictionary<int, CheckBox> _mapCheckBoxPorActividad;

        // Constructor para el diseñador (no usar en runtime)
        public frmRegisroActividad()
        {
            InitializeComponent();
        }

        public frmRegisroActividad(int ordentrabajoId) : this()
        {
            _ordentrabajoId = ordentrabajoId;

            this.Load += frmRegisroActividad_Load;
            button1.Click += button1_Click;   // Guardar

            btnComenzarActividad.Click += btnComenzarActividad_Click;
            btnActividadesTerminadas.Click += btnActividadesTerminadas_Click;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_actividades == null || _actividades.Count == 0)
            {
                MessageBox.Show("No hay actividades para guardar.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!pnlActvidadesCheklist.Enabled)
            {
                MessageBox.Show("Debe comenzar la actividad antes de marcar el checklist.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                foreach (var act in _actividades)
                {
                    if (!_mapCheckBoxPorActividad.TryGetValue(act.OtactividadID, out var chk))
                        continue;

                    string nuevoEstado = chk.Checked ? "COMPLETADA" : "PENDIENTE";

                    _cnOrdenTrabajo.ActualizarActividad(
                        act.OtactividadID,
                        nuevoEstado,
                        null     // Mismas observaciones para todas (puedes refinar luego)
                    );
                }

                MessageBox.Show("Actividades actualizadas correctamente.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar actividades: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmRegisroActividad_Load(object sender, EventArgs e)
        {
            if (_ordentrabajoId <= 0)
            {
                MessageBox.Show("No se recibió un Id de orden de trabajo válido.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            try
            {
                CargarDatosOrdenTrabajo();
                CargarActividadesOrdenTrabajo();
                PintarChecklistEnPanel();

                // El panel inicia bloqueado (solo visual)
                pnlActvidadesCheklist.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

        }

        private void CargarDatosOrdenTrabajo()
        {
            _ordenActual = _cnOrdenTrabajo.ObtenerOrdenTrabajo(_ordentrabajoId);

            if (_ordenActual == null)
                throw new InvalidOperationException("No se encontró la orden de trabajo.");

            // ====== Vehículo (misma lógica que en frmOrdenTrabajoControl) :contentReference[oaicite:3]{index=3}
            txtModelo.Text = _ordenActual.ModeloVehiculo;
            txtPlaca.Text = _ordenActual.Placa;
            txtVin.Text = _ordenActual.Vin;
            txtColor.Text = _ordenActual.Color;
            txtAnio.Text = _ordenActual.AnioVehiculo;
            txtKilometraje.Text = _ordenActual.KilometrajeEntrada?.ToString();
            txtCombustible.Text = _ordenActual.TipoCombustible;
            txtTransmision.Text = _ordenActual.Transmision;
            txtRegistro.Text = _ordenActual.NumeroRegistro;
            txtEstadoVehiculo.Text = _ordenActual.EstadoVehiculo;
        }

        private void CargarActividadesOrdenTrabajo()
        {
            _actividades = _cnOrdenTrabajo.ListarActividadesPorOrden(_ordentrabajoId)
                                         .OrderBy(a => a.OtactividadID)
                                         .ToList();
        }

        private void PintarChecklistEnPanel()
        {
            pnlActvidadesCheklist.Controls.Clear();
            pnlActvidadesCheklist.AutoScroll = true;
            _mapCheckBoxPorActividad = new Dictionary<int, CheckBox>();

            if (_actividades == null || _actividades.Count == 0)
            {
                var lbl = new Label
                {
                    Text = "No hay actividades registradas para esta OT.",
                    AutoSize = true,
                    Left = 10,
                    Top = 10
                };
                pnlActvidadesCheklist.Controls.Add(lbl);
                return;
            }

            int top = 10;

            foreach (var act in _actividades)
            {
                var chk = new CheckBox
                {
                    Text = act.Descripcion,
                    AutoSize = true,
                    Left = 10,
                    Top = top,
                    Checked = string.Equals(act.Estado, "COMPLETADA",
                        StringComparison.OrdinalIgnoreCase)
                };

                pnlActvidadesCheklist.Controls.Add(chk);
                _mapCheckBoxPorActividad[act.OtactividadID] = chk;

                top += 25;
            }
        }

        private void btnComenzarActividad_Click(object sender, EventArgs e)
        {
            try
            {
                // Cambiar estado a "En progreso" y marcar fecha de inicio
                // (ver métodos en CN_OrdenTrabajo más abajo)
                _cnOrdenTrabajo.MarcarInicioTrabajo(_ordentrabajoId);

                // Habilitar checklist
                pnlActvidadesCheklist.Enabled = true;

                MessageBox.Show("Se inició la ejecución de las actividades.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar actividades: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActividadesTerminadas_Click(object sender, EventArgs e)
        {
            try
            {
                // Guardar estados de las actividades por si acaso
                button1_Click(sender, e);

                // Marcar fecha fin y estado "Pendiente de control / Para control"
                _cnOrdenTrabajo.MarcarActividadesTerminadas(_ordentrabajoId);

                MessageBox.Show("Las actividades se marcaron como terminadas.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al marcar actividades terminadas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
