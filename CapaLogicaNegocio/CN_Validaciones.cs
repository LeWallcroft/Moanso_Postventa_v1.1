using System.Text.RegularExpressions;

namespace CapaLogica
{
    public class CN_Validaciones
    {
        public static bool ValidarDNI(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return false;

            return Regex.IsMatch(dni.Trim(), @"^\d{8}$");
        }

        public static bool ValidarPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                return false;

            return Regex.IsMatch(placa.Trim().ToUpper(), @"^[A-Z0-9-]{4,10}$");
        }

        public static bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true; // Email es opcional

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email.Trim();
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidarTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return true; // Teléfono es opcional

            // Validar formato básico de teléfono (puedes ajustar según tu país)
            return Regex.IsMatch(telefono.Trim(), @"^[\d\s\-\+\(\)]{7,20}$");
        }

        public static bool ValidarAnioVehiculo(int? anio)
        {
            if (!anio.HasValue)
                return true; // Año es opcional

            int añoActual = System.DateTime.Now.Year;
            return anio >= 1900 && anio <= añoActual + 1;
        }

        public static bool ValidarKilometraje(int? kilometraje)
        {
            if (!kilometraje.HasValue)
                return true; // Kilometraje es opcional

            return kilometraje >= 0 && kilometraje <= 1000000;
        }
    }
}