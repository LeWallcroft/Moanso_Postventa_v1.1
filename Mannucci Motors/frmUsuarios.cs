using CapaDominio;
using CapaLogicaNegocio;
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
            // Llenar combobox de roles
            cbRol.Items.AddRange(new string[] { "Admin", "Asesor" });

            CargarUsuarios();
            HabilitarBotones(true);
        }

        private void CargarUsuarios()
        {
            try
            {
                listaUsuarios = cnUsuario.ListarUsuarios();
                dgvUsuarios.DataSource = listaUsuarios;

                // Configurar columnas
                dgvUsuarios.Columns["UsuarioId"].HeaderText = "ID";
                dgvUsuarios.Columns["UsuarioId"].Width = 50;
                dgvUsuarios.Columns["Username"].HeaderText = "Usuario";
                dgvUsuarios.Columns["Rol"].HeaderText = "Rol";
                dgvUsuarios.Columns["Rol"].Width = 100;
                dgvUsuarios.Columns["Activo"].HeaderText = "Activo";
                dgvUsuarios.Columns["Activo"].Width = 60;
                dgvUsuarios.Columns["UltimoAcceso"].HeaderText = "Último Acceso";
                dgvUsuarios.Columns["UltimoAcceso"].Width = 150;

                // Ocultar columnas
                if (dgvUsuarios.Columns.Contains("Password"))
                    dgvUsuarios.Columns["Password"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HabilitarBotones(bool estado)
        {
            btnNuevo.Enabled = estado;
            btnEditar.Enabled = estado && dgvUsuarios.SelectedRows.Count > 0;
            btnEliminar.Enabled = estado && dgvUsuarios.SelectedRows.Count > 0;
            gbDatos.Visible = !estado;
        }

        private void LimpiarFormulario()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
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
            HabilitarBotones(false);
            txtUsername.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                isNuevo = false;

                DataGridViewRow row = dgvUsuarios.SelectedRows[0];
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                cbRol.Text = row.Cells["Rol"].Value.ToString();
                chkActivo.Checked = Convert.ToBoolean(row.Cells["Activo"].Value);
                txtPassword.Text = "Dejar en blanco para no cambiar";
                txtPassword.ForeColor = Color.Gray;
                txtPassword.UseSystemPasswordChar = false;

                HabilitarBotones(false);
                txtUsername.Focus();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvUsuarios.SelectedRows[0];
                string username = row.Cells["Username"].Value.ToString();
                int usuarioId = Convert.ToInt32(row.Cells["UsuarioId"].Value);

                if (MessageBox.Show($"¿Está seguro de eliminar al usuario '{username}'?",
                                  "Confirmar Eliminación",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string mensaje;
                        bool resultado = cnUsuario.EliminarUsuario(usuarioId, out mensaje);

                        if (resultado)
                        {
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
                        MostrarMensaje($"Error al eliminar usuario: {ex.Message}", true);
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MostrarMensaje("El nombre de usuario es requerido", true);
                    txtUsername.Focus();
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

                Usuario usuario = new Usuario
                {
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text,
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
                    usuario.UsuarioId = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["UsuarioId"].Value);
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
            btnEliminar.Enabled = dgvUsuarios.SelectedRows.Count > 0;
        }
    }
}