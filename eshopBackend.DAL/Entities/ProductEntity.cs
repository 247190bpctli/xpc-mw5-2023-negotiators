﻿namespace eshopBackend.DAL.Entities;

public class ProductEntity : BaseEntity
{
    public required string Name { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double Price { get; set; }

    public double Weight { get; set; }

    public int Stock { get; set; }

    public CategoryEntity? Category { get; set; }

    public Guid? CategoryId { get; set; } //for seeding only

    public ManufacturerEntity? Manufacturer { get; set; }

    public Guid? ManufacturerId { get; set; } //for seeding only

    public required List<ReviewEntity> Reviews { get; set; }
}