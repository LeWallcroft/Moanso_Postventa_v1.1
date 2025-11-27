namespace Dominio
{
    public class Modelo
    {
        public int ModeloID { get; set; }
        public int MarcaID { get; set; }
        public string Nombre { get; set; }
        public int? Anio { get; set; }
        public string TipoVehiculo { get; set; }
        public string Descripcion { get; set; }

        public string NombreCompleto => $"{Nombre} {(Anio.HasValue ? Anio.Value.ToString() : "")}";
    }
}