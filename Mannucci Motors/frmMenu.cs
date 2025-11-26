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
            // SOLO CAMBIO ESTO: Usar Nombre y Apellido en lugar de Username
            if (lblUser != null)
            {
                string nombreCompleto = $"{Sesion.UsuarioActual.Nombre} {Sesion.UsuarioActual.Apellido}";
                lblUser.Text = $"Usuario: {nombreCompleto} | Rol: {Sesion.UsuarioActual.Rol}";
            }

            // Iniciar reloj (igual que antes)
            _clock = new Timer { Interval = 1000 };
            _clock.Tick += (_, __) =>
            {
                if (lblDateTime != null)
                    lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            };
            _clock.Start();

            // Aplicar permisos según el rol del usuario (igual que antes)
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
            // CORREGIDO: Usar los roles correctos en minúscula
            if (Sesion.UsuarioActual.Rol.ToLower() == "administrador")
            {
                if (mnuAdmin != null) mnuAdmin.Visible = true;
            }
            else
            {
                if (mnuAdmin != null) mnuAdmin.Visible = false;
            }

            // Personalización adicional para asesor
            if (Sesion.UsuarioActual.Rol.ToLower() == "asesor")
            {
                if (mnuAdmin != null) mnuAdmin.Visible = false;
            }
        }

        // LOS MÉTODOS DE LOS MENÚS SE MANTIENEN EXACTAMENTE IGUAL
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
            OpenMdiSingle<frmDiagnostico>();
        }

        private void mnuTallerPresupuestos_Click(object sender, EventArgs e)
        {
            // Mantengo tu código original
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
            using (var frm = new frmBahias())
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog(this);
            }
        }

        private void mnuAdminTecnicos_Click(object sender, EventArgs e)
        {
            using (var frm = new frmTecnicos())
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog(this);
            }
        }
        private void mnuAdminReportes_Click(object sender, EventArgs e)
        {
            // Mantengo tu código original
        }
    }
}