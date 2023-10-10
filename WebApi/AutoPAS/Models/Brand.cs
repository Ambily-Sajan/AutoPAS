using System;
using System.Collections.Generic;

namespace AutoPAS.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public int? VehicleTypeId { get; set; }

    public string? Brand1 { get; set; }

    public string? Description { get; set; }

    public int? SortOrder { get; set; }

    public sbyte? IsActive { get; set; }

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();

    public virtual Vehicletype? VehicleType { get; set; }

    public virtual ICollection<Vehicle?> Vehicles { get; set; } = new List<Vehicle?>();
}
