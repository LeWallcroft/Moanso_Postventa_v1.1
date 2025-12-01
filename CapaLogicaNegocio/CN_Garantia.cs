using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class CN_Garantia
    {
        private CD_Garantia cdGarantia = new CD_Garantia();

        public bool VehiculoTieneGarantia(int vehiculoID)
        {
            return cdGarantia.VerificarGarantiaVehiculo(vehiculoID);
        }
    }
}
