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
            this.lblDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnuAgenda = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAgendaDisponibilidad = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuServicios = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuServiciosCatalogo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTallerDiagnostico = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTallerDiagnos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTallerPresupuestos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTallerOT = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdminUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdminBahias = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdminTecnicos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdminReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.statusMain.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusMain
            // 
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUser,
            this.lblDateTime});
            this.statusMain.Location = new System.Drawing.Point(0, 555);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(936, 22);
            this.statusMain.TabIndex = 4;
            this.statusMain.Text = "statusStrip1";
            // 
            // lblUser
            // 
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(95, 17);
            this.lblUser.Text = "Usuario: - | Rol: -";
            // 
            // lblDateTime
            // 
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(69, 17);
            this.lblDateTime.Text = "Fecha/Hora";
            // 
            // mnuAgenda
            // 
            this.mnuAgenda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAgendaDisponibilidad});
            this.mnuAgenda.Name = "mnuAgenda";
            this.mnuAgenda.Size = new System.Drawing.Size(66, 20);
            this.mnuAgenda.Text = "AGENDA";
            // 
            // mnuAgendaDisponibilidad
            // 
            this.mnuAgendaDisponibilidad.Name = "mnuAgendaDisponibilidad";
            this.mnuAgendaDisponibilidad.ShortcutKeyDisplayString = "Ctrl+D";
            this.mnuAgendaDisponibilidad.Size = new System.Drawing.Size(204, 22);
            this.mnuAgendaDisponibilidad.Text = "DISPONIBILIDAD";
            this.mnuAgendaDisponibilidad.Click += new System.EventHandler(this.mnuAgendaDisponibilidad_Click);
            // 
            // mnuServicios
            // 
            this.mnuServicios.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuServiciosCatalogo});
            this.mnuServicios.Name = "mnuServicios";
            this.mnuServicios.Size = new System.Drawing.Size(74, 20);
            this.mnuServicios.Text = "SERVICIOS";
            // 
            // mnuServiciosCatalogo
            // 
            this.mnuServiciosCatalogo.Name = "mnuServiciosCatalogo";
            this.mnuServiciosCatalogo.Size = new System.Drawing.Size(135, 22);
            this.mnuServiciosCatalogo.Text = "CATALOGO";
            this.mnuServiciosCatalogo.Click += new System.EventHandler(this.mnuServiciosCatalogo_Click);
            // 
            // mnuTallerDiagnostico
            // 
            this.mnuTallerDiagnostico.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTallerDiagnos,
            this.mnuTallerPresupuestos,
            this.mnuTallerOT});
            this.mnuTallerDiagnostico.Name = "mnuTallerDiagnostico";
            this.mnuTallerDiagnostico.Size = new System.Drawing.Size(58, 20);
            this.mnuTallerDiagnostico.Text = "TALLER";
            // 
            // mnuTallerDiagnos
            // 
            this.mnuTallerDiagnos.Name = "mnuTallerDiagnos";
            this.mnuTallerDiagnos.Size = new System.Drawing.Size(184, 22);
            this.mnuTallerDiagnos.Text = "DIAGNOSTICO";
            this.mnuTallerDiagnos.Click += new System.EventHandler(this.mnuTallerDiagnos_Click);
            // 
            // mnuTallerPresupuestos
            // 
            this.mnuTallerPresupuestos.Name = "mnuTallerPresupuestos";
            this.mnuTallerPresupuestos.Size = new System.Drawing.Size(184, 22);
            this.mnuTallerPresupuestos.Text = "PRESUPUESTO";
            this.mnuTallerPresupuestos.Click += new System.EventHandler(this.mnuTallerPresupuestos_Click);
            // 
            // mnuTallerOT
            // 
            this.mnuTallerOT.Name = "mnuTallerOT";
            this.mnuTallerOT.Size = new System.Drawing.Size(184, 22);
            this.mnuTallerOT.Text = "ORDEN DE TRABAJO";
            this.mnuTallerOT.Click += new System.EventHandler(this.mnuTallerOT_Click);
            // 
            // mnuAdmin
            // 
            this.mnuAdmin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAdminUsuarios,
            this.mnuAdminBahias,
            this.mnuAdminTecnicos,
            this.mnuAdminReportes});
            this.mnuAdmin.Name = "mnuAdmin";
            this.mnuAdmin.Size = new System.Drawing.Size(58, 20);
            this.mnuAdmin.Text = "ADMIN";
            // 
            // mnuAdminUsuarios
            // 
            this.mnuAdminUsuarios.Name = "mnuAdminUsuarios";
            this.mnuAdminUsuarios.Size = new System.Drawing.Size(130, 22);
            this.mnuAdminUsuarios.Text = "USUARIOS";
            this.mnuAdminUsuarios.Click += new System.EventHandler(this.mnuAdminUsuarios_Click);
            // 
            // mnuAdminBahias
            // 
            this.mnuAdminBahias.Name = "mnuAdminBahias";
            this.mnuAdminBahias.Size = new System.Drawing.Size(130, 22);
            this.mnuAdminBahias.Text = "BAHIAS";
            this.mnuAdminBahias.Click += new System.EventHandler(this.mnuAdminBahias_Click);
            // 
            // mnuAdminTecnicos
            // 
            this.mnuAdminTecnicos.Name = "mnuAdminTecnicos";
            this.mnuAdminTecnicos.Size = new System.Drawing.Size(130, 22);
            this.mnuAdminTecnicos.Text = "TECNICOS";
            this.mnuAdminTecnicos.Click += new System.EventHandler(this.mnuAdminTecnicos_Click);
            // 
            // mnuAdminReportes
            // 
            this.mnuAdminReportes.Name = "mnuAdminReportes";
            this.mnuAdminReportes.Size = new System.Drawing.Size(130, 22);
            this.mnuAdminReportes.Text = "REPORTES";
            this.mnuAdminReportes.Click += new System.EventHandler(this.mnuAdminReportes_Click);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAgenda,
            this.mnuServicios,
            this.mnuTallerDiagnostico,
            this.mnuAdmin});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuMain.Size = new System.Drawing.Size(936, 24);
            this.menuMain.TabIndex = 3;
            this.menuMain.Text = "menuStrip1";
            this.menuMain.Click += new System.EventHandler(this.frmMenu_Load);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(936, 577);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menuMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Taller - Sistema";
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
        private System.Windows.Forms.ToolStripMenuItem mnuServicios;
        private System.Windows.Forms.ToolStripMenuItem mnuServiciosCatalogo;
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
    }
}