using CapaDatos;
using CapaDominio;
using CapaDominio.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaLogica
{
    public class CN_Servicios
    {
        private CD_Servicios cdServicios = new CD_Servicios();

        // MÉTODO DE DIAGNÓSTICO - AGREGAR ESTE
        public string DiagnosticarInactivos()
        {
            try
            {
                return cdServicios.DiagnosticarInactivos();
            }
            catch (Exception ex)
            {
                return $"Error en diagnóstico: {ex.Message}";
            }
        }

        #region MÉTODOS PARA SERVICIOS

        // Listar todos los servicios activos
        public List<Servicio> ListarServicios()
        {
            try
            {
                return cdServicios.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar servicios: " + ex.Message);
            }
        }

        // Listar servicios inactivos - CON DIAGNÓSTICO MEJORADO
        public List<Servicio> ListarServiciosInactivos()
        {
            try
            {
                Console.WriteLine("=== CN_Servicios.ListarServiciosInactivos() INICIANDO ===");

                var servicios = cdServicios.ListarInactivos();

                Console.WriteLine($"=== CN_Servicios.ListarServiciosInactivos() COMPLETADO ===");
                Console.WriteLine($"Total servicios inactivos obtenidos: {servicios.Count}");

                // Diagnóstico detallado
                foreach (var servicio in servicios)
                {
                    Console.WriteLine($"Servicio inactivo: ID={servicio.ServicioID}, Nombre='{servicio.Nombre}', Activo={servicio.Activo}");
                }

                return servicios;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== ERROR en CN_Servicios.ListarServiciosInactivos(): {ex.Message} ===");
                throw new Exception("Error al listar servicios inactivos: " + ex.Message);
            }
        }

        // Resto de los métodos permanecen igual...
        // Obtener servicio por ID
        public Servicio ObtenerServicio(int servicioID)
        {
            try
            {
                if (servicioID <= 0)
                    throw new ArgumentException("ID de servicio no válido");

                var servicio = cdServicios.BuscarPorID(servicioID);

                if (servicio == null)
                    throw new Exception("Servicio no encontrado");

                return servicio;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener servicio: " + ex.Message);
            }
        }

        // Validar datos del servicio antes de insertar
        private ResultadoOperacion ValidarServicio(Servicio servicio)
        {
            var resultado = new ResultadoOperacion();

            if (string.IsNullOrEmpty(servicio.Nombre))
            {
                resultado.Mensaje = "El nombre del servicio es obligatorio";
                return resultado;
            }

            if (servicio.Nombre.Length > 200)
            {
                resultado.Mensaje = "El nombre no puede tener más de 200 caracteres";
                return resultado;
            }

            if (servicio.Precio <= 0)
            {
                resultado.Mensaje = "El precio debe ser mayor a cero";
                return resultado;
            }

            if (servicio.DuracionEstimada <= 0)
            {
                resultado.Mensaje = "La duración estimada debe ser mayor a cero";
                return resultado;
            }

            if (!string.IsNullOrEmpty(servicio.Descripcion) && servicio.Descripcion.Length > 1000)
            {
                resultado.Mensaje = "La descripción no puede tener más de 1000 caracteres";
                return resultado;
            }

            resultado.Exitoso = true;
            return resultado;
        }

        // Insertar nuevo servicio
        public ResultadoOperacion InsertarServicio(Servicio servicio)
        {
            var resultado = new ResultadoOperacion();

            try
            {
                // Validar datos
                resultado = ValidarServicio(servicio);
                if (!resultado.Exitoso)
                    return resultado;

                // Insertar en base de datos
                string mensaje = cdServicios.Insertar(servicio);

                if (mensaje.Contains("éxito") || mensaje.Contains("creado"))
                {
                    resultado.Exitoso = true;
                    resultado.Mensaje = mensaje;
                }
                else
                {
                    resultado.Mensaje = mensaje;
                }
            }
            catch (Exception ex)
            {
                resultado.Mensaje = "Error al insertar servicio: " + ex.Message;
            }

            return resultado;
        }

        // Actualizar servicio existente
        public ResultadoOperacion ActualizarServicio(Servicio servicio)
        {
            var resultado = new ResultadoOperacion();

            try
            {
                // Validar datos
                resultado = ValidarServicio(servicio);
                if (!resultado.Exitoso)
                    return resultado;

                // Verificar que el servicio existe
                var servicioExistente = cdServicios.BuscarPorID(servicio.ServicioID);
                if (servicioExistente == null)
                {
                    resultado.Mensaje = "El servicio no existe";
                    return resultado;
                }

                // Actualizar en base de datos
                string mensaje = cdServicios.Actualizar(servicio);

                if (mensaje.Contains("éxito") || mensaje.Contains("actualizado"))
                {
                    resultado.Exitoso = true;
                    resultado.Mensaje = mensaje;
                }
                else
                {
                    resultado.Mensaje = mensaje;
                }
            }
            catch (Exception ex)
            {
                resultado.Mensaje = "Error al actualizar servicio: " + ex.Message;
            }

            return resultado;
        }

        // Eliminar servicio (inactivar)
        public ResultadoOperacion EliminarServicio(int servicioID)
        {
            var resultado = new ResultadoOperacion();

            try
            {
                if (servicioID <= 0)
                {
                    resultado.Mensaje = "ID de servicio no válido";
                    return resultado;
                }

                // Verificar que el servicio existe
                var servicio = cdServicios.BuscarPorID(servicioID);
                if (servicio == null)
                {
                    resultado.Mensaje = "El servicio no existe";
                    return resultado;
                }

                // Eliminar (inactivar) en base de datos
                string mensaje = cdServicios.Eliminar(servicioID);

                if (mensaje.Contains("éxito") || mensaje.Contains("inactivado"))
                {
                    resultado.Exitoso = true;
                    resultado.Mensaje = mensaje;
                }
                else
                {
                    resultado.Mensaje = mensaje;
                }
            }
            catch (Exception ex)
            {
                resultado.Mensaje = "Error al eliminar servicio: " + ex.Message;
            }

            return resultado;
        }

        // Desactivar servicio (alias de EliminarServicio)
        public ResultadoOperacion DesactivarServicio(int servicioID)
        {
            return EliminarServicio(servicioID);
        }

        // Activar servicio
        public ResultadoOperacion ActivarServicio(int servicioID)
        {
            var resultado = new ResultadoOperacion();

            try
            {
                if (servicioID <= 0)
                {
                    resultado.Mensaje = "ID de servicio no válido";
                    return resultado;
                }

                // Verificar que el servicio existe
                var servicio = cdServicios.BuscarPorID(servicioID);
                if (servicio == null)
                {
                    resultado.Mensaje = "El servicio no existe";
                    return resultado;
                }

                // Activar en base de datos
                string mensaje = cdServicios.Activar(servicioID);

                if (mensaje.Contains("éxito") || mensaje.Contains("activado"))
                {
                    resultado.Exitoso = true;
                    resultado.Mensaje = mensaje;
                }
                else
                {
                    resultado.Mensaje = mensaje;
                }
            }
            catch (Exception ex)
            {
                resultado.Mensaje = "Error al activar servicio: " + ex.Message;
            }

            return resultado;
        }

        // Buscar servicios - MÉTODO CORREGIDO
        public List<Servicio> BuscarServicios(string criterio, bool incluirInactivos = false)
        {
            try
            {
                List<Servicio> servicios;

                if (incluirInactivos)
                {
                    // Combinar activos e inactivos
                    var activos = ListarServicios();
                    var inactivos = ListarServiciosInactivos();
                    servicios = new List<Servicio>();
                    servicios.AddRange(activos);
                    servicios.AddRange(inactivos);
                }
                else
                {
                    servicios = ListarServicios();
                }

                if (string.IsNullOrEmpty(criterio))
                    return servicios;

                // Filtrar por criterio (case insensitive)
                criterio = criterio.ToLower();
                return servicios.FindAll(s =>
                    s.Nombre.ToLower().Contains(criterio) ||
                    (!string.IsNullOrEmpty(s.Descripcion) && s.Descripcion.ToLower().Contains(criterio)) ||
                    (!string.IsNullOrEmpty(s.CategoriaNombre) && s.CategoriaNombre.ToLower().Contains(criterio)) ||
                    (!string.IsNullOrEmpty(s.TipoNombre) && s.TipoNombre.ToLower().Contains(criterio))
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar servicios: " + ex.Message);
            }
        }

        // Sobrecarga para compatibilidad
        public List<Servicio> BuscarServicios(string nombre)
        {
            return BuscarServicios(nombre, false);
        }

        // Verificar si un servicio puede ser eliminado
        public bool PuedeEliminarServicio(int servicioID)
        {
            try
            {
                // Aquí podrías agregar lógica para verificar si el servicio
                // está siendo usado en citas activas, etc.
                // Por ahora simplemente retornamos true
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar si se puede eliminar el servicio: " + ex.Message);
            }
        }

        #endregion

        #region MÉTODOS PARA CATEGORÍAS

        // Listar categorías
        public List<CategoriaServicio> ListarCategorias()
        {
            try
            {
                return cdServicios.ListarCategorias();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar categorías: " + ex.Message);
            }
        }

        // Obtener categoría por ID
        public CategoriaServicio ObtenerCategoria(int categoriaID)
        {
            try
            {
                var categorias = cdServicios.ListarCategorias();
                return categorias.Find(c => c.CategoriaservicioID == categoriaID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener categoría: " + ex.Message);
            }
        }

        #endregion

        #region MÉTODOS PARA TIPOS DE SERVICIO

        // Listar tipos de servicio
        public List<TipoServicio> ListarTiposServicio()
        {
            try
            {
                return cdServicios.ListarTiposServicio();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar tipos de servicio: " + ex.Message);
            }
        }

        // Obtener tipo de servicio por ID
        public TipoServicio ObtenerTipoServicio(int tipoServicioID)
        {
            try
            {
                var tipos = cdServicios.ListarTiposServicio();
                return tipos.Find(t => t.TiposervicioID == tipoServicioID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener tipo de servicio: " + ex.Message);
            }
        }

        #endregion

        #region MÉTODOS PARA REPUESTOS

        // Listar repuestos
        public List<Repuesto> ListarRepuestos()
        {
            try
            {
                return cdServicios.ListarRepuestos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar repuestos: " + ex.Message);
            }
        }

        // Listar repuestos de un servicio
        public List<RepuestoServicio> ListarRepuestosDeServicio(int servicioID)
        {
            try
            {
                if (servicioID <= 0)
                    throw new ArgumentException("ID de servicio no válido");

                return cdServicios.ListarRepuestosDeServicio(servicioID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar repuestos del servicio: " + ex.Message);
            }
        }

        // Agregar repuesto a servicio
        public ResultadoOperacion AgregarRepuestoAServicio(int servicioID, int repuestoID, int cantidad, string observaciones = "")
        {
            var resultado = new ResultadoOperacion();

            try
            {
                if (servicioID <= 0 || repuestoID <= 0)
                {
                    resultado.Mensaje = "IDs de servicio y repuesto son obligatorios";
                    return resultado;
                }

                if (cantidad <= 0)
                {
                    resultado.Mensaje = "La cantidad debe ser mayor a cero";
                    return resultado;
                }

                // Verificar que el servicio existe
                var servicio = cdServicios.BuscarPorID(servicioID);
                if (servicio == null)
                {
                    resultado.Mensaje = "El servicio no existe";
                    return resultado;
                }

                // Verificar que el repuesto existe
                var repuestos = cdServicios.ListarRepuestos();
                var repuesto = repuestos.Find(r => r.RepuestoID == repuestoID);
                if (repuesto == null)
                {
                    resultado.Mensaje = "El repuesto no existe";
                    return resultado;
                }

                // Verificar stock disponible
                if (repuesto.Stock < cantidad)
                {
                    resultado.Mensaje = $"Stock insuficiente. Stock disponible: {repuesto.Stock}";
                    return resultado;
                }

                // Agregar repuesto al servicio
                string mensaje = cdServicios.AgregarRepuestoAServicio(servicioID, repuestoID, cantidad, observaciones);

                if (mensaje.Contains("éxito"))
                {
                    resultado.Exitoso = true;
                    resultado.Mensaje = mensaje;
                }
                else
                {
                    resultado.Mensaje = mensaje;
                }
            }
            catch (Exception ex)
            {
                resultado.Mensaje = "Error al agregar repuesto al servicio: " + ex.Message;
            }

            return resultado;
        }

        // Sobrecarga del método AgregarRepuestoAServicio sin observaciones
        public ResultadoOperacion AgregarRepuestoAServicio(int servicioID, int repuestoID, int cantidad)
        {
            return AgregarRepuestoAServicio(servicioID, repuestoID, cantidad, "");
        }

        // Quitar repuesto de servicio
        public ResultadoOperacion QuitarRepuestoDeServicio(int servicioID, int repuestoID)
        {
            var resultado = new ResultadoOperacion();

            try
            {
                if (servicioID <= 0 || repuestoID <= 0)
                {
                    resultado.Mensaje = "IDs de servicio y repuesto son obligatorios";
                    return resultado;
                }

                // Verificar que la relación existe
                var repuestosServicio = cdServicios.ListarRepuestosDeServicio(servicioID);
                var repuestoExistente = repuestosServicio.Find(r => r.RepuestoID == repuestoID);
                if (repuestoExistente == null)
                {
                    resultado.Mensaje = "El repuesto no está asignado a este servicio";
                    return resultado;
                }

                // Quitar repuesto del servicio
                string mensaje = cdServicios.QuitarRepuestoDeServicio(servicioID, repuestoID);

                if (mensaje.Contains("éxito"))
                {
                    resultado.Exitoso = true;
                    resultado.Mensaje = mensaje;
                }
                else
                {
                    resultado.Mensaje = mensaje;
                }
            }
            catch (Exception ex)
            {
                resultado.Mensaje = "Error al quitar repuesto del servicio: " + ex.Message;
            }

            return resultado;
        }

        // Calcular costo total de repuestos para un servicio
        public decimal CalcularCostoRepuestosServicio(int servicioID)
        {
            try
            {
                var repuestos = cdServicios.ListarRepuestosDeServicio(servicioID);
                decimal total = 0;

                foreach (var repuesto in repuestos)
                {
                    total += repuesto.PrecioUnitario * repuesto.Cantidad;
                }

                return total;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular costo de repuestos: " + ex.Message);
            }
        }

        // Verificar repuestos con stock bajo
        public List<Repuesto> ObtenerRepuestosBajoStock()
        {
            try
            {
                var repuestos = cdServicios.ListarRepuestos();
                return repuestos.FindAll(r => r.Stock <= r.StockMinimo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener repuestos con stock bajo: " + ex.Message);
            }
        }

        #endregion

        #region MÉTODOS DE VALIDACIÓN Y UTILIDADES

        // Validar si un nombre de servicio ya existe
        public bool ExisteServicioConNombre(string nombre, int servicioIDExcluir = 0)
        {
            try
            {
                var servicios = cdServicios.Listar();
                return servicios.Exists(s =>
                    s.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                    s.ServicioID != servicioIDExcluir);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar nombre de servicio: " + ex.Message);
            }
        }

        // Obtener servicios activos para combobox
        public List<Servicio> ObtenerServiciosParaCombo()
        {
            try
            {
                return cdServicios.Listar().FindAll(s => s.Activo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener servicios para combo: " + ex.Message);
            }
        }

        // Obtener estadísticas de servicios
        public DataTable ObtenerEstadisticas()
        {
            try
            {
                // Aquí podrías implementar lógica para estadísticas
                // Por ahora retornamos null, puedes expandir esto según necesites
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener estadísticas: " + ex.Message);
            }
        }

        #endregion
    }
}