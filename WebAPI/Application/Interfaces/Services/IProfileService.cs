using WebAPI.Application.Contracts.Common;
using WebAPI.Application.Contracts.Responses;

namespace WebAPI.Application.Interfaces.Services;

public interface IProfileService
{
    Task<OperationResult<ProfileResponse>> GetUserAsync(string userId);
}
