namespace CapaDominio.Utilidades
{
    public class ResultadoOperacion
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public int? IdGenerado { get; set; } // Agrega esta propiedad

        public ResultadoOperacion()
        {
            Exitoso = false;
            Mensaje = string.Empty;
            IdGenerado = null;
        }

        public ResultadoOperacion(bool exitoso, string mensaje, int? idGenerado = null)
        {
            Exitoso = exitoso;
            Mensaje = mensaje;
            IdGenerado = idGenerado;
        }

        public static ResultadoOperacion Exito(string mensaje = "Operación exitosa", int? idGenerado = null)
        {
            return new ResultadoOperacion(true, mensaje, idGenerado);
        }

        public static ResultadoOperacion Error(string mensaje)
        {
            return new ResultadoOperacion(false, mensaje);
        }
    }
}