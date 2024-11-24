using System;
using System.Collections.Generic;

namespace Restaurante.Models.Tables;

public partial class DetalleFactura
{
    public int IdDetalleFactura { get; set; }

    public int NroFactura { get; set; }

    public int IdSupervisor { get; set; }

    public string Plato { get; set; } = null!;

    public decimal Valor { get; set; }

    public virtual Supervisor IdSupervisorNavigation { get; set; } = null!;

    public virtual Factura NroFacturaNavigation { get; set; } = null!;
}
