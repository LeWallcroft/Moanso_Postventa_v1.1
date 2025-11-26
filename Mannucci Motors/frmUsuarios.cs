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
            // CAMBIO: Agregar el rol "Tecnico" a la lista de roles
            cbRol.Items.AddRange(new string[] { "Administrador", "Asesor", "Tecnico" });

            CargarUsuarios();
            HabilitarBotones(true);
        }

        private void CargarUsuarios()
        {
            try
            {
                listaUsuarios = cnUsuario.ListarUsuarios();
                dgvUsuarios.DataSource = listaUsuarios;

                // SOLO CAMBIO: Nombres de columnas según nuevo modelo
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
                dgvUsuarios.Columns["UltimoLogin"].Width = 150;

                // Ocultar columnas que no queremos mostrar
                if (dgvUsuarios.Columns.Contains("PasswordHash"))
                    dgvUsuarios.Columns["PasswordHash"].Visible = false;
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
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
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

                HabilitarBotones(false);
                txtNombre.Focus();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvUsuarios.SelectedRows[0];
                string nombreCompleto = $"{row.Cells["Nombre"].Value} {row.Cells["Apellido"].Value}";
                int usuariosID = Convert.ToInt32(row.Cells["UsuariosID"].Value);

                if (MessageBox.Show($"¿Está seguro de eliminar al usuario '{nombreCompleto}'?",
                                  "Confirmar Eliminación",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string mensaje;
                        bool resultado = cnUsuario.EliminarUsuario(usuariosID, out mensaje);

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

                Usuario usuario = new Usuario
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    PasswordHash = txtPassword.Text,
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
            btnEliminar.Enabled = dgvUsuarios.SelectedRows.Count > 0;
        }

        // CAMBIO: Método para filtrar usuarios por rol (opcional, si quieres agregar filtros)
        private void FiltrarUsuariosPorRol(string rol)
        {
            if (listaUsuarios != null)
            {
                if (string.IsNullOrEmpty(rol))
                {
                    dgvUsuarios.DataSource = listaUsuarios;
                }
                else
                {
                    var usuariosFiltrados = listaUsuarios.FindAll(u => u.Rol == rol);
                    dgvUsuarios.DataSource = usuariosFiltrados;
                }
            }
        }

        // CAMBIO: Método para obtener solo técnicos (útil para otros formularios)
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