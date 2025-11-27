using System.Drawing;

namespace CapaPresentacion
{

    partial class frmServicios
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TabControl tcServicios;
        private System.Windows.Forms.TabPage tpListado;
        private System.Windows.Forms.TabPage tpDetalle;
        private System.Windows.Forms.DataGridView dgvServicios;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnDesactivar;
        private System.Windows.Forms.Button btnActivar;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel pnlDetalle;
        private System.Windows.Forms.GroupBox gbDatosServicio;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.TextBox txtDuracion;
        private System.Windows.Forms.ComboBox cbCategoria;
        private System.Windows.Forms.ComboBox cbTipoServicio;
        private System.Windows.Forms.CheckBox chkRequiereRepuestos;
        private System.Windows.Forms.CheckBox chkActivo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label lblDuracion;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label lblTipoServicio;
        private System.Windows.Forms.Panel pnlRepuestos;
        private System.Windows.Forms.DataGridView dgvRepuestosServicio;
        private System.Windows.Forms.Button btnAgregarRepuesto;
        private System.Windows.Forms.Button btnQuitarRepuesto;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblBuscar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tcServicios = new System.Windows.Forms.TabControl();
            this.tpListado = new System.Windows.Forms.TabPage();
            this.dgvServicios = new System.Windows.Forms.DataGridView();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnActivar = new System.Windows.Forms.Button();
            this.btnDesactivar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.tpDetalle = new System.Windows.Forms.TabPage();
            this.pnlDetalle = new System.Windows.Forms.Panel();
            this.pnlRepuestos = new System.Windows.Forms.Panel();
            this.dgvRepuestosServicio = new System.Windows.Forms.DataGridView();
            this.btnQuitarRepuesto = new System.Windows.Forms.Button();
            this.btnAgregarRepuesto = new System.Windows.Forms.Button();
            this.gbDatosServicio = new System.Windows.Forms.GroupBox();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.chkRequiereRepuestos = new System.Windows.Forms.CheckBox();
            this.cbTipoServicio = new System.Windows.Forms.ComboBox();
            this.lblTipoServicio = new System.Windows.Forms.Label();
            this.cbCategoria = new System.Windows.Forms.ComboBox();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.txtDuracion = new System.Windows.Forms.TextBox();
            this.lblDuracion = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.tcServicios.SuspendLayout();
            this.tpListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicios)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.tpDetalle.SuspendLayout();
            this.pnlDetalle.SuspendLayout();
            this.pnlRepuestos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepuestosServicio)).BeginInit();
            this.gbDatosServicio.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 8);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(483, 65);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Gestión de Servicios";
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.tcServicios);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 8);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(1200, 792);
            this.pnlMain.TabIndex = 2;
            // 
            // tcServicios
            // 
            this.tcServicios.Controls.Add(this.tpListado);
            this.tcServicios.Controls.Add(this.tpDetalle);
            this.tcServicios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcServicios.Location = new System.Drawing.Point(20, 20);
            this.tcServicios.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tcServicios.Name = "tcServicios";
            this.tcServicios.SelectedIndex = 0;
            this.tcServicios.Size = new System.Drawing.Size(1160, 752);
            this.tcServicios.TabIndex = 0;
            // 
            // tpListado
            // 
            this.tpListado.BackColor = System.Drawing.Color.White;
            this.tpListado.Controls.Add(this.dgvServicios);
            this.tpListado.Controls.Add(this.pnlToolbar);
            this.tpListado.Location = new System.Drawing.Point(8, 46);
            this.tpListado.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpListado.Name = "tpListado";
            this.tpListado.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpListado.Size = new System.Drawing.Size(1144, 698);
            this.tpListado.TabIndex = 0;
            this.tpListado.Text = "Listado de Servicios";
            // 
            // dgvServicios
            // 
            this.dgvServicios.AllowUserToAddRows = false;
            this.dgvServicios.AllowUserToDeleteRows = false;
            this.dgvServicios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvServicios.BackgroundColor = System.Drawing.Color.White;
            this.dgvServicios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvServicios.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvServicios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvServicios.ColumnHeadersHeight = 40;
            this.dgvServicios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvServicios.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvServicios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvServicios.EnableHeadersVisualStyles = false;
            this.dgvServicios.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvServicios.Location = new System.Drawing.Point(4, 65);
            this.dgvServicios.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvServicios.MultiSelect = false;
            this.dgvServicios.Name = "dgvServicios";
            this.dgvServicios.ReadOnly = true;
            this.dgvServicios.RowHeadersVisible = false;
            this.dgvServicios.RowHeadersWidth = 62;
            this.dgvServicios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServicios.Size = new System.Drawing.Size(1136, 628);
            this.dgvServicios.TabIndex = 1;
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.btnRefresh);
            this.pnlToolbar.Controls.Add(this.btnActivar);
            this.pnlToolbar.Controls.Add(this.btnDesactivar);
            this.pnlToolbar.Controls.Add(this.btnEditar);
            this.pnlToolbar.Controls.Add(this.btnNuevo);
            this.pnlToolbar.Controls.Add(this.txtBuscar);
            this.pnlToolbar.Controls.Add(this.btnBuscar);
            this.pnlToolbar.Controls.Add(this.lblBuscar);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(4, 5);
            this.pnlToolbar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(1136, 60);
            this.pnlToolbar.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(500, 10);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 40);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Actualizar";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnActivar
            // 
            this.btnActivar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnActivar.FlatAppearance.BorderSize = 0;
            this.btnActivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivar.ForeColor = System.Drawing.Color.White;
            this.btnActivar.Location = new System.Drawing.Point(392, 10);
            this.btnActivar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnActivar.Name = "btnActivar";
            this.btnActivar.Size = new System.Drawing.Size(100, 40);
            this.btnActivar.TabIndex = 6;
            this.btnActivar.Text = "Activar";
            this.btnActivar.UseVisualStyleBackColor = false;
            this.btnActivar.Click += new System.EventHandler(this.btnActivar_Click);
            // 
            // btnDesactivar
            // 
            this.btnDesactivar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDesactivar.FlatAppearance.BorderSize = 0;
            this.btnDesactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDesactivar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesactivar.ForeColor = System.Drawing.Color.White;
            this.btnDesactivar.Location = new System.Drawing.Point(284, 10);
            this.btnDesactivar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDesactivar.Name = "btnDesactivar";
            this.btnDesactivar.Size = new System.Drawing.Size(100, 40);
            this.btnDesactivar.TabIndex = 5;
            this.btnDesactivar.Text = "Desactivar";
            this.btnDesactivar.UseVisualStyleBackColor = false;
            this.btnDesactivar.Click += new System.EventHandler(this.btnDesactivar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(176, 10);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(100, 40);
            this.btnEditar.TabIndex = 4;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(68, 10);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(100, 40);
            this.btnNuevo.TabIndex = 3;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(800, 18);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(240, 39);
            this.txtBuscar.TabIndex = 1;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(1050, 10);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(80, 40);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBuscar.Location = new System.Drawing.Point(740, 22);
            this.lblBuscar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(98, 32);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Buscar:";
            // 
            // tpDetalle
            // 
            this.tpDetalle.BackColor = System.Drawing.Color.White;
            this.tpDetalle.Controls.Add(this.pnlDetalle);
            this.tpDetalle.Location = new System.Drawing.Point(8, 46);
            this.tpDetalle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpDetalle.Name = "tpDetalle";
            this.tpDetalle.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpDetalle.Size = new System.Drawing.Size(1144, 698);
            this.tpDetalle.TabIndex = 1;
            this.tpDetalle.Text = "Detalle del Servicio";
            // 
            // pnlDetalle
            // 
            this.pnlDetalle.BackColor = System.Drawing.Color.White;
            this.pnlDetalle.Controls.Add(this.pnlRepuestos);
            this.pnlDetalle.Controls.Add(this.gbDatosServicio);
            this.pnlDetalle.Controls.Add(this.pnlBotones);
            this.pnlDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetalle.Location = new System.Drawing.Point(4, 5);
            this.pnlDetalle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlDetalle.Name = "pnlDetalle";
            this.pnlDetalle.Size = new System.Drawing.Size(1136, 688);
            this.pnlDetalle.TabIndex = 0;
            // 
            // pnlRepuestos
            // 
            this.pnlRepuestos.BackColor = System.Drawing.Color.White;
            this.pnlRepuestos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRepuestos.Controls.Add(this.dgvRepuestosServicio);
            this.pnlRepuestos.Controls.Add(this.btnQuitarRepuesto);
            this.pnlRepuestos.Controls.Add(this.btnAgregarRepuesto);
            this.pnlRepuestos.Location = new System.Drawing.Point(20, 320);
            this.pnlRepuestos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlRepuestos.Name = "pnlRepuestos";
            this.pnlRepuestos.Size = new System.Drawing.Size(1100, 197);
            this.pnlRepuestos.TabIndex = 2;
            // 
            // dgvRepuestosServicio
            // 
            this.dgvRepuestosServicio.AllowUserToAddRows = false;
            this.dgvRepuestosServicio.AllowUserToDeleteRows = false;
            this.dgvRepuestosServicio.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRepuestosServicio.BackgroundColor = System.Drawing.Color.White;
            this.dgvRepuestosServicio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRepuestosServicio.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRepuestosServicio.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvRepuestosServicio.ColumnHeadersHeight = 30;
            this.dgvRepuestosServicio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRepuestosServicio.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvRepuestosServicio.EnableHeadersVisualStyles = false;
            this.dgvRepuestosServicio.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvRepuestosServicio.Location = new System.Drawing.Point(20, 50);
            this.dgvRepuestosServicio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvRepuestosServicio.MultiSelect = false;
            this.dgvRepuestosServicio.Name = "dgvRepuestosServicio";
            this.dgvRepuestosServicio.ReadOnly = true;
            this.dgvRepuestosServicio.RowHeadersVisible = false;
            this.dgvRepuestosServicio.RowHeadersWidth = 62;
            this.dgvRepuestosServicio.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRepuestosServicio.Size = new System.Drawing.Size(900, 180);
            this.dgvRepuestosServicio.TabIndex = 2;
            // 
            // btnQuitarRepuesto
            // 
            this.btnQuitarRepuesto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnQuitarRepuesto.FlatAppearance.BorderSize = 0;
            this.btnQuitarRepuesto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitarRepuesto.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitarRepuesto.ForeColor = System.Drawing.Color.White;
            this.btnQuitarRepuesto.Location = new System.Drawing.Point(940, 110);
            this.btnQuitarRepuesto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQuitarRepuesto.Name = "btnQuitarRepuesto";
            this.btnQuitarRepuesto.Size = new System.Drawing.Size(140, 40);
            this.btnQuitarRepuesto.TabIndex = 1;
            this.btnQuitarRepuesto.Text = "Quitar Repuesto";
            this.btnQuitarRepuesto.UseVisualStyleBackColor = false;
            this.btnQuitarRepuesto.Click += new System.EventHandler(this.btnQuitarRepuesto_Click);
            // 
            // btnAgregarRepuesto
            // 
            this.btnAgregarRepuesto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAgregarRepuesto.FlatAppearance.BorderSize = 0;
            this.btnAgregarRepuesto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarRepuesto.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarRepuesto.ForeColor = System.Drawing.Color.White;
            this.btnAgregarRepuesto.Location = new System.Drawing.Point(940, 50);
            this.btnAgregarRepuesto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAgregarRepuesto.Name = "btnAgregarRepuesto";
            this.btnAgregarRepuesto.Size = new System.Drawing.Size(140, 40);
            this.btnAgregarRepuesto.TabIndex = 0;
            this.btnAgregarRepuesto.Text = "Agregar Repuesto";
            this.btnAgregarRepuesto.UseVisualStyleBackColor = false;
            this.btnAgregarRepuesto.Click += new System.EventHandler(this.btnAgregarRepuesto_Click);
            // 
            // gbDatosServicio
            // 
            this.gbDatosServicio.BackColor = System.Drawing.Color.White;
            this.gbDatosServicio.Controls.Add(this.btnCancelar);
            this.gbDatosServicio.Controls.Add(this.chkActivo);
            this.gbDatosServicio.Controls.Add(this.btnGuardar);
            this.gbDatosServicio.Controls.Add(this.chkRequiereRepuestos);
            this.gbDatosServicio.Controls.Add(this.cbTipoServicio);
            this.gbDatosServicio.Controls.Add(this.lblTipoServicio);
            this.gbDatosServicio.Controls.Add(this.cbCategoria);
            this.gbDatosServicio.Controls.Add(this.lblCategoria);
            this.gbDatosServicio.Controls.Add(this.txtDuracion);
            this.gbDatosServicio.Controls.Add(this.lblDuracion);
            this.gbDatosServicio.Controls.Add(this.txtPrecio);
            this.gbDatosServicio.Controls.Add(this.lblPrecio);
            this.gbDatosServicio.Controls.Add(this.txtDescripcion);
            this.gbDatosServicio.Controls.Add(this.lblDescripcion);
            this.gbDatosServicio.Controls.Add(this.txtNombre);
            this.gbDatosServicio.Controls.Add(this.lblNombre);
            this.gbDatosServicio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDatosServicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gbDatosServicio.Location = new System.Drawing.Point(20, 20);
            this.gbDatosServicio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbDatosServicio.Name = "gbDatosServicio";
            this.gbDatosServicio.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbDatosServicio.Size = new System.Drawing.Size(1100, 280);
            this.gbDatosServicio.TabIndex = 1;
            this.gbDatosServicio.TabStop = false;
            this.gbDatosServicio.Text = "Datos del Servicio";
            // 
            // chkActivo
            // 
            this.chkActivo.AutoSize = true;
            this.chkActivo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActivo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkActivo.Location = new System.Drawing.Point(921, 222);
            this.chkActivo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(112, 36);
            this.chkActivo.TabIndex = 13;
            this.chkActivo.Text = "Activo";
            this.chkActivo.UseVisualStyleBackColor = true;
            // 
            // chkRequiereRepuestos
            // 
            this.chkRequiereRepuestos.AutoSize = true;
            this.chkRequiereRepuestos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRequiereRepuestos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkRequiereRepuestos.Location = new System.Drawing.Point(921, 182);
            this.chkRequiereRepuestos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkRequiereRepuestos.Name = "chkRequiereRepuestos";
            this.chkRequiereRepuestos.Size = new System.Drawing.Size(256, 36);
            this.chkRequiereRepuestos.TabIndex = 12;
            this.chkRequiereRepuestos.Text = "Requiere Repuestos";
            this.chkRequiereRepuestos.UseVisualStyleBackColor = true;
            this.chkRequiereRepuestos.CheckedChanged += new System.EventHandler(this.chkRequiereRepuestos_CheckedChanged);
            // 
            // cbTipoServicio
            // 
            this.cbTipoServicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoServicio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTipoServicio.FormattingEnabled = true;
            this.cbTipoServicio.Location = new System.Drawing.Point(600, 180);
            this.cbTipoServicio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbTipoServicio.Name = "cbTipoServicio";
            this.cbTipoServicio.Size = new System.Drawing.Size(280, 40);
            this.cbTipoServicio.TabIndex = 11;
            // 
            // lblTipoServicio
            // 
            this.lblTipoServicio.AutoSize = true;
            this.lblTipoServicio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoServicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTipoServicio.Location = new System.Drawing.Point(501, 187);
            this.lblTipoServicio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTipoServicio.Name = "lblTipoServicio";
            this.lblTipoServicio.Size = new System.Drawing.Size(170, 32);
            this.lblTipoServicio.TabIndex = 10;
            this.lblTipoServicio.Text = "Tipo Servicio:";
            // 
            // cbCategoria
            // 
            this.cbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategoria.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCategoria.FormattingEnabled = true;
            this.cbCategoria.Location = new System.Drawing.Point(600, 140);
            this.cbCategoria.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbCategoria.Name = "cbCategoria";
            this.cbCategoria.Size = new System.Drawing.Size(280, 40);
            this.cbCategoria.TabIndex = 9;
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCategoria.Location = new System.Drawing.Point(521, 147);
            this.lblCategoria.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(131, 32);
            this.lblCategoria.TabIndex = 8;
            this.lblCategoria.Text = "Categoría:";
            // 
            // txtDuracion
            // 
            this.txtDuracion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDuracion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDuracion.Location = new System.Drawing.Point(600, 100);
            this.txtDuracion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDuracion.Name = "txtDuracion";
            this.txtDuracion.Size = new System.Drawing.Size(120, 39);
            this.txtDuracion.TabIndex = 7;
            // 
            // lblDuracion
            // 
            this.lblDuracion.AutoSize = true;
            this.lblDuracion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuracion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDuracion.Location = new System.Drawing.Point(531, 107);
            this.lblDuracion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDuracion.Name = "lblDuracion";
            this.lblDuracion.Size = new System.Drawing.Size(126, 32);
            this.lblDuracion.TabIndex = 6;
            this.lblDuracion.Text = "Duración:";
            // 
            // txtPrecio
            // 
            this.txtPrecio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrecio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecio.Location = new System.Drawing.Point(600, 60);
            this.txtPrecio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(120, 39);
            this.txtPrecio.TabIndex = 5;
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPrecio.Location = new System.Drawing.Point(551, 67);
            this.lblPrecio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(93, 32);
            this.lblPrecio.TabIndex = 4;
            this.lblPrecio.Text = "Precio:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(120, 100);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescripcion.Size = new System.Drawing.Size(360, 140);
            this.txtDescripcion.TabIndex = 3;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDescripcion.Location = new System.Drawing.Point(41, 107);
            this.lblDescripcion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(156, 32);
            this.lblDescripcion.TabIndex = 2;
            this.lblDescripcion.Text = "Descripción:";
            // 
            // txtNombre
            // 
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(120, 60);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(360, 39);
            this.txtNombre.TabIndex = 1;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblNombre.Location = new System.Drawing.Point(61, 67);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(115, 32);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre:";
            // 
            // pnlBotones
            // 
            this.pnlBotones.BackColor = System.Drawing.Color.White;
            this.pnlBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBotones.Location = new System.Drawing.Point(0, 527);
            this.pnlBotones.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(1136, 161);
            this.pnlBotones.TabIndex = 0;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(964, 100);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(128, 40);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(964, 42);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(125, 40);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // frmServicios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmServicios";
            this.Text = "Gestión de Servicios";
            this.pnlMain.ResumeLayout(false);
            this.tcServicios.ResumeLayout(false);
            this.tpListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicios)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.tpDetalle.ResumeLayout(false);
            this.pnlDetalle.ResumeLayout(false);
            this.pnlRepuestos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepuestosServicio)).EndInit();
            this.gbDatosServicio.ResumeLayout(false);
            this.gbDatosServicio.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}