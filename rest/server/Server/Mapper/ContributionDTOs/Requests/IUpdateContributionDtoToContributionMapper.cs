using Server.Models.Database;
using Server.Models.DTOs.Contribution.Requests;

namespace Server.Mapper.ContributionDTOs.Requests
{
    public interface IUpdateContributionDtoToContributionMapper
    {
        public Contribution Map(UpdateContributionDto dto);
    }
}
