﻿namespace eshopBackend.DAL.Entities;

public class ProductEntity : BaseEntity
{
    public required string Name { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public string? Description { get; set; }
    
    public double Price { get; set; }
    
    public double Weight { get; set; }
    
    public int Stock { get; set; }
    
    public CategoryEntity? Category { get; set; }
    
    public ManufacturerEntity? Manufacturer { get; set; }
    
    public required List<ReviewEntity> Reviews { get; set; }
}