using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CapaDatos;
using Dominio;

namespace CapaLogica
{
    public class CN_Vehiculo
    {
        private CD_Vehiculo cdVehiculo = new CD_Vehiculo();
        private CD_MarcaModelo cdMarcaModelo = new CD_MarcaModelo();

        public Vehiculo BuscarVehiculoPorPlaca(string placa)
        {
            try
            {
                // Validaciones de negocio
                if (string.IsNullOrWhiteSpace(placa))
                    throw new ArgumentException("La placa no puede estar vacía");

                // Limpiar y formatear placa
                placa = placa.Trim().ToUpper();

                // Validar formato básico de placa
                if (!Regex.IsMatch(placa, @"^[A-Z0-9-]{4,10}$"))
                    throw new ArgumentException("El formato de la placa no es válido");

                // Buscar vehículo en la capa de datos
                Vehiculo vehiculo = cdVehiculo.BuscarPorPlaca(placa);

                return vehiculo;
            }
            catch (Exception ex)
            {
                throw new Exception("CN_Vehiculo - Error al buscar vehículo: " + ex.Message);
            }
        }

        public string RegistrarVehiculo(Vehiculo vehiculo, int usuarioID = 0)
        {
            try
            {
                // Validaciones de negocio
                if (vehiculo.ClienteID <= 0)
                    return "El cliente es obligatorio";

                if (vehiculo.ModeloID <= 0)
                    return "El modelo es obligatorio";

                if (string.IsNullOrWhiteSpace(vehiculo.Placa))
                    return "La placa es obligatoria";

                // Validar formato de placa
                vehiculo.Placa = vehiculo.Placa.Trim().ToUpper();
                if (!Regex.IsMatch(vehiculo.Placa, @"^[A-Z0-9-]{4,10}$"))
                    return "El formato de la placa no es válido";

                // Validar VIN si se proporciona
                if (!string.IsNullOrWhiteSpace(vehiculo.VIN))
                {
                    if (vehiculo.VIN.Length > 50)
                        return "El VIN no puede tener más de 50 caracteres";
                }

                // Validar color si se proporciona
                if (!string.IsNullOrWhiteSpace(vehiculo.Color))
                {
                    if (vehiculo.Color.Length > 50)
                        return "El color no puede tener más de 50 caracteres";
                }

                // Validar año si se proporciona
                if (vehiculo.Anio.HasValue)
                {
                    int añoActual = DateTime.Now.Year;
                    if (vehiculo.Anio < 1900 || vehiculo.Anio > añoActual + 1)
                        return $"El año del vehículo debe estar entre 1900 y {añoActual + 1}";
                }

                // Validar kilometraje si se proporciona
                if (vehiculo.Kilometraje.HasValue)
                {
                    if (vehiculo.Kilometraje < 0)
                        return "El kilometraje no puede ser negativo";

                    if (vehiculo.Kilometraje > 1000000)
                        return "El kilometraje no puede ser mayor a 1,000,000 km";
                }

                // Validar combustible si se proporciona
                if (!string.IsNullOrWhiteSpace(vehiculo.Combustible))
                {
                    if (vehiculo.Combustible.Length > 50)
                        return "El tipo de combustible no puede tener más de 50 caracteres";
                }

                // Validar transmisión si se proporciona
                if (!string.IsNullOrWhiteSpace(vehiculo.Transmision))
                {
                    if (vehiculo.Transmision.Length > 50)
                        return "El tipo de transmisión no puede tener más de 50 caracteres";
                }

                // Registrar vehículo en la capa de datos
                string resultado = cdVehiculo.RegistrarVehiculo(vehiculo, usuarioID);

                return resultado;
            }
            catch (Exception ex)
            {
                return "CN_Vehiculo - Error al registrar vehículo: " + ex.Message;
            }
        }

        public List<Marca> ListarMarcas()
        {
            try
            {
                // Obtener marcas de la capa de datos
                List<Marca> marcas = cdMarcaModelo.ListarMarcas();

                return marcas;
            }
            catch (Exception ex)
            {
                throw new Exception("CN_Vehiculo - Error al listar marcas: " + ex.Message);
            }
        }

        public List<Modelo> ListarModelosPorMarca(int marcaID)
        {
            try
            {
                // Validaciones de negocio
                if (marcaID <= 0)
                    return new List<Modelo>();

                // Obtener modelos de la capa de datos
                List<Modelo> modelos = cdMarcaModelo.ListarModelosPorMarca(marcaID);

                return modelos;
            }
            catch (Exception ex)
            {
                throw new Exception("CN_Vehiculo - Error al listar modelos: " + ex.Message);
            }
        }
    }
}