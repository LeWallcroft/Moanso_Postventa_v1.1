using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class CN_Marca
    {
        private CD_Marca cdMarca = new CD_Marca();

        public int ProcesarMarca(string nombreMarca)
        {
            if (string.IsNullOrWhiteSpace(nombreMarca))
            {
                throw new Exception("El nombre de la Marca es obligatorio.");
            }
            // Llamada a la lógica de crear o buscar
            return cdMarca.ObtenerOCrearMarcaId(nombreMarca.ToUpper()); // Usar mayúsculas para evitar duplicados "honda" vs "Honda"
        }
    }
}
