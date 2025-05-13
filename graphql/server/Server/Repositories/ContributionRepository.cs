using Server.Models.Database;
using SQLite;

namespace Server.Repositories
{
    public class ContributionRepository : IContributionRepository
    {
        private IVoteRepository _voteRepository;
        private ICommentRepository _commentRepository;

        private SQLiteAsyncConnection _dbConnection;
        private string dbPath = ".\\Resources\\Raw\\database.sqlite";

        public ContributionRepository(IVoteRepository voteRepository, ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            _voteRepository = voteRepository;

            if (_dbConnection == null)
            {
                _dbConnection = new SQLiteAsyncConnection(dbPath);
            }
        }

        public Task<Contribution> GetContributionByIdAsync(Guid contributionId) => _dbConnection.GetAsync<Contribution>(contributionId);

        public Task<List<Contribution>> GetContributionsAsync() => _dbConnection.Table<Contribution>().OrderBy(p => p.Date).ToListAsync();

        public Task<List<Contribution>> GetContributionsByIdsAsync(List<Guid> contributionIds) => _dbConnection.Table<Contribution>().Where(c => contributionIds.Contains(c.ContributionID)).OrderBy(p => p.Date).ToListAsync();

        public Task CreateContributionAsync(Contribution contribution) => _dbConnection.InsertAsync(contribution);

        public Task UpdateContributionAsync(Contribution contribution) => _dbConnection.UpdateAsync(contribution);

        public async Task DeleteContributionByIdAsync(Guid contributionId)
        {
            var votes = await _voteRepository.GetVotesByContributionIdAsync(contributionId);
            foreach (var vote in votes)
            {
                await _voteRepository.DeleteVoteByIdAsync(vote.VoteID);
            }

            var comments = await _commentRepository.GetCommentsByContributionIdAsync(contributionId);
            foreach (var comment in comments)
            {
                await _commentRepository.DeleteCommentByIdAsync(comment.CommentID);
            }

            _ = _dbConnection.DeleteAsync<Contribution>(contributionId);
        }

        public Task<List<Contribution>> GetContributionsByUserIdAsync(Guid userId) => _dbConnection.Table<Contribution>().Where(x => x.UserID == userId).OrderBy(p => p.Date).ToListAsync();

        public Task<List<Contribution>> GetContributionsByProjectIdAsync(Guid projectId) => _dbConnection.Table<Contribution>().Where(x => x.ProjectID == projectId).OrderBy(p => p.Date).ToListAsync();
    }
}
