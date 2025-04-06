﻿using WebAPI.Application.Contracts;
using WebAPI.Application.Contracts.Common;
using WebAPI.Application.Interfaces.Services;

namespace WebAPI.Application.Services;

public class ProfileService(IIdentityService identityService) : IProfileService
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<OperationResult<ProfileResponse>> GetUserAsync(string userId)
    {
        var user = await _identityService.GetUserByIdAsync(userId);

        if (user == null)
            return OperationResult<ProfileResponse>.Fail("User not found");

        var response = new ProfileResponse
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email!,
        };

        return OperationResult<ProfileResponse>.Ok(response);
    }

}