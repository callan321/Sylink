using WebAPI.Domain.Entities;

namespace WebAPI.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task AddAsync(RefreshToken token);
    Task DeleteAsync(string token);
}
