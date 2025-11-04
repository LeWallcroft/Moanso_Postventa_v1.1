using CapaDatos;
using CapaDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente cdCliente = new CD_Cliente();

        // Método para buscar cliente y vehículos por DNI (Paso 2)
        public Cliente ConsultarCliente(string dni, out List<Vehiculo> vehiculos)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                // Detenemos la ejecución si el DNI está vacío
                throw new Exception("El DNI es requerido para la búsqueda.");
            }

            // Aquí podrías añadir validación de formato (ej. longitud de DNI)

            return cdCliente.BuscarClienteYVehiculos(dni, out vehiculos);
        }

        // Método para registrar un nuevo cliente (Paso 2)
        public int RegistrarNuevoCliente(Cliente cliente)
        {
            // Validaciones de negocio antes de la inserción
            if (string.IsNullOrWhiteSpace(cliente.DNI))
                throw new Exception("El DNI es obligatorio.");
            if (string.IsNullOrWhiteSpace(cliente.Nombres))
                throw new Exception("Los Nombres son obligatorios.");
            if (string.IsNullOrWhiteSpace(cliente.Apellidos))
                throw new Exception("Los Apellidos son obligatorios.");

            // Llamada a la Capa Datos para la inserción
            int nuevoClienteId = cdCliente.CrearCliente(cliente);

            if (nuevoClienteId <= 0)
            {
                throw new Exception("No se pudo obtener el ID del nuevo cliente. El registro falló.");
            }

            return nuevoClienteId;
        }
    }
}
