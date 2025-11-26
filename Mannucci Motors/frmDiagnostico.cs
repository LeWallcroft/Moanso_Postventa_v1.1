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
    public partial class frmDiagnostico : Form
    {
        private CN_Diagnostico diagnosticoBL;
        private CitaDetalle citaActual; // Cambiado de "Cita detalleCita" a "CitaDetalle citaActual"        
        private int citaIdActual;
        private Diagnostico diagnosticoExistente;

        public frmDiagnostico()
        {
            InitializeComponent();
            diagnosticoBL = new CN_Diagnostico();
            citaActual = null; // Ahora coincide con la declaración            
            citaIdActual = 0;
            diagnosticoExistente = null;

            // Inicialmente deshabilitar panel de diagnóstico
            pnlRegistrarDiagnostico.Enabled = false;
            btnCrearDiagnostico.Enabled = false;
            ConfigurarModoSoloLectura();
        }
        private void ConfigurarModoSoloLectura()
        {
            richTextBox2.ReadOnly = true;
            richTextBox1.ReadOnly = true;
            richTextBox2.BackColor = SystemColors.Control;
            richTextBox1.BackColor = SystemColors.Control;
        }

        private void ConfigurarModoEdicion()
        {
            richTextBox2.ReadOnly = false;
            richTextBox1.ReadOnly = false;
            richTextBox2.BackColor = SystemColors.Window;
            richTextBox1.BackColor = SystemColors.Window;
        }

        private void CargarDiagnosticoExistente(int citaId)
        {
            try
            {
                diagnosticoExistente = diagnosticoBL.ObtenerDiagnosticoExistente(citaId);
                if (diagnosticoExistente != null)
                {
                    // Mostrar diagnóstico existente
                    richTextBox2.Text = diagnosticoExistente.Hallazgos;
                    richTextBox1.Text = diagnosticoExistente.Recomendaciones;
                    txtFechaDiagnostico.Text = diagnosticoExistente.Fecha.ToString("dd/MM/yyyy");
                    txtHoraDiagnostico.Text = diagnosticoExistente.Fecha.ToString("HH:mm");

                    // Mostrar panel en modo visualización
                    pnlRegistrarDiagnostico.Enabled = true;
                    ConfigurarModoSoloLectura();

                    // Cambiar texto del botón para indicar que ya existe diagnóstico
                    btnCrearDiagnostico.Text = "Diagnóstico Existente";
                    btnCrearDiagnostico.BackColor = Color.LightGray;
                }
                else
                {
                    // Limpiar campos si no existe diagnóstico
                    richTextBox2.Clear();
                    richTextBox1.Clear();
                    txtFechaDiagnostico.Clear();
                    txtHoraDiagnostico.Clear();

                    // Ocultar panel de diagnóstico
                    pnlRegistrarDiagnostico.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar diagnóstico existente: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDetalleCita(int citaId)
        {
            try
            {
                citaActual = diagnosticoBL.ObtenerDetalleCita(citaId);
                if (citaActual != null)
                {
                    // Llenar datos en el panel de consulta
                    rtxtResultadoCliente.Text = $"CLIENTE: {citaActual.ClienteNombre}\n" +
                                                $"DNI: {citaActual.DNI}\n" +
                                                $"Email: {citaActual.Email}\n" +
                                                $"Telefono: {citaActual.Telefono}\n" +
                                                $"Dirección: {citaActual.Direccion}\n" +
                                                $"Fecha Registro: {citaActual.FechaRegistro.ToString("dd/MM/yyyy")}\n";

                    txtCodigoCita.Text = citaActual.CitaId.ToString();
                    txtEstadoCita.Text = citaActual.EstadoCita;
                    txtTipoServicio.Text = citaActual.TipoServicio;
                    txtBahia.Text = citaActual.Bahia;
                    txtTecnico.Text = citaActual.Tecnico;
                    txtOrdenTrabajo.Text = citaActual.NumeroOT;
                    txtEstadoOrdenTrabajo.Text = citaActual.EstadoOT;
                    txtFechaCita.Text = citaActual.FechaCita.ToString("dd/MM/yyyy");
                    txtHora.Text = citaActual.HoraCita.ToString(@"hh\:mm");
                    txtFechaApertura.Text = citaActual.FechaApertura != DateTime.MinValue ?
                                          citaActual.FechaApertura.ToString("dd/MM/yyyy") : "N/A";
                    txtHoraApertura.Text = citaActual.HoraInicioReal.HasValue ?
                                         citaActual.HoraInicioReal.Value.ToString(@"hh\:mm") : "N/A";
                    txtPlacaAuto.Text = citaActual.Placa;
                    txtVinaAuto.Text = citaActual.VIN;
                    txtModeloAuto.Text = citaActual.Modelo;
                    txtMarcaAuto.Text = citaActual.Marca;
                    txtNombreAuto.Text = citaActual.Modelo;
                    txtAnioAuto.Text = citaActual.Anio.ToString();

                    CargarDiagnosticoExistente(citaId); // Cargar diagnóstico si existe

                    // Validar si se puede crear diagnóstico
                    ValidarCreacionDiagnostico();
                }
                else
                {
                    MessageBox.Show("No se encontró información de la cita seleccionada", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar detalle de cita: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpiarCampos();
            }
        }

        private void ValidarCreacionDiagnostico()
        {
            bool puedeCrear = diagnosticoBL.ValidarCitaParaDiagnostico(citaIdActual);
            btnCrearDiagnostico.Enabled = puedeCrear && diagnosticoExistente == null;

            if (diagnosticoExistente != null)
            {
                btnCrearDiagnostico.Text = "Diagnóstico Existente";
                btnCrearDiagnostico.BackColor = Color.LightGray;
            }
            else
            {
                btnCrearDiagnostico.Text = "Crear Diagnóstico";
                btnCrearDiagnostico.BackColor = Color.FromArgb(0, 152, 255);
            }

            if (!puedeCrear && diagnosticoExistente == null)
            {
                if (citaActual.OTId == 0)
                {
                    MessageBox.Show("No se puede crear diagnóstico porque no existe orden de trabajo para esta cita",
                                  "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (diagnosticoBL.ExisteDiagnostico(citaIdActual))
                {
                    MessageBox.Show("Ya existe un diagnóstico para esta cita",
                                  "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void LimpiarCampos()
        {
            rtxtResultadoCliente.Clear();
            txtCodigoCita.Clear();
            txtEstadoCita.Clear();
            txtTipoServicio.Clear();
            txtBahia.Clear();
            txtTecnico.Clear();
            txtOrdenTrabajo.Clear();
            txtEstadoOrdenTrabajo.Clear();
            txtFechaCita.Clear();
            txtHora.Clear();
            txtFechaApertura.Clear();
            txtHoraApertura.Clear();
            txtPlacaAuto.Clear();
            txtVinaAuto.Clear();
            txtModeloAuto.Clear();
            txtMarcaAuto.Clear();
            txtNombreAuto.Clear();
            txtAnioAuto.Clear();

            richTextBox2.Clear();
            richTextBox1.Clear();
            txtFechaDiagnostico.Clear();
            txtHoraDiagnostico.Clear();

            pnlRegistrarDiagnostico.Enabled = false;
            btnCrearDiagnostico.Enabled = false;
            diagnosticoExistente = null;
        }

        private void GuardarDiagnostico()
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(richTextBox2.Text))
            {
                MessageBox.Show("El diagnóstico es obligatorio", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                richTextBox2.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                MessageBox.Show("Las recomendaciones son obligatorias", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                richTextBox1.Focus();
                return;
            }

            if (citaActual.TecnicoId <= 0)
            {
                MessageBox.Show("No hay técnico asignado a esta cita", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string mensaje;
                bool resultado = diagnosticoBL.CrearDiagnostico(
                    citaIdActual,
                    richTextBox2.Text,
                    richTextBox1.Text,
                    citaActual.TecnicoId,
                    out mensaje);

                if (resultado)
                {
                    MessageBox.Show("Diagnóstico creado exitosamente", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarDiagnosticoExistente(citaIdActual);
                    ValidarCreacionDiagnostico();

                    ConfigurarModoSoloLectura();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar diagnóstico: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarClienteDiagnostico_Click(object sender, EventArgs e)
        {
            using (frmDiagnosticoTablaCliente frm = new frmDiagnosticoTablaCliente())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    citaIdActual = frm.CitaIdSeleccionada;
                    cmbClienteDiagnostico.Text = frm.ClienteSeleccionado;

                    //diagnosticoBL.DebugDetalleCita(citaIdActual); // Línea de depuración añadida

                    CargarDetalleCita(citaIdActual);
                }
            }
        }

        private void frmDiagnostico_Load(object sender, EventArgs e)
        {

        }

        private void pnlRegistrarDiagnostico_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCrearDiagnostico_Click(object sender, EventArgs e)
        {
            if (citaActual == null)
            {
                MessageBox.Show("Primero debe seleccionar una cita válida", "Advertencia",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!diagnosticoBL.ValidarCitaParaDiagnostico(citaIdActual))
            {
                MessageBox.Show("No se puede crear diagnóstico para esta cita. Verifique que tenga orden de trabajo y no tenga diagnóstico previo.",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Habilitar panel de diagnóstico
            pnlRegistrarDiagnostico.Enabled = true;

            // Establecer fecha y hora actual
            txtFechaDiagnostico.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtHoraDiagnostico.Text = DateTime.Now.ToString("HH:mm");

            if (diagnosticoExistente != null)
            {
                MessageBox.Show("Ya existe un diagnóstico para esta cita. No se puede crear uno nuevo.",
                              "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
           

            // Deshabilitar panel de repuestos (por ahora)
            pnlRepuestoEnable.Enabled = false;
            checkBox1.Enabled = false;

            // Enfocar en el primer campo
            richTextBox2.Focus();
        }

        private void btnRegistarDiagnostico_Click(object sender, EventArgs e)
        {
            GuardarDiagnostico();
        }

        private void btnCancelarDiagnostico_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cancelar? No se guardarán los datos del diagnóstico.",
                              "Cancelar Diagnóstico",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CargarDiagnosticoExistente(citaIdActual);
                ConfigurarModoSoloLectura();
                this.Close();
            }
        }
    }
}