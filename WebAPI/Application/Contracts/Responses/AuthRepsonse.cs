﻿namespace WebAPI.Application.Contracts.ResponsesData;

public class AuthStatusResponse
{
    public string? UserId { get; set; }
    public string? Email { get; set; }
    public string? DisplayName { get; set; }
}

