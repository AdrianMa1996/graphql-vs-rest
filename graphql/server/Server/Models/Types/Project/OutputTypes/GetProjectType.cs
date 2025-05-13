using HotChocolate.Authorization;
using Server.Mapper.ContributionTypes.OutputTypes;
using Server.Mapper.ProjectAndUserBindingTypes.OutputTypes;
using Server.Models.Types.Contribution.OutputTypes;
using Server.Models.Types.ProjectAndUserBinding.OutputTypes;
using Server.Repositories;

namespace Server.Models.Types.Project.OutputTypes
{
    public class GetProjectType
    {
        [Authorize]
        public Guid ProjectID { get; set; }

        [Authorize]
        public string Name { get; set; }

        [Authorize]
        public string Logo { get; set; }

        [Authorize]
        [GraphQLNonNullType]
        public async Task<IEnumerable<GetProjectAndUserBindingType>> ProjectAndUserBindingsAsync([Service] IProjectAndUserBindingRepository projectAndUserBindingRepository, [Service] IProjectAndUserBindingToGetProjectAndUserBindingTypeMapper projectAndUserBindingToGetProjectAndUserBindingTypeMapper)
        {
            var projectAndUserBindingList = await projectAndUserBindingRepository.GetProjectAndUserBindingsByProjectIdAsync(ProjectID);
            var getProjectAndUserBindingList = projectAndUserBindingList.Select(projectAndUserBindingToGetProjectAndUserBindingTypeMapper.Map).ToList();
            return getProjectAndUserBindingList;
        }

        [Authorize]
        [GraphQLNonNullType]
        public async Task<IEnumerable<GetContributionType>> ContributionsAsync([Service] IContributionRepository contributionRepository, [Service] IContributionToGetContributionTypeMapper contributionToGetContributionTypeMapper)
        {
            var contributionList = await contributionRepository.GetContributionsByProjectIdAsync(ProjectID);
            var getContributionList = contributionList.Select(contributionToGetContributionTypeMapper.Map);
            return getContributionList;
        }
    }
}
