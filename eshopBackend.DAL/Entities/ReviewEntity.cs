﻿namespace eshopBackend.DAL.Entities;

public class ReviewEntity : BaseEntity
{
    public required byte Stars { get; set; }
    
    public required string User { get; set; }

    public string? Description { get; set; }
}