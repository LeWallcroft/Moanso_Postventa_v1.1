using CapaDominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Bahia
    {
        private CD_Conexion conexion = new CD_Conexion();

        public List<Bahia> ListarActivas()
        {
            List<Bahia> lista = new List<Bahia>();
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Bahias_ListarActivas", con);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Bahia()
                            {
                                BahiaId = Convert.ToInt32(reader["BahiaId"]),
                                Nombre = reader["Nombre"].ToString(),
                                Tipo = reader["Tipo"].ToString(),
                                Activo = true
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar bahías activas: " + ex.Message);
                }
            }
            return lista;
        }

        // NUEVO MÉTODO: Listar todas las bahías
        public List<Bahia> ListarTodas()
        {
            List<Bahia> lista = new List<Bahia>();
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT BahiaId, Nombre, Tipo, Activo FROM Bahias ORDER BY Nombre", con);
                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Bahia()
                            {
                                BahiaId = Convert.ToInt32(reader["BahiaId"]),
                                Nombre = reader["Nombre"].ToString(),
                                Tipo = reader["Tipo"].ToString(),
                                Activo = Convert.ToBoolean(reader["Activo"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar todas las bahías: " + ex.Message);
                }
            }
            return lista;
        }

        // NUEVO MÉTODO: Habilitar bahía
        public string HabilitarBahia(int bahiaId)
        {
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("UPDATE Bahias SET Activo = 1 WHERE BahiaId = @BahiaId", con);
                cmd.Parameters.AddWithValue("@BahiaId", bahiaId);
                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        return "Bahía habilitada con éxito";
                    else
                        return "Error: No se encontró la bahía especificada";
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al habilitar bahía: " + ex.Message);
                }
            }
        }

        public string CrearBahia(string nombre, string tipo)
        {
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Bahia_Crear", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);
                try
                {
                    cmd.ExecuteNonQuery();
                    return mensajeParam.Value.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al crear bahía: " + ex.Message);
                }
            }
        }

        public string ModificarBahia(int bahiaId, string nombre, string tipo)
        {
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Bahia_Modificar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BahiaId", bahiaId);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);
                try
                {
                    cmd.ExecuteNonQuery();
                    return mensajeParam.Value.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al modificar bahía: " + ex.Message);
                }
            }
        }

        public string InhabilitarBahia(int bahiaId)
        {
            using (SqlConnection con = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Bahia_Inhabilitar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BahiaId", bahiaId);
                SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);
                try
                {
                    cmd.ExecuteNonQuery();
                    return mensajeParam.Value.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al inhabilitar bahía: " + ex.Message);
                }
            }
        }
    }
}