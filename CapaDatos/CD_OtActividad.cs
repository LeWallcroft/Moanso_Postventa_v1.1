using CapaDominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_OtActividad
    {
        private readonly CD_Conexion conexion = new CD_Conexion();

        public List<OtActividad> ListarPorOrdenTrabajo(int ordentrabajoID)
        {
            var lista = new List<OtActividad>();

            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT 
                    OtactividadID,
                    OrdentrabajoID,
                    ActividadTiposervicioID,
                    Descripcion,
                    Estado,
                    TiempoEstimado,
                    TiempoReal,
                    FechaInicio,
                    FechaFin                    
                FROM Otactividad
                WHERE OrdentrabajoID = @OrdentrabajoID
                ORDER BY OtactividadID;", cn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var act = new OtActividad
                        {
                            OtactividadID = Convert.ToInt32(dr["OtactividadID"]),
                            OrdentrabajoID = Convert.ToInt32(dr["OrdentrabajoID"]),
                            ActividadTiposervicioID = dr["ActividadTiposervicioID"] == DBNull.Value
                                ? (int?)null
                                : Convert.ToInt32(dr["ActividadTiposervicioID"]),
                            Descripcion = dr["Descripcion"] == DBNull.Value ? null : dr["Descripcion"].ToString(),
                            Estado = dr["Estado"] == DBNull.Value ? null : dr["Estado"].ToString(),
                            TiempoEstimado = dr["TiempoEstimado"] == DBNull.Value
                                ? (int?)null
                                : Convert.ToInt32(dr["TiempoEstimado"]),
                            TiempoReal = dr["TiempoReal"] == DBNull.Value
                                ? (int?)null
                                : Convert.ToInt32(dr["TiempoReal"]),
                            FechaInicio = dr["FechaInicio"] == DBNull.Value
                                ? (DateTime?)null
                                : Convert.ToDateTime(dr["FechaInicio"]),
                            FechaFin = dr["FechaFin"] == DBNull.Value
                                ? (DateTime?)null
                                : Convert.ToDateTime(dr["FechaFin"])
                            
                        };

                        lista.Add(act);
                    }
                }
            }

            return lista;
        }

        public void ActualizarActividad(int otactividadID, string estado, int? tiempoReal)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("sp_ActualizarEstadoActividad", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OtactividadID", otactividadID);
                cmd.Parameters.AddWithValue("@Estado", estado);

                if (tiempoReal.HasValue)
                    cmd.Parameters.AddWithValue("@TiempoReal", tiempoReal.Value);
                else
                    cmd.Parameters.AddWithValue("@TiempoReal", DBNull.Value);              

                cmd.ExecuteNonQuery();
            }
        }
    }
}
