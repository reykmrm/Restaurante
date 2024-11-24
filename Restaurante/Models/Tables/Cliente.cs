using System;
using System.Collections.Generic;

namespace Restaurante.Models.Tables;

public partial class Cliente
{
    public int Identificacion { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
