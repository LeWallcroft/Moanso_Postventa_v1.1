using System.Windows.Forms;

namespace Mannucci_Motors
{
    partial class frmControlCalidad
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbResultado = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnRectificar = new System.Windows.Forms.Button();
            this.btnAprobar = new System.Windows.Forms.Button();
            this.rtxtObsControl = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.gbChecklist = new System.Windows.Forms.GroupBox();
            this.dgvChecklist = new System.Windows.Forms.DataGridView();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCumple = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colObs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbRepuestos = new System.Windows.Forms.GroupBox();
            this.txtTotalRepuestos = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvRepuestos = new System.Windows.Forms.DataGridView();
            this.gbDatosOT = new System.Windows.Forms.GroupBox();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtServicio = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNroOT = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.gbResultado.SuspendLayout();
            this.gbChecklist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChecklist)).BeginInit();
            this.gbRepuestos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepuestos)).BeginInit();
            this.gbDatosOT.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.gbResultado);
            this.panel1.Controls.Add(this.gbChecklist);
            this.panel1.Controls.Add(this.gbRepuestos);
            this.panel1.Controls.Add(this.gbDatosOT);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 600);
            this.panel1.TabIndex = 0;
            // 
            // gbResultado
            // 
            this.gbResultado.Controls.Add(this.btnCancelar);
            this.gbResultado.Controls.Add(this.btnRectificar);
            this.gbResultado.Controls.Add(this.btnAprobar);
            this.gbResultado.Controls.Add(this.rtxtObsControl);
            this.gbResultado.Controls.Add(this.label7);
            this.gbResultado.Location = new System.Drawing.Point(12, 410);
            this.gbResultado.Name = "gbResultado";
            this.gbResultado.Size = new System.Drawing.Size(776, 178);
            this.gbResultado.TabIndex = 3;
            this.gbResultado.TabStop = false;
            this.gbResultado.Text = "Resultado del control de calidad";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(681, 129);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 30);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cerrar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnRectificar
            // 
            this.btnRectificar.Location = new System.Drawing.Point(600, 129);
            this.btnRectificar.Name = "btnRectificar";
            this.btnRectificar.Size = new System.Drawing.Size(75, 30);
            this.btnRectificar.TabIndex = 5;
            this.btnRectificar.Text = "Rectificar";
            this.btnRectificar.UseVisualStyleBackColor = true;
            this.btnRectificar.Click += new System.EventHandler(this.btnRectificar_Click);
            // 
            // btnAprobar
            // 
            this.btnAprobar.Location = new System.Drawing.Point(519, 129);
            this.btnAprobar.Name = "btnAprobar";
            this.btnAprobar.Size = new System.Drawing.Size(75, 30);
            this.btnAprobar.TabIndex = 4;
            this.btnAprobar.Text = "Aprobar";
            this.btnAprobar.UseVisualStyleBackColor = true;
            this.btnAprobar.Click += new System.EventHandler(this.btnAprobar_Click);
            // 
            // rtxtObsControl
            // 
            this.rtxtObsControl.Location = new System.Drawing.Point(18, 69);
            this.rtxtObsControl.Name = "rtxtObsControl";
            this.rtxtObsControl.Size = new System.Drawing.Size(738, 54);
            this.rtxtObsControl.TabIndex = 3;
            this.rtxtObsControl.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Observaciones:";
            // 
            // gbChecklist
            // 
            this.gbChecklist.Controls.Add(this.dgvChecklist);
            this.gbChecklist.Location = new System.Drawing.Point(12, 210);
            this.gbChecklist.Name = "gbChecklist";
            this.gbChecklist.Size = new System.Drawing.Size(776, 194);
            this.gbChecklist.TabIndex = 2;
            this.gbChecklist.TabStop = false;
            this.gbChecklist.Text = "Checklist de verificación";
            // 
            // dgvChecklist
            // 
            this.dgvChecklist.AllowUserToAddRows = false;
            this.dgvChecklist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChecklist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colItem,
            this.colDescripcion,
            this.colCumple,
            this.colObs});
            this.dgvChecklist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChecklist.Location = new System.Drawing.Point(3, 16);
            this.dgvChecklist.Name = "dgvChecklist";
            this.dgvChecklist.RowHeadersVisible = false;
            this.dgvChecklist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChecklist.Size = new System.Drawing.Size(770, 175);
            this.dgvChecklist.TabIndex = 0;
            this.dgvChecklist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChecklist_CellContentClick);
            // 
            // colItem
            // 
            this.colItem.HeaderText = "Item";
            this.colItem.Name = "colItem";
            this.colItem.ReadOnly = true;
            this.colItem.Width = 50;
            // 
            // colDescripcion
            // 
            this.colDescripcion.HeaderText = "Descripción";
            this.colDescripcion.Name = "colDescripcion";
            this.colDescripcion.ReadOnly = true;
            this.colDescripcion.Width = 350;
            // 
            // colCumple
            // 
            this.colCumple.HeaderText = "Cumple";
            this.colCumple.Name = "colCumple";
            this.colCumple.Width = 80;
            // 
            // colObs
            // 
            this.colObs.HeaderText = "Observaciones";
            this.colObs.Name = "colObs";
            this.colObs.Width = 250;
            // 
            // gbRepuestos
            // 
            this.gbRepuestos.Controls.Add(this.txtTotalRepuestos);
            this.gbRepuestos.Controls.Add(this.label6);
            this.gbRepuestos.Controls.Add(this.dgvRepuestos);
            this.gbRepuestos.Location = new System.Drawing.Point(400, 12);
            this.gbRepuestos.Name = "gbRepuestos";
            this.gbRepuestos.Size = new System.Drawing.Size(388, 192);
            this.gbRepuestos.TabIndex = 1;
            this.gbRepuestos.TabStop = false;
            this.gbRepuestos.Text = "Repuestos / insumos utilizados";
            // 
            // txtTotalRepuestos
            // 
            this.txtTotalRepuestos.Location = new System.Drawing.Point(282, 161);
            this.txtTotalRepuestos.Name = "txtTotalRepuestos";
            this.txtTotalRepuestos.ReadOnly = true;
            this.txtTotalRepuestos.Size = new System.Drawing.Size(100, 20);
            this.txtTotalRepuestos.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(241, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Total:";
            // 
            // dgvRepuestos
            // 
            this.dgvRepuestos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRepuestos.Location = new System.Drawing.Point(6, 19);
            this.dgvRepuestos.Name = "dgvRepuestos";
            this.dgvRepuestos.ReadOnly = true;
            this.dgvRepuestos.Size = new System.Drawing.Size(376, 136);
            this.dgvRepuestos.TabIndex = 0;
            // 
            // gbDatosOT
            // 
            this.gbDatosOT.Controls.Add(this.txtEstado);
            this.gbDatosOT.Controls.Add(this.label5);
            this.gbDatosOT.Controls.Add(this.txtServicio);
            this.gbDatosOT.Controls.Add(this.label4);
            this.gbDatosOT.Controls.Add(this.txtPlaca);
            this.gbDatosOT.Controls.Add(this.label3);
            this.gbDatosOT.Controls.Add(this.txtCliente);
            this.gbDatosOT.Controls.Add(this.label2);
            this.gbDatosOT.Controls.Add(this.txtNroOT);
            this.gbDatosOT.Controls.Add(this.label1);
            this.gbDatosOT.Location = new System.Drawing.Point(12, 12);
            this.gbDatosOT.Name = "gbDatosOT";
            this.gbDatosOT.Size = new System.Drawing.Size(382, 192);
            this.gbDatosOT.TabIndex = 0;
            this.gbDatosOT.TabStop = false;
            this.gbDatosOT.Text = "Datos de la Orden de Trabajo";
            // 
            // txtEstado
            // 
            this.txtEstado.Location = new System.Drawing.Point(89, 151);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.ReadOnly = true;
            this.txtEstado.Size = new System.Drawing.Size(267, 20);
            this.txtEstado.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Estado:";
            // 
            // txtServicio
            // 
            this.txtServicio.Location = new System.Drawing.Point(89, 121);
            this.txtServicio.Name = "txtServicio";
            this.txtServicio.ReadOnly = true;
            this.txtServicio.Size = new System.Drawing.Size(267, 20);
            this.txtServicio.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Servicio:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(89, 91);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.ReadOnly = true;
            this.txtPlaca.Size = new System.Drawing.Size(267, 20);
            this.txtPlaca.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Placa:";
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(89, 61);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.ReadOnly = true;
            this.txtCliente.Size = new System.Drawing.Size(267, 20);
            this.txtCliente.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cliente:";
            // 
            // txtNroOT
            // 
            this.txtNroOT.Location = new System.Drawing.Point(89, 31);
            this.txtNroOT.Name = "txtNroOT";
            this.txtNroOT.ReadOnly = true;
            this.txtNroOT.Size = new System.Drawing.Size(267, 20);
            this.txtNroOT.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "N° O.T.:";
            // 
            // frmControlCalidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Name = "frmControlCalidad";
            this.Text = "Control de Calidad";
            this.panel1.ResumeLayout(false);
            this.gbResultado.ResumeLayout(false);
            this.gbResultado.PerformLayout();
            this.gbChecklist.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChecklist)).EndInit();
            this.gbRepuestos.ResumeLayout(false);
            this.gbRepuestos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepuestos)).EndInit();
            this.gbDatosOT.ResumeLayout(false);
            this.gbDatosOT.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbResultado;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnRectificar;
        private System.Windows.Forms.Button btnAprobar;
        private System.Windows.Forms.RichTextBox rtxtObsControl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox gbChecklist;
        private System.Windows.Forms.DataGridView dgvChecklist;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescripcion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCumple;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObs;
        private System.Windows.Forms.GroupBox gbRepuestos;
        private System.Windows.Forms.TextBox txtTotalRepuestos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvRepuestos;
        private System.Windows.Forms.GroupBox gbDatosOT;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtServicio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNroOT;
        private System.Windows.Forms.Label label1;
    }
}
