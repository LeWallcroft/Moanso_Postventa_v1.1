using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaDatos;
using Dominio;

namespace CapaDatos
{
    public class CD_MarcaModelo
    {
        public List<Marca> ListarMarcas()
        {
            List<Marca> marcas = new List<Marca>();
            SqlConnection con = null;

            try
            {
                using (con = new CD_Conexion().AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_Marca_ListarActivas", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Marca marca = new Marca
                        {
                            MarcaID = Convert.ToInt32(reader["MarcaID"]),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : string.Empty,
                            PaisOrigen = reader["PaisOrigen"] != DBNull.Value ? reader["PaisOrigen"].ToString() : string.Empty
                        };

                        marcas.Add(marca);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar marcas: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }

            return marcas;
        }

        public List<Modelo> ListarModelosPorMarca(int marcaID)
        {
            List<Modelo> modelos = new List<Modelo>();
            SqlConnection con = null;

            try
            {
                using (con = new CD_Conexion().AbrirConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_Modelo_ListarPorMarca", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MarcaID", marcaID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Modelo modelo = new Modelo
                        {
                            ModeloID = Convert.ToInt32(reader["ModeloID"]),
                            MarcaID = marcaID,
                            Nombre = reader["Nombre"].ToString(),
                            Anio = reader["Anio"] != DBNull.Value ? Convert.ToInt32(reader["Anio"]) : (int?)null,
                            TipoVehiculo = reader["TipoVehiculo"] != DBNull.Value ? reader["TipoVehiculo"].ToString() : string.Empty,
                            Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : string.Empty
                        };

                        modelos.Add(modelo);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar modelos por marca: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }

            return modelos;
        }
    }
}