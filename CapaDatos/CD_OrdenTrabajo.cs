using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_OrdenTrabajo
    {
        public DataTable ListarCitas()
        {
            SqlConnection cn = new CD_Conexion().AbrirConexion();


            SqlDataAdapter cmd = new SqlDataAdapter("sp_Citas_Listar", cn);
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            return dt;
        }
    }
}
