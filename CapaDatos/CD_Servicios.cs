using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaDominio;

namespace CapaDatos
{
    public class CD_Servicios
    {
        private CD_Conexion conexion = new CD_Conexion();

        // MÉTODO DE DIAGNÓSTICO - AGREGAR ESTE
        public string DiagnosticarInactivos()
        {
            try
            {
                using (var con = conexion.AbrirConexion())
                using (var cmd = new SqlCommand("sp_Servicios_ListarInactivos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        int count = 0;
                        string resultado = "DIAGNÓSTICO - Servicios inactivos:\n";

                        while (dr.Read())
                        {
                            count++;
                            resultado += $"Servicio {count}: ID={dr["ServicioID"]}, Nombre='{dr["Nombre"]}', Activo={dr["Activo"]}\n";
                        }

                        resultado += $"Total encontrados: {count} servicios inactivos";
                        return resultado;
                    }
                }
            }
            catch (Exception ex)
            {
                return $"ERROR en diagnóstico: {ex.Message}";
            }
        }

        #region MÉTODOS PARA SERVICIOS

        // Listar todos los servicios activos
        public List<Servicio> Listar()
        {
            var lista = new List<Servicio>();

            using (var con = conexion.AbrirConexion())
            using (var cmd = new SqlCommand("sp_Servicios_ListarActivos", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Servicio
                        {
                            ServicioID = Convert.ToInt32(dr["ServicioID"]),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : "",
                            Precio = Convert.ToDecimal(dr["Precio"]),
                            DuracionEstimada = Convert.ToInt32(dr["DuracionEstimada"]),
                            RequiereRepuestos = Convert.ToBoolean(dr["RequiereRepuestos"]),
                            Activo = Convert.ToBoolean(dr["Activo"]),
                            CategoriaNombre = dr["CategoriaNombre"] != DBNull.Value ? dr["CategoriaNombre"].ToString() : "",
                            TipoNombre = dr["TipoNombre"] != DBNull.Value ? dr["TipoNombre"].ToString() : ""
                        });
                    }
                }
            }

            return lista;
        }

        // Listar todos los servicios inactivos - VERSIÓN MEJORADA
        public List<Servicio> ListarInactivos()
        {
            var lista = new List<Servicio>();

            try
            {
                using (var con = conexion.AbrirConexion())
                using (var cmd = new SqlCommand("sp_Servicios_ListarInactivos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var servicio = new Servicio
                            {
                                ServicioID = Convert.ToInt32(dr["ServicioID"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : "",
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                DuracionEstimada = Convert.ToInt32(dr["DuracionEstimada"]),
                                RequiereRepuestos = Convert.ToBoolean(dr["RequiereRepuestos"]),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                                CategoriaNombre = dr["CategoriaNombre"] != DBNull.Value ? dr["CategoriaNombre"].ToString() : "",
                                TipoNombre = dr["TipoNombre"] != DBNull.Value ? dr["TipoNombre"].ToString() : "",
                                UsuarioCreacion = dr["UsuarioCreacion"] != DBNull.Value ? dr["UsuarioCreacion"].ToString() : ""
                            };

                            // DIAGNÓSTICO EN CONSOLA
                            Console.WriteLine($"Cargando servicio inactivo: {servicio.Nombre}, Activo={servicio.Activo}, ID={servicio.ServicioID}");

                            lista.Add(servicio);
                        }
                    }
                }

                Console.WriteLine($"CD_Servicios.ListarInactivos() - Total servicios inactivos cargados: {lista.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR en ListarInactivos: {ex.Message}");
                throw new Exception("Error al listar servicios inactivos: " + ex.Message);
            }

            return lista;
        }

        // Resto de tus métodos permanecen igual...
        // Buscar servicio por ID
        public Servicio BuscarPorID(int servicioID)
        {
            Servicio servicio = null;

            using (var con = conexion.AbrirConexion())
            using (var cmd = new SqlCommand("sp_Servicios_BuscarPorID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ServicioID", servicioID);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        servicio = new Servicio
                        {
                            ServicioID = Convert.ToInt32(dr["ServicioID"]),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : "",
                            Precio = Convert.ToDecimal(dr["Precio"]),
                            DuracionEstimada = Convert.ToInt32(dr["DuracionEstimada"]),
                            RequiereRepuestos = Convert.ToBoolean(dr["RequiereRepuestos"]),
                            Activo = Convert.ToBoolean(dr["Activo"]),
                            CategoriaservicioID = dr["CategoriaservicioID"] != DBNull.Value ? Convert.ToInt32(dr["CategoriaservicioID"]) : (int?)null,
                            TiposervicioID = dr["TiposervicioID"] != DBNull.Value ? Convert.ToInt32(dr["TiposervicioID"]) : (int?)null,
                            UsuariosID = dr["UsuariosID"] != DBNull.Value ? Convert.ToInt32(dr["UsuariosID"]) : (int?)null
                        };
                    }
                }
            }

            return servicio;
        }

        // Insertar nuevo servicio
        public string Insertar(Servicio servicio)
        {
            string mensaje = "";

            using (var con = conexion.AbrirConexion())
            using (var cmd = new SqlCommand("sp_Servicios_Crear", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoriaservicioID",
                    servicio.CategoriaservicioID.HasValue ? (object)servicio.CategoriaservicioID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@UsuariosID",
                    servicio.UsuariosID.HasValue ? (object)servicio.UsuariosID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@TiposervicioID",
                    servicio.TiposervicioID.HasValue ? (object)servicio.TiposervicioID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Nombre", servicio.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion",
                    string.IsNullOrEmpty(servicio.Descripcion) ? DBNull.Value : (object)servicio.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", servicio.Precio);
                cmd.Parameters.AddWithValue("@DuracionEstimada", servicio.DuracionEstimada);
                cmd.Parameters.AddWithValue("@RequiereRepuestos", servicio.RequiereRepuestos);

                var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);

                try
                {
                    cmd.ExecuteNonQuery();
                    mensaje = mensajeParam.Value.ToString();
                }
                catch (Exception ex)
                {
                    mensaje = "Error al crear servicio: " + ex.Message;
                }
            }

            return mensaje;
        }

        // Actualizar servicio
        public string Actualizar(Servicio servicio)
        {
            string mensaje = "";

            using (var con = conexion.AbrirConexion())
            using (var cmd = new SqlCommand("sp_Servicios_Actualizar", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ServicioID", servicio.ServicioID);
                cmd.Parameters.AddWithValue("@CategoriaservicioID",
                    servicio.CategoriaservicioID.HasValue ? (object)servicio.CategoriaservicioID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@UsuariosID",
                    servicio.UsuariosID.HasValue ? (object)servicio.UsuariosID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@TiposervicioID",
                    servicio.TiposervicioID.HasValue ? (object)servicio.TiposervicioID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Nombre", servicio.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion",
                    string.IsNullOrEmpty(servicio.Descripcion) ? DBNull.Value : (object)servicio.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", servicio.Precio);
                cmd.Parameters.AddWithValue("@DuracionEstimada", servicio.DuracionEstimada);
                cmd.Parameters.AddWithValue("@RequiereRepuestos", servicio.RequiereRepuestos);
                cmd.Parameters.AddWithValue("@Activo", servicio.Activo);

                var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);

                try
                {
                    cmd.ExecuteNonQuery();
                    mensaje = mensajeParam.Value.ToString();
                }
                catch (Exception ex)
                {
                    mensaje = "Error al actualizar servicio: " + ex.Message;
                }
            }

            return mensaje;
        }

        // Eliminar/Inactivar servicio
        public string Eliminar(int servicioID)
        {
            string mensaje = "";

            using (var con = conexion.AbrirConexion())
            using (var cmd = new SqlCommand("sp_Servicios_Inactivar", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ServicioID", servicioID);

                var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);

                try
                {
                    cmd.ExecuteNonQuery();
                    mensaje = mensajeParam.Value.ToString();
                }
                catch (Exception ex)
                {
                    mensaje = "Error al eliminar servicio: " + ex.Message;
                }
            }

            return mensaje;
        }

        // Activar servicio
        public string Activar(int servicioID)
        {
            string mensaje = "";

            using (var con = conexion.AbrirConexion())
            using (var cmd = new SqlCommand("sp_Servicios_Activar", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ServicioID", servicioID);

                var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);

                try
                {
                    cmd.ExecuteNonQuery();
                    mensaje = mensajeParam.Value.ToString();
                }
                catch (Exception ex)
                {
                    mensaje = "Error al activar servicio: " + ex.Message;
                }
            }

            return mensaje;
        }

        #endregion

        #region MÉTODOS PARA CATEGORÍAS

        // Listar categorías activas
        public List<CategoriaServicio> ListarCategorias()
        {
            var lista = new List<CategoriaServicio>();

            using (var con = conexion.AbrirConexion())
            using (var cmd = new SqlCommand("sp_CategoriasServicio_ListarActivas", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new CategoriaServicio
                        {
                            CategoriaservicioID = Convert.ToInt32(dr["CategoriaservicioID"]),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : "",
                            FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                            Activo = Convert.ToBoolean(dr["Activo"])
                        });
                    }
                }
            }

            return lista;
        }

        #endregion

        #region MÉTODOS PARA TIPOS DE SERVICIO

        // Listar tipos de servicio activos
        public List<TipoServicio> ListarTiposServicio()
        {
            var lista = new List<TipoServicio>();

            using (var con = conexion.AbrirConexion())
            using (var cmd = new SqlCommand("sp_TiposServicio_ListarActivos", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new TipoServicio
                        {
                            TiposervicioID = Convert.ToInt32(dr["TiposervicioID"]),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : "",
                            FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                            Activo = Convert.ToBoolean(dr["Activo"])
                        });
                    }
                }
            }

            return lista;
        }

        #endregion

        #region MÉTODOS PARA REPUESTOS

        // Listar repuestos activos
        public List<Repuesto> ListarRepuestos()
        {
            var lista = new List<Repuesto>();

            using (var con = conexion.AbrirConexion())
            using (var cmd = new SqlCommand("sp_Repuestos_ListarActivos", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Repuesto
                        {
                            RepuestoID = Convert.ToInt32(dr["RepuestoID"]),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : "",
                            Precio = Convert.ToDecimal(dr["Precio"]),
                            Stock = Convert.ToInt32(dr["Stock"]),
                            StockMinimo = Convert.ToInt32(dr["StockMinimo"]),
                            Codigo = dr["Codigo"] != DBNull.Value ? dr["Codigo"].ToString() : "",
                            Proveedor = dr["Proveedor"] != DBNull.Value ? dr["Proveedor"].ToString() : "",
                            FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                            Activo = Convert.ToBoolean(dr["Activo"])
                        });
                    }
                }
            }

            return lista;
        }

        // Agregar repuesto a servicio
        public string AgregarRepuestoAServicio(int servicioID, int repuestoID, int cantidad, string observaciones = "")
        {
            string mensaje = "";

            using (var con = conexion.AbrirConexion())
            using (var cmd = new SqlCommand("sp_Servicios_AgregarRepuesto", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ServicioID", servicioID);
                cmd.Parameters.AddWithValue("@RepuestoID", repuestoID);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Observaciones",
                    string.IsNullOrEmpty(observaciones) ? DBNull.Value : (object)observaciones);

                var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);

                try
                {
                    cmd.ExecuteNonQuery();
                    mensaje = mensajeParam.Value.ToString();
                }
                catch (Exception ex)
                {
                    mensaje = "Error al agregar repuesto: " + ex.Message;
                }
            }

            return mensaje;
        }

        // Quitar repuesto de servicio
        public string QuitarRepuestoDeServicio(int servicioID, int repuestoID)
        {
            string mensaje = "";

            using (var con = conexion.AbrirConexion())
            using (var cmd = new SqlCommand("sp_Servicios_QuitarRepuesto", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ServicioID", servicioID);
                cmd.Parameters.AddWithValue("@RepuestoID", repuestoID);

                var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);

                try
                {
                    cmd.ExecuteNonQuery();
                    mensaje = mensajeParam.Value.ToString();
                }
                catch (Exception ex)
                {
                    mensaje = "Error al quitar repuesto: " + ex.Message;
                }
            }

            return mensaje;
        }

        // Listar repuestos de un servicio
        public List<RepuestoServicio> ListarRepuestosDeServicio(int servicioID)
        {
            var lista = new List<RepuestoServicio>();

            using (var con = conexion.AbrirConexion())
            using (var cmd = new SqlCommand("sp_Servicios_ListarRepuestos", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ServicioID", servicioID);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new RepuestoServicio
                        {
                            RepuestosservicioID = Convert.ToInt32(dr["RepuestosservicioID"]),
                            RepuestoID = Convert.ToInt32(dr["RepuestoID"]),
                            ServicioID = servicioID,
                            Cantidad = Convert.ToInt32(dr["Cantidad"]),
                            Observaciones = dr["Observaciones"] != DBNull.Value ? dr["Observaciones"].ToString() : "",
                            FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                            RepuestoNombre = dr["RepuestoNombre"].ToString(),
                            PrecioUnitario = Convert.ToDecimal(dr["PrecioUnitario"]),
                            StockActual = Convert.ToInt32(dr["Stock"])
                        });
                    }
                }
            }

            return lista;
        }

        #endregion
    }
}