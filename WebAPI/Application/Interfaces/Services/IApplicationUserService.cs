using WebAPI.Application.Contracts.ResponsesData;

namespace WebAPI.Application.Interfaces.Services;

public interface IApplicationUserService
{
    Task<OperationResult<AuthStatusResponse>> GetUserAsync(string userId);
}
