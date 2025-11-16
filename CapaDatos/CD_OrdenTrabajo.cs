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
        /*
        public DataTable ListarCitas()
        {
            SqlConnection cn = new CD_Conexion().AbrirConexion();


            SqlDataAdapter cmd = new SqlDataAdapter("sp_Citas_ListarPorFecha", cn);
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            return dt;
        }*/

        public DataTable ListarCitas(DateTime fecha)
        {
            SqlConnection cn = new CD_Conexion().AbrirConexion();
            SqlDataAdapter cmd = new SqlDataAdapter("sp_Citas_ListarPorFecha", cn);
            cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
            cmd.SelectCommand.Parameters.AddWithValue("@Fecha", fecha);
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            return dt;
        }

        public DataTable Listar_Cita2(DateTime fecha)
        {
            return ListarCitas(fecha);
        }
    }
}
