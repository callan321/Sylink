using WebAPI.Application.Contracts.Common;
using WebAPI.Application.Contracts.Responses;
using WebAPI.Application.Interfaces.Security;
using WebAPI.Application.Interfaces.Services;


namespace WebAPI.Application.Services;

public class ApplicationUserService(IIdentityService identityService) : IApplicationUserService
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<OperationResult<AuthStatusResponse>> GetUserAsync(string userId)
    {
        var user = await _identityService.GetUserByIdAsync(userId);

        if (user == null)
            return OperationResult<AuthStatusResponse>.Fail("User not found");

        var response = new AuthStatusResponse
        {
            UserId = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email!,
        };

        return OperationResult<AuthStatusResponse>.Ok(response);
    }

}