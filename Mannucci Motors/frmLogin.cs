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
        }

        private void Logout(object sender, FormClosedEventArgs e)
        {
            txtUsuario.Text = "";
            txtContraseña.Text = "";
            this.Show();
            txtUsuario.Focus();
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            try
            {
                Usuario usuarioLogueado = cnUsuario.Login(txtUsuario.Text, txtContraseña.Text);

                if (usuarioLogueado != null)
                {
                    // Guardar la sesión del usuario y su rol
                    Sesion.UsuarioActual = usuarioLogueado;

                    // Mostrar mensaje de bienvenida (usando Username en lugar de NombresDemo)
                    MessageBox.Show($"Bienvenido, {Sesion.UsuarioActual.Username} ({Sesion.UsuarioActual.Rol})",
                                    "Login Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir el formulario de menú
                    var frmMenu = new frmMenu();
                    frmMenu.FormClosed += Logout;   // Para limpiar sesión al cerrar el formulario
                    frmMenu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Login",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}