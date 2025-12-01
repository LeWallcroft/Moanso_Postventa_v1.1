namespace Mannucci_Motors
{
    partial class frmNuevaCita
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
            this.tbNuevaCita = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvVehiculosRegistrados = new System.Windows.Forms.DataGridView();
            this.gbDatosCliente = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnContinuarCalendario = new System.Windows.Forms.Button();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.txtDNIBusqueda = new System.Windows.Forms.TextBox();
            this.lblDNIBusqueda = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnAtrasBuscarCliente = new System.Windows.Forms.Button();
            this.dgvBahias = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnContinuarDetalle = new System.Windows.Forms.Button();
            this.cmbBahias = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mcFecha = new System.Windows.Forms.MonthCalendar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnAtrasCalendario = new System.Windows.Forms.Button();
            this.btnContinuarResumen = new System.Windows.Forms.Button();
            this.txtDuracion = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbServicios = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnAtrasDetalles = new System.Windows.Forms.Button();
            this.btnCrearOrdenTrabajo = new System.Windows.Forms.Button();
            this.btnRegistrarCita = new System.Windows.Forms.Button();
            this.dgvResumen = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.tbNuevaCita.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehiculosRegistrados)).BeginInit();
            this.gbDatosCliente.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBahias)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumen)).BeginInit();
            this.SuspendLayout();
            // 
            // tbNuevaCita
            // 
            this.tbNuevaCita.Controls.Add(this.tabPage1);
            this.tbNuevaCita.Controls.Add(this.tabPage2);
            this.tbNuevaCita.Controls.Add(this.tabPage3);
            this.tbNuevaCita.Controls.Add(this.tabPage4);
            this.tbNuevaCita.Location = new System.Drawing.Point(12, 12);
            this.tbNuevaCita.Name = "tbNuevaCita";
            this.tbNuevaCita.SelectedIndex = 0;
            this.tbNuevaCita.Size = new System.Drawing.Size(850, 587);
            this.tbNuevaCita.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.gbDatosCliente);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(842, 561);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Buscar Cliente";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox2.Controls.Add(this.dgvVehiculosRegistrados);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBox2.Location = new System.Drawing.Point(6, 316);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(830, 231);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "VEHICULOS REGISTRADOS";
            // 
            // dgvVehiculosRegistrados
            // 
            this.dgvVehiculosRegistrados.BackgroundColor = System.Drawing.Color.White;
            this.dgvVehiculosRegistrados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVehiculosRegistrados.Location = new System.Drawing.Point(19, 33);
            this.dgvVehiculosRegistrados.Name = "dgvVehiculosRegistrados";
            this.dgvVehiculosRegistrados.Size = new System.Drawing.Size(788, 193);
            this.dgvVehiculosRegistrados.TabIndex = 0;
            this.dgvVehiculosRegistrados.SelectionChanged += new System.EventHandler(this.dgvVehiculosRegistrados_SelectionChanged);
            // 
            // gbDatosCliente
            // 
            this.gbDatosCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gbDatosCliente.Controls.Add(this.btnCancelar);
            this.gbDatosCliente.Controls.Add(this.btnContinuarCalendario);
            this.gbDatosCliente.Controls.Add(this.txtDireccion);
            this.gbDatosCliente.Controls.Add(this.lblDireccion);
            this.gbDatosCliente.Controls.Add(this.txtTelefono);
            this.gbDatosCliente.Controls.Add(this.lblTelefono);
            this.gbDatosCliente.Controls.Add(this.txtEmail);
            this.gbDatosCliente.Controls.Add(this.lblEmail);
            this.gbDatosCliente.Controls.Add(this.txtApellido);
            this.gbDatosCliente.Controls.Add(this.lblApellido);
            this.gbDatosCliente.Controls.Add(this.txtNombre);
            this.gbDatosCliente.Controls.Add(this.lblNombre);
            this.gbDatosCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDatosCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gbDatosCliente.Location = new System.Drawing.Point(5, 163);
            this.gbDatosCliente.Margin = new System.Windows.Forms.Padding(2);
            this.gbDatosCliente.Name = "gbDatosCliente";
            this.gbDatosCliente.Padding = new System.Windows.Forms.Padding(2);
            this.gbDatosCliente.Size = new System.Drawing.Size(831, 137);
            this.gbDatosCliente.TabIndex = 9;
            this.gbDatosCliente.TabStop = false;
            this.gbDatosCliente.Text = "DATOS DEL CLIENTE";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(687, 81);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(96, 31);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnContinuarCalendario
            // 
            this.btnContinuarCalendario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnContinuarCalendario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinuarCalendario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinuarCalendario.ForeColor = System.Drawing.Color.White;
            this.btnContinuarCalendario.Location = new System.Drawing.Point(669, 28);
            this.btnContinuarCalendario.Margin = new System.Windows.Forms.Padding(2);
            this.btnContinuarCalendario.Name = "btnContinuarCalendario";
            this.btnContinuarCalendario.Size = new System.Drawing.Size(139, 27);
            this.btnContinuarCalendario.TabIndex = 12;
            this.btnContinuarCalendario.Text = "CONTINUAR";
            this.btnContinuarCalendario.UseVisualStyleBackColor = false;
            this.btnContinuarCalendario.Click += new System.EventHandler(this.btnContinuarCalendario_Click);
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(90, 91);
            this.txtDireccion.Margin = new System.Windows.Forms.Padding(2);
            this.txtDireccion.MaxLength = 300;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(451, 21);
            this.txtDireccion.TabIndex = 11;
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDireccion.Location = new System.Drawing.Point(18, 94);
            this.lblDireccion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(72, 15);
            this.lblDireccion.TabIndex = 10;
            this.lblDireccion.Text = "Dirección:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(405, 58);
            this.txtTelefono.Margin = new System.Windows.Forms.Padding(2);
            this.txtTelefono.MaxLength = 20;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(190, 21);
            this.txtTelefono.TabIndex = 9;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTelefono.Location = new System.Drawing.Point(334, 64);
            this.lblTelefono.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(67, 15);
            this.lblTelefono.TabIndex = 8;
            this.lblTelefono.Text = "Teléfono:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(90, 58);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmail.MaxLength = 150;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(226, 21);
            this.txtEmail.TabIndex = 7;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEmail.Location = new System.Drawing.Point(18, 61);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(48, 15);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email:";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(405, 25);
            this.txtApellido.Margin = new System.Windows.Forms.Padding(2);
            this.txtApellido.MaxLength = 100;
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(190, 21);
            this.txtApellido.TabIndex = 5;
            // 
            // lblApellido
            // 
            this.lblApellido.AutoSize = true;
            this.lblApellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblApellido.Location = new System.Drawing.Point(334, 25);
            this.lblApellido.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(63, 15);
            this.lblApellido.TabIndex = 4;
            this.lblApellido.Text = "Apellido:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(90, 25);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(226, 21);
            this.txtNombre.TabIndex = 3;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblNombre.Location = new System.Drawing.Point(18, 28);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(62, 15);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "Nombre:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox1.Controls.Add(this.btnBuscarCliente);
            this.groupBox1.Controls.Add(this.txtDNIBusqueda);
            this.groupBox1.Controls.Add(this.lblDNIBusqueda);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBox1.Location = new System.Drawing.Point(6, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(830, 94);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BUSCAR POR DNI";
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnBuscarCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarCliente.ForeColor = System.Drawing.Color.White;
            this.btnBuscarCliente.Location = new System.Drawing.Point(669, 32);
            this.btnBuscarCliente.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(139, 28);
            this.btnBuscarCliente.TabIndex = 3;
            this.btnBuscarCliente.Text = "BUSCAR ";
            this.btnBuscarCliente.UseVisualStyleBackColor = false;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // txtDNIBusqueda
            // 
            this.txtDNIBusqueda.Location = new System.Drawing.Point(83, 39);
            this.txtDNIBusqueda.Margin = new System.Windows.Forms.Padding(2);
            this.txtDNIBusqueda.MaxLength = 8;
            this.txtDNIBusqueda.Name = "txtDNIBusqueda";
            this.txtDNIBusqueda.Size = new System.Drawing.Size(149, 21);
            this.txtDNIBusqueda.TabIndex = 2;
            // 
            // lblDNIBusqueda
            // 
            this.lblDNIBusqueda.AutoSize = true;
            this.lblDNIBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNIBusqueda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDNIBusqueda.Location = new System.Drawing.Point(28, 41);
            this.lblDNIBusqueda.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDNIBusqueda.Name = "lblDNIBusqueda";
            this.lblDNIBusqueda.Size = new System.Drawing.Size(35, 15);
            this.lblDNIBusqueda.TabIndex = 1;
            this.lblDNIBusqueda.Text = "DNI:";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-4, 3);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2);
            this.label1.Size = new System.Drawing.Size(845, 49);
            this.label1.TabIndex = 0;
            this.label1.Text = "BUSCAR CLIENTE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(842, 561);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Calendario";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel3.Controls.Add(this.btnAtrasBuscarCliente);
            this.panel3.Controls.Add(this.dgvBahias);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(307, 150);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(529, 405);
            this.panel3.TabIndex = 9;
            // 
            // btnAtrasBuscarCliente
            // 
            this.btnAtrasBuscarCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAtrasBuscarCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtrasBuscarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtrasBuscarCliente.ForeColor = System.Drawing.Color.White;
            this.btnAtrasBuscarCliente.Location = new System.Drawing.Point(389, 28);
            this.btnAtrasBuscarCliente.Margin = new System.Windows.Forms.Padding(2);
            this.btnAtrasBuscarCliente.Name = "btnAtrasBuscarCliente";
            this.btnAtrasBuscarCliente.Size = new System.Drawing.Size(96, 31);
            this.btnAtrasBuscarCliente.TabIndex = 14;
            this.btnAtrasBuscarCliente.Text = "ATRAS";
            this.btnAtrasBuscarCliente.UseVisualStyleBackColor = false;
            this.btnAtrasBuscarCliente.Click += new System.EventHandler(this.btnAtrasBuscarCliente_Click);
            // 
            // dgvBahias
            // 
            this.dgvBahias.BackgroundColor = System.Drawing.Color.White;
            this.dgvBahias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBahias.Location = new System.Drawing.Point(23, 85);
            this.dgvBahias.Name = "dgvBahias";
            this.dgvBahias.Size = new System.Drawing.Size(487, 297);
            this.dgvBahias.TabIndex = 6;
            this.dgvBahias.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvBahias_CellFormatting);
            this.dgvBahias.SelectionChanged += new System.EventHandler(this.dgvBahias_SelectionChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(21, 36);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "BAHIAS DISPONIBLES";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel2.Controls.Add(this.btnContinuarDetalle);
            this.panel2.Controls.Add(this.cmbBahias);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(307, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(529, 89);
            this.panel2.TabIndex = 7;
            // 
            // btnContinuarDetalle
            // 
            this.btnContinuarDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnContinuarDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinuarDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinuarDetalle.ForeColor = System.Drawing.Color.White;
            this.btnContinuarDetalle.Location = new System.Drawing.Point(371, 35);
            this.btnContinuarDetalle.Margin = new System.Windows.Forms.Padding(2);
            this.btnContinuarDetalle.Name = "btnContinuarDetalle";
            this.btnContinuarDetalle.Size = new System.Drawing.Size(139, 28);
            this.btnContinuarDetalle.TabIndex = 8;
            this.btnContinuarDetalle.Text = "CONTINUAR";
            this.btnContinuarDetalle.UseVisualStyleBackColor = false;
            this.btnContinuarDetalle.Click += new System.EventHandler(this.btnContinuarDetalle_Click);
            // 
            // cmbBahias
            // 
            this.cmbBahias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBahias.FormattingEnabled = true;
            this.cmbBahias.Location = new System.Drawing.Point(144, 48);
            this.cmbBahias.Name = "cmbBahias";
            this.cmbBahias.Size = new System.Drawing.Size(189, 21);
            this.cmbBahias.TabIndex = 7;
            this.cmbBahias.SelectedIndexChanged += new System.EventHandler(this.cmbBahias_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(20, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Nombre Bahías:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(21, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "BAHIAS";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.mcFecha);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(6, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(295, 506);
            this.panel1.TabIndex = 2;
            // 
            // mcFecha
            // 
            this.mcFecha.CalendarDimensions = new System.Drawing.Size(1, 3);
            this.mcFecha.Location = new System.Drawing.Point(24, 35);
            this.mcFecha.Name = "mcFecha";
            this.mcFecha.TabIndex = 6;
            this.mcFecha.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mcFecha_DateChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(21, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "FECHA";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(-6, 3);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(2);
            this.label2.Size = new System.Drawing.Size(845, 49);
            this.label2.TabIndex = 1;
            this.label2.Text = "DISPONIBILIDAD FECHAS Y BAHIAS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtDescripcion);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.txtPrecio);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.btnAtrasCalendario);
            this.tabPage3.Controls.Add(this.btnContinuarResumen);
            this.tabPage3.Controls.Add(this.txtDuracion);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.cmbServicios);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.txtObservaciones);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(842, 561);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Detalles Adicionales";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcion.Location = new System.Drawing.Point(87, 330);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(381, 38);
            this.txtDescripcion.TabIndex = 19;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(84, 303);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 15);
            this.label13.TabIndex = 18;
            this.label13.Text = "DESCRIPCIÓN:";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecio.ForeColor = System.Drawing.Color.Black;
            this.txtPrecio.Location = new System.Drawing.Point(600, 217);
            this.txtPrecio.Multiline = true;
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(114, 24);
            this.txtPrecio.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(524, 221);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 15);
            this.label12.TabIndex = 16;
            this.label12.Text = "PRECIO:";
            // 
            // btnAtrasCalendario
            // 
            this.btnAtrasCalendario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAtrasCalendario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtrasCalendario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtrasCalendario.ForeColor = System.Drawing.Color.White;
            this.btnAtrasCalendario.Location = new System.Drawing.Point(206, 446);
            this.btnAtrasCalendario.Margin = new System.Windows.Forms.Padding(2);
            this.btnAtrasCalendario.Name = "btnAtrasCalendario";
            this.btnAtrasCalendario.Size = new System.Drawing.Size(107, 41);
            this.btnAtrasCalendario.TabIndex = 15;
            this.btnAtrasCalendario.Text = "ATRAS";
            this.btnAtrasCalendario.UseVisualStyleBackColor = false;
            this.btnAtrasCalendario.Click += new System.EventHandler(this.btnAtrasCalendario_Click);
            // 
            // btnContinuarResumen
            // 
            this.btnContinuarResumen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnContinuarResumen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinuarResumen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinuarResumen.ForeColor = System.Drawing.Color.White;
            this.btnContinuarResumen.Location = new System.Drawing.Point(470, 446);
            this.btnContinuarResumen.Margin = new System.Windows.Forms.Padding(2);
            this.btnContinuarResumen.Name = "btnContinuarResumen";
            this.btnContinuarResumen.Size = new System.Drawing.Size(136, 41);
            this.btnContinuarResumen.TabIndex = 12;
            this.btnContinuarResumen.Text = "CONTINUAR";
            this.btnContinuarResumen.UseVisualStyleBackColor = false;
            this.btnContinuarResumen.Click += new System.EventHandler(this.btnContinuarResumen_Click);
            // 
            // txtDuracion
            // 
            this.txtDuracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDuracion.ForeColor = System.Drawing.Color.Black;
            this.txtDuracion.Location = new System.Drawing.Point(527, 330);
            this.txtDuracion.Multiline = true;
            this.txtDuracion.Name = "txtDuracion";
            this.txtDuracion.Size = new System.Drawing.Size(168, 24);
            this.txtDuracion.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(524, 303);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 15);
            this.label10.TabIndex = 10;
            this.label10.Text = "DURACIÓN:";
            // 
            // cmbServicios
            // 
            this.cmbServicios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServicios.FormattingEnabled = true;
            this.cmbServicios.Location = new System.Drawing.Point(235, 220);
            this.cmbServicios.Name = "cmbServicios";
            this.cmbServicios.Size = new System.Drawing.Size(233, 21);
            this.cmbServicios.TabIndex = 9;
            this.cmbServicios.SelectedIndexChanged += new System.EventHandler(this.cmbServicios_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(84, 221);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 15);
            this.label9.TabIndex = 8;
            this.label9.Text = "SERVICIOS:";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(235, 121);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(399, 42);
            this.txtObservaciones.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(84, 124);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 15);
            this.label8.TabIndex = 6;
            this.label8.Text = "OBSERVACIONES:";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1, 3);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(2);
            this.label6.Size = new System.Drawing.Size(845, 49);
            this.label6.TabIndex = 2;
            this.label6.Text = "DETALLES ADICIONALES";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnAtrasDetalles);
            this.tabPage4.Controls.Add(this.btnCrearOrdenTrabajo);
            this.tabPage4.Controls.Add(this.btnRegistrarCita);
            this.tabPage4.Controls.Add(this.dgvResumen);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(842, 561);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Resumen";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnAtrasDetalles
            // 
            this.btnAtrasDetalles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAtrasDetalles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtrasDetalles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtrasDetalles.ForeColor = System.Drawing.Color.White;
            this.btnAtrasDetalles.Location = new System.Drawing.Point(356, 479);
            this.btnAtrasDetalles.Margin = new System.Windows.Forms.Padding(2);
            this.btnAtrasDetalles.Name = "btnAtrasDetalles";
            this.btnAtrasDetalles.Size = new System.Drawing.Size(133, 40);
            this.btnAtrasDetalles.TabIndex = 15;
            this.btnAtrasDetalles.Text = "ATRAS";
            this.btnAtrasDetalles.UseVisualStyleBackColor = false;
            // 
            // btnCrearOrdenTrabajo
            // 
            this.btnCrearOrdenTrabajo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCrearOrdenTrabajo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrearOrdenTrabajo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearOrdenTrabajo.ForeColor = System.Drawing.Color.White;
            this.btnCrearOrdenTrabajo.Location = new System.Drawing.Point(662, 470);
            this.btnCrearOrdenTrabajo.Margin = new System.Windows.Forms.Padding(2);
            this.btnCrearOrdenTrabajo.Name = "btnCrearOrdenTrabajo";
            this.btnCrearOrdenTrabajo.Size = new System.Drawing.Size(141, 58);
            this.btnCrearOrdenTrabajo.TabIndex = 14;
            this.btnCrearOrdenTrabajo.Text = "CREAR ORDEN DE TRABAJO";
            this.btnCrearOrdenTrabajo.UseVisualStyleBackColor = false;
            // 
            // btnRegistrarCita
            // 
            this.btnRegistrarCita.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRegistrarCita.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarCita.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarCita.ForeColor = System.Drawing.Color.White;
            this.btnRegistrarCita.Location = new System.Drawing.Point(52, 479);
            this.btnRegistrarCita.Margin = new System.Windows.Forms.Padding(2);
            this.btnRegistrarCita.Name = "btnRegistrarCita";
            this.btnRegistrarCita.Size = new System.Drawing.Size(133, 40);
            this.btnRegistrarCita.TabIndex = 13;
            this.btnRegistrarCita.Text = "REGISTRAR CITA";
            this.btnRegistrarCita.UseVisualStyleBackColor = false;
            this.btnRegistrarCita.Click += new System.EventHandler(this.btnRegistrarCita_Click);
            // 
            // dgvResumen
            // 
            this.dgvResumen.BackgroundColor = System.Drawing.Color.White;
            this.dgvResumen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResumen.Location = new System.Drawing.Point(30, 71);
            this.dgvResumen.Name = "dgvResumen";
            this.dgvResumen.Size = new System.Drawing.Size(787, 376);
            this.dgvResumen.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(1, 3);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(2);
            this.label11.Size = new System.Drawing.Size(845, 49);
            this.label11.TabIndex = 3;
            this.label11.Text = "RESUMEN";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmNuevaCita
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 602);
            this.Controls.Add(this.tbNuevaCita);
            this.Name = "frmNuevaCita";
            this.Text = "frmNuevaCita";
            this.tbNuevaCita.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehiculosRegistrados)).EndInit();
            this.gbDatosCliente.ResumeLayout(false);
            this.gbDatosCliente.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBahias)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbNuevaCita;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDNIBusqueda;
        private System.Windows.Forms.TextBox txtDNIBusqueda;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.GroupBox gbDatosCliente;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnContinuarCalendario;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvVehiculosRegistrados;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MonthCalendar mcFecha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvBahias;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbBahias;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbServicios;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.TextBox txtDuracion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnAtrasBuscarCliente;
        private System.Windows.Forms.Button btnContinuarDetalle;
        private System.Windows.Forms.Button btnAtrasCalendario;
        private System.Windows.Forms.Button btnContinuarResumen;
        private System.Windows.Forms.DataGridView dgvResumen;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnAtrasDetalles;
        private System.Windows.Forms.Button btnCrearOrdenTrabajo;
        private System.Windows.Forms.Button btnRegistrarCita;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPrecio;
    }
}