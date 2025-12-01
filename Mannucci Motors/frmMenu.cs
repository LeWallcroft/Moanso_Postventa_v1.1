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

            // ✅ CORRECCIÓN: Ocultar completamente el panel container
            this.Controls.Remove(panelContainer);
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            // SOLO CAMBIO ESTO: Usar Nombre y Apellido en lugar de Username
            if (lblUser != null)
            {
                if (Sesion.UsuarioActual != null)
                {
                    string nombreCompleto = $"{Sesion.UsuarioActual.Nombre} {Sesion.UsuarioActual.Apellido}";
                    lblUser.Text = $"Usuario: {nombreCompleto} | Rol: {Sesion.UsuarioActual.Rol}";
                }
                else
                {
                    lblUser.Text = "Usuario no disponible";
                }
            }

            // Iniciar reloj (igual que antes)
            _clock = new Timer { Interval = 1000 };
            _clock.Tick += (_, __) =>
            {
                if (lblDateTime != null)
                    lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            };
            _clock.Start();

            // Aplicar permisos según el rol del usuario
            ApplyRolePermissions();

            // ✅ CORRECCIÓN: Mostrar mensaje de bienvenida en la barra de estado
            MostrarMensajeBienvenida();

            // ✅ NUEVO: Actualizar estado de los menús
            ActualizarEstadoMenus();
        }

        private void frmMenu_FormClosing(object sender, FormClosedEventArgs e)
        {
            _clock?.Stop();
            foreach (var child in this.MdiChildren)
                child.Close();
        }

        // ✅ CORREGIDO MÉTODO: Abrir un solo formulario a la vez
        private void OpenMdiSingle<T>() where T : Form, new()
        {
            var existing = this.MdiChildren.OfType<T>().FirstOrDefault();
            if (existing != null)
            {
                // ✅ Si ya está abierto, no hacer nada (evitar múltiples clics)
                existing.BringToFront();
                existing.Focus();
                return;
            }

            // ✅ CERRAR TODOS los formularios abiertos antes de abrir uno nuevo
            CerrarTodosLosFormularios();

            var frm = new T
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };

            // ✅ CORRECCIÓN: Conectar el evento FormClosed ANTES de mostrar el formulario
            frm.FormClosed += ChildForm_FormClosed;

            frm.Show();
            frm.BringToFront();
            frm.Focus();

            // ✅ Actualizar estado de menús cuando se abre un formulario
            ActualizarEstadoMenus();
        }

        // ✅ NUEVO: Evento unificado para cuando se cierra cualquier formulario hijo
        private void ChildForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // ✅ Usar BeginInvoke para evitar bloqueos
            this.BeginInvoke(new Action(() =>
            {
                ActualizarEstadoMenus();
                MostrarMensajeBienvenida();
            }));
        }

        // ✅ NUEVO: Método para cerrar todos los formularios MDI
        private void CerrarTodosLosFormularios()
        {
            // Crear una lista temporal para evitar modificar la colección durante la iteración
            var formsToClose = this.MdiChildren.OfType<Form>().ToList();

            foreach (var form in formsToClose)
            {
                form.Close();
            }
        }

        // ✅ CORREGIDO COMPLETAMENTE: Método para actualizar el estado de los menús
        private void ActualizarEstadoMenus()
        {
            // ✅ VERIFICACIÓN SIMPLE: Si no hay formularios, habilitar todo según permisos
            if (this.MdiChildren.Length == 0)
            {
                HabilitarTodosLosMenus();
                return;
            }

            // ✅ SI HAY FORMULARIOS: Solo deshabilitar temporalmente durante la navegación
            // pero permitir cambiar a otros formularios
            var formularioActual = this.MdiChildren[0];

            // ✅ PARA ADMINISTRADOR: Siempre permitir cambiar entre módulos
            if (Sesion.UsuarioActual.Rol.ToLower() == "administrador")
            {
                // Administrador tiene acceso completo, no deshabilitar menús
                HabilitarTodosLosMenus();
                return;
            }

            // ✅ PARA OTROS ROLES: Lógica original de deshabilitar menús no correspondientes
            DeshabilitarMenusNoCorrespondientes(formularioActual);
        }

        // ✅ NUEVO: Método para deshabilitar solo menús no correspondientes (para no administradores)
        private void DeshabilitarMenusNoCorrespondientes(Form formularioActual)
        {
            // Solo aplicar esta lógica para roles que no son administrador
            var rol = Sesion.UsuarioActual.Rol.ToLower();
            if (rol == "administrador") return;

            // Habilitar todos primero
            HabilitarTodosLosMenus();

            // Luego deshabilitar los que no correspondan
            if (formularioActual is frmDisponibilidad)
            {
                // Si está en Agenda, deshabilitar otros módulos
                if (mnuTallerDiagnostico != null) mnuTallerDiagnostico.Enabled = false;
                if (mnuAdmin != null) mnuAdmin.Enabled = false;
            }
            else if (formularioActual is frmServicios)
            {
                // Si está en Servicios, deshabilitar otros módulos
                if (mnuAgenda != null) mnuAgenda.Enabled = false;
                if (mnuTallerDiagnostico != null) mnuTallerDiagnostico.Enabled = false;
                if (mnuAdmin != null) mnuAdmin.Enabled = false;
            }
            else if ( formularioActual is frmOrdenesTrabajo)
            {
                // Si está en Taller, deshabilitar otros módulos
                if (mnuAgenda != null) mnuAgenda.Enabled = false;
                if (mnuAdmin != null) mnuAdmin.Enabled = false;
            }
            else if (formularioActual is frmUsuarios || formularioActual is frmTecnicos ||
                     formularioActual is frmBahias || formularioActual is frmGestionClientes)
            {
                // Si está en Admin, deshabilitar otros módulos (solo para no administradores)
                if (mnuAgenda != null) mnuAgenda.Enabled = false;
                if (mnuTallerDiagnostico != null) mnuTallerDiagnostico.Enabled = false;
            }
        }

        // ✅ ELIMINADO: Método DeshabilitarTodosLosMenus (ya no se usa)

        // ✅ CORREGIDO: Habilitar todos los menús principales según permisos
        private void HabilitarTodosLosMenus()
        {
            var rol = Sesion.UsuarioActual.Rol.ToLower();

            // ✅ ADMINISTRADOR: Acceso completo a todos los menús
            if (rol == "administrador")
            {
                if (mnuAgenda != null) mnuAgenda.Enabled = true;
                if (mnuTallerDiagnostico != null) mnuTallerDiagnostico.Enabled = true;
                if (mnuAdmin != null) mnuAdmin.Enabled = true;
                return;
            }

            // ✅ ASESOR: Acceso a todo excepto Admin
            if (rol == "asesor")
            {
                if (mnuAgenda != null) mnuAgenda.Enabled = true;
                if (mnuTallerDiagnostico != null) mnuTallerDiagnostico.Enabled = true;
                if (mnuAdmin != null) mnuAdmin.Enabled = false;
                return;
            }

            // ✅ TÉCNICO: Sin acceso
            if (rol == "tecnico")
            {
                if (mnuAgenda != null) mnuAgenda.Enabled = false;
                if (mnuTallerDiagnostico != null) mnuTallerDiagnostico.Enabled = false;
                if (mnuAdmin != null) mnuAdmin.Enabled = false;
                return;
            }
        }

        // ✅ ELIMINADO: Método HabilitarMenuCorrespondiente (reemplazado por nueva lógica)

        // ✅ CORREGIDO: Método para mostrar mensaje de bienvenida
        private void MostrarMensajeBienvenida()
        {
            if (lblDateTime != null && this.MdiChildren.Length == 0)
            {
                var originalText = lblDateTime.Text;

                var rol = Sesion.UsuarioActual.Rol.ToLower();
                if (rol == "tecnico")
                {
                    lblDateTime.Text = "Usuario Técnico - Acceso limitado al sistema";
                }
                else
                {
                    lblDateTime.Text = "Bienvenido al Sistema Mannucci Motors - Seleccione una opción del menú";
                }

                var timer = new Timer { Interval = 5000 };
                timer.Tick += (s, e) =>
                {
                    if (lblDateTime != null && this.MdiChildren.Length == 0)
                    {
                        lblDateTime.Text = originalText;
                    }
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
        }

        private void ApplyRolePermissions()
        {
            // ✅ CORRECCIÓN: Permisos según tu especificación
            var rol = Sesion.UsuarioActual.Rol.ToLower();

            if (rol == "administrador")
            {
                // Administrador: acceso a todo
                if (mnuAgenda != null) mnuAgenda.Visible = true;
                if (mnuTallerDiagnostico != null) mnuTallerDiagnostico.Visible = true;
                if (mnuAdmin != null) mnuAdmin.Visible = true;
            }
            else if (rol == "asesor")
            {
                // Asesor: acceso a todo excepto Admin
                if (mnuAgenda != null) mnuAgenda.Visible = true;
                if (mnuTallerDiagnostico != null) mnuTallerDiagnostico.Visible = true;
                if (mnuAdmin != null) mnuAdmin.Visible = false;
            }
            else if (rol == "tecnico")
            {
                // Técnico: NO tiene acceso al sistema
                if (mnuAgenda != null) mnuAgenda.Visible = false;
                if (mnuTallerDiagnostico != null) mnuTallerDiagnostico.Visible = false;
                if (mnuAdmin != null) mnuAdmin.Visible = false;
            }
        }


        // ========== EVENTOS DE MENÚ ==========
        private void mnuAgendaDisponibilidad_Click(object sender, EventArgs e)
        {
            if (Sesion.UsuarioActual.Rol.ToLower() == "tecnico")
            {
                MostrarMensajeAccesoDenegado();
                return;
            }
            OpenMdiSingle<frmNuevaCita>();
        }
           
        private void mnuTallerPresupuestos_Click(object sender, EventArgs e)
        {
            if (Sesion.UsuarioActual.Rol.ToLower() == "tecnico")
            {
                MostrarMensajeAccesoDenegado();
                return;
            }
            // Mantengo tu código original
        }

        private void mnuTallerOT_Click(object sender, EventArgs e)
        {
            if (Sesion.UsuarioActual.Rol.ToLower() == "tecnico")
            {
                MostrarMensajeAccesoDenegado();
                return;
            }
            OpenMdiSingle<frmOrdenesTrabajo>();
        }

        private void mnuAdminUsuarios_Click(object sender, EventArgs e)
        {
            if (Sesion.UsuarioActual.Rol.ToLower() != "administrador")
            {
                MostrarMensajeAccesoDenegado();
                return;
            }
            OpenMdiSingle<frmUsuarios>();
        }

        private void mnuAdminBahias_Click(object sender, EventArgs e)
        {
            if (Sesion.UsuarioActual.Rol.ToLower() != "administrador")
            {
                MostrarMensajeAccesoDenegado();
                return;
            }
            OpenMdiSingle<frmBahias>();
        }

        private void mnuAdminTecnicos_Click(object sender, EventArgs e)
        {
            if (Sesion.UsuarioActual.Rol.ToLower() != "administrador")
            {
                MostrarMensajeAccesoDenegado();
                return;
            }
            OpenMdiSingle<frmTecnicos>();
        }

        private void mnuAdminReportes_Click(object sender, EventArgs e)
        {
            if (Sesion.UsuarioActual.Rol.ToLower() != "administrador")
            {
                MostrarMensajeAccesoDenegado();
                return;
            }
            OpenMdiSingle<frmReportes>();
        }

        // ✅ NUEVOS EVENTOS PARA LOS MENÚS AGREGADOS
        private void mnuAdminClientes_Click(object sender, EventArgs e)
        {
            if (Sesion.UsuarioActual.Rol.ToLower() != "administrador")
            {
                MostrarMensajeAccesoDenegado();
                return;
            }
            OpenMdiSingle<frmGestionClientes>();
        }

        private void mnuAdminServicios_Click(object sender, EventArgs e)
        {
            if (Sesion.UsuarioActual.Rol.ToLower() != "administrador")
            {
                MostrarMensajeAccesoDenegado();
                return;
            }
            OpenMdiSingle<frmServicios>();
        }

        private void mnuAdminRepuestos_Click(object sender, EventArgs e)
        {
            if (Sesion.UsuarioActual.Rol.ToLower() != "administrador")
            {
                MostrarMensajeAccesoDenegado();
                return;
            }
            OpenMdiSingle<frmRepuestos>();
        }

        // ✅ NUEVO: Método para mostrar mensaje de acceso denegado
        private void MostrarMensajeAccesoDenegado()
        {
            MessageBox.Show("No tiene permisos para acceder a esta función.",
                          "Acceso Denegado",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Warning);
        }

        // ✅ NUEVO: Verificar si hay formularios abiertos al hacer clic en áreas vacías
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (this.MdiChildren.Length == 0 && e.Button == MouseButtons.Left)
            {
                MostrarMensajeBienvenida();
            }
        }
    }
}