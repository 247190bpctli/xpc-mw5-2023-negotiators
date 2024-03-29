﻿namespace eshopBackend.DAL.DTOs;

public record EditCartDto
{
    public int DeliveryType { get; init; }
    public string DeliveryAddress { get; init; } = null!;
    public int PaymentType { get; init; }
    public string PaymentDetails { get; init; } = null!;
}