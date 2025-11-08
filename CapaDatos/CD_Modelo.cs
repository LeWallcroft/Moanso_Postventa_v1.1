using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Modelo
    {
        private CD_Conexion conexion = new CD_Conexion();

        // Método principal: Busca el modelo por MarcaId y Nombre. Si no existe, lo inserta.
        public int ObtenerOCrearModeloId(int marcaId, string nombre)
        {
            int modeloId = 0;
            string query = "SELECT ModeloId FROM dbo.Modelos WHERE MarcaId = @MarcaId AND Nombre = @Nombre";

            using (SqlConnection con = conexion.AbrirConexion())
            {
                // 1. Intentar Buscar
                SqlCommand cmdBuscar = new SqlCommand(query, con);
                cmdBuscar.Parameters.AddWithValue("@MarcaId", marcaId);
                cmdBuscar.Parameters.AddWithValue("@Nombre", nombre);
                object result = cmdBuscar.ExecuteScalar();

                if (result != null)
                {
                    modeloId = Convert.ToInt32(result);
                }
                else
                {
                    // 2. Si no existe, Insertar
                    string queryInsert = "INSERT INTO dbo.Modelos (MarcaId, Nombre) OUTPUT INSERTED.ModeloId VALUES (@MarcaId, @Nombre)";
                    SqlCommand cmdInsert = new SqlCommand(queryInsert, con);
                    cmdInsert.Parameters.AddWithValue("@MarcaId", marcaId);
                    cmdInsert.Parameters.AddWithValue("@Nombre", nombre);

                    modeloId = (int)cmdInsert.ExecuteScalar();
                }
            }
            return modeloId;
        }
    }
}
