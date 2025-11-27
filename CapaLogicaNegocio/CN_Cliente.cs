using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CapaDatos;
using Dominio;

namespace CapaLogica
{
    public class CN_Cliente
    {
        private CD_Cliente cdCliente = new CD_Cliente();
        private CD_Vehiculo cdVehiculo = new CD_Vehiculo();

        public Cliente BuscarClientePorDNI(string dni)
        {
            try
            {
                // Validaciones de negocio
                if (string.IsNullOrWhiteSpace(dni))
                    throw new ArgumentException("El DNI no puede estar vacío");

                // Limpiar y formatear DNI
                dni = dni.Trim();

                // Validar formato de DNI (8 dígitos)
                if (!Regex.IsMatch(dni, @"^\d{8}$"))
                    throw new ArgumentException("El DNI debe contener exactamente 8 dígitos");

                // Buscar cliente en la capa de datos
                Cliente cliente = cdCliente.BuscarPorDNI(dni);

                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception("CN_Cliente - Error al buscar cliente: " + ex.Message);
            }
        }

        public string RegistrarCliente(Cliente cliente)
        {
            try
            {
                // Validaciones de negocio
                if (string.IsNullOrWhiteSpace(cliente.DNI))
                    return "El DNI es obligatorio";

                if (!Regex.IsMatch(cliente.DNI.Trim(), @"^\d{8}$"))
                    return "El DNI debe contener exactamente 8 dígitos";

                if (string.IsNullOrWhiteSpace(cliente.Nombre))
                    return "El nombre es obligatorio";

                if (string.IsNullOrWhiteSpace(cliente.Apellido))
                    return "El apellido es obligatorio";

                // Validar longitud de campos
                if (cliente.Nombre.Length > 100)
                    return "El nombre no puede tener más de 100 caracteres";

                if (cliente.Apellido.Length > 100)
                    return "El apellido no puede tener más de 100 caracteres";

                // Validar email si se proporciona
                if (!string.IsNullOrWhiteSpace(cliente.Email))
                {
                    if (cliente.Email.Length > 150)
                        return "El email no puede tener más de 150 caracteres";

                    if (!EsEmailValido(cliente.Email))
                        return "El formato del email no es válido";
                }

                // Validar teléfono si se proporciona
                if (!string.IsNullOrWhiteSpace(cliente.Telefono))
                {
                    if (cliente.Telefono.Length > 20)
                        return "El teléfono no puede tener más de 20 caracteres";
                }

                // Validar dirección si se proporciona
                if (!string.IsNullOrWhiteSpace(cliente.Direccion))
                {
                    if (cliente.Direccion.Length > 300)
                        return "La dirección no puede tener más de 300 caracteres";
                }

                // Registrar cliente en la capa de datos
                string resultado = cdCliente.RegistrarCliente(cliente);

                return resultado;
            }
            catch (Exception ex)
            {
                return "CN_Cliente - Error al registrar cliente: " + ex.Message;
            }
        }

        public List<Vehiculo> ListarVehiculosCliente(int clienteID)
        {
            try
            {
                // Validaciones de negocio
                if (clienteID <= 0)
                    throw new ArgumentException("ID de cliente inválido");

                // Obtener vehículos de la capa de datos
                List<Vehiculo> vehiculos = cdVehiculo.ListarPorCliente(clienteID);

                return vehiculos;
            }
            catch (Exception ex)
            {
                throw new Exception("CN_Cliente - Error al listar vehículos: " + ex.Message);
            }
        }

        private bool EsEmailValido(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email.Trim();
            }
            catch
            {
                return false;
            }
        }
    }
}