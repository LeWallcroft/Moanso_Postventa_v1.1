using CapaDominio;
using CapaDominio.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Repuesto
    {
        private readonly CD_Conexion _conexion;

        public CD_Repuesto()
        {
            _conexion = new CD_Conexion();
        }

        public async Task<List<Repuesto>> ListarActivosAsync()
        {
            var repuestos = new List<Repuesto>();

            using (var connection = _conexion.AbrirConexion())
            {
                using (var command = new SqlCommand("sp_Repuestos_ListarActivos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            repuestos.Add(MapToRepuesto(reader));
                        }
                    }
                }
            }

            return repuestos;
        }

        public async Task<List<Repuesto>> BuscarPorNombreAsync(string nombre)
        {
            var repuestos = new List<Repuesto>();

            using (var connection = _conexion.AbrirConexion())
            {
                using (var command = new SqlCommand("sp_Repuestos_BuscarPorNombre", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nombre", nombre ?? string.Empty);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            repuestos.Add(MapToRepuesto(reader));
                        }
                    }
                }
            }

            return repuestos;
        }

        public async Task<Repuesto> BuscarPorIdAsync(int repuestoId)
        {
            using (var connection = _conexion.AbrirConexion())
            {
                using (var command = new SqlCommand("sp_Repuestos_BuscarPorID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RepuestoID", repuestoId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapToRepuesto(reader);
                        }
                    }
                }
            }

            return null;
        }

        public async Task<List<Repuesto>> ObtenerStockBajoAsync()
        {
            var repuestos = new List<Repuesto>();

            using (var connection = _conexion.AbrirConexion())
            {
                using (var command = new SqlCommand("sp_Repuestos_ObtenerStockBajo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            repuestos.Add(MapToRepuesto(reader));
                        }
                    }
                }
            }

            return repuestos;
        }

        public async Task<ResultadoOperacion> CrearAsync(Repuesto repuesto)
        {
            using (var connection = _conexion.AbrirConexion())
            {
                using (var command = new SqlCommand("sp_Repuestos_Crear", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Nombre", repuesto.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", (object)repuesto.Descripcion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Precio", repuesto.Precio);
                    command.Parameters.AddWithValue("@Stock", repuesto.Stock);
                    command.Parameters.AddWithValue("@StockMinimo", repuesto.StockMinimo);
                    command.Parameters.AddWithValue("@Codigo", (object)repuesto.Codigo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Proveedor", (object)repuesto.Proveedor ?? DBNull.Value);

                    var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(mensajeParam);

                    await command.ExecuteNonQueryAsync();

                    var mensaje = mensajeParam.Value?.ToString() ?? "Error desconocido";
                    var exitoso = mensaje.ToLower().Contains("éxito") || mensaje.ToLower().Contains("creado");

                    if (exitoso)
                    {
                        return ResultadoOperacion.Exito(mensaje);
                    }
                    else
                    {
                        return ResultadoOperacion.Error(mensaje);
                    }
                }
            }
        }

        public async Task<ResultadoOperacion> ActualizarAsync(Repuesto repuesto)
        {
            using (var connection = _conexion.AbrirConexion())
            {
                using (var command = new SqlCommand("sp_Repuestos_Actualizar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RepuestoID", repuesto.RepuestoID);
                    command.Parameters.AddWithValue("@Nombre", repuesto.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", (object)repuesto.Descripcion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Precio", repuesto.Precio);
                    command.Parameters.AddWithValue("@Stock", repuesto.Stock);
                    command.Parameters.AddWithValue("@StockMinimo", repuesto.StockMinimo);
                    command.Parameters.AddWithValue("@Codigo", (object)repuesto.Codigo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Proveedor", (object)repuesto.Proveedor ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Activo", repuesto.Activo);

                    var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(mensajeParam);

                    await command.ExecuteNonQueryAsync();

                    var mensaje = mensajeParam.Value?.ToString() ?? "Error desconocido";
                    var exitoso = mensaje.ToLower().Contains("éxito") || mensaje.ToLower().Contains("actualizado");

                    if (exitoso)
                    {
                        return ResultadoOperacion.Exito(mensaje);
                    }
                    else
                    {
                        return ResultadoOperacion.Error(mensaje);
                    }
                }
            }
        }

        public async Task<ResultadoOperacion> InactivarAsync(int repuestoId)
        {
            using (var connection = _conexion.AbrirConexion())
            {
                using (var command = new SqlCommand("sp_Repuestos_Inactivar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RepuestoID", repuestoId);

                    var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(mensajeParam);

                    await command.ExecuteNonQueryAsync();

                    var mensaje = mensajeParam.Value?.ToString() ?? "Error desconocido";
                    var exitoso = mensaje.ToLower().Contains("éxito") || mensaje.ToLower().Contains("inactivado");

                    if (exitoso)
                    {
                        return ResultadoOperacion.Exito(mensaje);
                    }
                    else
                    {
                        return ResultadoOperacion.Error(mensaje);
                    }
                }
            }
        }

        public async Task<ResultadoOperacion> ActivarAsync(int repuestoId)
        {
            using (var connection = _conexion.AbrirConexion())
            {
                using (var command = new SqlCommand("sp_Repuestos_Activar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RepuestoID", repuestoId);

                    var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(mensajeParam);

                    await command.ExecuteNonQueryAsync();

                    var mensaje = mensajeParam.Value?.ToString() ?? "Error desconocido";
                    var exitoso = mensaje.ToLower().Contains("éxito") || mensaje.ToLower().Contains("activado");

                    if (exitoso)
                    {
                        return ResultadoOperacion.Exito(mensaje);
                    }
                    else
                    {
                        return ResultadoOperacion.Error(mensaje);
                    }
                }
            }
        }

        public async Task<ResultadoOperacion> ActualizarStockAsync(int repuestoId, int cantidad, string motivo)
        {
            using (var connection = _conexion.AbrirConexion())
            {
                using (var command = new SqlCommand("sp_Repuestos_ActualizarStock", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RepuestoID", repuestoId);
                    command.Parameters.AddWithValue("@Cantidad", cantidad);
                    command.Parameters.AddWithValue("@Motivo", motivo ?? "Ajuste de stock");

                    var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(mensajeParam);

                    await command.ExecuteNonQueryAsync();

                    var mensaje = mensajeParam.Value?.ToString() ?? "Error desconocido";
                    var exitoso = mensaje.ToLower().Contains("éxito") || mensaje.ToLower().Contains("actualizado");

                    if (exitoso)
                    {
                        return ResultadoOperacion.Exito(mensaje);
                    }
                    else
                    {
                        return ResultadoOperacion.Error(mensaje);
                    }
                }
            }
        }

        public async Task<object> ObtenerEstadisticasAsync()
        {
            using (var connection = _conexion.AbrirConexion())
            {
                using (var command = new SqlCommand("sp_Repuestos_Estadisticas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new
                            {
                                TotalRepuestosActivos = reader["TotalRepuestosActivos"] as int? ?? 0,
                                TotalRepuestosBajoStock = reader["TotalRepuestosBajoStock"] as int? ?? 0,
                                ValorTotalInventario = reader["ValorTotalInventario"] as decimal? ?? 0,
                                PrecioPromedio = reader["PrecioPromedio"] as decimal? ?? 0,
                                TotalRepuestosUltimoMes = reader["TotalRepuestosUltimoMes"] as int? ?? 0
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<bool> VerificarUsoEnServiciosAsync(int repuestoId)
        {
            using (var connection = _conexion.AbrirConexion())
            {
                using (var command = new SqlCommand("sp_Repuestos_PuedeUsarseEnServicios", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RepuestoID", repuestoId);

                    var resultado = await command.ExecuteScalarAsync();
                    return resultado != null && Convert.ToBoolean(resultado);
                }
            }
        }

        public async Task<List<Repuesto>> ListarInactivosAsync()
        {
            var repuestos = new List<Repuesto>();

            using (var connection = _conexion.AbrirConexion())
            {
                // Como no tenemos un SP específico para inactivos, usamos una consulta directa
                var query = @"
                    SELECT RepuestoID, Nombre, Descripcion, Precio, Stock, StockMinimo, 
                           Codigo, Proveedor, FechaCreacion, Activo
                    FROM Repuesto 
                    WHERE Activo = 0
                    ORDER BY Nombre";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            repuestos.Add(MapToRepuesto(reader));
                        }
                    }
                }
            }

            return repuestos;
        }

        public async Task<List<Repuesto>> ListarTodosAsync()
        {
            var repuestos = new List<Repuesto>();

            using (var connection = _conexion.AbrirConexion())
            {
                var query = @"
                    SELECT RepuestoID, Nombre, Descripcion, Precio, Stock, StockMinimo, 
                           Codigo, Proveedor, FechaCreacion, Activo
                    FROM Repuesto 
                    ORDER BY Nombre";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            repuestos.Add(MapToRepuesto(reader));
                        }
                    }
                }
            }

            return repuestos;
        }

        private Repuesto MapToRepuesto(SqlDataReader reader)
        {
            return new Repuesto
            {
                RepuestoID = Convert.ToInt32(reader["RepuestoID"]),
                Nombre = reader["Nombre"].ToString(),
                Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : null,
                Precio = Convert.ToDecimal(reader["Precio"]),
                Stock = Convert.ToInt32(reader["Stock"]),
                StockMinimo = Convert.ToInt32(reader["StockMinimo"]),
                Codigo = reader["Codigo"] != DBNull.Value ? reader["Codigo"].ToString() : null,
                Proveedor = reader["Proveedor"] != DBNull.Value ? reader["Proveedor"].ToString() : null,
                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                Activo = Convert.ToBoolean(reader["Activo"])
            };
        }
    }
}