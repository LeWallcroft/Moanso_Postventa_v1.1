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
    public partial class frmTecnicos : Form
    {
        private CN_Tecnico cnTecnico = new CN_Tecnico();
        private CN_Usuario cnUsuario = new CN_Usuario();
        private int tecnicoIdSeleccionado = 0;
        private bool modoEdicion = false;
        private List<Tecnico> todosLosTecnicos = new List<Tecnico>();

        public frmTecnicos()
        {
            InitializeComponent();

            // Suscribirse a eventos de sincronización
            SincronizadorGlobal.EstadoUsuarioCambiado += OnEstadoUsuarioCambiado;
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
                // Verificar si algún técnico está asociado a este usuario
                var tecnicoAfectado = todosLosTecnicos.FirstOrDefault(t => t.UsuariosId == usuarioID);
                if (tecnicoAfectado != null)
                {
                    // Recargar combo de usuarios técnicos
                    CargarCombos();

                    // Recargar técnicos para reflejar cambios
                    CargarTecnicos();

                    MessageBox.Show($"Técnico '{tecnicoAfectado.NombreCompleto}' sincronizado: {(activo ? "Activado" : "Desactivado")}",
                                  "Sincronización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en sincronización técnico: {ex.Message}");
            }
        }

        private void frmTecnicos_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCombos();
                CargarComboFiltroEstado(); // ✅ NUEVO: Cargar combo de filtro
                CargarTecnicos();
                LimpiarFormulario();
                ConfigurarDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el formulario: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCombos()
        {
            try
            {
                cmbUsuario.Items.Clear();
                cmbUsuario.Items.Add(new { Text = "SIN ASIGNAR", Value = (int?)null });

                // USAR EL NUEVO MÉTODO que lista TODOS los usuarios técnicos
                var usuariosDisponibles = cnTecnico.ListarTodosUsuariosTecnicos();

                // Cargar los usuarios en el combo
                foreach (var usuario in usuariosDisponibles)
                {
                    cmbUsuario.Items.Add(new
                    {
                        Text = $"{usuario.Nombre} {usuario.Apellido} ({usuario.Email})",
                        Value = usuario.UsuariosID
                    });
                }

                cmbUsuario.DisplayMember = "Text";
                cmbUsuario.ValueMember = "Value";
                cmbUsuario.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar listas: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ NUEVO MÉTODO: Cargar combo de filtro por estado
        private void CargarComboFiltroEstado()
        {
            try
            {
                cmbFiltroEstado.Items.Clear();

                // Agregar opciones de filtro
                cmbFiltroEstado.Items.Add(new { Text = "TODOS", Value = -1 });
                cmbFiltroEstado.Items.Add(new { Text = "ACTIVOS", Value = 1 });
                cmbFiltroEstado.Items.Add(new { Text = "INACTIVOS", Value = 0 });

                cmbFiltroEstado.DisplayMember = "Text";
                cmbFiltroEstado.ValueMember = "Value";
                cmbFiltroEstado.SelectedIndex = 0; // Seleccionar "TODOS" por defecto
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar filtros: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarTecnicos()
        {
            try
            {
                todosLosTecnicos = cnTecnico.ListarTodosLosTecnicos();
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar técnicos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltros()
        {
            try
            {
                var tecnicosFiltrados = todosLosTecnicos.AsEnumerable();

                // Filtrar por estado activo/inactivo
                if (cmbFiltroEstado.SelectedItem != null)
                {
                    dynamic estadoSeleccionado = cmbFiltroEstado.SelectedItem;
                    int estadoValor = estadoSeleccionado.Value;

                    if (estadoValor != -1) // No es "TODOS"
                    {
                        bool activo = estadoValor == 1;
                        tecnicosFiltrados = tecnicosFiltrados.Where(t => t.Activo == activo);
                    }
                }

                // Filtrar por búsqueda de texto
                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    string busqueda = txtBuscar.Text.ToLower();
                    tecnicosFiltrados = tecnicosFiltrados.Where(t =>
                        t.NombreCompleto.ToLower().Contains(busqueda) ||
                        t.Especialidad.ToLower().Contains(busqueda) ||
                        (t.Usuario?.Email?.ToLower() ?? "").Contains(busqueda));
                }

                MostrarTecnicosEnGrid(tecnicosFiltrados.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar filtros: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarTecnicosEnGrid(List<Tecnico> tecnicos)
        {
            dgvTecnicos.Rows.Clear();

            foreach (var tecnico in tecnicos)
            {
                int rowIndex = dgvTecnicos.Rows.Add(
                    tecnico.TecnicoID,
                    tecnico.NombreCompleto,
                    tecnico.Especialidad,
                    tecnico.FechaContratacion.ToString("dd/MM/yyyy"),
                    tecnico.SalarioFormateado,
                    tecnico.DisponibleDisplay,
                    tecnico.ActivoDisplay
                );

                // Aplicar colores según el estado
                AplicarColorFila(dgvTecnicos.Rows[rowIndex], tecnico);
            }

            lblTotalRegistros.Text = $"Total: {tecnicos.Count} técnico(s)";
            dgvTecnicos.ClearSelection();
        }

        private void AplicarColorFila(DataGridViewRow fila, Tecnico tecnico)
        {
            // Configurar estilo de selección en rojo claro
            fila.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 192, 192);
            fila.DefaultCellStyle.SelectionForeColor = Color.Black;

            if (!tecnico.Activo)
            {
                // Técnico inactivo - Gris claro
                fila.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                fila.DefaultCellStyle.ForeColor = Color.Gray;
            }
            else if (!tecnico.Disponible)
            {
                // Técnico no disponible - Amarillo muy suave (casi blanco)
                fila.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 240);
                fila.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            }
            else
            {
                // Técnico activo y disponible - Blanco
                fila.DefaultCellStyle.BackColor = Color.White;
                fila.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvTecnicos.AutoGenerateColumns = false;
            dgvTecnicos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTecnicos.ReadOnly = true;
            dgvTecnicos.AllowUserToAddRows = false;

            // ✅ CORRECCIÓN: Usar solo rojo, gris y blanco
            dgvTecnicos.EnableHeadersVisualStyles = false;
            dgvTecnicos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(192, 0, 0); // Rojo
            dgvTecnicos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTecnicos.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

            // Configurar color de las líneas de la grilla
            dgvTecnicos.GridColor = Color.FromArgb(224, 224, 224); // Gris claro
        }

        private void LimpiarFormulario()
        {
            cmbUsuario.SelectedIndex = 0; // SIN ASIGNAR
            txtEspecialidad.Text = string.Empty;
            dtpFechaContratacion.Value = DateTime.Now;
            numSalario.Value = 0;
            chkDisponible.Checked = true;

            tecnicoIdSeleccionado = 0;
            modoEdicion = false;

            btnGuardar.Text = "GUARDAR";
            btnNuevo.Enabled = true;
            btnInactivar.Enabled = false;

            // ✅ CORRECCIÓN: Usar solo rojo y gris
            btnInactivar.Text = "INACTIVAR";
            btnInactivar.BackColor = Color.FromArgb(192, 0, 0); // Rojo
            btnInactivar.ForeColor = Color.White;

            groupBoxDatos.Text = "NUEVO TÉCNICO";

            // Limpiar selección del grid
            dgvTecnicos.ClearSelection();
        }

        private void CargarDatosEnFormulario(Tecnico tecnico)
        {
            if (tecnico == null) return;

            txtEspecialidad.Text = tecnico.Especialidad;
            dtpFechaContratacion.Value = tecnico.FechaContratacion;
            numSalario.Value = tecnico.Salario;
            chkDisponible.Checked = tecnico.Disponible;

            // Manejo mejorado de selección de usuario
            bool usuarioEncontrado = false;
            foreach (dynamic item in cmbUsuario.Items)
            {
                if (item.Value == tecnico.UsuariosId)
                {
                    cmbUsuario.SelectedItem = item;
                    usuarioEncontrado = true;
                    break;
                }
            }

            if (!usuarioEncontrado)
            {
                // Si el usuario no está en la lista (puede estar inactivo), mostrar info
                cmbUsuario.SelectedIndex = 0;
                if (tecnico.UsuariosId.HasValue)
                {
                    MessageBox.Show($"El usuario asignado a este técnico no está disponible en la lista actual.\n" +
                                  $"Puede deberse a que el usuario está inactivo o ha cambiado de rol.",
                                  "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            tecnicoIdSeleccionado = tecnico.TecnicoID;
            modoEdicion = true;
            btnGuardar.Text = "ACTUALIZAR";
            btnInactivar.Enabled = true;

            // ✅ CORRECCIÓN: Cambiar texto y color del botón según estado
            if (tecnico.Activo)
            {
                btnInactivar.Text = "INACTIVAR";
                btnInactivar.BackColor = Color.FromArgb(192, 0, 0); // Rojo
                btnInactivar.ForeColor = Color.White;
            }
            else
            {
                btnInactivar.Text = "ACTIVAR";
                btnInactivar.BackColor = Color.Gray; // Gris
                btnInactivar.ForeColor = Color.White;
            }

            groupBoxDatos.Text = $"EDITANDO TÉCNICO: {tecnico.NombreCompleto}";
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtEspecialidad.Text))
            {
                MessageBox.Show("La especialidad es requerida", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEspecialidad.Focus();
                return false;
            }

            if (txtEspecialidad.Text.Length > 200)
            {
                MessageBox.Show("La especialidad no puede tener más de 200 caracteres", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEspecialidad.Focus();
                return false;
            }

            if (numSalario.Value <= 0)
            {
                MessageBox.Show("El salario debe ser mayor a 0", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSalario.Focus();
                return false;
            }

            if (dtpFechaContratacion.Value > DateTime.Now)
            {
                MessageBox.Show("La fecha de contratación no puede ser futura", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaContratacion.Focus();
                return false;
            }

            return true;
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
                // Manejo seguro del SelectedItem
                int? usuarioId = null;
                if (cmbUsuario.SelectedItem != null)
                {
                    dynamic selectedItem = cmbUsuario.SelectedItem;
                    usuarioId = selectedItem.Value;
                }

                string especialidad = txtEspecialidad.Text.Trim();
                DateTime fechaContratacion = dtpFechaContratacion.Value;
                decimal salario = numSalario.Value;
                bool disponible = chkDisponible.Checked;

                string mensaje;
                bool resultado;

                if (modoEdicion)
                {
                    // Obtener el estado actual del técnico
                    var tecnicoActual = todosLosTecnicos.FirstOrDefault(t => t.TecnicoID == tecnicoIdSeleccionado);
                    bool activo = tecnicoActual?.Activo ?? true;

                    resultado = cnTecnico.ActualizarTecnico(
                        tecnicoIdSeleccionado, usuarioId, especialidad,
                        fechaContratacion, salario, disponible, activo, out mensaje);
                }
                else
                {
                    resultado = cnTecnico.CrearTecnico(
                        usuarioId, especialidad, fechaContratacion, salario, disponible, out mensaje);
                }

                if (resultado)
                {
                    // ✅ CORRECCIÓN: Notificar cambio SIEMPRE que se guarda un técnico
                    if (modoEdicion)
                    {
                        // Obtener el técnico actualizado
                        var tecnicoActualizado = cnTecnico.ObtenerTecnicoPorId(tecnicoIdSeleccionado);
                        if (tecnicoActualizado != null)
                        {
                            // Notificar cambio de estado del técnico
                            SincronizadorGlobal.NotificarEstadoTecnicoCambiado(tecnicoIdSeleccionado, tecnicoActualizado.Activo);

                            // Si cambió el usuario asociado, notificar también
                            if (tecnicoActualizado.UsuariosId.HasValue)
                            {
                                SincronizadorGlobal.NotificarEstadoUsuarioCambiado(tecnicoActualizado.UsuariosId.Value, true);
                            }
                        }
                    }
                    else
                    {
                        // Para técnicos nuevos, notificar creación
                        SincronizadorGlobal.NotificarCambioBahia(); // Esto actualizará formularios relacionados
                    }

                    MessageBox.Show(mensaje, "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarTecnicos();
                    CargarCombos();
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar técnico: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInactivar_Click(object sender, EventArgs e)
        {
            if (tecnicoIdSeleccionado > 0)
            {
                var tecnico = todosLosTecnicos.FirstOrDefault(t => t.TecnicoID == tecnicoIdSeleccionado);
                if (tecnico != null)
                {
                    string accion = tecnico.Activo ? "INACTIVAR" : "ACTIVAR";
                    string nombreTecnico = tecnico.NombreCompleto;

                    var confirmacion = MessageBox.Show(
                        $"¿Está seguro de {accion} al técnico '{nombreTecnico}'?",
                        $"Confirmar {accion}",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirmacion == DialogResult.Yes)
                    {
                        string mensaje;
                        bool resultado;

                        bool nuevoEstado = !tecnico.Activo;

                        if (tecnico.Activo)
                        {
                            resultado = cnTecnico.InactivarTecnico(tecnicoIdSeleccionado, out mensaje);
                        }
                        else
                        {
                            resultado = cnTecnico.ActivarTecnico(tecnicoIdSeleccionado, out mensaje);
                        }

                        if (resultado)
                        {
                            // ✅ CORRECCIÓN 1: NOTIFICAR CAMBIO DE ESTADO DEL TÉCNICO (SIEMPRE)
                            SincronizadorGlobal.NotificarEstadoTecnicoCambiado(tecnicoIdSeleccionado, nuevoEstado);

                            // ✅ CORRECCIÓN 2: SINCRONIZAR USUARIO TANTO PARA ACTIVAR COMO DESACTIVAR
                            if (tecnico.UsuariosId.HasValue)
                            {
                                string mensajeUsuario;
                                bool resultadoUsuario = cnUsuario.CambiarEstadoUsuario(
                                    tecnico.UsuariosId.Value, nuevoEstado, out mensajeUsuario);

                                if (resultadoUsuario)
                                {
                                    // ✅ CORRECCIÓN 3: NOTIFICAR CAMBIO DE ESTADO DEL USUARIO
                                    SincronizadorGlobal.NotificarEstadoUsuarioCambiado(tecnico.UsuariosId.Value, nuevoEstado);
                                }
                                else
                                {
                                    MessageBox.Show($"Técnico {accion.ToLower()} pero hubo un problema al sincronizar el usuario: {mensajeUsuario}",
                                                  "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }

                            MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarTecnicos();
                            CargarCombos();
                            LimpiarFormulario();
                        }
                        else
                        {
                            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
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
            TimerBuscar.Stop();
            TimerBuscar.Start();
        }

        private void TimerBuscar_Tick(object sender, EventArgs e)
        {
            TimerBuscar.Stop();
            AplicarFiltros();
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            cmbFiltroEstado.SelectedIndex = 0;
            txtBuscar.Text = string.Empty;
            AplicarFiltros();
        }

        // ========== EVENTO DE SELECCIÓN EN GRID ==========
        private void dgvTecnicos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvTecnicos.Rows.Count)
            {
                DataGridViewRow row = dgvTecnicos.Rows[e.RowIndex];

                if (row.Cells["colId"].Value != null)
                {
                    int tecnicoId = Convert.ToInt32(row.Cells["colId"].Value);
                    var tecnico = todosLosTecnicos.FirstOrDefault(t => t.TecnicoID == tecnicoId);

                    if (tecnico != null)
                    {
                        CargarDatosEnFormulario(tecnico);
                    }
                }
            }
        }

        // ========== EVENTOS DE TECLADO ==========
        private void txtEspecialidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                dtpFechaContratacion.Focus();
            }
        }

        // Evento para actualizar lista de usuarios
        private void btnActualizarListaUsuarios_Click(object sender, EventArgs e)
        {
            CargarCombos();
            MessageBox.Show("Lista de usuarios actualizada", "Información",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Evento para ver detalles del técnico
        private void btnVerDetalles_Click(object sender, EventArgs e)
        {
            if (tecnicoIdSeleccionado > 0)
            {
                var tecnico = todosLosTecnicos.FirstOrDefault(t => t.TecnicoID == tecnicoIdSeleccionado);
                if (tecnico != null)
                {
                    string detalles = $"ID: {tecnico.TecnicoID}\n" +
                                    $"Nombre: {tecnico.NombreCompleto}\n" +
                                    $"Especialidad: {tecnico.Especialidad}\n" +
                                    $"Fecha Contratación: {tecnico.FechaContratacion:dd/MM/yyyy}\n" +
                                    $"Salario: {tecnico.SalarioFormateado}\n" +
                                    $"Disponible: {tecnico.DisponibleDisplay}\n" +
                                    $"Estado: {tecnico.ActivoDisplay}\n" +
                                    $"Email: {tecnico.Usuario?.Email ?? "N/A"}\n" +
                                    $"Rol: {tecnico.Usuario?.Rol ?? "N/A"}";

                    MessageBox.Show(detalles, "Detalles del Técnico",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // LIMPIAR SUSCRIPCIONES AL CERRAR
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            SincronizadorGlobal.EstadoUsuarioCambiado -= OnEstadoUsuarioCambiado;
            base.OnFormClosed(e);
        }
    }
}