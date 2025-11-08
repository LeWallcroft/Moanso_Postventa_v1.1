using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Marca
    {
        private CD_Conexion conexion = new CD_Conexion();

        // Método principal: Busca la marca por nombre. Si no existe, la inserta y devuelve el ID.
        public int ObtenerOCrearMarcaId(string nombre)
        {
            int marcaId = 0;
            string query = "SELECT MarcaId FROM dbo.Marcas WHERE Nombre = @Nombre";

            using (SqlConnection con = conexion.AbrirConexion())
            {
                // 1. Intentar Buscar
                SqlCommand cmdBuscar = new SqlCommand(query, con);
                cmdBuscar.Parameters.AddWithValue("@Nombre", nombre);
                object result = cmdBuscar.ExecuteScalar();

                if (result != null)
                {
                    marcaId = Convert.ToInt32(result);
                }
                else
                {
                    // 2. Si no existe, Insertar
                    string queryInsert = "INSERT INTO dbo.Marcas (Nombre) OUTPUT INSERTED.MarcaId VALUES (@Nombre)";
                    SqlCommand cmdInsert = new SqlCommand(queryInsert, con);
                    cmdInsert.Parameters.AddWithValue("@Nombre", nombre);

                    marcaId = (int)cmdInsert.ExecuteScalar();
                }
            }
            return marcaId;
        }
    }
}
