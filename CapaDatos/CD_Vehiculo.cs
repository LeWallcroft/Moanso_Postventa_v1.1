using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaDatos;
using Dominio;

namespace CapaDatos
{
    public class CD_Vehiculo
    {
        public List<Vehiculo> ListarPorCliente(int clienteID)
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            SqlConnection con = null;

            try
            {
                using (con = new CD_Conexion().AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_Vehiculo_ListarPorCliente", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClienteID", clienteID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculo vehiculo = new Vehiculo
                        {
                            VehiculoID = Convert.ToInt32(reader["VehiculoID"]),
                            Placa = reader["Placa"].ToString(),
                            VIN = reader["VIN"] != DBNull.Value ? reader["VIN"].ToString() : string.Empty,
                            Color = reader["Color"] != DBNull.Value ? reader["Color"].ToString() : string.Empty,
                            Anio = reader["Anio"] != DBNull.Value ? Convert.ToInt32(reader["Anio"]) : (int?)null,
                            Kilometraje = reader["Kilometraje"] != DBNull.Value ? Convert.ToInt32(reader["Kilometraje"]) : (int?)null,
                            Combustible = reader["Combustible"] != DBNull.Value ? reader["Combustible"].ToString() : string.Empty,
                            Transmision = reader["Transmision"] != DBNull.Value ? reader["Transmision"].ToString() : string.Empty,
                            Modelo = reader["Modelo"].ToString(),
                            Marca = reader["Marca"].ToString(),
                            TipoVehiculo = reader["TipoVehiculo"] != DBNull.Value ? reader["TipoVehiculo"].ToString() : string.Empty,
                            FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"])
                        };

                        vehiculos.Add(vehiculo);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar vehículos del cliente: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }

            return vehiculos;
        }

        public Vehiculo BuscarPorPlaca(string placa)
        {
            Vehiculo vehiculo = null;
            SqlConnection con = null;

            try
            {
                using (con = new CD_Conexion().AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_Vehiculo_BuscarPorPlaca", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Placa", placa);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        vehiculo = new Vehiculo
                        {
                            VehiculoID = Convert.ToInt32(reader["VehiculoID"]),
                            Placa = reader["Placa"].ToString(),
                            VIN = reader["VIN"] != DBNull.Value ? reader["VIN"].ToString() : string.Empty,
                            Color = reader["Color"] != DBNull.Value ? reader["Color"].ToString() : string.Empty,
                            Anio = reader["Anio"] != DBNull.Value ? Convert.ToInt32(reader["Anio"]) : (int?)null,
                            Kilometraje = reader["Kilometraje"] != DBNull.Value ? Convert.ToInt32(reader["Kilometraje"]) : (int?)null,
                            Combustible = reader["Combustible"] != DBNull.Value ? reader["Combustible"].ToString() : string.Empty,
                            Transmision = reader["Transmision"] != DBNull.Value ? reader["Transmision"].ToString() : string.Empty,
                            ClienteID = Convert.ToInt32(reader["ClienteID"]),
                            ClienteDNI = reader["ClienteDNI"].ToString(),
                            ClienteNombre = reader["ClienteNombre"].ToString(),
                            ClienteApellido = reader["ClienteApellido"].ToString(),
                            Modelo = reader["Modelo"].ToString(),
                            Marca = reader["Marca"].ToString(),
                            TipoVehiculo = reader["TipoVehiculo"] != DBNull.Value ? reader["TipoVehiculo"].ToString() : string.Empty,
                            FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"])
                        };
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar vehículo por placa: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }

            return vehiculo;
        }

        public string RegistrarVehiculo(Vehiculo vehiculo, int usuarioID = 0)
        {
            string mensaje = string.Empty;
            SqlConnection con = null;

            try
            {
                using (con = new CD_Conexion().AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_Vehiculo_Registrar", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ClienteID", vehiculo.ClienteID);
                    cmd.Parameters.AddWithValue("@ModeloID", vehiculo.ModeloID);
                    cmd.Parameters.AddWithValue("@Placa", vehiculo.Placa);
                    cmd.Parameters.AddWithValue("@VIN", (object)vehiculo.VIN ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Color", (object)vehiculo.Color ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Anio", (object)vehiculo.Anio ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Kilometraje", (object)vehiculo.Kilometraje ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Combustible", (object)vehiculo.Combustible ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Transmision", (object)vehiculo.Transmision ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UsuarioID", usuarioID > 0 ? (object)usuarioID : DBNull.Value);

                    SqlParameter outputParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                    mensaje = outputParam.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar vehículo: " + ex.Message;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }

            return mensaje;
        }
    }
}