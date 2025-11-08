using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class CN_Modelo
    {
        private CD_Modelo cdModelo = new CD_Modelo();

        public int ProcesarModelo(int marcaId, string nombreModelo)
        {
            if (marcaId <= 0)
            {
                throw new Exception("Error interno: Se requiere el ID de la Marca para registrar el Modelo.");
            }
            if (string.IsNullOrWhiteSpace(nombreModelo))
            {
                throw new Exception("El nombre del Modelo es obligatorio.");
            }
            // Llamada a la lógica de crear o buscar
            return cdModelo.ObtenerOCrearModeloId(marcaId, nombreModelo.ToUpper()); // Usar mayúsculas
        }
    }
}
