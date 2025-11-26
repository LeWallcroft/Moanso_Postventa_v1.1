using CapaDatos;
using CapaDominio;
using System;
using System.Collections.Generic;

namespace CapaLogicaNegocio
{
    public class CN_Bahia
    {
        private CD_Bahia cdBahia = new CD_Bahia();

        // MÉTODO PRINCIPAL: Listar todas las bahías
        public List<Bahia> ListarTodasLasBahias()
        {
            try
            {
                return cdBahia.ListarTodas();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar bahías: " + ex.Message);
            }
        }

        // MÉTODO: Crear bahía completa
        public bool CrearBahiaCompleta(string nombre, string descripcion, int capacidad,
                                      int estadoId, int? usuarioId, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    mensaje = "El nombre de la bahía es requerido";
                    return false;
                }
                if (string.IsNullOrWhiteSpace(descripcion))
                {
                    mensaje = "La descripción es requerida";
                    return false;
                }
                if (capacidad <= 0)
                {
                    mensaje = "La capacidad debe ser mayor a 0";
                    return false;
                }
                if (nombre.Length > 100)
                {
                    mensaje = "El nombre no puede tener más de 100 caracteres";
                    return false;
                }

                string resultado = cdBahia.CrearBahiaCompleta(nombre.Trim(), descripcion.Trim(),
                                                             capacidad, estadoId, usuarioId);
                mensaje = resultado;
                return resultado.Contains("éxito");
            }
            catch (Exception ex)
            {
                mensaje = "Error al crear bahía: " + ex.Message;
                return false;
            }
        }

        // MÉTODO: Actualizar bahía completa
        public bool ActualizarBahiaCompleta(int bahiaId, string nombre, string descripcion,
                                           int capacidad, int estadoId, int? usuarioId, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    mensaje = "El nombre de la bahía es requerido";
                    return false;
                }
                if (string.IsNullOrWhiteSpace(descripcion))
                {
                    mensaje = "La descripción es requerida";
                    return false;
                }
                if (capacidad <= 0)
                {
                    mensaje = "La capacidad debe ser mayor a 0";
                    return false;
                }
                if (nombre.Length > 100)
                {
                    mensaje = "El nombre no puede tener más de 100 caracteres";
                    return false;
                }

                string resultado = cdBahia.ActualizarBahiaCompleta(bahiaId, nombre.Trim(),
                                                                  descripcion.Trim(), capacidad,
                                                                  estadoId, usuarioId);
                mensaje = resultado;
                return resultado.Contains("éxito");
            }
            catch (Exception ex)
            {
                mensaje = "Error al actualizar bahía: " + ex.Message;
                return false;
            }
        }

        // MÉTODO: Inhabilitar bahía
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

        // MÉTODO: Habilitar bahía
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