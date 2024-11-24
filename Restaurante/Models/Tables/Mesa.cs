using System;
using System.Collections.Generic;

namespace Restaurante.Models.Tables;

public partial class Mesa
{
    public int NroMesa { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Reservada { get; set; }

    public int Puestos { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
