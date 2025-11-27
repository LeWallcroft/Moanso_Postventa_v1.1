using System;

namespace CapaDominio.Utilidades
{
    public static class SincronizadorGlobal
    {
        // Eventos para sincronización
        public static event Action<int, bool> EstadoUsuarioCambiado;
        public static event Action<int, bool> EstadoTecnicoCambiado;
        public static event Action<int, bool> EstadoBahiaCambiado;
        public static event Action CambioBahia; // Nuevo evento para cambios generales

        // Métodos para notificar cambios
        public static void NotificarEstadoUsuarioCambiado(int usuarioID, bool activo)
        {
            EstadoUsuarioCambiado?.Invoke(usuarioID, activo);
        }

        public static void NotificarEstadoTecnicoCambiado(int tecnicoID, bool activo)
        {
            EstadoTecnicoCambiado?.Invoke(tecnicoID, activo);
        }

        public static void NotificarEstadoBahiaCambiado(int bahiaID, bool activo)
        {
            EstadoBahiaCambiado?.Invoke(bahiaID, activo);
        }

        // Nuevo método para notificar cambios generales en bahías
        public static void NotificarCambioBahia()
        {
            CambioBahia?.Invoke();
        }
    }
}