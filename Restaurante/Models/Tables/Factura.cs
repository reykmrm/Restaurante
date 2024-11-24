using System;
using System.Collections.Generic;

namespace Restaurante.Models.Tables;

public partial class Factura
{
    public int NroFactura { get; set; }

    public int IdCliente { get; set; }

    public int NroMesa { get; set; }

    public int IdMesero { get; set; }

    public DateOnly Fecha { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Mesero IdMeseroNavigation { get; set; } = null!;

    public virtual Mesa NroMesaNavigation { get; set; } = null!;
}
