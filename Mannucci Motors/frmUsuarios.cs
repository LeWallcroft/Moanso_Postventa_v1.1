using CapaDominio;
using CapaLogicaNegocio;
using CapaDominio.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mannucci_Motors
{
    public partial class frmUsuarios : Form
    {
        private CN_Usuario cnUsuario = new CN_Usuario();
        private List<Usuario> listaUsuarios;
        private bool isNuevo = false;

        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            // Agregar el rol "Tecnico" a la lista de roles
            cbRol.Items.AddRange(new string[] { "Administrador", "Asesor", "Tecnico" });

            CargarUsuarios();
            HabilitarBotones(true);

            // Asegurar que el GroupBox esté centrado inicialmente
            CentrarGroupBox();
        }

        // Método para centrar el GroupBox
        private void CentrarGroupBox()
        {
            gbDatos.Location = new Point(
                (this.ClientSize.Width - gbDatos.Width) / 2,
                (this.ClientSize.Height - gbDatos.Height) / 2
            );
        }

        private void CargarUsuarios()
        {
            try
            {
                listaUsuarios = cnUsuario.ListarUsuarios();
                dgvUsuarios.DataSource = listaUsuarios;

                // Nombres de columnas según nuevo modelo
                dgvUsuarios.Columns["UsuariosID"].HeaderText = "ID";
                dgvUsuarios.Columns["UsuariosID"].Width = 50;
                dgvUsuarios.Columns["Nombre"].HeaderText = "Nombre";
                dgvUsuarios.Columns["Nombre"].Width = 100;
                dgvUsuarios.Columns["Apellido"].HeaderText = "Apellido";
                dgvUsuarios.Columns["Apellido"].Width = 100;
                dgvUsuarios.Columns["Email"].HeaderText = "Email";
                dgvUsuarios.Columns["Email"].Width = 150;
                dgvUsuarios.Columns["Rol"].HeaderText = "Rol";
                dgvUsuarios.Columns["Rol"].Width = 100;
                dgvUsuarios.Columns["Activo"].HeaderText = "Activo";
                dgvUsuarios.Columns["Activo"].Width = 60;
                dgvUsuarios.Columns["UltimoLogin"].HeaderText = "Último Login";
                dgvUsuarios.Columns["UltimoLogin"].Width = 100;

                // Ocultar columnas que no queremos mostrar
                if (dgvUsuarios.Columns.Contains("PasswordHash"))
                    dgvUsuarios.Columns["PasswordHash"].Visible = false;

                // Actualizar el texto del botón basado en la selección actual
                ActualizarBotonEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para actualizar el texto del botón de estado
        private void ActualizarBotonEstado()
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvUsuarios.SelectedRows[0];
                bool estaActivo = Convert.ToBoolean(row.Cells["Activo"].Value);

                if (estaActivo)
                {
                    btnCambiarEstado.Text = "INHABILITAR";
                    btnCambiarEstado.BackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    btnCambiarEstado.Text = "HABILITAR";
                    btnCambiarEstado.BackColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
                }
            }
            else
            {
                btnCambiarEstado.Text = "CAMBIAR ESTADO";
                btnCambiarEstado.BackColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            }
        }

        private void HabilitarBotones(bool estado)
        {
            btnNuevo.Enabled = estado;
            btnEditar.Enabled = estado && dgvUsuarios.SelectedRows.Count > 0;
            btnCambiarEstado.Enabled = estado && dgvUsuarios.SelectedRows.Count > 0;
            gbDatos.Visible = !estado;

            // Controlar la visibilidad del DataGridView cuando el formulario está activo
            dgvUsuarios.Visible = estado;

            // Actualizar el botón de estado cuando se habilitan/deshabilitan los botones
            if (estado)
            {
                ActualizarBotonEstado();
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtPassword.ForeColor = Color.Black;
            txtPassword.UseSystemPasswordChar = true;
            cbRol.SelectedIndex = -1;
            chkActivo.Checked = true;
        }

        private void MostrarMensaje(string mensaje, bool esError = false)
        {
            MessageBox.Show(mensaje,
                          esError ? "Error" : "Información",
                          MessageBoxButtons.OK,
                          esError ? MessageBoxIcon.Error : MessageBoxIcon.Information);
        }

        // EVENTOS GENERADOS DESDE EL DISEÑADOR
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            isNuevo = true;
            LimpiarFormulario();

            CentrarGroupBox();
            HabilitarBotones(false);
            txtNombre.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                isNuevo = false;

                DataGridViewRow row = dgvUsuarios.SelectedRows[0];
                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtApellido.Text = row.Cells["Apellido"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                cbRol.Text = row.Cells["Rol"].Value.ToString();
                chkActivo.Checked = Convert.ToBoolean(row.Cells["Activo"].Value);
                txtPassword.Text = "Dejar en blanco para no cambiar";
                txtPassword.ForeColor = Color.Gray;
                txtPassword.UseSystemPasswordChar = false;

                CentrarGroupBox();
                HabilitarBotones(false);
                txtNombre.Focus();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario para editar.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Evento para cambiar estado (Habilitar/Inhabilitar)
        private void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvUsuarios.SelectedRows[0];
                string nombreCompleto = $"{row.Cells["Nombre"].Value} {row.Cells["Apellido"].Value}";
                int usuariosID = Convert.ToInt32(row.Cells["UsuariosID"].Value);
                bool estaActivo = Convert.ToBoolean(row.Cells["Activo"].Value);
                string rol = row.Cells["Rol"].Value.ToString();

                string accion = estaActivo ? "inhabilitar" : "habilitar";
                string mensajeConfirmacion = estaActivo ?
                    $"¿Está seguro de INHABILITAR al usuario '{nombreCompleto}'?\n\nEl usuario no podrá acceder al sistema." :
                    $"¿Está seguro de HABILITAR al usuario '{nombreCompleto}'?\n\nEl usuario podrá acceder al sistema nuevamente.";

                if (MessageBox.Show(mensajeConfirmacion,
                                  $"Confirmar {accion.ToUpper()}",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string mensaje;
                        bool resultado = cnUsuario.CambiarEstadoUsuario(usuariosID, !estaActivo, out mensaje);

                        if (resultado)
                        {
                            // NOTIFICAR EL CAMBIO A OTROS FORMULARIOS
                            SincronizadorGlobal.NotificarEstadoUsuarioCambiado(usuariosID, !estaActivo);

                            MostrarMensaje(mensaje);
                            CargarUsuarios();
                        }
                        else
                        {
                            MostrarMensaje(mensaje, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        MostrarMensaje($"Error al {accion} usuario: {ex.Message}", true);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario para cambiar su estado.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MostrarMensaje("El nombre es requerido", true);
                    txtNombre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtApellido.Text))
                {
                    MostrarMensaje("El apellido es requerido", true);
                    txtApellido.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MostrarMensaje("El email es requerido", true);
                    txtEmail.Focus();
                    return;
                }

                if (isNuevo && string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MostrarMensaje("La contraseña es requerida para nuevos usuarios", true);
                    txtPassword.Focus();
                    return;
                }

                if (cbRol.SelectedIndex == -1)
                {
                    MostrarMensaje("Debe seleccionar un rol", true);
                    cbRol.Focus();
                    return;
                }

                string password = txtPassword.Text;
                if (!isNuevo && password == "Dejar en blanco para no cambiar")
                {
                    password = "";
                }

                Usuario usuario = new Usuario
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    PasswordHash = password,
                    Rol = cbRol.Text,
                    Activo = chkActivo.Checked
                };

                string mensaje;
                bool resultado;

                if (isNuevo)
                {
                    resultado = cnUsuario.RegistrarUsuario(usuario, out mensaje);
                }
                else
                {
                    usuario.UsuariosID = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["UsuariosID"].Value);
                    resultado = cnUsuario.EditarUsuario(usuario, out mensaje);
                }

                if (resultado)
                {
                    MostrarMensaje(mensaje);
                    CargarUsuarios();
                    HabilitarBotones(true);
                    LimpiarFormulario();
                }
                else
                {
                    MostrarMensaje(mensaje, true);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al guardar usuario: {ex.Message}", true);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            HabilitarBotones(true);
            LimpiarFormulario();
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            btnEditar.Enabled = dgvUsuarios.SelectedRows.Count > 0;
            btnCambiarEstado.Enabled = dgvUsuarios.SelectedRows.Count > 0;

            // Actualizar el botón de estado cuando cambia la selección
            ActualizarBotonEstado();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (!isNuevo && txtPassword.Text == "Dejar en blanco para no cambiar")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (!isNuevo && string.IsNullOrEmpty(txtPassword.Text))
            {
                txtPassword.Text = "Dejar en blanco para no cambiar";
                txtPassword.ForeColor = Color.Gray;
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        public List<Usuario> ObtenerTecnicos()
        {
            try
            {
                return cnUsuario.ListarUsuarios().FindAll(u => u.Rol == "Tecnico" && u.Activo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener técnicos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Usuario>();
            }
        }
    }
}