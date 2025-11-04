namespace Mannucci_Motors
{
    partial class frmDisponibilidad
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
            this.mcFecha = new System.Windows.Forms.MonthCalendar();
            this.pnlFiltroFecha = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvFranjas = new System.Windows.Forms.DataGridView();
            this.pnlFiltroCombo = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbBahia = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNuevaCita = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.pnlFiltroFecha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFranjas)).BeginInit();
            this.pnlFiltroCombo.SuspendLayout();
            this.SuspendLayout();
            // 
            // mcFecha
            // 
            this.mcFecha.CalendarDimensions = new System.Drawing.Size(1, 3);
            this.mcFecha.Location = new System.Drawing.Point(9, 34);
            this.mcFecha.Name = "mcFecha";
            this.mcFecha.TabIndex = 0;
            this.mcFecha.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mcFecha_DateChanged);
            // 
            // pnlFiltroFecha
            // 
            this.pnlFiltroFecha.BackColor = System.Drawing.Color.White;
            this.pnlFiltroFecha.Controls.Add(this.label1);
            this.pnlFiltroFecha.Controls.Add(this.mcFecha);
            this.pnlFiltroFecha.Location = new System.Drawing.Point(12, 12);
            this.pnlFiltroFecha.Name = "pnlFiltroFecha";
            this.pnlFiltroFecha.Size = new System.Drawing.Size(270, 501);
            this.pnlFiltroFecha.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fecha";
            // 
            // dgvFranjas
            // 
            this.dgvFranjas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFranjas.Location = new System.Drawing.Point(298, 134);
            this.dgvFranjas.Name = "dgvFranjas";
            this.dgvFranjas.Size = new System.Drawing.Size(632, 379);
            this.dgvFranjas.TabIndex = 0;
            // 
            // pnlFiltroCombo
            // 
            this.pnlFiltroCombo.BackColor = System.Drawing.Color.White;
            this.pnlFiltroCombo.Controls.Add(this.label3);
            this.pnlFiltroCombo.Controls.Add(this.cmbBahia);
            this.pnlFiltroCombo.Controls.Add(this.label2);
            this.pnlFiltroCombo.Location = new System.Drawing.Point(298, 12);
            this.pnlFiltroCombo.Name = "pnlFiltroCombo";
            this.pnlFiltroCombo.Size = new System.Drawing.Size(624, 70);
            this.pnlFiltroCombo.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Número de Bahia:";
            // 
            // cmbBahia
            // 
            this.cmbBahia.FormattingEnabled = true;
            this.cmbBahia.Location = new System.Drawing.Point(121, 31);
            this.cmbBahia.Name = "cmbBahia";
            this.cmbBahia.Size = new System.Drawing.Size(208, 21);
            this.cmbBahia.TabIndex = 4;
            this.cmbBahia.SelectedIndexChanged += new System.EventHandler(this.cmbBahia_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bahías";
            // 
            // btnNuevaCita
            // 
            this.btnNuevaCita.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(255)))));
            this.btnNuevaCita.ForeColor = System.Drawing.Color.White;
            this.btnNuevaCita.Location = new System.Drawing.Point(298, 97);
            this.btnNuevaCita.Name = "btnNuevaCita";
            this.btnNuevaCita.Size = new System.Drawing.Size(99, 31);
            this.btnNuevaCita.TabIndex = 3;
            this.btnNuevaCita.Text = "Crear Cita";
            this.btnNuevaCita.UseVisualStyleBackColor = false;
            this.btnNuevaCita.Click += new System.EventHandler(this.btnNuevaCita_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Location = new System.Drawing.Point(403, 97);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(99, 31);
            this.btnRefrescar.TabIndex = 4;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = true;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // frmDisponibilidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(942, 528);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.btnNuevaCita);
            this.Controls.Add(this.pnlFiltroCombo);
            this.Controls.Add(this.dgvFranjas);
            this.Controls.Add(this.pnlFiltroFecha);
            this.Name = "frmDisponibilidad";
            this.Text = "Disponibilidad citas";
            this.pnlFiltroFecha.ResumeLayout(false);
            this.pnlFiltroFecha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFranjas)).EndInit();
            this.pnlFiltroCombo.ResumeLayout(false);
            this.pnlFiltroCombo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar mcFecha;
        private System.Windows.Forms.Panel pnlFiltroFecha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvFranjas;
        private System.Windows.Forms.Panel pnlFiltroCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbBahia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNuevaCita;
        private System.Windows.Forms.Button btnRefrescar;
    }
}