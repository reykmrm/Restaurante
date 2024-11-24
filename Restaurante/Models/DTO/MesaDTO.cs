namespace Restaurante.Models.DTO
{
    public class MesaDTO
    {
        public int NroMesa { get; set; }

        public string Nombre { get; set; } = null!;

        public bool? Reservada { get; set; }

        public int Puestos { get; set; }
    }
}
