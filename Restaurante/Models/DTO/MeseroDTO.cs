namespace Restaurante.Models.DTO
{
    public class MeseroDTO
    {
        public int IdMesero { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public int? Edad { get; set; }
        public DateOnly Antiguedad { get; set; }
    }
}
