using CapaLogicaNegocio;
using CapaDominio;
using CapaDominio.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmBahias : Form
    {
        private CN_Bahia cnBahia = new CN_Bahia();
        private CN_Usuario cnUsuario = new CN_Usuario();
        private CN_Tecnico cnTecnico = new CN_Tecnico();
        private int bahiaIdSeleccionada = 0;
        private bool modoEdicion = false;
        private List<Bahia> todasLasBahias = new List<Bahia>();

        public frmBahias()
        {
            InitializeComponent();

            // CONFIGURACIÓN MDI CORRECTA
            this.WindowState = FormWindowState.Maximized;
            this.Dock = DockStyle.Fill;

            // Suscribirse a eventos de sincronización
            SincronizadorGlobal.EstadoUsuarioCambiado += OnEstadoUsuarioCambiado;
            SincronizadorGlobal.EstadoTecnicoCambiado += OnEstadoTecnicoCambiado;
            SincronizadorGlobal.CambioBahia += OnCambioBahia;
        }

        // Manejar cuando un usuario técnico cambia de estado
        private void OnEstadoUsuarioCambiado(int usuarioID, bool activo)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<int, bool>(OnEstadoUsuarioCambiado), usuarioID, activo);
                return;
            }

            try
            {
                // Verificar si el usuario afectado es un técnico
                var usuario = cnUsuario.ObtenerUsuarioPorId(usuarioID);
                if (usuario != null && usuario.Rol == "Tecnico")
                {
                    // Recargar combo de técnicos
                    CargarCombos();

                    // Recargar bahías si alguna está asignada a este usuario
                    var bahiasAfectadas = todasLasBahias.Where(b => b.UsuariosId == usuarioID).ToList();
                    if (bahiasAfectadas.Any())
                    {
                        CargarBahias();
                        MostrarMensajeSincronizacion($"Se actualizó el estado del técnico {usuario.Nombre}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en sincronización usuario: {ex.Message}");
            }
        }

        // Manejar cuando un técnico cambia de estado
        private void OnEstadoTecnicoCambiado(int tecnicoID, bool activo)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<int, bool>(OnEstadoTecnicoCambiado), tecnicoID, activo);
                return;
            }

            try
            {
                // Recargar combo de técnicos
                CargarCombos();

                // Recargar bahías para reflejar cambios visuales
                CargarBahias();

                MostrarMensajeSincronizacion("Lista de técnicos actualizada");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en sincronización técnico: {ex.Message}");
            }
        }

        // Manejar cuando hay cambios generales en bahías
        private void OnCambioBahia()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(OnCambioBahia));
                return;
            }

            try
            {
                CargarBahias();
                Console.WriteLine("Bahías actualizadas por notificación global");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar bahías: {ex.Message}");
            }
        }

        private void frmBahias_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCombos();
                CargarBahias();
                LimpiarFormulario();
                ConfigurarDataGridView();
            }
            catch (Exception ex)
            {
                ManejarError($"Error al cargar el formulario: {ex.Message}", "Error de Carga");
            }
        }

        private void CargarCombos()
        {
            try
            {
                // Cargar estados de bahía primero (esto siempre debería funcionar)
                cmbEstado.Items.Clear();
                cmbFiltroEstado.Items.Clear();

                var estados = new List<object>
                {
                    new { Text = "DISPONIBLE", Value = 1 },
                    new { Text = "OCUPADA", Value = 2 },
                    new { Text = "MANTENIMIENTO", Value = 3 },
                    new { Text = "INACTIVA", Value = 4 }
                };

                foreach (var estado in estados)
                {
                    cmbEstado.Items.Add(estado);
                    cmbFiltroEstado.Items.Add(estado);
                }

                cmbEstado.DisplayMember = "Text";
                cmbEstado.ValueMember = "Value";
                cmbFiltroEstado.DisplayMember = "Text";
                cmbFiltroEstado.ValueMember = "Value";

                // Cargar técnicos activos con manejo de errores mejorado
                CargarTecnicosActivos();
            }
            catch (Exception ex)
            {
                ManejarError($"Error al cargar listas: {ex.Message}", "Error de Carga");
            }
        }

        private void CargarTecnicosActivos()
        {
            try
            {
                cmbUsuario.Items.Clear();
                cmbUsuario.Items.Add(new { Text = "SIN ASIGNAR", Value = (int?)null });

                Console.WriteLine("🔧 Cargando técnicos activos...");

                // ✅ SOLUCIÓN DEFINITIVA
                var tecnicosActivos = cnTecnico.ListarTecnicosActivos();

                Console.WriteLine($"✅ Se encontraron {tecnicosActivos?.Count ?? 0} técnicos activos");

                if (tecnicosActivos == null || !tecnicosActivos.Any())
                {
                    cmbUsuario.Items.Add(new { Text = "SIN TÉCNICOS DISPONIBLES", Value = (int?)null });
                    return;
                }

                int tecnicosCargados = 0;
                foreach (var tecnico in tecnicosActivos)
                {
                    if (tecnico.UsuariosId.HasValue)
                    {
                        string texto = $"{tecnico.NombreCompleto} - {tecnico.Especialidad}";
                        cmbUsuario.Items.Add(new
                        {
                            Text = texto,
                            Value = tecnico.UsuariosId
                        });
                        tecnicosCargados++;
                        Console.WriteLine($"✅ Cargado: {texto}");
                    }
                }

                Console.WriteLine($"🎯 Total cargados en combo: {tecnicosCargados}");

                cmbUsuario.DisplayMember = "Text";
                cmbUsuario.ValueMember = "Value";

                // ✅ Seleccionar el primer item por defecto
                if (cmbUsuario.Items.Count > 0)
                    cmbUsuario.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR CRÍTICO: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"❌ INNER ERROR: {ex.InnerException.Message}");
                }

                // ✅ RECUPERACIÓN ELEGANTE
                cmbUsuario.Items.Clear();
                cmbUsuario.Items.Add(new { Text = "SIN ASIGNAR", Value = (int?)null });
                cmbUsuario.Items.Add(new { Text = "ERROR - VER CONSOLA", Value = (int?)null });

                cmbUsuario.DisplayMember = "Text";
                cmbUsuario.ValueMember = "Value";
            }
        }

        private void CargarBahias()
        {
            try
            {
                todasLasBahias = cnBahia.ListarTodasLasBahias();
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                ManejarError($"Error al cargar bahías: {ex.Message}", "Error de Carga");
            }
        }

        private void AplicarFiltros()
        {
            try
            {
                var bahiasFiltradas = todasLasBahias.AsEnumerable();

                // Filtrar por estado
                if (cmbFiltroEstado.SelectedItem != null)
                {
                    dynamic estadoSeleccionado = cmbFiltroEstado.SelectedItem;
                    int estadoId = estadoSeleccionado.Value;
                    bahiasFiltradas = bahiasFiltradas.Where(b => b.EstadobahiaId == estadoId);
                }

                // Filtrar por búsqueda de texto
                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    string busqueda = txtBuscar.Text.ToLower();
                    bahiasFiltradas = bahiasFiltradas.Where(b =>
                        b.Nombre.ToLower().Contains(busqueda) ||
                        (b.Descripcion != null && b.Descripcion.ToLower().Contains(busqueda)));
                }

                MostrarBahiasEnGrid(bahiasFiltradas.ToList());
            }
            catch (Exception ex)
            {
                ManejarError($"Error al aplicar filtros: {ex.Message}", "Error de Filtro");
            }
        }

        private void MostrarBahiasEnGrid(List<Bahia> bahias)
        {
            try
            {
                dgvBahias.Rows.Clear();

                foreach (var bahia in bahias)
                {
                    string nombreUsuario = ObtenerNombreTecnicoAsignado(bahia.UsuariosId);

                    int rowIndex = dgvBahias.Rows.Add(
                        bahia.BahiaId,
                        bahia.Nombre,
                        bahia.Descripcion ?? "",
                        bahia.Capacidad,
                        bahia.EstadoBahia?.Nombre ?? "SIN ESTADO",
                        nombreUsuario,
                        bahia.Activo ? "SÍ" : "NO"
                    );

                    AplicarColorFila(dgvBahias.Rows[rowIndex], bahia);
                }

                dgvBahias.ClearSelection();
            }
            catch (Exception ex)
            {
                ManejarError($"Error al mostrar bahías en grid: {ex.Message}", "Error de Visualización");
            }
        }

        private string ObtenerNombreTecnicoAsignado(int? usuarioId)
        {
            if (!usuarioId.HasValue)
                return "SIN ASIGNAR";

            try
            {
                // Intentar buscar en técnicos activos
                var tecnicosActivos = cnTecnico.ListarTecnicosActivos();

                if (tecnicosActivos != null)
                {
                    var tecnico = tecnicosActivos.FirstOrDefault(t => t.UsuariosId == usuarioId);

                    if (tecnico != null)
                    {
                        // Verificar que el usuario también esté activo
                        try
                        {
                            var usuario = cnUsuario.ObtenerUsuarioPorId(usuarioId.Value);
                            if (usuario != null && usuario.Activo)
                                return tecnico.NombreCompleto;
                            else
                                return "TÉCNICO INACTIVO";
                        }
                        catch
                        {
                            return tecnico.NombreCompleto; // Si falla la verificación, al menos mostrar el nombre
                        }
                    }
                }

                return "TÉCNICO NO DISPONIBLE";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener técnico: {ex.Message}");
                return "ERROR AL CARGAR";
            }
        }

        private void AplicarColorFila(DataGridViewRow fila, Bahia bahia)
        {
            try
            {
                // ✅ CORRECCIÓN: Cambiar LightBlue por gris claro para selección
                fila.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 220, 220); // Gris claro
                fila.DefaultCellStyle.SelectionForeColor = Color.Black;

                if (!bahia.Activo)
                {
                    fila.DefaultCellStyle.BackColor = Color.LightGray;
                    fila.DefaultCellStyle.ForeColor = Color.DarkGray;
                }
                else
                {
                    string colorHex = ObtenerColorPorEstado(bahia.EstadobahiaId);
                    Color color = ColorTranslator.FromHtml(colorHex);
                    fila.DefaultCellStyle.BackColor = ControlPaint.LightLight(color);
                    fila.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al aplicar color a fila: {ex.Message}");
                fila.DefaultCellStyle.BackColor = Color.White;
                fila.DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private string ObtenerColorPorEstado(int estadoId)
        {
            switch (estadoId)
            {
                case 1: return "#28a745"; // Verde - DISPONIBLE
                case 2: return "#dc3545"; // Rojo - OCUPADA
                case 3: return "#ffc107"; // Amarillo - MANTENIMIENTO
                case 4: return "#6c757d"; // Gris - INACTIVA
                default: return "#ffffff"; // Blanco
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvBahias.AutoGenerateColumns = false;
            dgvBahias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBahias.ReadOnly = true;
            dgvBahias.AllowUserToAddRows = false;

            dgvBahias.EnableHeadersVisualStyles = false;

            // ✅ CORRECCIÓN: Cambiar SteelBlue por rojo de la paleta
            dgvBahias.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(192, 0, 0); // Rojo
            dgvBahias.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Blanco
            dgvBahias.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

            // ✅ CORRECCIÓN ADICIONAL: Configurar colores de selección
            dgvBahias.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 200, 200); // Rojo claro
            dgvBahias.DefaultCellStyle.SelectionForeColor = Color.Black;

            // ✅ CORRECCIÓN: Configurar colores de filas alternas
            dgvBahias.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250); // Gris muy claro
            dgvBahias.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // ✅ CORRECCIÓN: Configurar colores de las filas normales
            dgvBahias.DefaultCellStyle.BackColor = Color.White;
            dgvBahias.DefaultCellStyle.ForeColor = Color.Black;
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            cmbEstado.SelectedIndex = -1;
            cmbUsuario.SelectedIndex = 0;
            numCapacidad.Value = 1;

            bahiaIdSeleccionada = 0;
            modoEdicion = false;

            btnGuardar.Text = "GUARDAR";
            btnNuevo.Enabled = true;
            groupBoxDatos.Text = "NUEVA BAHÍA";
        }

        private void CargarDatosEnFormulario(Bahia bahia)
        {
            if (bahia == null) return;

            try
            {
                txtNombre.Text = bahia.Nombre;
                txtDescripcion.Text = bahia.Descripcion ?? "";
                numCapacidad.Value = bahia.Capacidad;

                // Seleccionar estado
                foreach (dynamic item in cmbEstado.Items)
                {
                    if (item.Value == bahia.EstadobahiaId)
                    {
                        cmbEstado.SelectedItem = item;
                        break;
                    }
                }

                // Seleccionar usuario/técnico con validación
                bool usuarioEncontrado = false;
                foreach (dynamic item in cmbUsuario.Items)
                {
                    if (item.Value == bahia.UsuariosId)
                    {
                        cmbUsuario.SelectedItem = item;
                        usuarioEncontrado = true;
                        break;
                    }
                }

                if (!usuarioEncontrado && bahia.UsuariosId.HasValue)
                {
                    cmbUsuario.SelectedIndex = 0;
                    MostrarAdvertenciaIntegridad(bahia);
                }
                else if (!usuarioEncontrado)
                {
                    cmbUsuario.SelectedIndex = 0;
                }

                bahiaIdSeleccionada = bahia.BahiaId;
                modoEdicion = true;
                btnGuardar.Text = "ACTUALIZAR";
                groupBoxDatos.Text = "EDITANDO BAHÍA";
            }
            catch (Exception ex)
            {
                ManejarError($"Error al cargar datos en formulario: {ex.Message}", "Error de Carga");
            }
        }

        private void MostrarAdvertenciaIntegridad(Bahia bahia)
        {
            string mensaje = $"El técnico asignado a esta bahía no está disponible en la lista actual.\n" +
                           $"Puede deberse a que:\n" +
                           $"• El técnico está inactivo\n" +
                           $"• El usuario asociado cambió de rol\n" +
                           $"• El técnico fue eliminado\n\n" +
                           $"Seleccione un técnico disponible de la lista.";

            MessageBox.Show(mensaje, "Información de Integridad",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre de la bahía es requerido", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un estado", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEstado.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("La descripción es requerida", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return false;
            }

            return true;
        }

        // ========== MÉTODOS DE MANEJO DE ERRORES ==========
        private void ManejarError(string mensaje, string titulo = "Error")
        {
            Console.WriteLine($"ERROR: {mensaje}");
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MostrarMensajeSincronizacion(string mensaje)
        {
            Console.WriteLine($"SINCRONIZACIÓN: {mensaje}");
        }

        // ========== EVENTOS DE BOTONES ==========
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            try
            {
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                int capacidad = (int)numCapacidad.Value;
                int estadoId = ((dynamic)cmbEstado.SelectedItem).Value;
                int? usuarioId = ((dynamic)cmbUsuario.SelectedItem).Value;

                string mensaje;
                bool resultado;

                if (modoEdicion)
                {
                    resultado = cnBahia.ActualizarBahiaCompleta(
                        bahiaIdSeleccionada, nombre, descripcion,
                        capacidad, estadoId, usuarioId, out mensaje);
                }
                else
                {
                    resultado = cnBahia.CrearBahiaCompleta(
                        nombre, descripcion, capacidad,
                        estadoId, usuarioId, out mensaje);
                }

                if (resultado)
                {
                    MessageBox.Show(mensaje, "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarBahias();
                    LimpiarFormulario();

                    // Notificar cambios a otros formularios
                    SincronizadorGlobal.NotificarCambioBahia();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ManejarError($"Error al guardar bahía: {ex.Message}", "Error de Guardado");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ========== EVENTOS DE FILTROS ==========
        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Usar timer para evitar búsquedas muy frecuentes
            TimerBusqueda().Start();
        }

        private System.Windows.Forms.Timer TimerBusqueda()
        {
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 500; // 500ms de delay
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                timer.Dispose();
                AplicarFiltros();
            };
            return timer;
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            cmbFiltroEstado.SelectedIndex = -1;
            txtBuscar.Text = string.Empty;
            AplicarFiltros();
        }

        // ========== EVENTO DE SELECCIÓN EN GRID ==========
        private void dgvBahias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvBahias.Rows.Count)
            {
                DataGridViewRow row = dgvBahias.Rows[e.RowIndex];

                if (row.Cells["colId"].Value != null && row.Cells["colId"].Value != DBNull.Value)
                {
                    int bahiaId = Convert.ToInt32(row.Cells["colId"].Value);
                    var bahia = todasLasBahias.FirstOrDefault(b => b.BahiaId == bahiaId);

                    if (bahia != null)
                    {
                        CargarDatosEnFormulario(bahia);
                    }
                }
            }
        }

        // ========== EVENTOS DE TECLADO ==========
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtDescripcion.Focus();
            }
        }

        // BOTÓN PARA ACTUALIZAR TÉCNICOS
        private void btnActualizarTecnicos_Click(object sender, EventArgs e)
        {
            try
            {
                CargarCombos();
                CargarBahias();
                MessageBox.Show("Lista de técnicos y bahías actualizada correctamente", "Información",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ManejarError($"Error al actualizar técnicos: {ex.Message}", "Error de Actualización");
            }
        }

        // LIMPIAR SUSCRIPCIONES AL CERRAR
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                SincronizadorGlobal.EstadoUsuarioCambiado -= OnEstadoUsuarioCambiado;
                SincronizadorGlobal.EstadoTecnicoCambiado -= OnEstadoTecnicoCambiado;
                SincronizadorGlobal.CambioBahia -= OnCambioBahia;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al limpiar suscripciones: {ex.Message}");
            }
            base.OnFormClosed(e);
        }

        // MÉTODO PARA FORZAR ACTUALIZACIÓN DESDE OTROS FORMULARIOS
        public void ForzarActualizacion()
        {
            CargarCombos();
            CargarBahias();
        }
    }
}