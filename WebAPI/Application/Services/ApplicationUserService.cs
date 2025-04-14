using WebAPI.Application.Contracts.ResponsesData;
using WebAPI.Application.Interfaces.Security;
using WebAPI.Application.Interfaces.Services;


namespace WebAPI.Application.Services;

public class ApplicationUserService(IIdentityService identityService) : IApplicationUserService
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<OperationResult<AuthStatusResponseData>> GetUserAsync(string userId)
    {
        var user = await _identityService.GetUserByIdAsync(userId);

        if (user == null)
            return OperationResult<AuthStatusResponseData>.Fail("User not found");

        var response = new AuthStatusResponseData
        {
            UserId = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email!,
        };

        return OperationResult<AuthStatusResponseData>.Ok(response);
    }

}