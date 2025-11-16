using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogicaNegocio;

namespace Mannucci_Motors
{
    public partial class OrdenTrabajo : Form
    {
        public OrdenTrabajo()
        {
            InitializeComponent();
            /*CN_OrdenTrabajo CN_OT= new CN_OrdenTrabajo();*/
            /*DGV_ORDENTRABAJO.DataSource = CN_OT.Listar_Cita2();*/
        }

        private void OrdenTrabajo_Load(object sender, EventArgs e)
        {

        }

        private void DGV_ORDENTRABAJO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = DGV_ORDENTRABAJO.Rows[e.RowIndex];

                txt_cita_id.Text = fila.Cells[0].Value?.ToString();
                txt_estado.Text = fila.Cells[3].Value?.ToString();
                txt_Tecnico.Text = fila.Cells[9].Value?.ToString();



            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
