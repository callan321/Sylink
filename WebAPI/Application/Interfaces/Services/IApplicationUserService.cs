using WebAPI.Application.Contracts.Common;
using WebAPI.Application.Contracts.Responses;

namespace WebAPI.Application.Interfaces.Services;

public interface IApplicationUserService
{
    Task<OperationResult<AuthStatusResponse>> GetUserAsync(string userId);
}
