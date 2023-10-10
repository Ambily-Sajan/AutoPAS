using System;
using System.Collections.Generic;

namespace AutoPAS.Models;

public partial class Vehicletype
{
    public int? VehicleTypeId { get; set; }

    public string VehicleType1 { get; set; } = null!;

    public string? Description { get; set; }

    public sbyte? IsActive { get; set; }

    public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
