namespace Restaurante.Models.DTO
{
    public class DetalleFacturaDTO
    {
        public int IdDetalleFactura { get; set; }
        public int NroFactura { get; set; }
        public int IdSupervisor { get; set; }
        public string Plato { get; set; } = null!;
        public decimal Valor { get; set; }
    }
}
