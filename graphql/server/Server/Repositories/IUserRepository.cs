using Server.Models.Database;

namespace Server.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserByIdAsync(Guid userId);

        public Task<User> GetUserByNameAsync(string userName);

        public Task<List<User>> GetUsersAsync();

        public Task<List<User>> GetUsersByIdsAsync(List<Guid> userIds);

        public Task CreateUserAsync(User user);

        public Task UpdateUserAsync(User user);

        public Task DeleteUserByIdAsync(Guid userId);
    }
}
