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
    public class CD_Cliente
    {
        private CD_Conexion conexion = new CD_Conexion();

        // Método 1: Búsqueda por DNI (Usa sp_Recepcion_BuscarClienteYVehiculos)
        public Cliente BuscarClienteYVehiculos(string dni, out List<Vehiculo> vehiculos)
        {
            Cliente clienteEncontrado = null;
            vehiculos = new List<Vehiculo>();

            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Recepcion_BuscarClienteYVehiculos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DNI", dni);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Primer Resultado: Cliente
                    if (reader.Read())
                    {
                        clienteEncontrado = new Cliente
                        {
                            ClienteId = Convert.ToInt32(reader["ClienteId"]),
                            Nombres = reader["Nombres"].ToString(),
                            Apellidos = reader["Apellidos"].ToString(),
                            Email = reader["Email"].ToString(),
                            // Telefono1 es el que se usa en la BD para el principal
                            Telefono = reader["Telefono1"].ToString(),
                            DNI = dni
                        };
                    }

                    // Segundo Resultado: Vehículos del Cliente
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            vehiculos.Add(new Vehiculo
                            {
                                VehiculoId = Convert.ToInt32(reader["VehiculoId"]),
                                Placa = reader["Placa"].ToString(),
                                // VIN puede ser NULL
                                VIN = reader["VIN"] == DBNull.Value ? null : reader["VIN"].ToString(),
                                Marca = reader["MarcaNombre"].ToString(),
                                Modelo = reader["ModeloNombre"].ToString(),
                                Anio = Convert.ToInt32(reader["Anio"])
                            });
                        }
                    }
                }
            }
            return clienteEncontrado;
        }

        // Método 2: Creación de un nuevo cliente
        public int CrearCliente(Cliente cliente)
        {
            int clienteId = 0;

            // El DNI está mapeado como Documento en la entidad Cliente original, pero usamos DNI aquí
            string query = "INSERT INTO dbo.Clientes (DNI, Nombres, Apellidos, Email, Telefono1, Direccion) " +
                           "OUTPUT INSERTED.ClienteId " +
                           "VALUES (@DNI, @Nombres, @Apellidos, @Email, @Telefono1, NULL)";

            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@DNI", cliente.DNI);
                cmd.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@Email", (object)cliente.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefono1", (object)cliente.Telefono ?? DBNull.Value);

                try
                {
                    // ExecuteScalar devuelve la primera columna de la primera fila (en este caso, ClienteId)
                    clienteId = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar un nuevo cliente: " + ex.Message);
                }
            }
            return clienteId;
        }
    }
}
