using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class Vehiculo
    {
        public int VehiculoId { get; set; }
        public int ClienteId { get; set; }
        public string Placa { get; set; }
        public string VIN { get; set; } // VIN es clave en la BD
        public int MarcaId { get; set; }
        public int ModeloId { get; set; }
        public int Anio { get; set; }
        // Propiedades de la Marca/Modelo (útiles para mostrar)
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Identificador => $"{Marca} {Modelo} ({Placa})";
    }
}
