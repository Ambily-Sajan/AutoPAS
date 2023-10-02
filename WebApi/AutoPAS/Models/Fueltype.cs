using System;
using System.Collections.Generic;

namespace AutoPAS.Models;

public partial class Fueltype
{
    public int FuelTypeId { get; set; }

    public string? FuelType1 { get; set; }

    public string? Description { get; set; }

    public sbyte? IsActive { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
