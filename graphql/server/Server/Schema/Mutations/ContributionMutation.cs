using HotChocolate.Authorization;
using Server.Mapper.ContributionTypes.InputTypes;
using Server.Models.Types.Contribution.InputTypes;
using Server.Repositories;

namespace Server.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class ContributionMutation
    {
        [Authorize]
        public async Task<string> CreateContributionAsync(CreateContributionType input, [Service] IContributionRepository contributionRepository, [Service] ICreateContributionTypeToContributionMapper createContributionTypeToContributionMapper)
        {
            var contribution = createContributionTypeToContributionMapper.Map(input);
            await contributionRepository.CreateContributionAsync(contribution);
            return "Contribution created successfully";
        }

        [Authorize]
        public async Task<string> UpdateContributionAsync(UpdateContributionType input, [Service] IContributionRepository contributionRepository, [Service] IUpdateContributionTypeToContributionMapper updateContributionTypeToContributionMapper)
        {
            var existingContribution = await contributionRepository.GetContributionByIdAsync(input.ContributionID);
            var contribution = updateContributionTypeToContributionMapper.Map(existingContribution, input);
            await contributionRepository.UpdateContributionAsync(contribution);
            return "Contribution updated successfully";
        }

        [Authorize]
        public async Task<string> DeleteContributionByIdAsync(Guid contributionId, [Service] IContributionRepository contributionRepository)
        {
            await contributionRepository.DeleteContributionByIdAsync(contributionId);
            return "Contribution deleted successfully";
        }
    }
}
