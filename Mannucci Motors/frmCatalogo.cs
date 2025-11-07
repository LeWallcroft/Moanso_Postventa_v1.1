using CapaDominio;
using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mannucci_Motors
{
    public partial class frmCatalogo : Form
    {
        private CN_Servicio cnServicio = new CN_Servicio();
        public frmCatalogo()
        {
            InitializeComponent();
        }

        private void frmCatalogo_Load(object sender, EventArgs e)
        {
            ConfigurarControles();
            CargarTipos();
            CargarServiciosEnCards();
        }

        private void ConfigurarControles()
        {
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;

            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;
        }


        private void CargarServiciosEnCards(string tipoFiltro = "")
        {
            flowLayoutPanel1.Controls.Clear();

            List<Servicio> lista;
            if (tipoFiltro == "" || tipoFiltro == "Todos")
                lista = cnServicio.ListarServicios();
            else
                lista = cnServicio.ListarServicios(tipoFiltro);

            foreach (var s in lista)
            {
                Panel card = new Panel
                {
                    Width = 250,
                    Height = 120,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(10)
                };

                if (s.Tipo.Equals("General", StringComparison.OrdinalIgnoreCase))
                    card.BackColor = Color.FromArgb(240, 248, 255);
                else if (s.Tipo.Equals("Express", StringComparison.OrdinalIgnoreCase))
                    card.BackColor = Color.FromArgb(255, 250, 240);
                else if (s.Tipo.Equals("Lubricacion", StringComparison.OrdinalIgnoreCase))
                    card.BackColor = Color.FromArgb(245, 255, 245);

                Label lblNombre = new Label
                {
                    Text = s.Nombre,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Location = new Point(10, 10),
                    AutoSize = true
                };

                Label lblTipo = new Label
                {
                    Text = $"Tipo: {s.Tipo}",
                    Location = new Point(10, 35),
                    AutoSize = true
                };

                Label lblDuracion = new Label
                {
                    Text = $"Duración: {s.DuracionMin} min",
                    Location = new Point(10, 55),
                    AutoSize = true
                };

                Label lblPrecio = new Label
                {
                    Text = $"Precio: S/ {s.PrecioBase:N2}",
                    ForeColor = Color.DarkGreen,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Location = new Point(10, 75),
                    AutoSize = true
                };

                card.Controls.Add(lblNombre);
                card.Controls.Add(lblTipo);
                card.Controls.Add(lblDuracion);
                card.Controls.Add(lblPrecio);

                flowLayoutPanel1.Controls.Add(card);
            }

            lblTotal.Text = $"Total servicios: {lista.Count}";
        }


        private void CargarTipos()
        {
            cboTipo.Items.Clear();
            cboTipo.Items.Add("Todos");
            cboTipo.Items.Add("General");
            cboTipo.Items.Add("Express");
            cboTipo.Items.Add("Lubricacion");
            cboTipo.SelectedIndex = 0;
        }

        private void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarServiciosEnCards(cboTipo.Text);
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarServiciosEnCards(cboTipo.Text);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
