using Server.Models.Database;
using Server.Models.DTOs.Contribution.Responses;

namespace Server.Mapper.ContributionDTOs.Responses
{
    public interface IContributionToGetContributionDtoMapper
    {
        public GetContributionDto Map(Contribution contribution);
    }
}
