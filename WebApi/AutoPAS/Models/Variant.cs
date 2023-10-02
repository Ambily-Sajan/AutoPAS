using System;
using System.Collections.Generic;

namespace AutoPAS.Models;

public partial class Variant
{
    public int VariantId { get; set; }

    public string? Variant1 { get; set; }

    public string? Description { get; set; }

    public sbyte? IsActive { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
