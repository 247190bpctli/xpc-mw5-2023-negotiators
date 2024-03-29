﻿namespace eshopBackend.DAL.Entities;

public class CategoryEntity : BaseEntity
{
    public required string Name { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string Description { get; set; } = null!;
}