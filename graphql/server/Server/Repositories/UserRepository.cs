using Server.Models.Database;
using SQLite;

namespace Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IProjectAndUserBindingRepository _projectAndUserBindingRepository;
        private IVoteRepository _voteRepository;
        private ICommentRepository _commentRepository;
        private IContributionRepository _contributionRepository;

        private SQLiteAsyncConnection _dbConnection;
        private string dbPath = ".\\Resources\\Raw\\database.sqlite";

        public UserRepository(IProjectAndUserBindingRepository projectAndUserBindingRepository, IVoteRepository voteRepository, ICommentRepository commentRepository, IContributionRepository contributionRepository)
        {
            _projectAndUserBindingRepository = projectAndUserBindingRepository;
            _voteRepository = voteRepository;
            _commentRepository = commentRepository;
            _contributionRepository = contributionRepository;

            if (_dbConnection == null)
            {
                _dbConnection = new SQLiteAsyncConnection(dbPath);
            }
        }

        public Task<User> GetUserByIdAsync(Guid userId) => _dbConnection.GetAsync<User>(userId);

        public Task<User> GetUserByNameAsync(string userName) => _dbConnection.Table<User>().Where(u => u.Name == userName).FirstOrDefaultAsync();

        public Task<List<User>> GetUsersAsync() => _dbConnection.Table<User>().OrderBy(p => p.Name).ToListAsync();

        public Task<List<User>> GetUsersByIdsAsync(List<Guid> userIds) => _dbConnection.Table<User>().Where(p => userIds.Contains(p.UserID)).OrderBy(p => p.Name).ToListAsync();

        public Task CreateUserAsync(User user) => _dbConnection.InsertAsync(user);

        public Task UpdateUserAsync(User user) => _dbConnection.UpdateAsync(user);

        public async Task DeleteUserByIdAsync(Guid userId)
        {
            var projectAndUserBindings = await _projectAndUserBindingRepository.GetProjectAndUserBindingsByUserIdAsync(userId);
            foreach (var projectAndUserBinding in projectAndUserBindings)
            {
                await _projectAndUserBindingRepository.DeleteProjectAndUserBindingByIdAsync(projectAndUserBinding.ProjectAndUserBindingID);
            }

            var votes = await _voteRepository.GetVotesByUserIdAsync(userId);
            foreach (var vote in votes)
            {
                await _voteRepository.DeleteVoteByIdAsync(vote.VoteID);
            }

            var comments = await _commentRepository.GetCommentsByUserIdAsync(userId);
            foreach(var comment in comments)
            {
                await _commentRepository.DeleteCommentByIdAsync(comment.CommentID);
            }

            var contributions = await _contributionRepository.GetContributionsByUserIdAsync(userId);
            foreach(var contribution in contributions) 
            {
                await _contributionRepository.DeleteContributionByIdAsync(contribution.ContributionID);
            }

            _ = _dbConnection.DeleteAsync<User>(userId);
        }
    }
}
