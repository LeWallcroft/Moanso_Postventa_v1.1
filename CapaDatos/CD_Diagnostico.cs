using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Diagnostico
    {
        private CD_Conexion conexion = new CD_Conexion();

        public DataTable ObtenerClientesConCitas()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = conexion.AbrirConexion();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_Diagnostico_ObtenerClientesConCitas", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        public DataTable ObtenerDetalleCita(int citaId)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = conexion.AbrirConexion();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_Diagnostico_ObtenerDetalleCita", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CitaId", citaId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        public bool CrearDiagnostico(int citaId, string hallazgos, string recomendaciones, int tecnicoId, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            SqlConnection conn = conexion.AbrirConexion();

            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_Diagnostico_Crear", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CitaId", citaId);
                    cmd.Parameters.AddWithValue("@Hallazgos", string.IsNullOrEmpty(hallazgos) ? DBNull.Value : (object)hallazgos);
                    cmd.Parameters.AddWithValue("@Recomendaciones", string.IsNullOrEmpty(recomendaciones) ? DBNull.Value : (object)recomendaciones);
                    cmd.Parameters.AddWithValue("@TecnicoId", tecnicoId);

                    SqlParameter paramMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    paramMensaje.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramMensaje);

                    cmd.ExecuteNonQuery();

                    mensaje = paramMensaje.Value.ToString();
                    resultado = !mensaje.Contains("Error");
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al crear diagnóstico: " + ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return resultado;
        }

        public bool ExisteDiagnostico(int citaId)
        {
            SqlConnection conn = conexion.AbrirConexion();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_Diagnostico_Existe", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CitaId", citaId);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }


        public DataTable ObtenerDiagnosticoPorCita(int citaId)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = conexion.AbrirConexion();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_Diagnostico_ObtenerPorCita", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CitaId", citaId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }
    }
}