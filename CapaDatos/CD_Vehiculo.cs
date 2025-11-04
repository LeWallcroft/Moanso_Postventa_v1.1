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
    public class CD_Vehiculo
    {
        private CD_Conexion conexion = new CD_Conexion();

        // Método: Creación de un nuevo vehículo
        public int CrearVehiculo(Vehiculo vehiculo)
        {
            int vehiculoId = 0;

            // La consulta asume que ya tenemos MarcaId y ModeloId, que es lo que la tabla Vehiculos requiere.
            string query = "INSERT INTO dbo.Vehiculos (ClienteId, Placa, VIN, MarcaId, ModeloId, Anio) " +
                           "OUTPUT INSERTED.VehiculoId " +
                           "VALUES (@ClienteId, @Placa, @VIN, @MarcaId, @ModeloId, @Anio)";

            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ClienteId", vehiculo.ClienteId);
                cmd.Parameters.AddWithValue("@Placa", vehiculo.Placa);
                cmd.Parameters.AddWithValue("@VIN", (object)vehiculo.VIN ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MarcaId", vehiculo.MarcaId);
                cmd.Parameters.AddWithValue("@ModeloId", vehiculo.ModeloId);
                cmd.Parameters.AddWithValue("@Anio", vehiculo.Anio);

                try
                {
                    vehiculoId = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar un nuevo vehículo: " + ex.Message);
                }
            }
            return vehiculoId;
        }
    }
}
