namespace Restaurante.Models.DTO
{
    public class FacturaDTO
    {
        public int NroFactura { get; set; }
        public int IdCliente { get; set; }
        public int NroMesa { get; set; }
        public int IdMesero { get; set; }
        public DateOnly Fecha { get; set; }
    }
}
