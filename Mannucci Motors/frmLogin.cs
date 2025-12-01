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
    public partial class frmLogin : Form
    {
        private CN_Usuario cnUsuario = new CN_Usuario();

        public static class Sesion
        {
            public static Usuario UsuarioActual { get; set; }
        }

        public frmLogin()
        {
            InitializeComponent();

            // EVITAR que el formulario se pueda redimensionar
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Configuración inicial del formulario
            this.AcceptButton = btnLogin;
            this.CancelButton = btn_Salir;

            // Establecer foco en el campo de email
            this.Load += (s, e) => txtEmail.Focus();
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrEmpty(txtEmail.Text))
                {
                    MessageBox.Show("Por favor ingrese su email.", "Validación",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtContraseña.Text))
                {
                    MessageBox.Show("Por favor ingrese su contraseña.", "Validación",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContraseña.Focus();
                    return;
                }

                // Mostrar indicador de carga
                Cursor = Cursors.WaitCursor;
                btnLogin.Enabled = false;

                // CORREGIDO: Usar Email en lugar de Username
                Usuario usuarioLogueado = cnUsuario.Login(txtEmail.Text.Trim(), txtContraseña.Text);

                if (usuarioLogueado != null)
                {
                    // CORRECCIÓN: Normalizar el rol para coincidir con la BD
                    usuarioLogueado.Rol = NormalizarRol(usuarioLogueado.Rol);

                    // Guardar la sesión del usuario en ambas ubicaciones para compatibilidad
                    Sesion.UsuarioActual = usuarioLogueado;
                    CN_Usuario.UsuarioActual = usuarioLogueado;

                    // Registrar el acceso en logs si es necesario
                    RegistrarAcceso();

                    // Abrir el formulario de menú
                    var frmMenu = new frmMenu();
                    frmMenu.FormClosed += Logout;   // Para limpiar sesión al cerrar el formulario
                    frmMenu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Email o contraseña incorrectos.", "Error de Login",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContraseña.SelectAll();
                    txtContraseña.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Restaurar controles
                Cursor = Cursors.Default;
                btnLogin.Enabled = true;
            }
        }

        // CORRECCIÓN: Método para normalizar el rol según la BD
        private string NormalizarRol(string rol)
        {
            if (string.IsNullOrEmpty(rol)) return rol;

            switch (rol.ToLower())
            {
                case "admin":
                case "administrador":
                    return "Administrador";
                case "asesor":
                    return "Asesor";
                case "tecnico":
                case "técnico":
                    return "Tecnico";
                default:
                    return rol;
            }
        }

        // CORREGIDO: Un solo método Logout
        private void Logout(object sender, FormClosedEventArgs e)
        {
            txtEmail.Text = "";
            txtContraseña.Text = "";

            // Limpiar sesión en ambas ubicaciones
            Sesion.UsuarioActual = null;
            CN_Usuario.UsuarioActual = null;

            this.Show();
            txtEmail.Focus();
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea salir del sistema?", "Confirmar salida",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // Método para registrar el acceso (opcional)
        private void RegistrarAcceso()
        {
            try
            {
                // Aquí puedes agregar lógica para registrar el acceso
                // Por ejemplo, en una tabla de logs o auditoría
                Console.WriteLine($"Usuario {Sesion.UsuarioActual.Email} accedió al sistema el {DateTime.Now}");
            }
            catch (Exception)
            {
                // Silenciosamente falla el registro de acceso
            }
        }

        // Eventos adicionales para mejor UX
        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtContraseña.Focus();
            }
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnLogin.PerformClick();
            }
        }

        // CORREGIDO: Mostrar información sobre el formato esperado
        private void linkLabelAyuda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Ingrese su dirección de email como usuario.\n\n" +
                           "Ejemplo: usuario@mannucci.com",
                           "Ayuda - Información de Login",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // CORREGIDO: Un solo método OnFormClosed
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Sesion.UsuarioActual = null;
            CN_Usuario.UsuarioActual = null;
            base.OnFormClosed(e);
        }
    }
}