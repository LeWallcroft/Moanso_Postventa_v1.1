namespace Mannucci_Motors
{
    partial class frmOrdenesTrabajo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrdenesTrabajo));
            this.pnlMenuOT = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTarea = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvListasOTs = new System.Windows.Forms.DataGridView();
            this.pnlMenuOT.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListasOTs)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMenuOT
            // 
            this.pnlMenuOT.BackColor = System.Drawing.Color.White;
            this.pnlMenuOT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMenuOT.Controls.Add(this.button1);
            this.pnlMenuOT.Controls.Add(this.btnTarea);
            this.pnlMenuOT.Controls.Add(this.btnEditar);
            this.pnlMenuOT.Location = new System.Drawing.Point(11, 12);
            this.pnlMenuOT.Name = "pnlMenuOT";
            this.pnlMenuOT.Size = new System.Drawing.Size(764, 77);
            this.pnlMenuOT.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(691, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 64);
            this.button1.TabIndex = 3;
            this.button1.Text = "Salir";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnTarea
            // 
            this.btnTarea.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTarea.Image = ((System.Drawing.Image)(resources.GetObject("btnTarea.Image")));
            this.btnTarea.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTarea.Location = new System.Drawing.Point(77, 7);
            this.btnTarea.Name = "btnTarea";
            this.btnTarea.Size = new System.Drawing.Size(60, 64);
            this.btnTarea.TabIndex = 2;
            this.btnTarea.Text = "Registrar tarea";
            this.btnTarea.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTarea.UseVisualStyleBackColor = true;
            this.btnTarea.Click += new System.EventHandler(this.btnTarea_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEditar.Location = new System.Drawing.Point(11, 7);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(60, 64);
            this.btnEditar.TabIndex = 0;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.dgvListasOTs);
            this.panel1.Controls.Add(this.pnlMenuOT);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(799, 486);
            this.panel1.TabIndex = 1;
            // 
            // dgvListasOTs
            // 
            this.dgvListasOTs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListasOTs.Location = new System.Drawing.Point(11, 107);
            this.dgvListasOTs.Name = "dgvListasOTs";
            this.dgvListasOTs.Size = new System.Drawing.Size(764, 335);
            this.dgvListasOTs.TabIndex = 1;
            // 
            // frmOrdenesTrabajo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(799, 485);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Name = "frmOrdenesTrabajo";
            this.Text = "Ordenes de Trabajo";
            this.pnlMenuOT.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListasOTs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenuOT;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnTarea;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvListasOTs;
    }
}