using Server.Models.Database;
using SQLite;

namespace Server.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private SQLiteAsyncConnection _dbConnection;
        private string dbPath = ".\\Resources\\Raw\\database.sqlite";

        public VoteRepository()
        {
            if (_dbConnection == null)
            {
                _dbConnection = new SQLiteAsyncConnection(dbPath);
            }
        }

        public Task<Vote> GetVoteByIdAsync(Guid voteId) => _dbConnection.GetAsync<Vote>(voteId);

        public Task<List<Vote>> GetVotesAsync() => _dbConnection.Table<Vote>().ToListAsync();

        public Task CreateVoteAsync(Vote vote) => _dbConnection.InsertAsync(vote);

        public Task UpdateVoteAsync(Vote vote) => _dbConnection.UpdateAsync(vote);

        public Task DeleteVoteByIdAsync(Guid voteId) => _dbConnection.DeleteAsync<Vote>(voteId);

        public Task<List<Vote>> GetVotesByUserIdAsync(Guid userId) => _dbConnection.Table<Vote>().Where(x => x.UserID == userId).ToListAsync();

        public Task<List<Vote>> GetVotesByContributionIdAsync(Guid contributionId) => _dbConnection.Table<Vote>().Where(x => x.ContributionID == contributionId).ToListAsync();
    }
}
