namespace Restaurante.Models.DTO
{
    public class ClienteDTO
    {
        public int Identificacion { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
    }

}
