using System;
using System.Text;
using System.Windows.Forms;
using CapaLogicaNegocio;
using CapaDominio;
using CapaPresentacion;

namespace Mannucci_Motors
{
    public partial class frmOrdenTrabajoControl : Form
    {
        private readonly int _ordentrabajoId;
        private readonly CN_OrdenTrabajo _cnOrdenTrabajo = new CN_OrdenTrabajo();
        private OrdenTrabajo _ordenActual;

        public frmOrdenTrabajoControl(int ordentrabajoId)
        {
            InitializeComponent();
            _ordentrabajoId = ordentrabajoId;

            this.Load += FrmOrdenTrabajoControl_Load;
            btnAgregarTecnico.Click += btnAgregarTecnico_Click_1;

            btnTarea.Click += btnTarea_Click;
        }

        private void FrmOrdenTrabajoControl_Load(object sender, EventArgs e)
        {
            ConfigurarSoloLectura();
            CargarDatosOrdenTrabajo();
        }

        private void ConfigurarSoloLectura()
        {
            // Panel OT
            SetReadOnly(txtNorden, txtTipoServicio, txtFeInicio,
                        txtFeTerminado, txtFeControl, txtFeEntrega,
                        txtEstado);

            // Panel Cliente
            rchtxtDatosCliente.ReadOnly = true;

            // Panel Vehículo
            SetReadOnly(txtModelo, txtPlaca, txtVin, txtColor,
                        txtAnio, txtKilometraje, txtCombustible,
                        txtTransmision, txtRegistro, txtEstadoVehiculo);

            // Actividades predefinidas solo lectura
            rchtxtActividadesPredifinidas.ReadOnly = true;

            //Observaciones bloqueadas al inicio
            rchtxtObservacionesOT.ReadOnly = true;
            rchtxtObservacionesOT.BackColor = System.Drawing.SystemColors.ControlLightLight;
        }

        private void SetReadOnly(params TextBox[] textBoxes)
        {
            foreach (var txt in textBoxes)
            {
                if (txt == null) continue;
                txt.ReadOnly = true;
                txt.BackColor = System.Drawing.SystemColors.ControlLightLight;
            }
        }

        private void CargarDatosOrdenTrabajo()
        {
            try
            {
                _ordenActual = _cnOrdenTrabajo.ObtenerOrdenTrabajo(_ordentrabajoId);

                if (_ordenActual == null)
                {
                    MessageBox.Show("No se encontró la orden de trabajo.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                // ====== PANEL OT ======
                txtNorden.Text = _ordenActual.OrdentrabajoID.ToString();
                txtTipoServicio.Text = _ordenActual.TipoServicio;
                txtFeInicio.Text = _ordenActual.FechaInicio?.ToString("dd/MM/yyyy HH:mm");
                txtFeTerminado.Text = _ordenActual.FechaFin?.ToString("dd/MM/yyyy HH:mm");
                txtFeControl.Text = _ordenActual.FechaCreacion.ToString("dd/MM/yyyy HH:mm");

                txtFeControl.Text = string.Empty;
                txtFeEntrega.Text = string.Empty;

                txtOperario.Text = string.IsNullOrEmpty(_ordenActual.NombreTecnico)
                  ? "Sin asignar"
                  : _ordenActual.NombreTecnico;
               
                txtEstado.Text = _ordenActual.EstadoOT;                

                // ====== PANEL CLIENTE (RichTextBox) ======
                var sbCliente = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(_ordenActual.NombreCliente))
                    sbCliente.AppendLine($"Cliente: {_ordenActual.NombreCliente}");
                if (!string.IsNullOrWhiteSpace(_ordenActual.DocumentoCliente))
                    sbCliente.AppendLine($"Documento: {_ordenActual.DocumentoCliente}");
                if (!string.IsNullOrWhiteSpace(_ordenActual.TelefonoCliente))
                    sbCliente.AppendLine($"Teléfono: {_ordenActual.TelefonoCliente}");
                if (!string.IsNullOrWhiteSpace(_ordenActual.EmailCliente))
                    sbCliente.AppendLine($"Email: {_ordenActual.EmailCliente}");
                if (!string.IsNullOrWhiteSpace(_ordenActual.DireccionCliente))
                    sbCliente.AppendLine($"Dirección: {_ordenActual.DireccionCliente}");

                rchtxtDatosCliente.Text = sbCliente.ToString();

                // ====== PANEL VEHÍCULO ======
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

                // ====== OBSERVACIONES ======
                rchtxtObservacionesOT.Text = _ordenActual.Observaciones ?? string.Empty;

                

                // ====== ACTIVIDADES PREDEFINIDAS ======

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la OT: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void CargarActividadesOrdenTrabajo()
        {
            try
            {
                rchtxtActividadesPredifinidas.Text =
                    _cnOrdenTrabajo.ObtenerActividadesOrdenTexto(_ordentrabajoId, soloRealizadas: false);

                rchtxtActividadesRealizadas.Text =
                    _cnOrdenTrabajo.ObtenerActividadesOrdenTexto(_ordentrabajoId, soloRealizadas: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar actividades de la OT: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgregarTecnico_Click_1(object sender, EventArgs e)
        {
            using (var frm = new frmSeleccionTecnico())
            {
                if (frm.ShowDialog() == DialogResult.OK && frm.TecnicoSeleccionado != null)
                {
                    try
                    {
                        // Si ya hay técnico, avisamos que se va a reemplazar
                        if (_ordenActual.TecnicoID != null)
                        {
                            var r = MessageBox.Show(
                                "Esta orden ya tiene un técnico asignado.\n" +
                                "¿Desea reemplazarlo por el técnico seleccionado?",
                                "Reemplazar técnico",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                            if (r != DialogResult.Yes)
                                return;

                            // Quitar técnico anterior en BD
                            _cnOrdenTrabajo.AsignarTecnicoAOrden(_ordentrabajoId, null);
                            _ordenActual.TecnicoID = null;
                            _ordenActual.NombreTecnico = null;
                            txtOperario.Text = "Sin asignar";
                        }

                        // Asignar nuevo técnico
                        _cnOrdenTrabajo.AsignarTecnicoAOrden(_ordentrabajoId, frm.TecnicoSeleccionado.TecnicoID);

                        _ordenActual.TecnicoID = frm.TecnicoSeleccionado.TecnicoID;
                        _ordenActual.NombreTecnico = frm.TecnicoSeleccionado.NombreCompleto;
                        txtOperario.Text = _ordenActual.NombreTecnico;

                        // (opcional) aquí puedes actualizar el estado a "Asignado" (ver sección estados)
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al asignar técnico: " + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Confirmación opcional
            if (_ordenActual.TecnicoID == null && string.IsNullOrWhiteSpace(txtOperario.Text))
                return;

            if (MessageBox.Show("¿Desea quitar el técnico asignado a esta OT?",
                                "Confirmar", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // Quitar en BD (TecnicoID = NULL)
                _cnOrdenTrabajo.AsignarTecnicoAOrden(_ordentrabajoId, null);

                // Actualizar modelo en memoria
                _ordenActual.TecnicoID = null;
                _ordenActual.NombreTecnico = null;

                // Limpiar en pantalla
                txtOperario.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al quitar técnico: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTarea_Click(object sender, EventArgs e)
        {
            using (var frm = new frmRegisroActividad(_ordentrabajoId))
            {
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Refrescamos actividades de la OT luego de que el técnico guarde
                    CargarActividadesOrdenTrabajo();
                }
            }
        }


        private void EditarObservaciones()
        {
            rchtxtObservacionesOT.ReadOnly = false;
            rchtxtObservacionesOT.BackColor = System.Drawing.Color.White;  // Para que el usuario vea que se puede escribir
        }

        // Guardar observaciones
        private void GuardarObservaciones()
        {
            try
            {
                // Aquí guardas la observación en la base de datos
                _cnOrdenTrabajo.ActualizarObservaciones(_ordentrabajoId, rchtxtObservacionesOT.Text.Trim());

                MessageBox.Show("Observaciones guardadas correctamente.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rchtxtObservacionesOT.ReadOnly = true;  // Bloquear de nuevo
                rchtxtObservacionesOT.BackColor = System.Drawing.SystemColors.ControlLightLight;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar las observaciones: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditarObs_Click(object sender, EventArgs e)
        {
            EditarObservaciones();
        }

        private void btnGuardarObs_Click(object sender, EventArgs e)
        {
            GuardarObservaciones();
        }
    }
}
