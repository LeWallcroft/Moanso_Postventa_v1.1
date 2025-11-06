using CapaDatos;
using CapaDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class CN_Servicio
    {
        private CD_Servicio cdServicio = new CD_Servicio();

        public List<Servicio> ListarServicios(string tipoBahia)
        {
            try
            {
                var todosServicios = cdServicio.ListarServiciosActivos();

                // DEBUG: Ver qué estamos recibiendo
                Console.WriteLine($"=== FILTRANDO SERVICIOS ===");
                Console.WriteLine($"Tipo de bahía recibido: '{tipoBahia}'");
                Console.WriteLine($"Total de servicios: {todosServicios.Count}");

                foreach (var servicio in todosServicios)
                {
                    Console.WriteLine($"Servicio: {servicio.Nombre}, Tipo: '{servicio.Tipo}'");
                }

                // Filtrado más flexible
                var serviciosFiltrados = todosServicios
                    .Where(s => s.Tipo != null &&
                           s.Tipo.Trim().ToLower() == tipoBahia.Trim().ToLower())
                    .ToList();

                Console.WriteLine($"Servicios después del filtro: {serviciosFiltrados.Count}");

                return serviciosFiltrados;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al filtrar servicios por tipo de bahía: " + ex.Message);
            }
        }

        public List<Servicio> ListarServicios()
        {
            return cdServicio.ListarServiciosActivos();
        }

        public int ObtenerDuracionMin(int servicioId)
        {
            var servicio = cdServicio.ListarServiciosActivos()
                                .FirstOrDefault(s => s.ServicioId == servicioId);
            return servicio?.DuracionMin ?? 120;
        }
    }
}