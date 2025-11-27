namespace CapaPresentacion
{
    partial class frmBahias
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelControles;
        private System.Windows.Forms.GroupBox groupBoxDatos;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numCapacidad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbUsuario;
        private System.Windows.Forms.GroupBox groupBoxAcciones;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DataGridView dgvBahias;
        private System.Windows.Forms.Panel panelFiltros;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbFiltroEstado;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnLimpiarFiltros;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelControles = new System.Windows.Forms.Panel();
            this.groupBoxDatos = new System.Windows.Forms.GroupBox();
            this.cmbUsuario = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numCapacidad = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxAcciones = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.dgvBahias = new System.Windows.Forms.DataGridView();
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbFiltroEstado = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCapacidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelHeader.SuspendLayout();
            this.panelControles.SuspendLayout();
            this.groupBoxDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidad)).BeginInit();
            this.groupBoxAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBahias)).BeginInit();
            this.panelFiltros.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelHeader.Controls.Add(this.lblTitulo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(2400, 115);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(2400, 115);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "GESTIÓN DE BAHÍAS";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelControles
            // 
            this.panelControles.BackColor = System.Drawing.Color.White;
            this.panelControles.Controls.Add(this.groupBoxDatos);
            this.panelControles.Controls.Add(this.groupBoxAcciones);
            this.panelControles.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControles.Location = new System.Drawing.Point(0, 115);
            this.panelControles.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panelControles.Name = "panelControles";
            this.panelControles.Size = new System.Drawing.Size(800, 1231);
            this.panelControles.TabIndex = 1;
            // 
            // groupBoxDatos
            // 
            this.groupBoxDatos.BackColor = System.Drawing.Color.White;
            this.groupBoxDatos.Controls.Add(this.cmbUsuario);
            this.groupBoxDatos.Controls.Add(this.label6);
            this.groupBoxDatos.Controls.Add(this.cmbEstado);
            this.groupBoxDatos.Controls.Add(this.label5);
            this.groupBoxDatos.Controls.Add(this.numCapacidad);
            this.groupBoxDatos.Controls.Add(this.label4);
            this.groupBoxDatos.Controls.Add(this.txtDescripcion);
            this.groupBoxDatos.Controls.Add(this.label3);
            this.groupBoxDatos.Controls.Add(this.txtNombre);
            this.groupBoxDatos.Controls.Add(this.label1);
            this.groupBoxDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDatos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxDatos.Location = new System.Drawing.Point(0, 0);
            this.groupBoxDatos.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBoxDatos.Name = "groupBoxDatos";
            this.groupBoxDatos.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBoxDatos.Size = new System.Drawing.Size(800, 943);
            this.groupBoxDatos.TabIndex = 0;
            this.groupBoxDatos.TabStop = false;
            this.groupBoxDatos.Text = "DATOS DE LA BAHÍA";
            // 
            // cmbUsuario
            // 
            this.cmbUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUsuario.ForeColor = System.Drawing.Color.Black;
            this.cmbUsuario.FormattingEnabled = true;
            this.cmbUsuario.Location = new System.Drawing.Point(240, 564);
            this.cmbUsuario.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmbUsuario.Name = "cmbUsuario";
            this.cmbUsuario.Size = new System.Drawing.Size(496, 37);
            this.cmbUsuario.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(40, 570);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 29);
            this.label6.TabIndex = 10;
            this.label6.Text = "Usuario Asign.:";
            // 
            // cmbEstado
            // 
            this.cmbEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEstado.ForeColor = System.Drawing.Color.Black;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(240, 391);
            this.cmbEstado.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(496, 37);
            this.cmbEstado.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(40, 397);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 29);
            this.label5.TabIndex = 8;
            this.label5.Text = "Estado:";
            // 
            // numCapacidad
            // 
            this.numCapacidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCapacidad.ForeColor = System.Drawing.Color.Black;
            this.numCapacidad.Location = new System.Drawing.Point(240, 478);
            this.numCapacidad.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.numCapacidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCapacidad.Name = "numCapacidad";
            this.numCapacidad.Size = new System.Drawing.Size(160, 35);
            this.numCapacidad.TabIndex = 7;
            this.numCapacidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(40, 482);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 29);
            this.label4.TabIndex = 6;
            this.label4.Text = "Capacidad:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcion.Location = new System.Drawing.Point(240, 173);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(496, 195);
            this.txtDescripcion.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(40, 179);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Descripción:";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.ForeColor = System.Drawing.Color.Black;
            this.txtNombre.Location = new System.Drawing.Point(240, 77);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(496, 35);
            this.txtNombre.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(40, 83);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre:";
            // 
            // groupBoxAcciones
            // 
            this.groupBoxAcciones.BackColor = System.Drawing.Color.White;
            this.groupBoxAcciones.Controls.Add(this.btnCerrar);
            this.groupBoxAcciones.Controls.Add(this.btnCancelar);
            this.groupBoxAcciones.Controls.Add(this.btnGuardar);
            this.groupBoxAcciones.Controls.Add(this.btnNuevo);
            this.groupBoxAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxAcciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAcciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxAcciones.Location = new System.Drawing.Point(0, 943);
            this.groupBoxAcciones.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBoxAcciones.Name = "groupBoxAcciones";
            this.groupBoxAcciones.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBoxAcciones.Size = new System.Drawing.Size(800, 288);
            this.groupBoxAcciones.TabIndex = 1;
            this.groupBoxAcciones.TabStop = false;
            this.groupBoxAcciones.Text = "ACCIONES";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Gray;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(400, 163);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(340, 87);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "CERRAR";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Gray;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(40, 163);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(340, 87);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(400, 58);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(340, 87);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(40, 58);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(340, 87);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "NUEVA BAHÍA";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // dgvBahias
            // 
            this.dgvBahias.AllowUserToAddRows = false;
            this.dgvBahias.AllowUserToDeleteRows = false;
            this.dgvBahias.BackgroundColor = System.Drawing.Color.White;
            this.dgvBahias.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvBahias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBahias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNombre,
            this.colDescripcion,
            this.colCapacidad,
            this.colEstado,
            this.colUsuario,
            this.colActivo});
            this.dgvBahias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBahias.GridColor = System.Drawing.Color.Gray;
            this.dgvBahias.Location = new System.Drawing.Point(800, 192);
            this.dgvBahias.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dgvBahias.Name = "dgvBahias";
            this.dgvBahias.ReadOnly = true;
            this.dgvBahias.RowHeadersWidth = 82;
            this.dgvBahias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBahias.Size = new System.Drawing.Size(1600, 1154);
            this.dgvBahias.TabIndex = 2;
            this.dgvBahias.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBahias_CellClick);
            // 
            // panelFiltros
            // 
            this.panelFiltros.BackColor = System.Drawing.Color.Gainsboro;
            this.panelFiltros.Controls.Add(this.btnLimpiarFiltros);
            this.panelFiltros.Controls.Add(this.txtBuscar);
            this.panelFiltros.Controls.Add(this.label8);
            this.panelFiltros.Controls.Add(this.cmbFiltroEstado);
            this.panelFiltros.Controls.Add(this.label7);
            this.panelFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltros.Location = new System.Drawing.Point(800, 115);
            this.panelFiltros.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panelFiltros.Name = "panelFiltros";
            this.panelFiltros.Size = new System.Drawing.Size(1600, 77);
            this.panelFiltros.TabIndex = 3;
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.BackColor = System.Drawing.Color.Gray;
            this.btnLimpiarFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarFiltros.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(1130, 14);
            this.btnLimpiarFiltros.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(200, 48);
            this.btnLimpiarFiltros.TabIndex = 4;
            this.btnLimpiarFiltros.Text = "Limpiar Filtros";
            this.btnLimpiarFiltros.UseVisualStyleBackColor = false;
            this.btnLimpiarFiltros.Click += new System.EventHandler(this.btnLimpiarFiltros_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(722, 20);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(396, 31);
            this.txtBuscar.TabIndex = 3;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(617, 25);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 26);
            this.label8.TabIndex = 2;
            this.label8.Text = "Buscar:";
            // 
            // cmbFiltroEstado
            // 
            this.cmbFiltroEstado.FormattingEnabled = true;
            this.cmbFiltroEstado.Location = new System.Drawing.Point(140, 19);
            this.cmbFiltroEstado.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmbFiltroEstado.Name = "cmbFiltroEstado";
            this.cmbFiltroEstado.Size = new System.Drawing.Size(396, 33);
            this.cmbFiltroEstado.TabIndex = 1;
            this.cmbFiltroEstado.SelectedIndexChanged += new System.EventHandler(this.cmbFiltroEstado_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(40, 25);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 26);
            this.label7.TabIndex = 0;
            this.label7.Text = "Estado:";
            // 
            // colId
            // 
            this.colId.HeaderText = "ID";
            this.colId.MinimumWidth = 10;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Width = 50;
            // 
            // colNombre
            // 
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.MinimumWidth = 10;
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            // 
            // colDescripcion
            // 
            this.colDescripcion.HeaderText = "Descripción";
            this.colDescripcion.MinimumWidth = 10;
            this.colDescripcion.Name = "colDescripcion";
            this.colDescripcion.ReadOnly = true;
            this.colDescripcion.Width = 200;
            // 
            // colCapacidad
            // 
            this.colCapacidad.HeaderText = "Capacidad";
            this.colCapacidad.MinimumWidth = 10;
            this.colCapacidad.Name = "colCapacidad";
            this.colCapacidad.ReadOnly = true;
            this.colCapacidad.Width = 80;
            // 
            // colEstado
            // 
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 10;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            this.colEstado.Width = 120;
            // 
            // colUsuario
            // 
            this.colUsuario.HeaderText = "Usuario";
            this.colUsuario.MinimumWidth = 10;
            this.colUsuario.Name = "colUsuario";
            this.colUsuario.ReadOnly = true;
            this.colUsuario.Width = 120;
            // 
            // colActivo
            // 
            this.colActivo.HeaderText = "Activo";
            this.colActivo.MinimumWidth = 10;
            this.colActivo.Name = "colActivo";
            this.colActivo.ReadOnly = true;
            this.colActivo.Width = 60;
            // 
            // frmBahias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(2400, 1346);
            this.Controls.Add(this.dgvBahias);
            this.Controls.Add(this.panelFiltros);
            this.Controls.Add(this.panelControles);
            this.Controls.Add(this.panelHeader);
            this.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.Name = "frmBahias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestión de Bahías";
            this.Load += new System.EventHandler(this.frmBahias_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelControles.ResumeLayout(false);
            this.groupBoxDatos.ResumeLayout(false);
            this.groupBoxDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidad)).EndInit();
            this.groupBoxAcciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBahias)).EndInit();
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltros.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCapacidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActivo;
    }
}