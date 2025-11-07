using CapaDatos;
using CapaDominio;
using System;
using System.Collections.Generic;

namespace CapaLogicaNegocio
{
    public class CN_Bahia
    {
        private CD_Bahia cdBahia = new CD_Bahia();

        public List<Bahia> ListarBahiasActivas()
        {
            try
            {
                return cdBahia.ListarActivas();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al listar bahías: " + ex.Message);
            }
        }

        // NUEVO MÉTODO: Listar todas las bahías (activas e inactivas)
        public List<Bahia> ListarTodasLasBahias()
        {
            try
            {
                return cdBahia.ListarTodas();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al listar todas las bahías: " + ex.Message);
            }
        }

        public bool CrearBahia(string nombre, string tipo, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    mensaje = "El nombre de la bahía es requerido";
                    return false;
                }
                if (string.IsNullOrWhiteSpace(tipo))
                {
                    mensaje = "El tipo de bahía es requerido";
                    return false;
                }
                if (nombre.Length > 50)
                {
                    mensaje = "El nombre no puede tener más de 50 caracteres";
                    return false;
                }

                string resultado = cdBahia.CrearBahia(nombre.Trim(), tipo);
                mensaje = resultado;
                return resultado.Contains("éxito");
            }
            catch (Exception ex)
            {
                mensaje = "Error al crear bahía: " + ex.Message;
                return false;
            }
        }

        public bool ModificarBahia(int bahiaId, string nombre, string tipo, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    mensaje = "El nombre de la bahía es requerido";
                    return false;
                }
                if (string.IsNullOrWhiteSpace(tipo))
                {
                    mensaje = "El tipo de bahía es requerido";
                    return false;
                }
                if (nombre.Length > 50)
                {
                    mensaje = "El nombre no puede tener más de 50 caracteres";
                    return false;
                }

                string resultado = cdBahia.ModificarBahia(bahiaId, nombre.Trim(), tipo);
                mensaje = resultado;
                return resultado.Contains("éxito");
            }
            catch (Exception ex)
            {
                mensaje = "Error al modificar bahía: " + ex.Message;
                return false;
            }
        }

        public bool InhabilitarBahia(int bahiaId, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                string resultado = cdBahia.InhabilitarBahia(bahiaId);
                mensaje = resultado;
                return resultado.Contains("éxito");
            }
            catch (Exception ex)
            {
                mensaje = "Error al inhabilitar bahía: " + ex.Message;
                return false;
            }
        }

        // NUEVO MÉTODO: Habilitar bahía
        public bool HabilitarBahia(int bahiaId, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                string resultado = cdBahia.HabilitarBahia(bahiaId);
                mensaje = resultado;
                return resultado.Contains("éxito");
            }
            catch (Exception ex)
            {
                mensaje = "Error al habilitar bahía: " + ex.Message;
                return false;
            }
        }
    }
}