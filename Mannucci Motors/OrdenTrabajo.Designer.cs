
namespace Mannucci_Motors
{
    partial class OrdenTrabajo
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
            this.DGV_ORDENTRABAJO = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_cita_id = new System.Windows.Forms.TextBox();
            this.txt_estado = new System.Windows.Forms.TextBox();
            this.txt_FechaApertura = new System.Windows.Forms.TextBox();
            this.txt_HoraInicio = new System.Windows.Forms.TextBox();
            this.txt_HoraFin = new System.Windows.Forms.TextBox();
            this.txt_DuracionEstimadaMin = new System.Windows.Forms.TextBox();
            this.txt_DuracionRealMin = new System.Windows.Forms.TextBox();
            this.txt_Tecnico = new System.Windows.Forms.TextBox();
            this.txt_Observaciones = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_ORDENTRABAJO)).BeginInit();
            this.SuspendLayout();
            // 
            // DGV_ORDENTRABAJO
            // 
            this.DGV_ORDENTRABAJO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_ORDENTRABAJO.Location = new System.Drawing.Point(33, 23);
            this.DGV_ORDENTRABAJO.Name = "DGV_ORDENTRABAJO";
            this.DGV_ORDENTRABAJO.RowHeadersWidth = 51;
            this.DGV_ORDENTRABAJO.RowTemplate.Height = 24;
            this.DGV_ORDENTRABAJO.Size = new System.Drawing.Size(1191, 372);
            this.DGV_ORDENTRABAJO.TabIndex = 26;
            this.DGV_ORDENTRABAJO.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_ORDENTRABAJO_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 430);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 27;
            this.label1.Text = "Cita Id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 464);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 29;
            this.label3.Text = "Estado";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(77, 504);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 17);
            this.label4.TabIndex = 30;
            this.label4.Text = "Fecha Apertura";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(77, 545);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 17);
            this.label5.TabIndex = 31;
            this.label5.Text = "Hora de Inicio";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(77, 585);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 17);
            this.label6.TabIndex = 32;
            this.label6.Text = "Hora Fin";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(409, 430);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 17);
            this.label7.TabIndex = 33;
            this.label7.Text = "Duracion Estimada Min";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(409, 464);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 17);
            this.label8.TabIndex = 34;
            this.label8.Text = "Duracion Real Min";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(409, 499);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 17);
            this.label9.TabIndex = 35;
            this.label9.Text = "Tecnico Principal";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(409, 532);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 17);
            this.label10.TabIndex = 36;
            this.label10.Text = "Observaciones";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1016, 446);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 52);
            this.button1.TabIndex = 37;
            this.button1.Text = "Modificar OT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_cita_id
            // 
            this.txt_cita_id.Location = new System.Drawing.Point(200, 427);
            this.txt_cita_id.Name = "txt_cita_id";
            this.txt_cita_id.Size = new System.Drawing.Size(128, 22);
            this.txt_cita_id.TabIndex = 40;
            // 
            // txt_estado
            // 
            this.txt_estado.Location = new System.Drawing.Point(200, 461);
            this.txt_estado.Name = "txt_estado";
            this.txt_estado.Size = new System.Drawing.Size(128, 22);
            this.txt_estado.TabIndex = 41;
            // 
            // txt_FechaApertura
            // 
            this.txt_FechaApertura.Location = new System.Drawing.Point(200, 501);
            this.txt_FechaApertura.Name = "txt_FechaApertura";
            this.txt_FechaApertura.Size = new System.Drawing.Size(128, 22);
            this.txt_FechaApertura.TabIndex = 42;
            // 
            // txt_HoraInicio
            // 
            this.txt_HoraInicio.Location = new System.Drawing.Point(200, 542);
            this.txt_HoraInicio.Name = "txt_HoraInicio";
            this.txt_HoraInicio.Size = new System.Drawing.Size(128, 22);
            this.txt_HoraInicio.TabIndex = 43;
            // 
            // txt_HoraFin
            // 
            this.txt_HoraFin.Location = new System.Drawing.Point(200, 580);
            this.txt_HoraFin.Name = "txt_HoraFin";
            this.txt_HoraFin.Size = new System.Drawing.Size(128, 22);
            this.txt_HoraFin.TabIndex = 44;
            // 
            // txt_DuracionEstimadaMin
            // 
            this.txt_DuracionEstimadaMin.Location = new System.Drawing.Point(568, 427);
            this.txt_DuracionEstimadaMin.Name = "txt_DuracionEstimadaMin";
            this.txt_DuracionEstimadaMin.Size = new System.Drawing.Size(128, 22);
            this.txt_DuracionEstimadaMin.TabIndex = 45;
            // 
            // txt_DuracionRealMin
            // 
            this.txt_DuracionRealMin.Location = new System.Drawing.Point(568, 459);
            this.txt_DuracionRealMin.Name = "txt_DuracionRealMin";
            this.txt_DuracionRealMin.Size = new System.Drawing.Size(128, 22);
            this.txt_DuracionRealMin.TabIndex = 46;
            // 
            // txt_Tecnico
            // 
            this.txt_Tecnico.Location = new System.Drawing.Point(568, 494);
            this.txt_Tecnico.Name = "txt_Tecnico";
            this.txt_Tecnico.Size = new System.Drawing.Size(128, 22);
            this.txt_Tecnico.TabIndex = 47;
            // 
            // txt_Observaciones
            // 
            this.txt_Observaciones.Location = new System.Drawing.Point(568, 527);
            this.txt_Observaciones.Name = "txt_Observaciones";
            this.txt_Observaciones.Size = new System.Drawing.Size(128, 22);
            this.txt_Observaciones.TabIndex = 48;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(853, 446);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 52);
            this.button2.TabIndex = 49;
            this.button2.Text = "Guardar";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // OrdenTrabajo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 769);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txt_Observaciones);
            this.Controls.Add(this.txt_Tecnico);
            this.Controls.Add(this.txt_DuracionRealMin);
            this.Controls.Add(this.txt_DuracionEstimadaMin);
            this.Controls.Add(this.txt_HoraFin);
            this.Controls.Add(this.txt_HoraInicio);
            this.Controls.Add(this.txt_FechaApertura);
            this.Controls.Add(this.txt_estado);
            this.Controls.Add(this.txt_cita_id);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DGV_ORDENTRABAJO);
            this.Name = "OrdenTrabajo";
            this.Text = "OrdenTrabajo";
            this.Load += new System.EventHandler(this.OrdenTrabajo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_ORDENTRABAJO)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView DGV_ORDENTRABAJO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_cita_id;
        private System.Windows.Forms.TextBox txt_estado;
        private System.Windows.Forms.TextBox txt_FechaApertura;
        private System.Windows.Forms.TextBox txt_HoraInicio;
        private System.Windows.Forms.TextBox txt_HoraFin;
        private System.Windows.Forms.TextBox txt_DuracionEstimadaMin;
        private System.Windows.Forms.TextBox txt_DuracionRealMin;
        private System.Windows.Forms.TextBox txt_Tecnico;
        private System.Windows.Forms.TextBox txt_Observaciones;
        private System.Windows.Forms.Button button2;
    }
}