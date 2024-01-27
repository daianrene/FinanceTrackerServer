using FinanceTracker.Models;

namespace FinanceTracker.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
