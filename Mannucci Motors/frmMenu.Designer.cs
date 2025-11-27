namespace Mannucci_Motors
{
    partial class frmMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.lblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.mnuAgenda = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAgendaDisponibilidad = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTallerDiagnostico = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTallerDiagnos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTallerPresupuestos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTallerOT = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdminUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdminBahias = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdminTecnicos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdminClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdminServicios = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdminRepuestos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdminReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.statusMain.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusMain
            // 
            this.statusMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.statusMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUser,
            this.toolStripStatusLabel1,
            this.lblDateTime,
            this.toolStripStatusLabel2,
            this.lblVersion});
            this.statusMain.Location = new System.Drawing.Point(0, 585);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(1000, 42);
            this.statusMain.TabIndex = 4;
            this.statusMain.Text = "statusStrip1";
            // 
            // lblUser
            // 
            this.lblUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(210, 32);
            this.lblUser.Text = "Usuario: - | Rol: -";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(546, 32);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // lblDateTime
            // 
            this.lblDateTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.ForeColor = System.Drawing.Color.White;
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(149, 32);
            this.lblDateTime.Text = "Fecha/Hora";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(20, 32);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(60, 32);
            this.lblVersion.Text = "v1.0";
            // 
            // menuMain
            // 
            this.menuMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.menuMain.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAgenda,
            this.mnuTallerDiagnostico,
            this.mnuAdmin});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1000, 42);
            this.menuMain.TabIndex = 3;
            this.menuMain.Text = "menuStrip1";
            // 
            // mnuAgenda
            // 
            this.mnuAgenda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAgendaDisponibilidad});
            this.mnuAgenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuAgenda.ForeColor = System.Drawing.Color.White;
            this.mnuAgenda.Name = "mnuAgenda";
            this.mnuAgenda.Size = new System.Drawing.Size(138, 38);
            this.mnuAgenda.Text = "AGENDA";
            // 
            // mnuAgendaDisponibilidad
            // 
            this.mnuAgendaDisponibilidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mnuAgendaDisponibilidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuAgendaDisponibilidad.ForeColor = System.Drawing.Color.White;
            this.mnuAgendaDisponibilidad.Name = "mnuAgendaDisponibilidad";
            this.mnuAgendaDisponibilidad.ShortcutKeyDisplayString = "Ctrl+D";
            this.mnuAgendaDisponibilidad.Size = new System.Drawing.Size(414, 44);
            this.mnuAgendaDisponibilidad.Text = "DISPONIBILIDAD";
            this.mnuAgendaDisponibilidad.Click += new System.EventHandler(this.mnuAgendaDisponibilidad_Click);
            // 
            // mnuTallerDiagnostico
            // 
            this.mnuTallerDiagnostico.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTallerDiagnos,
            this.mnuTallerPresupuestos,
            this.mnuTallerOT});
            this.mnuTallerDiagnostico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuTallerDiagnostico.ForeColor = System.Drawing.Color.White;
            this.mnuTallerDiagnostico.Name = "mnuTallerDiagnostico";
            this.mnuTallerDiagnostico.Size = new System.Drawing.Size(129, 38);
            this.mnuTallerDiagnostico.Text = "TALLER";
            // 
            // mnuTallerDiagnos
            // 
            this.mnuTallerDiagnos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mnuTallerDiagnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuTallerDiagnos.ForeColor = System.Drawing.Color.White;
            this.mnuTallerDiagnos.Name = "mnuTallerDiagnos";
            this.mnuTallerDiagnos.Size = new System.Drawing.Size(387, 44);
            this.mnuTallerDiagnos.Text = "DIAGNOSTICO";
            this.mnuTallerDiagnos.Click += new System.EventHandler(this.mnuTallerDiagnos_Click);
            // 
            // mnuTallerPresupuestos
            // 
            this.mnuTallerPresupuestos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mnuTallerPresupuestos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuTallerPresupuestos.ForeColor = System.Drawing.Color.White;
            this.mnuTallerPresupuestos.Name = "mnuTallerPresupuestos";
            this.mnuTallerPresupuestos.Size = new System.Drawing.Size(387, 44);
            this.mnuTallerPresupuestos.Text = "PRESUPUESTO";
            this.mnuTallerPresupuestos.Click += new System.EventHandler(this.mnuTallerPresupuestos_Click);
            // 
            // mnuTallerOT
            // 
            this.mnuTallerOT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mnuTallerOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuTallerOT.ForeColor = System.Drawing.Color.White;
            this.mnuTallerOT.Name = "mnuTallerOT";
            this.mnuTallerOT.Size = new System.Drawing.Size(387, 44);
            this.mnuTallerOT.Text = "ORDEN DE TRABAJO";
            this.mnuTallerOT.Click += new System.EventHandler(this.mnuTallerOT_Click);
            // 
            // mnuAdmin
            // 
            this.mnuAdmin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAdminUsuarios,
            this.mnuAdminBahias,
            this.mnuAdminTecnicos,
            this.mnuAdminClientes,
            this.mnuAdminServicios,
            this.mnuAdminRepuestos,
            this.mnuAdminReportes});
            this.mnuAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuAdmin.ForeColor = System.Drawing.Color.White;
            this.mnuAdmin.Name = "mnuAdmin";
            this.mnuAdmin.Size = new System.Drawing.Size(114, 38);
            this.mnuAdmin.Text = "ADMIN";
            // 
            // mnuAdminUsuarios
            // 
            this.mnuAdminUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mnuAdminUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuAdminUsuarios.ForeColor = System.Drawing.Color.White;
            this.mnuAdminUsuarios.Name = "mnuAdminUsuarios";
            this.mnuAdminUsuarios.Size = new System.Drawing.Size(359, 44);
            this.mnuAdminUsuarios.Text = "USUARIOS";
            this.mnuAdminUsuarios.Click += new System.EventHandler(this.mnuAdminUsuarios_Click);
            // 
            // mnuAdminBahias
            // 
            this.mnuAdminBahias.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mnuAdminBahias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuAdminBahias.ForeColor = System.Drawing.Color.White;
            this.mnuAdminBahias.Name = "mnuAdminBahias";
            this.mnuAdminBahias.Size = new System.Drawing.Size(359, 44);
            this.mnuAdminBahias.Text = "BAHIAS";
            this.mnuAdminBahias.Click += new System.EventHandler(this.mnuAdminBahias_Click);
            // 
            // mnuAdminTecnicos
            // 
            this.mnuAdminTecnicos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mnuAdminTecnicos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuAdminTecnicos.ForeColor = System.Drawing.Color.White;
            this.mnuAdminTecnicos.Name = "mnuAdminTecnicos";
            this.mnuAdminTecnicos.Size = new System.Drawing.Size(359, 44);
            this.mnuAdminTecnicos.Text = "TECNICOS";
            this.mnuAdminTecnicos.Click += new System.EventHandler(this.mnuAdminTecnicos_Click);
            // 
            // mnuAdminClientes
            // 
            this.mnuAdminClientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mnuAdminClientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuAdminClientes.ForeColor = System.Drawing.Color.White;
            this.mnuAdminClientes.Name = "mnuAdminClientes";
            this.mnuAdminClientes.Size = new System.Drawing.Size(359, 44);
            this.mnuAdminClientes.Text = "CLIENTES";
            this.mnuAdminClientes.Click += new System.EventHandler(this.mnuAdminClientes_Click);
            // 
            // mnuAdminServicios
            // 
            this.mnuAdminServicios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mnuAdminServicios.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuAdminServicios.ForeColor = System.Drawing.Color.White;
            this.mnuAdminServicios.Name = "mnuAdminServicios";
            this.mnuAdminServicios.Size = new System.Drawing.Size(359, 44);
            this.mnuAdminServicios.Text = "SERVICIOS";
            this.mnuAdminServicios.Click += new System.EventHandler(this.mnuAdminServicios_Click);
            // 
            // mnuAdminRepuestos
            // 
            this.mnuAdminRepuestos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mnuAdminRepuestos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuAdminRepuestos.ForeColor = System.Drawing.Color.White;
            this.mnuAdminRepuestos.Name = "mnuAdminRepuestos";
            this.mnuAdminRepuestos.Size = new System.Drawing.Size(359, 44);
            this.mnuAdminRepuestos.Text = "REPUESTOS";
            this.mnuAdminRepuestos.Click += new System.EventHandler(this.mnuAdminRepuestos_Click);
            // 
            // mnuAdminReportes
            // 
            this.mnuAdminReportes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mnuAdminReportes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuAdminReportes.ForeColor = System.Drawing.Color.White;
            this.mnuAdminReportes.Name = "mnuAdminReportes";
            this.mnuAdminReportes.Size = new System.Drawing.Size(359, 44);
            this.mnuAdminReportes.Text = "REPORTES";
            this.mnuAdminReportes.Click += new System.EventHandler(this.mnuAdminReportes_Click);
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.White;
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 42);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1000, 543);
            this.panelContainer.TabIndex = 5;
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 627);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menuMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuMain;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mannucci Motors - Sistema de Gestión";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMenu_FormClosing);
            this.Load += new System.EventHandler(this.frmMenu_Load);
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.ToolStripMenuItem mnuAgenda;
        private System.Windows.Forms.ToolStripMenuItem mnuAgendaDisponibilidad;
        private System.Windows.Forms.ToolStripMenuItem mnuTallerDiagnostico;
        private System.Windows.Forms.ToolStripMenuItem mnuTallerDiagnos;
        private System.Windows.Forms.ToolStripMenuItem mnuTallerPresupuestos;
        private System.Windows.Forms.ToolStripMenuItem mnuTallerOT;
        private System.Windows.Forms.ToolStripMenuItem mnuAdmin;
        private System.Windows.Forms.ToolStripMenuItem mnuAdminUsuarios;
        private System.Windows.Forms.ToolStripMenuItem mnuAdminBahias;
        private System.Windows.Forms.ToolStripMenuItem mnuAdminTecnicos;
        private System.Windows.Forms.ToolStripMenuItem mnuAdminReportes;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripStatusLabel lblUser;
        private System.Windows.Forms.ToolStripStatusLabel lblDateTime;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblVersion;
        private System.Windows.Forms.ToolStripMenuItem mnuAdminClientes;
        private System.Windows.Forms.ToolStripMenuItem mnuAdminServicios;
        private System.Windows.Forms.ToolStripMenuItem mnuAdminRepuestos;
    }
}