using System;

namespace CapaDominio
{
    public class Tecnico
    {
        public int TecnicoID { get; set; }
        public int? UsuariosId { get; set; }
        public string Especialidad { get; set; }
        public DateTime FechaContratacion { get; set; }
        public decimal Salario { get; set; }
        public bool Disponible { get; set; }
        public bool Activo { get; set; }

        // Propiedades de navegación
        public Usuario Usuario { get; set; }

        // ✅ CORRECCIÓN: Propiedades de visualización con manejo seguro de nulls
        public string NombreCompleto
        {
            get
            {
                if (Usuario != null && !string.IsNullOrEmpty(Usuario.Nombre))
                    return $"{Usuario.Nombre} {Usuario.Apellido}".Trim();
                return "SIN ASIGNAR";
            }
        }

        public string EmailUsuario
        {
            get
            {
                return Usuario?.Email ?? "N/A";
            }
        }

        public string RolUsuario
        {
            get
            {
                return Usuario?.Rol ?? "N/A";
            }
        }

        public string DisponibleDisplay => Disponible ? "SÍ" : "NO";
        public string ActivoDisplay => Activo ? "ACTIVO" : "INACTIVO";
        public string SalarioFormateado => Salario.ToString("C2");

        // ✅ NUEVA: Propiedad para mostrar fecha formateada
        public string FechaContratacionDisplay => FechaContratacion.ToString("dd/MM/yyyy");

        // ✅ NUEVA: Propiedad para estado combinado
        public string EstadoCompleto
        {
            get
            {
                if (!Activo) return "INACTIVO";
                return Disponible ? "DISPONIBLE" : "OCUPADO";
            }
        }

        // ✅ CORRECCIÓN: Constructor mejorado
        public Tecnico()
        {
            // Inicializar con valores por defecto
            Especialidad = string.Empty;
            FechaContratacion = DateTime.Today;
            Salario = 0;
            Disponible = true;
            Activo = true;
            Usuario = new Usuario();
        }

        // ✅ NUEVO: Constructor con parámetros
        public Tecnico(int? usuarioId, string especialidad, decimal salario, bool disponible = true)
        {
            UsuariosId = usuarioId;
            Especialidad = especialidad ?? string.Empty;
            FechaContratacion = DateTime.Today;
            Salario = salario;
            Disponible = disponible;
            Activo = true;
            Usuario = new Usuario();
        }

        // ✅ NUEVO: Método para validar datos del técnico
        public bool Validar(out string mensajeError)
        {
            mensajeError = string.Empty;

            if (string.IsNullOrWhiteSpace(Especialidad))
            {
                mensajeError = "La especialidad es requerida";
                return false;
            }

            if (Salario <= 0)
            {
                mensajeError = "El salario debe ser mayor a cero";
                return false;
            }

            if (FechaContratacion > DateTime.Today)
            {
                mensajeError = "La fecha de contratación no puede ser futura";
                return false;
            }

            return true;
        }

        // ✅ NUEVO: Método para clonar técnico
        public Tecnico Clonar()
        {
            return new Tecnico
            {
                TecnicoID = this.TecnicoID,
                UsuariosId = this.UsuariosId,
                Especialidad = this.Especialidad,
                FechaContratacion = this.FechaContratacion,
                Salario = this.Salario,
                Disponible = this.Disponible,
                Activo = this.Activo,
            };
        }
    }
}