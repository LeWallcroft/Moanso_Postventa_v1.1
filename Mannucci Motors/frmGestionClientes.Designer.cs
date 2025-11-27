namespace Mannucci_Motors
{
    partial class frmGestionClientes
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.gbBusquedaCliente = new System.Windows.Forms.GroupBox();
            this.lblInfoCliente = new System.Windows.Forms.Label();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.txtDNIBusqueda = new System.Windows.Forms.TextBox();
            this.lblDNIBusqueda = new System.Windows.Forms.Label();
            this.gbDatosCliente = new System.Windows.Forms.GroupBox();
            this.btnLimpiarCliente = new System.Windows.Forms.Button();
            this.btnRegistrarCliente = new System.Windows.Forms.Button();
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
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.lblDNI = new System.Windows.Forms.Label();
            this.gbVehiculos = new System.Windows.Forms.GroupBox();
            this.gbNuevoVehiculo = new System.Windows.Forms.GroupBox();
            this.btnRegistrarVehiculo = new System.Windows.Forms.Button();
            this.cbTransmision = new System.Windows.Forms.ComboBox();
            this.lblTransmision = new System.Windows.Forms.Label();
            this.cbCombustible = new System.Windows.Forms.ComboBox();
            this.lblCombustible = new System.Windows.Forms.Label();
            this.txtKilometraje = new System.Windows.Forms.TextBox();
            this.lblKilometraje = new System.Windows.Forms.Label();
            this.txtAnio = new System.Windows.Forms.TextBox();
            this.lblAnio = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.txtVIN = new System.Windows.Forms.TextBox();
            this.lblVIN = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.cbModelo = new System.Windows.Forms.ComboBox();
            this.lblModelo = new System.Windows.Forms.Label();
            this.cbMarca = new System.Windows.Forms.ComboBox();
            this.lblMarca = new System.Windows.Forms.Label();
            this.dgvVehiculos = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            this.panelContenedor.SuspendLayout();
            this.gbBusquedaCliente.SuspendLayout();
            this.gbDatosCliente.SuspendLayout();
            this.gbVehiculos.SuspendLayout();
            this.gbNuevoVehiculo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehiculos)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelHeader.Controls.Add(this.lblTitulo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(2073, 94);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(2073, 94);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "GESTIÓN DE CLIENTES Y VEHÍCULOS";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelContenedor
            // 
            this.panelContenedor.AutoScroll = true;
            this.panelContenedor.BackColor = System.Drawing.Color.White;
            this.panelContenedor.Controls.Add(this.gbBusquedaCliente);
            this.panelContenedor.Controls.Add(this.gbDatosCliente);
            this.panelContenedor.Controls.Add(this.gbVehiculos);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(0, 94);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.panelContenedor.Size = new System.Drawing.Size(2073, 943);
            this.panelContenedor.TabIndex = 1;
            // 
            // gbBusquedaCliente
            // 
            this.gbBusquedaCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gbBusquedaCliente.Controls.Add(this.lblInfoCliente);
            this.gbBusquedaCliente.Controls.Add(this.btnBuscarCliente);
            this.gbBusquedaCliente.Controls.Add(this.txtDNIBusqueda);
            this.gbBusquedaCliente.Controls.Add(this.lblDNIBusqueda);
            this.gbBusquedaCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbBusquedaCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gbBusquedaCliente.Location = new System.Drawing.Point(30, 31);
            this.gbBusquedaCliente.Name = "gbBusquedaCliente";
            this.gbBusquedaCliente.Size = new System.Drawing.Size(2031, 125);
            this.gbBusquedaCliente.TabIndex = 0;
            this.gbBusquedaCliente.TabStop = false;
            this.gbBusquedaCliente.Text = "BUSCAR CLIENTE EXISTENTE";
            // 
            // lblInfoCliente
            // 
            this.lblInfoCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblInfoCliente.Location = new System.Drawing.Point(720, 55);
            this.lblInfoCliente.Name = "lblInfoCliente";
            this.lblInfoCliente.Size = new System.Drawing.Size(720, 31);
            this.lblInfoCliente.TabIndex = 3;
            this.lblInfoCliente.Text = "Ingrese el DNI del cliente para buscar o registrar uno nuevo";
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnBuscarCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarCliente.ForeColor = System.Drawing.Color.White;
            this.btnBuscarCliente.Location = new System.Drawing.Point(396, 47);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(270, 47);
            this.btnBuscarCliente.TabIndex = 2;
            this.btnBuscarCliente.Text = "BUSCAR CLIENTE";
            this.btnBuscarCliente.UseVisualStyleBackColor = false;
            // 
            // txtDNIBusqueda
            // 
            this.txtDNIBusqueda.Location = new System.Drawing.Point(108, 50);
            this.txtDNIBusqueda.MaxLength = 8;
            this.txtDNIBusqueda.Name = "txtDNIBusqueda";
            this.txtDNIBusqueda.Size = new System.Drawing.Size(268, 35);
            this.txtDNIBusqueda.TabIndex = 1;
            // 
            // lblDNIBusqueda
            // 
            this.lblDNIBusqueda.AutoSize = true;
            this.lblDNIBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNIBusqueda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDNIBusqueda.Location = new System.Drawing.Point(36, 55);
            this.lblDNIBusqueda.Name = "lblDNIBusqueda";
            this.lblDNIBusqueda.Size = new System.Drawing.Size(64, 29);
            this.lblDNIBusqueda.TabIndex = 0;
            this.lblDNIBusqueda.Text = "DNI:";
            // 
            // gbDatosCliente
            // 
            this.gbDatosCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gbDatosCliente.Controls.Add(this.btnLimpiarCliente);
            this.gbDatosCliente.Controls.Add(this.btnRegistrarCliente);
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
            this.gbDatosCliente.Controls.Add(this.txtDNI);
            this.gbDatosCliente.Controls.Add(this.lblDNI);
            this.gbDatosCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDatosCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gbDatosCliente.Location = new System.Drawing.Point(30, 188);
            this.gbDatosCliente.Name = "gbDatosCliente";
            this.gbDatosCliente.Size = new System.Drawing.Size(2031, 263);
            this.gbDatosCliente.TabIndex = 1;
            this.gbDatosCliente.TabStop = false;
            this.gbDatosCliente.Text = "DATOS DEL CLIENTE";
            // 
            // btnLimpiarCliente
            // 
            this.btnLimpiarCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnLimpiarCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarCliente.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarCliente.Location = new System.Drawing.Point(1627, 171);
            this.btnLimpiarCliente.Name = "btnLimpiarCliente";
            this.btnLimpiarCliente.Size = new System.Drawing.Size(180, 47);
            this.btnLimpiarCliente.TabIndex = 13;
            this.btnLimpiarCliente.Text = "LIMPIAR";
            this.btnLimpiarCliente.UseVisualStyleBackColor = false;
            // 
            // btnRegistrarCliente
            // 
            this.btnRegistrarCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRegistrarCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarCliente.ForeColor = System.Drawing.Color.White;
            this.btnRegistrarCliente.Location = new System.Drawing.Point(1261, 169);
            this.btnRegistrarCliente.Name = "btnRegistrarCliente";
            this.btnRegistrarCliente.Size = new System.Drawing.Size(252, 47);
            this.btnRegistrarCliente.TabIndex = 12;
            this.btnRegistrarCliente.Text = "REGISTRAR CLIENTE";
            this.btnRegistrarCliente.UseVisualStyleBackColor = false;
            this.btnRegistrarCliente.Click += new System.EventHandler(this.btnRegistrarCliente_Click);
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(180, 175);
            this.txtDireccion.MaxLength = 300;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(898, 35);
            this.txtDireccion.TabIndex = 11;
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDireccion.Location = new System.Drawing.Point(36, 180);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(131, 29);
            this.lblDireccion.TabIndex = 10;
            this.lblDireccion.Text = "Dirección:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(810, 112);
            this.txtTelefono.MaxLength = 20;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(268, 35);
            this.txtTelefono.TabIndex = 9;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTelefono.Location = new System.Drawing.Point(666, 117);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(125, 29);
            this.lblTelefono.TabIndex = 8;
            this.lblTelefono.Text = "Teléfono:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(180, 112);
            this.txtEmail.MaxLength = 150;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(448, 35);
            this.txtEmail.TabIndex = 7;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEmail.Location = new System.Drawing.Point(36, 117);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(86, 29);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email:";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(1430, 48);
            this.txtApellido.MaxLength = 100;
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(377, 35);
            this.txtApellido.TabIndex = 5;
            // 
            // lblApellido
            // 
            this.lblApellido.AutoSize = true;
            this.lblApellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblApellido.Location = new System.Drawing.Point(1286, 53);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(128, 29);
            this.lblApellido.TabIndex = 4;
            this.lblApellido.Text = "Apellido:*";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(810, 49);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(396, 35);
            this.txtNombre.TabIndex = 3;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblNombre.Location = new System.Drawing.Point(666, 54);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(125, 29);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "Nombre:*";
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(180, 50);
            this.txtDNI.MaxLength = 8;
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(448, 35);
            this.txtDNI.TabIndex = 1;
            // 
            // lblDNI
            // 
            this.lblDNI.AutoSize = true;
            this.lblDNI.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDNI.Location = new System.Drawing.Point(36, 55);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.Size = new System.Drawing.Size(75, 29);
            this.lblDNI.TabIndex = 0;
            this.lblDNI.Text = "DNI:*";
            // 
            // gbVehiculos
            // 
            this.gbVehiculos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gbVehiculos.Controls.Add(this.gbNuevoVehiculo);
            this.gbVehiculos.Controls.Add(this.dgvVehiculos);
            this.gbVehiculos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbVehiculos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gbVehiculos.Location = new System.Drawing.Point(30, 479);
            this.gbVehiculos.Name = "gbVehiculos";
            this.gbVehiculos.Size = new System.Drawing.Size(2031, 438);
            this.gbVehiculos.TabIndex = 2;
            this.gbVehiculos.TabStop = false;
            this.gbVehiculos.Text = "VEHÍCULOS REGISTRADOS";
            // 
            // gbNuevoVehiculo
            // 
            this.gbNuevoVehiculo.Controls.Add(this.btnRegistrarVehiculo);
            this.gbNuevoVehiculo.Controls.Add(this.cbTransmision);
            this.gbNuevoVehiculo.Controls.Add(this.lblTransmision);
            this.gbNuevoVehiculo.Controls.Add(this.cbCombustible);
            this.gbNuevoVehiculo.Controls.Add(this.lblCombustible);
            this.gbNuevoVehiculo.Controls.Add(this.txtKilometraje);
            this.gbNuevoVehiculo.Controls.Add(this.lblKilometraje);
            this.gbNuevoVehiculo.Controls.Add(this.txtAnio);
            this.gbNuevoVehiculo.Controls.Add(this.lblAnio);
            this.gbNuevoVehiculo.Controls.Add(this.txtColor);
            this.gbNuevoVehiculo.Controls.Add(this.lblColor);
            this.gbNuevoVehiculo.Controls.Add(this.txtVIN);
            this.gbNuevoVehiculo.Controls.Add(this.lblVIN);
            this.gbNuevoVehiculo.Controls.Add(this.txtPlaca);
            this.gbNuevoVehiculo.Controls.Add(this.lblPlaca);
            this.gbNuevoVehiculo.Controls.Add(this.cbModelo);
            this.gbNuevoVehiculo.Controls.Add(this.lblModelo);
            this.gbNuevoVehiculo.Controls.Add(this.cbMarca);
            this.gbNuevoVehiculo.Controls.Add(this.lblMarca);
            this.gbNuevoVehiculo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbNuevoVehiculo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gbNuevoVehiculo.Location = new System.Drawing.Point(36, 250);
            this.gbNuevoVehiculo.Name = "gbNuevoVehiculo";
            this.gbNuevoVehiculo.Size = new System.Drawing.Size(1963, 156);
            this.gbNuevoVehiculo.TabIndex = 1;
            this.gbNuevoVehiculo.TabStop = false;
            this.gbNuevoVehiculo.Text = "REGISTRAR NUEVO VEHÍCULO";
            // 
            // btnRegistrarVehiculo
            // 
            this.btnRegistrarVehiculo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRegistrarVehiculo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarVehiculo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarVehiculo.ForeColor = System.Drawing.Color.White;
            this.btnRegistrarVehiculo.Location = new System.Drawing.Point(1614, 42);
            this.btnRegistrarVehiculo.Name = "btnRegistrarVehiculo";
            this.btnRegistrarVehiculo.Size = new System.Drawing.Size(248, 86);
            this.btnRegistrarVehiculo.TabIndex = 18;
            this.btnRegistrarVehiculo.Text = "REGISTRAR VEHÍCULO";
            this.btnRegistrarVehiculo.UseVisualStyleBackColor = false;
            // 
            // cbTransmision
            // 
            this.cbTransmision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTransmision.FormattingEnabled = true;
            this.cbTransmision.Items.AddRange(new object[] {
            "Manual",
            "Automática",
            "CVT"});
            this.cbTransmision.Location = new System.Drawing.Point(1278, 97);
            this.cbTransmision.Name = "cbTransmision";
            this.cbTransmision.Size = new System.Drawing.Size(178, 37);
            this.cbTransmision.TabIndex = 17;
            // 
            // lblTransmision
            // 
            this.lblTransmision.AutoSize = true;
            this.lblTransmision.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransmision.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTransmision.Location = new System.Drawing.Point(1116, 102);
            this.lblTransmision.Name = "lblTransmision";
            this.lblTransmision.Size = new System.Drawing.Size(164, 29);
            this.lblTransmision.TabIndex = 16;
            this.lblTransmision.Text = "Transmisión:";
            // 
            // cbCombustible
            // 
            this.cbCombustible.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCombustible.FormattingEnabled = true;
            this.cbCombustible.Items.AddRange(new object[] {
            "Gasolina",
            "Diésel",
            "Eléctrico",
            "Híbrido",
            "GLP"});
            this.cbCombustible.Location = new System.Drawing.Point(918, 97);
            this.cbCombustible.Name = "cbCombustible";
            this.cbCombustible.Size = new System.Drawing.Size(178, 37);
            this.cbCombustible.TabIndex = 15;
            // 
            // lblCombustible
            // 
            this.lblCombustible.AutoSize = true;
            this.lblCombustible.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCombustible.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCombustible.Location = new System.Drawing.Point(756, 102);
            this.lblCombustible.Name = "lblCombustible";
            this.lblCombustible.Size = new System.Drawing.Size(167, 29);
            this.lblCombustible.TabIndex = 14;
            this.lblCombustible.Text = "Combustible:";
            // 
            // txtKilometraje
            // 
            this.txtKilometraje.Location = new System.Drawing.Point(594, 97);
            this.txtKilometraje.MaxLength = 7;
            this.txtKilometraje.Name = "txtKilometraje";
            this.txtKilometraje.Size = new System.Drawing.Size(142, 35);
            this.txtKilometraje.TabIndex = 13;
            // 
            // lblKilometraje
            // 
            this.lblKilometraje.AutoSize = true;
            this.lblKilometraje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKilometraje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblKilometraje.Location = new System.Drawing.Point(522, 102);
            this.lblKilometraje.Name = "lblKilometraje";
            this.lblKilometraje.Size = new System.Drawing.Size(58, 29);
            this.lblKilometraje.TabIndex = 12;
            this.lblKilometraje.Text = "Km:";
            // 
            // txtAnio
            // 
            this.txtAnio.Location = new System.Drawing.Point(396, 97);
            this.txtAnio.MaxLength = 4;
            this.txtAnio.Name = "txtAnio";
            this.txtAnio.Size = new System.Drawing.Size(106, 35);
            this.txtAnio.TabIndex = 11;
            // 
            // lblAnio
            // 
            this.lblAnio.AutoSize = true;
            this.lblAnio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAnio.Location = new System.Drawing.Point(324, 102);
            this.lblAnio.Name = "lblAnio";
            this.lblAnio.Size = new System.Drawing.Size(65, 29);
            this.lblAnio.TabIndex = 10;
            this.lblAnio.Text = "Año:";
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(126, 97);
            this.txtColor.MaxLength = 50;
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(178, 35);
            this.txtColor.TabIndex = 9;
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblColor.Location = new System.Drawing.Point(36, 102);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(84, 29);
            this.lblColor.TabIndex = 8;
            this.lblColor.Text = "Color:";
            // 
            // txtVIN
            // 
            this.txtVIN.Location = new System.Drawing.Point(1062, 42);
            this.txtVIN.MaxLength = 50;
            this.txtVIN.Name = "txtVIN";
            this.txtVIN.Size = new System.Drawing.Size(214, 35);
            this.txtVIN.TabIndex = 7;
            // 
            // lblVIN
            // 
            this.lblVIN.AutoSize = true;
            this.lblVIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVIN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblVIN.Location = new System.Drawing.Point(990, 47);
            this.lblVIN.Name = "lblVIN";
            this.lblVIN.Size = new System.Drawing.Size(62, 29);
            this.lblVIN.TabIndex = 6;
            this.lblVIN.Text = "VIN:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(792, 42);
            this.txtPlaca.MaxLength = 20;
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(178, 35);
            this.txtPlaca.TabIndex = 5;
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPlaca.Location = new System.Drawing.Point(702, 47);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(96, 29);
            this.lblPlaca.TabIndex = 4;
            this.lblPlaca.Text = "Placa:*";
            // 
            // cbModelo
            // 
            this.cbModelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModelo.FormattingEnabled = true;
            this.cbModelo.Location = new System.Drawing.Point(468, 42);
            this.cbModelo.Name = "cbModelo";
            this.cbModelo.Size = new System.Drawing.Size(214, 37);
            this.cbModelo.TabIndex = 3;
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModelo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblModelo.Location = new System.Drawing.Point(360, 47);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(119, 29);
            this.lblModelo.TabIndex = 2;
            this.lblModelo.Text = "Modelo:*";
            // 
            // cbMarca
            // 
            this.cbMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMarca.FormattingEnabled = true;
            this.cbMarca.Location = new System.Drawing.Point(126, 42);
            this.cbMarca.Name = "cbMarca";
            this.cbMarca.Size = new System.Drawing.Size(214, 37);
            this.cbMarca.TabIndex = 1;
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMarca.Location = new System.Drawing.Point(36, 47);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(102, 29);
            this.lblMarca.TabIndex = 0;
            this.lblMarca.Text = "Marca:*";
            // 
            // dgvVehiculos
            // 
            this.dgvVehiculos.AllowUserToAddRows = false;
            this.dgvVehiculos.AllowUserToDeleteRows = false;
            this.dgvVehiculos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVehiculos.BackgroundColor = System.Drawing.Color.White;
            this.dgvVehiculos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVehiculos.Location = new System.Drawing.Point(36, 47);
            this.dgvVehiculos.Name = "dgvVehiculos";
            this.dgvVehiculos.ReadOnly = true;
            this.dgvVehiculos.RowHeadersWidth = 62;
            this.dgvVehiculos.Size = new System.Drawing.Size(1956, 184);
            this.dgvVehiculos.TabIndex = 0;
            // 
            // frmGestionClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(2073, 1037);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panelHeader);
            this.Name = "frmGestionClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Clientes y Vehículos";
            this.panelHeader.ResumeLayout(false);
            this.panelContenedor.ResumeLayout(false);
            this.gbBusquedaCliente.ResumeLayout(false);
            this.gbBusquedaCliente.PerformLayout();
            this.gbDatosCliente.ResumeLayout(false);
            this.gbDatosCliente.PerformLayout();
            this.gbVehiculos.ResumeLayout(false);
            this.gbNuevoVehiculo.ResumeLayout(false);
            this.gbNuevoVehiculo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehiculos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.GroupBox gbBusquedaCliente;
        private System.Windows.Forms.Label lblInfoCliente;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.TextBox txtDNIBusqueda;
        private System.Windows.Forms.Label lblDNIBusqueda;
        private System.Windows.Forms.GroupBox gbDatosCliente;
        private System.Windows.Forms.Button btnLimpiarCliente;
        private System.Windows.Forms.Button btnRegistrarCliente;
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
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.GroupBox gbVehiculos;
        private System.Windows.Forms.GroupBox gbNuevoVehiculo;
        private System.Windows.Forms.Button btnRegistrarVehiculo;
        private System.Windows.Forms.ComboBox cbTransmision;
        private System.Windows.Forms.Label lblTransmision;
        private System.Windows.Forms.ComboBox cbCombustible;
        private System.Windows.Forms.Label lblCombustible;
        private System.Windows.Forms.TextBox txtKilometraje;
        private System.Windows.Forms.Label lblKilometraje;
        private System.Windows.Forms.TextBox txtAnio;
        private System.Windows.Forms.Label lblAnio;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.TextBox txtVIN;
        private System.Windows.Forms.Label lblVIN;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label lblPlaca;
        private System.Windows.Forms.ComboBox cbModelo;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.ComboBox cbMarca;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.DataGridView dgvVehiculos;
    }
}