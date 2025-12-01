using CapaDominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_OrdenTrabajo
    {
        private readonly CD_Conexion conexion = new CD_Conexion();
        public List<OrdenTrabajo> Listar()
        {
            var lista = new List<OrdenTrabajo>();

            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Ordentrabajo_Listar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                

                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    if (!dr.HasRows)
                    {
                        // DEBUG
                        Console.WriteLine("El SP no devolvió filas.");
                        return lista;
                    }



                    while (dr.Read())
                    {
                        var ot = new OrdenTrabajo
                        {
                            OrdentrabajoID = Convert.ToInt32(dr["OrdentrabajoID"]),
                            FechaInicio = dr["FechaInicio"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaInicio"]),
                            Observaciones = dr["Observaciones"] == DBNull.Value ? null : dr["Observaciones"].ToString(),  
                            Prioridad = dr["Prioridad"] == DBNull.Value ? 1 : Convert.ToInt32(dr["Prioridad"]),
                            FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),

                            
                            NombreCliente = dr["NombreCliente"]?.ToString(),
                            Placa = dr["Placa"]?.ToString(),
                            DescripcionVehiculo = dr["DescripcionVehiculo"]?.ToString(),
                            TipoServicio = dr["TipoServicio"]?.ToString(),
                            NombreTecnico = dr["NombreTecnico"]?.ToString(),
                            EstadoOT = dr["EstadoOT"]?.ToString(),

                            TecnicoID = dr["TecnicoID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["TecnicoID"]),
                            EstadootID = dr["EstadootID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["EstadootID"])
                        };

                        lista.Add(ot);
                    }
                }
            }

            return lista;
        }      

        public OrdenTrabajo ObtenerPorId(int ordentrabajoID)
        {
            OrdenTrabajo ot = null;

            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Ordentrabajo_ObtenerPorId", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        ot = new OrdenTrabajo
                        {
                            OrdentrabajoID = Convert.ToInt32(dr["OrdentrabajoID"]),
                            CitaID = dr["CitaID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["CitaID"]),
                            EstadootID = dr["EstadootID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["EstadootID"]),
                            UsuariosID = dr["UsuariosID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["UsuariosID"]),
                            TecnicoID = dr["TecnicoID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["TecnicoID"]),
                            FechaInicio = dr["FechaInicio"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaInicio"]),
                            FechaFin = dr["FechaFin"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaFin"]),
                            Observaciones = dr["Observaciones"] == DBNull.Value ? null : dr["Observaciones"].ToString(),
                            Prioridad = dr["Prioridad"] == DBNull.Value ? 1 : Convert.ToInt32(dr["Prioridad"]),
                            KilometrajeEntrada = dr["KilometrajeEntrada"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["KilometrajeEntrada"]),
                            KilometrajeSalida = dr["KilometrajeSalida"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["KilometrajeSalida"]),
                            FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),

                            // UI existentes
                            NombreCliente = dr["NombreCliente"]?.ToString(),
                            Placa = dr["Placa"]?.ToString(),
                            ModeloVehiculo = dr["ModeloVehiculo"] == DBNull.Value
                                            ? null
                                            : dr["ModeloVehiculo"].ToString(),

                            DescripcionVehiculo = dr["DescripcionVehiculo"] == DBNull.Value
                                            ? null
                                            : dr["DescripcionVehiculo"].ToString(),
                            TipoServicio = dr["TipoServicio"]?.ToString(),
                            NombreTecnico = dr["NombreTecnico"]?.ToString(),
                            EstadoOT = dr["EstadoOT"]?.ToString(),

                            // Nuevos campos CLIENTE
                            DocumentoCliente = dr["DocumentoCliente"] == DBNull.Value ? null : dr["DocumentoCliente"].ToString(),
                            TelefonoCliente = dr["TelefonoCliente"] == DBNull.Value ? null : dr["TelefonoCliente"].ToString(),
                            EmailCliente = dr["EmailCliente"] == DBNull.Value ? null : dr["EmailCliente"].ToString(),
                            DireccionCliente = dr["DireccionCliente"] == DBNull.Value ? null : dr["DireccionCliente"].ToString(),

                            // Nuevos campos VEHÍCULO
                            
                            Vin = dr["Vin"] == DBNull.Value ? null : dr["Vin"].ToString(),
                            Color = dr["Color"] == DBNull.Value ? null : dr["Color"].ToString(),
                            AnioVehiculo = dr["AnioVehiculo"] == DBNull.Value ? null : dr["AnioVehiculo"].ToString(),
                            TipoCombustible = dr["TipoCombustible"] == DBNull.Value ? null : dr["TipoCombustible"].ToString(),
                            Transmision = dr["Transmision"] == DBNull.Value ? null : dr["Transmision"].ToString(),
                            NumeroRegistro = dr["NumeroRegistro"] == DBNull.Value ? null : dr["NumeroRegistro"].ToString(),
                            EstadoVehiculo = dr["EstadoVehiculo"] == DBNull.Value ? null : dr["EstadoVehiculo"].ToString()
                        };
                    }
                }
            }

            return ot;
        }



        public void AsignarTecnico(int ordentrabajoID, int? tecnicoID)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Ordentrabajo_AsignarTecnico", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);

                if (tecnicoID.HasValue)
                if (tecnicoID.HasValue)
                    cmd.Parameters.AddWithValue("@TecnicoID", tecnicoID.Value);
                else
                    cmd.Parameters.AddWithValue("@TecnicoID", DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }


        public void ActualizarObservaciones(int ordentrabajoID, string observaciones)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("sp_ActualizarObservaciones", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);
                cmd.Parameters.AddWithValue("@Observaciones", observaciones);

                cmd.ExecuteNonQuery();
            }
        }

        public void MarcarInicioTrabajo(int ordentrabajoID)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Ordentrabajo_MarcarInicioTrabajo", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);
                cmd.ExecuteNonQuery();
            }
        }

        public void MarcarActividadesTerminadas(int ordentrabajoID)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Ordentrabajo_MarcarActividadesTerminadas", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);
                cmd.ExecuteNonQuery();
            }
        }

        public void CambiarEstadoOT(int ordentrabajoID, int estadootID)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Ordentrabajo_CambiarEstado", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);
                cmd.Parameters.AddWithValue("@EstadootID", estadootID);
                cmd.ExecuteNonQuery();
            }
        }



        public List<EstadoOT> ListarEstadosOT()
        {
            var lista = new List<EstadoOT>();

            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT EstadootID, Nombre, Descripcion FROM Estadoot WHERE Activo = 1 ORDER BY Nombre", cn))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new EstadoOT
                        {
                            EstadootID = Convert.ToInt32(dr["EstadootID"]),
                            Nombre = dr["Nombre"]?.ToString(),
                            Descripcion = dr["Descripcion"] == DBNull.Value ? null : dr["Descripcion"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        public OrdenPago ObtenerOrdenPagoPorOrden(int ordentrabajoID)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("sp_OrdenPago_ObtenerPorOT", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new OrdenPago
                        {
                            OrdenPagoID = Convert.ToInt32(dr["OrdenPagoID"]),
                            OrdentrabajoID = Convert.ToInt32(dr["OrdentrabajoID"]),
                            Serie = dr["Serie"]?.ToString(),
                            Estado = dr["Estado"]?.ToString(),
                            Observaciones = dr["Observaciones"] == DBNull.Value ? null : dr["Observaciones"].ToString(),
                            Total = dr["Total"] == DBNull.Value ? 0m : Convert.ToDecimal(dr["Total"])
                        };
                    }
                }
            }
            return null;
        }

        public List<RepuestoOT> ListarRepuestosPorOrden(int ordentrabajoID)
        {
            var lista = new List<RepuestoOT>();

            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Otrepuesto_ListarPorOrdenTrabajo", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new RepuestoOT
                        {
                            OtrepuestoID = Convert.ToInt32(dr["OtrepuestoID"]),
                            OrdentrabajoID = Convert.ToInt32(dr["OrdentrabajoID"]),
                            RepuestoID = Convert.ToInt32(dr["RepuestoID"]),
                            Codigo = dr["Codigo"]?.ToString(),
                            Nombre = dr["Nombre"]?.ToString(),
                            Cantidad = Convert.ToInt32(dr["Cantidad"]),
                            PrecioUnitario = Convert.ToDecimal(dr["PrecioUnitario"]),
                            Subtotal = Convert.ToDecimal(dr["Subtotal"]),
                            EsDefault = Convert.ToBoolean(dr["EsDefault"])
                        });
                    }
                }
            }

            return lista;
        }

        public void EliminarRepuestoExtra(int otrepuestoID)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Otrepuesto_EliminarExtra", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OtrepuestoID", otrepuestoID);
                cmd.ExecuteNonQuery();
            }
        }

        public void AgregarRepuestoExtra(int ordentrabajoID, int repuestoID, int cantidad)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Otrepuesto_AgregarExtra", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);
                cmd.Parameters.AddWithValue("@RepuestoID", repuestoID);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.ExecuteNonQuery();
            }
        }

        public int CrearDesdeCita(int citaId, int usuariosId, int prioridad, int? kilometrajeEntrada)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Ordentrabajo_CrearDesdeCita", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CitaID", citaId);
                cmd.Parameters.AddWithValue("@UsuariosID", usuariosId);
                cmd.Parameters.AddWithValue("@Prioridad", prioridad);

                if (kilometrajeEntrada.HasValue)
                    cmd.Parameters.AddWithValue("@KilometrajeEntrada", kilometrajeEntrada.Value);
                else
                    cmd.Parameters.AddWithValue("@KilometrajeEntrada", DBNull.Value);

                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }


        public DateTime? ObtenerFechaControl(int ordentrabajoID)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand(@"
                    SELECT TOP 1 c.FechaControl
                    FROM Controlcalidad c
                    INNER JOIN Entregavehiculo e ON e.ControlcalidadID = c.ControlcalidadID
                    WHERE e.OrdentrabajoID = @id
                    ORDER BY c.ControlcalidadID DESC", cn))
            {
                cmd.Parameters.AddWithValue("@id", ordentrabajoID);
                var result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value) return null;
                return Convert.ToDateTime(result);
            }
        }



        public DateTime? ObtenerFechaEntrega(int ordentrabajoID)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand(@"
                    SELECT TOP 1 FechaEntrega
                    FROM Entregavehiculo
                    WHERE OrdentrabajoID = @id
                    ORDER BY EntregavehiculoID DESC", cn))
            {
                cmd.Parameters.AddWithValue("@id", ordentrabajoID);
                var result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value) return null;
                return Convert.ToDateTime(result);
            }
        }



        public void RegistrarControlCalidad(
              int ordentrabajoID,
              int usuariosID,
              string resultado,
              string observaciones,
              string xmlChecklist)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Controlcalidad_Procesar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrdentrabajoID", ordentrabajoID);
                cmd.Parameters.AddWithValue("@TecnicoID", usuariosID); 
                cmd.Parameters.AddWithValue("@Resultado", resultado);
                cmd.Parameters.AddWithValue("@Observaciones",
                    string.IsNullOrWhiteSpace(observaciones) ? (object)DBNull.Value : observaciones);
                cmd.Parameters.AddWithValue("@Checklist", xmlChecklist);

                cmd.ExecuteNonQuery();
            }
        }

    }
}
