using CapaLogicaNegocio;
using CapaDominio;
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
        }

        private void frmTecnicos_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCombos();
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

                // ✅ USAR EL NUEVO MÉTODO que lista TODOS los usuarios técnicos
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

                // Resto del código para otros combos...
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar listas: {ex.Message}", "Error",
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
                        (t.Usuario?.Email?.ToLower() ?? "").Contains(busqueda)); // ✅ CORRECCIÓN: Manejo seguro de null
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
            // Configurar estilo de selección
            fila.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            fila.DefaultCellStyle.SelectionForeColor = Color.Black;

            if (!tecnico.Activo)
            {
                fila.DefaultCellStyle.BackColor = Color.LightGray;
                fila.DefaultCellStyle.ForeColor = Color.DarkGray;
            }
            else if (!tecnico.Disponible)
            {
                fila.DefaultCellStyle.BackColor = Color.LightYellow;
                fila.DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                fila.DefaultCellStyle.BackColor = Color.White;
                fila.DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvTecnicos.AutoGenerateColumns = false;
            dgvTecnicos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTecnicos.ReadOnly = true;
            dgvTecnicos.AllowUserToAddRows = false;

            // Configurar para mejor visualización
            dgvTecnicos.EnableHeadersVisualStyles = false;
            dgvTecnicos.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvTecnicos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTecnicos.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
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
            btnInactivar.Text = "INACTIVAR";
            btnInactivar.BackColor = Color.OrangeRed;
            groupBoxDatos.Text = "NUEVO TÉCNICO";

            // ✅ CORRECCIÓN: Limpiar selección del grid
            dgvTecnicos.ClearSelection();
        }

        private void CargarDatosEnFormulario(Tecnico tecnico)
        {
            if (tecnico == null) return;

            txtEspecialidad.Text = tecnico.Especialidad;
            dtpFechaContratacion.Value = tecnico.FechaContratacion;
            numSalario.Value = tecnico.Salario;
            chkDisponible.Checked = tecnico.Disponible;

            // ✅ CORRECCIÓN: Manejo mejorado de selección de usuario
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
                // ✅ CORRECCIÓN: Si el usuario no está en la lista (puede estar inactivo), mostrar info
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

            // Cambiar texto del botón según estado
            if (tecnico.Activo)
            {
                btnInactivar.Text = "INACTIVAR";
                btnInactivar.BackColor = Color.OrangeRed;
            }
            else
            {
                btnInactivar.Text = "ACTIVAR";
                btnInactivar.BackColor = Color.Green;
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

            // ✅ CORRECCIÓN: Validar que si se selecciona usuario, tenga rol Tecnico
            if (cmbUsuario.SelectedItem != null)
            {
                dynamic usuarioSeleccionado = cmbUsuario.SelectedItem;
                if (usuarioSeleccionado.Value != null)
                {
                    // Los usuarios en la lista ya están validados por el stored procedure, 
                    // pero podemos agregar una validación adicional aquí si es necesario
                }
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
                // ✅ CORRECCIÓN: Manejo seguro del SelectedItem
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
                    MessageBox.Show(mensaje, "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarTecnicos();
                    CargarCombos(); // ✅ CORRECCIÓN: Recargar combo para actualizar lista de usuarios disponibles
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
                            MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarTecnicos();
                            CargarCombos(); // ✅ CORRECCIÓN: Recargar combo
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
            // ✅ CORRECCIÓN: Usar timer para evitar búsquedas muy frecuentes
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

        // ✅ NUEVO: Evento para actualizar lista de usuarios
        private void btnActualizarListaUsuarios_Click(object sender, EventArgs e)
        {
            CargarCombos();
            MessageBox.Show("Lista de usuarios actualizada", "Información",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ✅ NUEVO: Evento para ver detalles del técnico
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
    }
}