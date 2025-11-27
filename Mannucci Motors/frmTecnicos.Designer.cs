namespace CapaPresentacion
{
    partial class frmTecnicos
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelControles;
        private System.Windows.Forms.GroupBox groupBoxDatos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbUsuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEspecialidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaContratacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numSalario;
        private System.Windows.Forms.CheckBox chkDisponible;
        private System.Windows.Forms.GroupBox groupBoxAcciones;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DataGridView dgvTecnicos;
        private System.Windows.Forms.Panel panelFiltros;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbFiltroEstado;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.Button btnInactivar;
        private System.Windows.Forms.Timer TimerBuscar;
        private System.Windows.Forms.Label lblTotalRegistros;
        private System.Windows.Forms.Button btnActualizarListaUsuarios;
        private System.Windows.Forms.Button btnVerDetalles;

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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelControles = new System.Windows.Forms.Panel();
            this.groupBoxDatos = new System.Windows.Forms.GroupBox();
            this.btnActualizarListaUsuarios = new System.Windows.Forms.Button();
            this.chkDisponible = new System.Windows.Forms.CheckBox();
            this.numSalario = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaContratacion = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEspecialidad = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbUsuario = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxAcciones = new System.Windows.Forms.GroupBox();
            this.btnVerDetalles = new System.Windows.Forms.Button();
            this.btnInactivar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.dgvTecnicos = new System.Windows.Forms.DataGridView();
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbFiltroEstado = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TimerBuscar = new System.Windows.Forms.Timer(this.components);
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTecnico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEspecialidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaContratacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDisponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelHeader.SuspendLayout();
            this.panelControles.SuspendLayout();
            this.groupBoxDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSalario)).BeginInit();
            this.groupBoxAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTecnicos)).BeginInit();
            this.panelFiltros.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelHeader.Controls.Add(this.lblTitulo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(6);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1800, 109);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1800, 109);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "GESTIÓN DE TÉCNICOS";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelControles
            // 
            this.panelControles.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelControles.Controls.Add(this.groupBoxDatos);
            this.panelControles.Controls.Add(this.groupBoxAcciones);
            this.panelControles.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControles.Location = new System.Drawing.Point(0, 109);
            this.panelControles.Margin = new System.Windows.Forms.Padding(6);
            this.panelControles.Name = "panelControles";
            this.panelControles.Size = new System.Drawing.Size(600, 985);
            this.panelControles.TabIndex = 1;
            // 
            // groupBoxDatos
            // 
            this.groupBoxDatos.BackColor = System.Drawing.Color.White;
            this.groupBoxDatos.Controls.Add(this.btnActualizarListaUsuarios);
            this.groupBoxDatos.Controls.Add(this.chkDisponible);
            this.groupBoxDatos.Controls.Add(this.numSalario);
            this.groupBoxDatos.Controls.Add(this.label4);
            this.groupBoxDatos.Controls.Add(this.dtpFechaContratacion);
            this.groupBoxDatos.Controls.Add(this.label3);
            this.groupBoxDatos.Controls.Add(this.txtEspecialidad);
            this.groupBoxDatos.Controls.Add(this.label2);
            this.groupBoxDatos.Controls.Add(this.cmbUsuario);
            this.groupBoxDatos.Controls.Add(this.label1);
            this.groupBoxDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDatos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBoxDatos.Location = new System.Drawing.Point(0, 0);
            this.groupBoxDatos.Margin = new System.Windows.Forms.Padding(6);
            this.groupBoxDatos.Name = "groupBoxDatos";
            this.groupBoxDatos.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxDatos.Size = new System.Drawing.Size(600, 623);
            this.groupBoxDatos.TabIndex = 0;
            this.groupBoxDatos.TabStop = false;
            this.groupBoxDatos.Text = "DATOS DEL TÉCNICO";
            // 
            // btnActualizarListaUsuarios
            // 
            this.btnActualizarListaUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnActualizarListaUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarListaUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarListaUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnActualizarListaUsuarios.Location = new System.Drawing.Point(420, 62);
            this.btnActualizarListaUsuarios.Margin = new System.Windows.Forms.Padding(6);
            this.btnActualizarListaUsuarios.Name = "btnActualizarListaUsuarios";
            this.btnActualizarListaUsuarios.Size = new System.Drawing.Size(150, 50);
            this.btnActualizarListaUsuarios.TabIndex = 9;
            this.btnActualizarListaUsuarios.Text = "Actualizar";
            this.btnActualizarListaUsuarios.UseVisualStyleBackColor = false;
            this.btnActualizarListaUsuarios.Click += new System.EventHandler(this.btnActualizarListaUsuarios_Click);
            // 
            // chkDisponible
            // 
            this.chkDisponible.AutoSize = true;
            this.chkDisponible.Checked = true;
            this.chkDisponible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisponible.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisponible.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDisponible.Location = new System.Drawing.Point(180, 400);
            this.chkDisponible.Margin = new System.Windows.Forms.Padding(6);
            this.chkDisponible.Name = "chkDisponible";
            this.chkDisponible.Size = new System.Drawing.Size(161, 33);
            this.chkDisponible.TabIndex = 8;
            this.chkDisponible.Text = "Disponible";
            this.chkDisponible.UseVisualStyleBackColor = true;
            // 
            // numSalario
            // 
            this.numSalario.DecimalPlaces = 2;
            this.numSalario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSalario.Location = new System.Drawing.Point(180, 341);
            this.numSalario.Margin = new System.Windows.Forms.Padding(6);
            this.numSalario.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numSalario.Name = "numSalario";
            this.numSalario.Size = new System.Drawing.Size(300, 35);
            this.numSalario.TabIndex = 7;
            this.numSalario.ThousandsSeparator = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(30, 344);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 29);
            this.label4.TabIndex = 6;
            this.label4.Text = "Salario:";
            // 
            // dtpFechaContratacion
            // 
            this.dtpFechaContratacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaContratacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaContratacion.Location = new System.Drawing.Point(180, 262);
            this.dtpFechaContratacion.Margin = new System.Windows.Forms.Padding(6);
            this.dtpFechaContratacion.Name = "dtpFechaContratacion";
            this.dtpFechaContratacion.Size = new System.Drawing.Size(298, 35);
            this.dtpFechaContratacion.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(30, 266);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fecha Cont.:";
            // 
            // txtEspecialidad
            // 
            this.txtEspecialidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEspecialidad.Location = new System.Drawing.Point(180, 141);
            this.txtEspecialidad.Margin = new System.Windows.Forms.Padding(6);
            this.txtEspecialidad.Multiline = true;
            this.txtEspecialidad.Name = "txtEspecialidad";
            this.txtEspecialidad.Size = new System.Drawing.Size(388, 92);
            this.txtEspecialidad.TabIndex = 3;
            this.txtEspecialidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEspecialidad_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(30, 144);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Especialidad:";
            // 
            // cmbUsuario
            // 
            this.cmbUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUsuario.FormattingEnabled = true;
            this.cmbUsuario.Location = new System.Drawing.Point(180, 62);
            this.cmbUsuario.Margin = new System.Windows.Forms.Padding(6);
            this.cmbUsuario.Name = "cmbUsuario";
            this.cmbUsuario.Size = new System.Drawing.Size(223, 37);
            this.cmbUsuario.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(30, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario:";
            // 
            // groupBoxAcciones
            // 
            this.groupBoxAcciones.BackColor = System.Drawing.Color.White;
            this.groupBoxAcciones.Controls.Add(this.btnVerDetalles);
            this.groupBoxAcciones.Controls.Add(this.btnInactivar);
            this.groupBoxAcciones.Controls.Add(this.btnCerrar);
            this.groupBoxAcciones.Controls.Add(this.btnCancelar);
            this.groupBoxAcciones.Controls.Add(this.btnGuardar);
            this.groupBoxAcciones.Controls.Add(this.btnNuevo);
            this.groupBoxAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxAcciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAcciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBoxAcciones.Location = new System.Drawing.Point(0, 623);
            this.groupBoxAcciones.Margin = new System.Windows.Forms.Padding(6);
            this.groupBoxAcciones.Name = "groupBoxAcciones";
            this.groupBoxAcciones.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxAcciones.Size = new System.Drawing.Size(600, 362);
            this.groupBoxAcciones.TabIndex = 1;
            this.groupBoxAcciones.TabStop = false;
            this.groupBoxAcciones.Text = "ACCIONES";
            // 
            // btnVerDetalles
            // 
            this.btnVerDetalles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnVerDetalles.FlatAppearance.BorderSize = 0;
            this.btnVerDetalles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerDetalles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerDetalles.ForeColor = System.Drawing.Color.White;
            this.btnVerDetalles.Location = new System.Drawing.Point(304, 211);
            this.btnVerDetalles.Margin = new System.Windows.Forms.Padding(6);
            this.btnVerDetalles.Name = "btnVerDetalles";
            this.btnVerDetalles.Size = new System.Drawing.Size(255, 62);
            this.btnVerDetalles.TabIndex = 5;
            this.btnVerDetalles.Text = "VER DETALLES";
            this.btnVerDetalles.UseVisualStyleBackColor = false;
            this.btnVerDetalles.Click += new System.EventHandler(this.btnVerDetalles_Click);
            // 
            // btnInactivar
            // 
            this.btnInactivar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnInactivar.FlatAppearance.BorderSize = 0;
            this.btnInactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInactivar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInactivar.ForeColor = System.Drawing.Color.White;
            this.btnInactivar.Location = new System.Drawing.Point(304, 133);
            this.btnInactivar.Margin = new System.Windows.Forms.Padding(6);
            this.btnInactivar.Name = "btnInactivar";
            this.btnInactivar.Size = new System.Drawing.Size(255, 62);
            this.btnInactivar.TabIndex = 4;
            this.btnInactivar.Text = "INACTIVAR";
            this.btnInactivar.UseVisualStyleBackColor = false;
            this.btnInactivar.Click += new System.EventHandler(this.btnInactivar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Gray;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(34, 289);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(6);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(525, 62);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "CERRAR";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Gray;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(34, 211);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(255, 62);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(34, 133);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(6);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(255, 62);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(165, 47);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(6);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(255, 62);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "NUEVO TÉCNICO";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // dgvTecnicos
            // 
            this.dgvTecnicos.AllowUserToAddRows = false;
            this.dgvTecnicos.AllowUserToDeleteRows = false;
            this.dgvTecnicos.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTecnicos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTecnicos.ColumnHeadersHeight = 50;
            this.dgvTecnicos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTecnicos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colTecnico,
            this.colEspecialidad,
            this.colFechaContratacion,
            this.colSalario,
            this.colDisponible,
            this.colActivo});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTecnicos.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTecnicos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTecnicos.EnableHeadersVisualStyles = false;
            this.dgvTecnicos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvTecnicos.Location = new System.Drawing.Point(600, 168);
            this.dgvTecnicos.Margin = new System.Windows.Forms.Padding(6);
            this.dgvTecnicos.Name = "dgvTecnicos";
            this.dgvTecnicos.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTecnicos.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTecnicos.RowHeadersWidth = 82;
            this.dgvTecnicos.RowTemplate.Height = 35;
            this.dgvTecnicos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTecnicos.Size = new System.Drawing.Size(1200, 926);
            this.dgvTecnicos.TabIndex = 2;
            this.dgvTecnicos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTecnicos_CellClick);
            // 
            // panelFiltros
            // 
            this.panelFiltros.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelFiltros.Controls.Add(this.lblTotalRegistros);
            this.panelFiltros.Controls.Add(this.btnLimpiarFiltros);
            this.panelFiltros.Controls.Add(this.txtBuscar);
            this.panelFiltros.Controls.Add(this.label6);
            this.panelFiltros.Controls.Add(this.cmbFiltroEstado);
            this.panelFiltros.Controls.Add(this.label5);
            this.panelFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltros.Location = new System.Drawing.Point(600, 109);
            this.panelFiltros.Margin = new System.Windows.Forms.Padding(6);
            this.panelFiltros.Name = "panelFiltros";
            this.panelFiltros.Size = new System.Drawing.Size(1200, 59);
            this.panelFiltros.TabIndex = 3;
            // 
            // lblTotalRegistros
            // 
            this.lblTotalRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalRegistros.AutoSize = true;
            this.lblTotalRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTotalRegistros.Location = new System.Drawing.Point(1020, 17);
            this.lblTotalRegistros.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTotalRegistros.Name = "lblTotalRegistros";
            this.lblTotalRegistros.Size = new System.Drawing.Size(149, 26);
            this.lblTotalRegistros.TabIndex = 5;
            this.lblTotalRegistros.Text = "Total: 0 técs.";
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.BackColor = System.Drawing.Color.Gray;
            this.btnLimpiarFiltros.FlatAppearance.BorderSize = 0;
            this.btnLimpiarFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarFiltros.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(870, 9);
            this.btnLimpiarFiltros.Margin = new System.Windows.Forms.Padding(6);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(120, 44);
            this.btnLimpiarFiltros.TabIndex = 4;
            this.btnLimpiarFiltros.Text = "Limpiar";
            this.btnLimpiarFiltros.UseVisualStyleBackColor = false;
            this.btnLimpiarFiltros.Click += new System.EventHandler(this.btnLimpiarFiltros_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(540, 12);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(6);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(298, 35);
            this.txtBuscar.TabIndex = 3;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(450, 17);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 29);
            this.label6.TabIndex = 2;
            this.label6.Text = "Buscar:";
            // 
            // cmbFiltroEstado
            // 
            this.cmbFiltroEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cmbFiltroEstado.FormattingEnabled = true;
            this.cmbFiltroEstado.Location = new System.Drawing.Point(120, 12);
            this.cmbFiltroEstado.Margin = new System.Windows.Forms.Padding(6);
            this.cmbFiltroEstado.Name = "cmbFiltroEstado";
            this.cmbFiltroEstado.Size = new System.Drawing.Size(298, 37);
            this.cmbFiltroEstado.TabIndex = 1;
            this.cmbFiltroEstado.SelectedIndexChanged += new System.EventHandler(this.cmbFiltroEstado_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(30, 17);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 29);
            this.label5.TabIndex = 0;
            this.label5.Text = "Estado:";
            // 
            // TimerBuscar
            // 
            this.TimerBuscar.Interval = 500;
            this.TimerBuscar.Tick += new System.EventHandler(this.TimerBuscar_Tick);
            // 
            // colId
            // 
            this.colId.HeaderText = "ID";
            this.colId.MinimumWidth = 10;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Width = 80;
            // 
            // colTecnico
            // 
            this.colTecnico.HeaderText = "Técnico";
            this.colTecnico.MinimumWidth = 10;
            this.colTecnico.Name = "colTecnico";
            this.colTecnico.ReadOnly = true;
            this.colTecnico.Width = 180;
            // 
            // colEspecialidad
            // 
            this.colEspecialidad.HeaderText = "Especialidad";
            this.colEspecialidad.MinimumWidth = 10;
            this.colEspecialidad.Name = "colEspecialidad";
            this.colEspecialidad.ReadOnly = true;
            this.colEspecialidad.Width = 200;
            // 
            // colFechaContratacion
            // 
            this.colFechaContratacion.HeaderText = "Fecha Contratación";
            this.colFechaContratacion.MinimumWidth = 10;
            this.colFechaContratacion.Name = "colFechaContratacion";
            this.colFechaContratacion.ReadOnly = true;
            // 
            // colSalario
            // 
            this.colSalario.HeaderText = "Salario";
            this.colSalario.MinimumWidth = 10;
            this.colSalario.Name = "colSalario";
            this.colSalario.ReadOnly = true;
            // 
            // colDisponible
            // 
            this.colDisponible.HeaderText = "Disponible";
            this.colDisponible.MinimumWidth = 10;
            this.colDisponible.Name = "colDisponible";
            this.colDisponible.ReadOnly = true;
            // 
            // colActivo
            // 
            this.colActivo.HeaderText = "Activo";
            this.colActivo.MinimumWidth = 10;
            this.colActivo.Name = "colActivo";
            this.colActivo.ReadOnly = true;
            this.colActivo.Width = 80;
            // 
            // frmTecnicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1800, 1094);
            this.Controls.Add(this.dgvTecnicos);
            this.Controls.Add(this.panelFiltros);
            this.Controls.Add(this.panelControles);
            this.Controls.Add(this.panelHeader);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MinimumSize = new System.Drawing.Size(1487, 898);
            this.Name = "frmTecnicos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Técnicos";
            this.Load += new System.EventHandler(this.frmTecnicos_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelControles.ResumeLayout(false);
            this.groupBoxDatos.ResumeLayout(false);
            this.groupBoxDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSalario)).EndInit();
            this.groupBoxAcciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTecnicos)).EndInit();
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltros.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTecnico;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEspecialidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaContratacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisponible;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActivo;
    }
}