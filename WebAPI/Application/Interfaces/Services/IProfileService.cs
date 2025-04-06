using WebAPI.Application.Contracts;
using WebAPI.Application.Contracts.Common;

namespace WebAPI.Application.Interfaces.Services;

public interface IProfileService
{
    Task<OperationResult<ProfileResponse>> GetUserAsync(string userId);

}
