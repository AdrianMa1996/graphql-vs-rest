using Server.Models.Database;

namespace Server.Services
{
    public interface ITokenService
    {
        public Task<string> CreateToken(User user);
    }
}
