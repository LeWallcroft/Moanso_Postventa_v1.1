using CapaDatos;
using CapaDominio;
using CapaDominio.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class CN_Repuesto
    {
        private readonly CD_Repuesto _datosRepuesto;

        public CN_Repuesto()
        {
            _datosRepuesto = new CD_Repuesto();
        }

        public async Task<List<Repuesto>> ObtenerRepuestosActivosAsync()
        {
            try
            {
                return await _datosRepuesto.ListarActivosAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener repuestos activos: {ex.Message}", ex);
            }
        }

        public async Task<List<Repuesto>> BuscarRepuestosAsync(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                    return await _datosRepuesto.ListarActivosAsync();

                return await _datosRepuesto.BuscarPorNombreAsync(nombre);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar repuestos: {ex.Message}", ex);
            }
        }

        public async Task<Repuesto> ObtenerRepuestoPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID del repuesto debe ser mayor a cero");

                var repuesto = await _datosRepuesto.BuscarPorIdAsync(id);

                if (repuesto == null)
                    throw new Exception($"Repuesto con ID {id} no encontrado");

                return repuesto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener repuesto: {ex.Message}", ex);
            }
        }

        public async Task<List<Repuesto>> ObtenerRepuestosStockBajoAsync()
        {
            try
            {
                return await _datosRepuesto.ObtenerStockBajoAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener repuestos con stock bajo: {ex.Message}", ex);
            }
        }

        public async Task<ResultadoOperacion> CrearRepuestoAsync(Repuesto repuesto)
        {
            try
            {
                var validacion = ValidarRepuesto(repuesto);
                if (!validacion.Exitoso)
                    return validacion;

                // Validar que no exista un repuesto con el mismo nombre
                var repuestosExistentes = await _datosRepuesto.BuscarPorNombreAsync(repuesto.Nombre);
                if (repuestosExistentes.Any(r => r.Nombre.ToLower() == repuesto.Nombre.ToLower()))
                {
                    return ResultadoOperacion.Error($"Ya existe un repuesto con el nombre '{repuesto.Nombre}'");
                }

                return await _datosRepuesto.CrearAsync(repuesto);
            }
            catch (Exception ex)
            {
                return ResultadoOperacion.Error($"Error al crear repuesto: {ex.Message}");
            }
        }

        public async Task<ResultadoOperacion> ActualizarRepuestoAsync(Repuesto repuesto)
        {
            try
            {
                var validacion = ValidarRepuesto(repuesto);
                if (!validacion.Exitoso)
                    return validacion;

                if (repuesto.RepuestoID <= 0)
                    return ResultadoOperacion.Error("ID de repuesto inválido");

                // Validar que no exista otro repuesto con el mismo nombre
                var repuestosExistentes = await _datosRepuesto.BuscarPorNombreAsync(repuesto.Nombre);
                if (repuestosExistentes.Any(r => r.RepuestoID != repuesto.RepuestoID && r.Nombre.ToLower() == repuesto.Nombre.ToLower()))
                {
                    return ResultadoOperacion.Error($"Ya existe otro repuesto con el nombre '{repuesto.Nombre}'");
                }

                return await _datosRepuesto.ActualizarAsync(repuesto);
            }
            catch (Exception ex)
            {
                return ResultadoOperacion.Error($"Error al actualizar repuesto: {ex.Message}");
            }
        }

        public async Task<ResultadoOperacion> InactivarRepuestoAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return ResultadoOperacion.Error("ID de repuesto inválido");

                // Verificar si el repuesto existe
                var repuesto = await _datosRepuesto.BuscarPorIdAsync(id);
                if (repuesto == null)
                    return ResultadoOperacion.Error("Repuesto no encontrado");

                // Verificar si el repuesto está siendo usado en servicios activos
                var puedeUsarse = await _datosRepuesto.VerificarUsoEnServiciosAsync(id);
                if (puedeUsarse)
                {
                    return ResultadoOperacion.Error("No se puede inactivar el repuesto porque está siendo usado en servicios activos");
                }

                return await _datosRepuesto.InactivarAsync(id);
            }
            catch (Exception ex)
            {
                return ResultadoOperacion.Error($"Error al inactivar repuesto: {ex.Message}");
            }
        }

        public async Task<ResultadoOperacion> ActivarRepuestoAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return ResultadoOperacion.Error("ID de repuesto inválido");

                // Verificar si el repuesto existe
                var repuesto = await _datosRepuesto.BuscarPorIdAsync(id);
                if (repuesto == null)
                    return ResultadoOperacion.Error("Repuesto no encontrado");

                return await _datosRepuesto.ActivarAsync(id);
            }
            catch (Exception ex)
            {
                return ResultadoOperacion.Error($"Error al activar repuesto: {ex.Message}");
            }
        }

        public async Task<ResultadoOperacion> ActualizarStockAsync(int id, int cantidad, string motivo)
        {
            try
            {
                if (id <= 0)
                    return ResultadoOperacion.Error("ID de repuesto inválido");

                if (cantidad < 0)
                    return ResultadoOperacion.Error("La cantidad no puede ser negativa");

                if (string.IsNullOrWhiteSpace(motivo))
                    return ResultadoOperacion.Error("Debe especificar un motivo para el cambio de stock");

                // Verificar si el repuesto existe
                var repuesto = await _datosRepuesto.BuscarPorIdAsync(id);
                if (repuesto == null)
                    return ResultadoOperacion.Error("Repuesto no encontrado");

                return await _datosRepuesto.ActualizarStockAsync(id, cantidad, motivo);
            }
            catch (Exception ex)
            {
                return ResultadoOperacion.Error($"Error al actualizar stock: {ex.Message}");
            }
        }

        public async Task<object> ObtenerEstadisticasAsync()
        {
            try
            {
                return await _datosRepuesto.ObtenerEstadisticasAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener estadísticas: {ex.Message}", ex);
            }
        }

        public async Task<List<Repuesto>> ObtenerRepuestosInactivosAsync()
        {
            try
            {
                return await _datosRepuesto.ListarInactivosAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener repuestos inactivos: {ex.Message}", ex);
            }
        }

        public async Task<List<Repuesto>> ObtenerTodosLosRepuestosAsync()
        {
            try
            {
                return await _datosRepuesto.ListarTodosAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener todos los repuestos: {ex.Message}", ex);
            }
        }

        public async Task<bool> ValidarStockSuficienteAsync(int repuestoId, int cantidadRequerida)
        {
            try
            {
                var repuesto = await _datosRepuesto.BuscarPorIdAsync(repuestoId);
                if (repuesto == null)
                    return false;

                return repuesto.Stock >= cantidadRequerida;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar stock: {ex.Message}", ex);
            }
        }

        public async Task<int> ObtenerStockActualAsync(int repuestoId)
        {
            try
            {
                var repuesto = await _datosRepuesto.BuscarPorIdAsync(repuestoId);
                if (repuesto == null)
                    throw new Exception("Repuesto no encontrado");

                return repuesto.Stock;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener stock actual: {ex.Message}", ex);
            }
        }

        // Método de validación interna
        private ResultadoOperacion ValidarRepuesto(Repuesto repuesto)
        {
            if (repuesto == null)
                return ResultadoOperacion.Error("El repuesto no puede ser nulo");

            if (string.IsNullOrWhiteSpace(repuesto.Nombre))
                return ResultadoOperacion.Error("El nombre del repuesto es obligatorio");

            if (repuesto.Nombre.Length > 200)
                return ResultadoOperacion.Error("El nombre no puede tener más de 200 caracteres");

            if (repuesto.Precio <= 0)
                return ResultadoOperacion.Error("El precio debe ser mayor a cero");

            if (repuesto.Stock < 0)
                return ResultadoOperacion.Error("El stock no puede ser negativo");

            if (repuesto.StockMinimo < 0)
                return ResultadoOperacion.Error("El stock mínimo no puede ser negativo");

            if (repuesto.Descripcion?.Length > 500)
                return ResultadoOperacion.Error("La descripción no puede tener más de 500 caracteres");

            if (repuesto.Codigo?.Length > 100)
                return ResultadoOperacion.Error("El código no puede tener más de 100 caracteres");

            if (repuesto.Proveedor?.Length > 200)
                return ResultadoOperacion.Error("El proveedor no puede tener más de 200 caracteres");

            return ResultadoOperacion.Exito();
        }
    }
}