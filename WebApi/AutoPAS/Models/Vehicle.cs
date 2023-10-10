using System;
using System.Collections.Generic;

namespace AutoPAS.Models;

public partial class Vehicle
{
    public string VehicleId { get; set; } = null!;

    public int? VehicleTypeid { get; set; }

    public int? Rtoid { get; set; }

    public int? BrandId { get; set; }

    public int? ModelId { get; set; }

    public int? VariantId { get; set; }

    public int? BodyTypeId { get; set; }

    public int? TransmissionTypeId { get; set; }

    public string? RegistrationNumber { get; set; }

    public DateTime? DateOfPurchase { get; set; }

    public string? Color { get; set; }

    public string? ChasisNumber { get; set; }

    public string? EngineNumber { get; set; }

    public int? CubicCapacity { get; set; }

    public int? SeatingCapacity { get; set; }

    public int? YearOfManufacture { get; set; }

    public decimal? Idv { get; set; }

    public decimal? ExShowroomPrice { get; set; }

    public int? FuelTypeId { get; set; }

    public virtual Bodytype BodyType { get; set; } = null!;

    public virtual Brand Brand { get; set; } = null!;

    public virtual Fueltype FuelType { get; set; } = null!;

    public virtual Model Model { get; set; } = null!;

    public virtual Rto Rto { get; set; } = null!;

    public virtual Transmissiontype TransmissionType { get; set; } = null!;

    public virtual Variant Variant { get; set; } = null!;

    public virtual Vehicletype VehicleType { get; set; }
}