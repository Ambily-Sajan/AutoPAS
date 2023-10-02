using System;
using System.Collections.Generic;

namespace AutoPAS.Models;

public partial class Bodytype
{
    public int BodyTypeId { get; set; }

    public string? BodyType1 { get; set; }

    public string? Description { get; set; }

    public sbyte? IsActive { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
