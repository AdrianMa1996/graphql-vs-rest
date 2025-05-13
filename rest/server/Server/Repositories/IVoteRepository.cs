using Server.Models.Database;

namespace Server.Repositories
{
    public interface IVoteRepository
    {
        public Task<Vote> GetVoteByIdAsync(Guid voteId);

        public Task<List<Vote>> GetVotesAsync();

        public Task CreateVoteAsync(Vote vote);

        public Task UpdateVoteAsync(Vote vote);

        public Task DeleteVoteByIdAsync(Guid voteId);

        public Task<List<Vote>> GetVotesByUserIdAsync(Guid userId);

        public Task<List<Vote>> GetVotesByContributionIdAsync(Guid contributionId);
    }
}
