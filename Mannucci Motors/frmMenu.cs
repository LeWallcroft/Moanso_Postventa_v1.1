using CapaPresentacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Mannucci_Motors.frmLogin;

namespace Mannucci_Motors
{

    public partial class frmMenu : Form
    {
        private Timer _clock;

        public frmMenu()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;

        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            // Mostrar nombre de usuario y rol desde la sesión guardada en frmLogin
            if (lblUser != null)
                lblUser.Text = $"Usuario: {Sesion.UsuarioActual.Username} | Rol: {Sesion.UsuarioActual.Rol}";

            // Iniciar reloj
            _clock = new Timer { Interval = 1000 };
            _clock.Tick += (_, __) =>
            {
                if (lblDateTime != null)
                    lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            };
            _clock.Start();

            // Aplicar permisos según el rol del usuario
            ApplyRolePermissions();
        }

        private void frmMenu_FormClosing(object sender, FormClosedEventArgs e)
        {
            _clock?.Stop();
            foreach (var child in this.MdiChildren)
                child.Close();
        }

        private T OpenMdiSingle<T>() where T : Form, new()
        {
            var existing = this.MdiChildren.OfType<T>().FirstOrDefault();
            if (existing != null)
            {
                existing.WindowState = FormWindowState.Normal;
                existing.BringToFront();
                existing.Activate();
                return existing;
            }

            var frm = new T
            {
                MdiParent = this,
                StartPosition = FormStartPosition.Manual,
                WindowState = FormWindowState.Normal
            };

            frm.Show();

            int x = (this.ClientSize.Width - frm.Width) / 2;
            int y = (this.ClientSize.Height - frm.Height) / 2;

            frm.Location = new Point(Math.Max(0, x), Math.Max(0, y));

            if (this.menuMain != null)
                this.menuMain.Enabled = false;

            frm.FormClosed += (s, e) =>
            {
                if (this.menuMain != null)
                    this.menuMain.Enabled = true;
            };

            frm.BringToFront();
            frm.Activate();

            return frm;
        }


        private void ApplyRolePermissions()
        {
            // Verificar si el rol es Admin y mostrar u ocultar la pestaña de Admin
            if (Sesion.UsuarioActual.Rol == "Admin")
            {
                if (mnuAdmin != null) mnuAdmin.Visible = true;  // Mostrar la pestaña Admin
            }
            else
            {
                if (mnuAdmin != null) mnuAdmin.Visible = false; // Ocultar la pestaña Admin
            }

            // Personalización adicional dependiendo del rol del usuario
            if (Sesion.UsuarioActual.Rol == "Asesor")
            {
                if (mnuAdmin != null) mnuAdmin.Visible = false;
            }
        }

        private void mnuAgendaDisponibilidad_Click(object sender, EventArgs e)
        {
            OpenMdiSingle<frmDisponibilidad>();
        }

        private void mnuServiciosCatalogo_Click(object sender, EventArgs e)
        {
            OpenMdiSingle<frmCatalogo>();
        }

        private void mnuTallerDiagnos_Click(object sender, EventArgs e)
        {

        }

        private void mnuTallerPresupuestos_Click(object sender, EventArgs e)
        {

        }

        private void mnuTallerOT_Click(object sender, EventArgs e)
        {
            OpenMdiSingle<OrdenTrabajo>();
        }

        private void mnuAdminUsuarios_Click(object sender, EventArgs e)
        {
            OpenMdiSingle<frmUsuarios>();
        }

        private void mnuAdminBahias_Click(object sender, EventArgs e)
        {
            OpenMdiSingle<frmBahias>();
        }

        private void mnuAdminTecnicos_Click(object sender, EventArgs e)
        {

        }

        private void mnuAdminReportes_Click(object sender, EventArgs e)
        {

        }
    }
}