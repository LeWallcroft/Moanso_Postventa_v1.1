using CapaDatos;
using CapaDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class CN_Vehiculo
    {
        private CD_Vehiculo cdVehiculo = new CD_Vehiculo();
        // NOTA: En un caso real, necesitarías CD_Marca y CD_Modelo para validar y obtener IDs aquí.

        // Método para registrar un nuevo vehículo (Paso 2)
        public int RegistrarNuevoVehiculo(Vehiculo vehiculo)
        {
            // Validaciones de negocio
            if (vehiculo.ClienteId <= 0)
                throw new Exception("El vehículo debe estar asociado a un cliente válido.");
            if (string.IsNullOrWhiteSpace(vehiculo.Placa))
                throw new Exception("La Placa es obligatoria.");
            if (vehiculo.MarcaId <= 0 || vehiculo.ModeloId <= 0)
                throw new Exception("La Marca y el Modelo son obligatorios (ID no válido).");
            if (vehiculo.Anio < 1900 || vehiculo.Anio > DateTime.Now.Year + 1)
                throw new Exception("El Año del vehículo no es válido.");

            // Llamada a la Capa Datos para la inserción
            int nuevoVehiculoId = cdVehiculo.CrearVehiculo(vehiculo);

            if (nuevoVehiculoId <= 0)
            {
                throw new Exception("No se pudo obtener el ID del nuevo vehículo. El registro falló.");
            }

            return nuevoVehiculoId;
        }
    }
}
