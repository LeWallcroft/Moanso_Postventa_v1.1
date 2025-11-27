using System;

namespace Dominio
{
    public class Vehiculo
    {
        public int VehiculoID { get; set; }
        public int ClienteID { get; set; }
        public int ModeloID { get; set; }
        public string Placa { get; set; }
        public string VIN { get; set; }
        public string Color { get; set; }
        public int? Anio { get; set; }
        public int? Kilometraje { get; set; }
        public string Combustible { get; set; }
        public string Transmision { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }

        // Propiedades de navegación (para mostrar información relacionada)
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string TipoVehiculo { get; set; }
        public string ClienteDNI { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteApellido { get; set; }
        public string ClienteNombreCompleto => $"{ClienteNombre} {ClienteApellido}";
    }
}