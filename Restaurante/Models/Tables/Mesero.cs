using System;
using System.Collections.Generic;

namespace Restaurante.Models.Tables;

public partial class Mesero
{
    public int IdMesero { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public int? Edad { get; set; }

    public DateOnly Antiguedad { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
