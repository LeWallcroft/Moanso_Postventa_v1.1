using CapaDatos;
using CapaDominio;
using System;
using System.Collections.Generic;

namespace CapaLogicaNegocio
{
    public class CN_Tecnico
    {
        private CD_Tecnico cdTecnico = new CD_Tecnico();

        public List<Tecnico> ListarTodosLosTecnicos()
        {
            try
            {
                return cdTecnico.ListarTodos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar técnicos: " + ex.Message);
            }
        }

        // ✅ NUEVO: Listar solo técnicos activos
        public List<Tecnico> ListarTecnicosActivos()
        {
            try
            {
                return cdTecnico.ListarActivos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar técnicos activos: " + ex.Message);
            }
        }

        // ✅ NUEVO: Listar usuarios disponibles para asignar como técnicos
        public List<Usuario> ListarUsuariosParaTecnicos()
        {
            try
            {
                return cdTecnico.ListarUsuariosParaTecnicos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios para técnicos: " + ex.Message);
            }
        }

        // ✅ CORRECCIÓN: Método corregido para usar el nuevo retorno bool
        public bool CrearTecnico(int? usuarioId, string especialidad, DateTime fechaContratacion,
                                decimal salario, bool disponible, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                // Validaciones de negocio
                if (string.IsNullOrWhiteSpace(especialidad))
                {
                    mensaje = "La especialidad es requerida";
                    return false;
                }

                if (salario <= 0)
                {
                    mensaje = "El salario debe ser mayor a 0";
                    return false;
                }

                if (fechaContratacion > DateTime.Now)
                {
                    mensaje = "La fecha de contratación no puede ser futura";
                    return false;
                }

                if (especialidad.Length > 200)
                {
                    mensaje = "La especialidad no puede tener más de 200 caracteres";
                    return false;
                }

                // ✅ CORRECCIÓN: Usar el nuevo método que retorna bool
                bool resultado = cdTecnico.CrearTecnico(usuarioId, especialidad.Trim(),
                                                       fechaContratacion, salario, disponible, out mensaje);

                return resultado;
            }
            catch (Exception ex)
            {
                mensaje = "Error al crear técnico: " + ex.Message;
                return false;
            }
        }


        // ✅ CORRECCIÓN: Método corregido para usar el nuevo retorno bool
        public bool ActualizarTecnico(int tecnicoId, int? usuarioId, string especialidad,
                                     DateTime fechaContratacion, decimal salario, bool disponible,
                                     bool activo, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                // Validaciones de negocio
                if (string.IsNullOrWhiteSpace(especialidad))
                {
                    mensaje = "La especialidad es requerida";
                    return false;
                }

                if (salario <= 0)
                {
                    mensaje = "El salario debe ser mayor a 0";
                    return false;
                }

                if (fechaContratacion > DateTime.Now)
                {
                    mensaje = "La fecha de contratación no puede ser futura";
                    return false;
                }

                if (especialidad.Length > 200)
                {
                    mensaje = "La especialidad no puede tener más de 200 caracteres";
                    return false;
                }

                // ✅ CORRECCIÓN: Usar el nuevo método que retorna bool
                bool resultado = cdTecnico.ActualizarTecnico(tecnicoId, usuarioId, especialidad.Trim(),
                                                           fechaContratacion, salario, disponible, activo, out mensaje);

                return resultado;
            }
            catch (Exception ex)
            {
                mensaje = "Error al actualizar técnico: " + ex.Message;
                return false;
            }
        }

        // ✅ CORRECCIÓN: Método corregido para usar el nuevo retorno bool
        public bool InactivarTecnico(int tecnicoId, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                // ✅ CORRECCIÓN: Usar el nuevo método que retorna bool
                bool resultado = cdTecnico.InactivarTecnico(tecnicoId, out mensaje);
                return resultado;
            }
            catch (Exception ex)
            {
                mensaje = "Error al inactivar técnico: " + ex.Message;
                return false;
            }
        }

        // ✅ CORRECCIÓN: Método corregido para usar el nuevo retorno bool
        public bool ActivarTecnico(int tecnicoId, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                // ✅ CORRECCIÓN: Usar el nuevo método que retorna bool
                bool resultado = cdTecnico.ActivarTecnico(tecnicoId, out mensaje);
                return resultado;
            }
            catch (Exception ex)
            {
                mensaje = "Error al activar técnico: " + ex.Message;
                return false;
            }
        }

        // ✅ NUEVO: Obtener técnico por ID
        public Tecnico ObtenerTecnicoPorId(int tecnicoId)
        {
            try
            {
                return cdTecnico.ObtenerTecnicoPorId(tecnicoId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener técnico: " + ex.Message);
            }
        }

        // ✅ NUEVO: Verificar si usuario ya es técnico
        public bool UsuarioEsTecnico(int usuarioId)
        {
            try
            {
                return cdTecnico.UsuarioEsTecnico(usuarioId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar si usuario es técnico: " + ex.Message);
            }
        }

        // ✅ NUEVO: Validar datos del técnico antes de guardar
        public bool ValidarTecnico(int? usuarioId, string especialidad, decimal salario,
                                  DateTime fechaContratacion, out string mensaje)
        {
            mensaje = string.Empty;

            if (usuarioId.HasValue && usuarioId.Value <= 0)
            {
                mensaje = "El ID de usuario no es válido";
                return false;
            }

            if (string.IsNullOrWhiteSpace(especialidad))
            {
                mensaje = "La especialidad es requerida";
                return false;
            }

            if (especialidad.Length > 200)
            {
                mensaje = "La especialidad no puede tener más de 200 caracteres";
                return false;
            }

            if (salario <= 0)
            {
                mensaje = "El salario debe ser mayor a cero";
                return false;
            }

            if (fechaContratacion > DateTime.Today)
            {
                mensaje = "La fecha de contratación no puede ser futura";
                return false;
            }

            if (fechaContratacion < DateTime.Today.AddYears(-50))
            {
                mensaje = "La fecha de contratación no es válida";
                return false;
            }

            return true;
        }

        // ✅ NUEVO: Método para crear técnico usando objeto Tecnico
        public bool CrearTecnico(Tecnico tecnico, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                // Usar el método de validación
                if (!ValidarTecnico(tecnico.UsuariosId, tecnico.Especialidad, tecnico.Salario,
                                   tecnico.FechaContratacion, out mensaje))
                {
                    return false;
                }

                // Llamar al método existente
                return CrearTecnico(tecnico.UsuariosId, tecnico.Especialidad, tecnico.FechaContratacion,
                                  tecnico.Salario, tecnico.Disponible, out mensaje);
            }
            catch (Exception ex)
            {
                mensaje = "Error al crear técnico: " + ex.Message;
                return false;
            }
        }

        // ✅ NUEVO: Método para actualizar técnico usando objeto Tecnico
        public bool ActualizarTecnico(Tecnico tecnico, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                // Usar el método de validación
                if (!ValidarTecnico(tecnico.UsuariosId, tecnico.Especialidad, tecnico.Salario,
                                   tecnico.FechaContratacion, out mensaje))
                {
                    return false;
                }

                // Llamar al método existente
                return ActualizarTecnico(tecnico.TecnicoID, tecnico.UsuariosId, tecnico.Especialidad,
                                       tecnico.FechaContratacion, tecnico.Salario, tecnico.Disponible,
                                       tecnico.Activo, out mensaje);
            }
            catch (Exception ex)
            {
                mensaje = "Error al actualizar técnico: " + ex.Message;
                return false;
            }
        }

        // ✅ NUEVO: Listar TODOS los usuarios técnicos activos
        // En CN_Tecnico.cs - Agregar este método
        public List<Usuario> ListarTodosUsuariosTecnicos()
        {
            try
            {
                return cdTecnico.ListarTodosUsuariosTecnicos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar todos los usuarios técnicos: " + ex.Message);
            }
        }


        // ✅ NUEVO: Método para cambiar disponibilidad
        public bool CambiarDisponibilidad(int tecnicoId, bool disponible, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                var tecnico = ObtenerTecnicoPorId(tecnicoId);
                if (tecnico == null)
                {
                    mensaje = "Técnico no encontrado";
                    return false;
                }

                return ActualizarTecnico(tecnicoId, tecnico.UsuariosId, tecnico.Especialidad,
                                       tecnico.FechaContratacion, tecnico.Salario, disponible,
                                       tecnico.Activo, out mensaje);
            }
            catch (Exception ex)
            {
                mensaje = "Error al cambiar disponibilidad: " + ex.Message;
                return false;
            }
        }



        public List<Tecnico> ListarTecnicosActivosDisponibles()
        {
            try
            {
                return cdTecnico.ListarActivosDisponibles();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar técnicos activos y disponibles: " + ex.Message);
            }
        }

        public int ObtenerTecnicoIDporUsuarioID(int usuarioId)
        {
            var lista = cdTecnico.ListarTodos(); // o ListarActivos()

            var tecnico = lista.Find(t => t.UsuariosId == usuarioId);

            if (tecnico != null)
                return tecnico.TecnicoID;

            return 0;
        }


    }
}