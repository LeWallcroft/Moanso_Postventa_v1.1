using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Garantia
    {
        public bool VerificarGarantiaVehiculo(int vehiculoID)
        {
            bool esValida = false;
            SqlConnection con = null;

            try
            {
                using (con = new CD_Conexion().AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_ValidarGarantia", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VehiculoID", vehiculoID);

                    // Configurar parámetro de salida
                    SqlParameter outputParam = new SqlParameter("@EsValida", SqlDbType.Bit);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                    // Leer el resultado
                    esValida = Convert.ToBoolean(outputParam.Value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar garantía: " + ex.Message);
            }

            return esValida;
        }
    }
}
