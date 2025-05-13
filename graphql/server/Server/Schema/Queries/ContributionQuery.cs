using Server.Mapper.ContributionTypes.OutputTypes;
using Server.Models.Types.Contribution.OutputTypes;
using Server.Repositories;

namespace Server.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class ContributionQuery
    {
        private readonly IContributionRepository _contributionRepository;
        private readonly IContributionToGetContributionTypeMapper _contributionToGetContributionTypeMapper;

        public ContributionQuery(IContributionRepository contributionRepository, IContributionToGetContributionTypeMapper contributionToGetContributionTypeMapper)
        {
            _contributionRepository = contributionRepository;
            _contributionToGetContributionTypeMapper = contributionToGetContributionTypeMapper;
        }

        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<GetContributionType>> GetContributions()
        {
            try
            {
                var contributionList = await _contributionRepository.GetContributionsAsync();
                var getContributionList = contributionList.Select(_contributionToGetContributionTypeMapper.Map).ToList();
                return getContributionList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetContributionType> GetContributionById(Guid contributionId)
        {
            try
            {
                var contribution = await _contributionRepository.GetContributionByIdAsync(contributionId);
                return _contributionToGetContributionTypeMapper.Map(contribution);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
