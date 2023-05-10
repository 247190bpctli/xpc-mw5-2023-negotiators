﻿namespace eshopBackend.DAL.DTOs;

public record EditProductDto
{
    public Guid ProductId { get; init; }
    public string Name { get; init; }
    public string? ImageUrl { get; init; }
    public string? Description { get; init; }
    public double Price { get; init; }
    public double Weight { get; init; }
    public int Stock { get; init; }
    public Guid? CategoryId { get; init; }
    public Guid? ManufacturerId { get; init; }
}