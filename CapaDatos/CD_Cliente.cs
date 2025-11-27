using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaDatos;
using Dominio;

namespace CapaDatos
{
    public class CD_Cliente
    {
        public Cliente BuscarPorDNI(string dni)
        {
            Cliente cliente = null;
            SqlConnection con = null;

            try
            {
                using (con = new CD_Conexion().AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_Cliente_BuscarPorDNI", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DNI", dni);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        cliente = new Cliente
                        {
                            ClienteID = Convert.ToInt32(reader["ClienteID"]),
                            DNI = reader["DNI"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty,
                            Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : string.Empty,
                            Direccion = reader["Direccion"] != DBNull.Value ? reader["Direccion"].ToString() : string.Empty,
                            FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                            Activo = Convert.ToBoolean(reader["Activo"])
                        };
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar cliente por DNI: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }

            return cliente;
        }

        public string RegistrarCliente(Cliente cliente)
        {
            string mensaje = string.Empty;
            SqlConnection con = null;

            try
            {
                using (con = new CD_Conexion().AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_Cliente_Registrar", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DNI", cliente.DNI);
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                    cmd.Parameters.AddWithValue("@Email", (object)cliente.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Telefono", (object)cliente.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Direccion", (object)cliente.Direccion ?? DBNull.Value);

                    SqlParameter outputParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                    mensaje = outputParam.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar cliente: " + ex.Message;
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