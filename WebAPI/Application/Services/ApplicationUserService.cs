using WebAPI.Application.Contracts.Common;
using WebAPI.Application.Contracts.Responses;
using WebAPI.Application.Interfaces.Services;

namespace WebAPI.Application.Services;

public class ApplicationUserService(IIdentityService identityService) : IApplicationUserService
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