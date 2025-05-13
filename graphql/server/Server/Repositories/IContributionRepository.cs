using Server.Models.Database;

namespace Server.Repositories
{
    public interface IContributionRepository
    {
        public Task<Contribution> GetContributionByIdAsync(Guid contributionId);

        public Task<List<Contribution>> GetContributionsAsync();

        public Task<List<Contribution>> GetContributionsByIdsAsync(List<Guid> contributionIds);

        public Task CreateContributionAsync(Contribution contribution);

        public Task UpdateContributionAsync(Contribution contribution);

        public Task DeleteContributionByIdAsync(Guid contributionId);

        public Task<List<Contribution>> GetContributionsByUserIdAsync(Guid userId);

        public Task<List<Contribution>> GetContributionsByProjectIdAsync(Guid projectId);
    }
}
