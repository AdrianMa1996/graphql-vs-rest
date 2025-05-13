using Server.Models.Database;
using Server.Models.DTOs.Contribution.Requests;

namespace Server.Mapper.ContributionDTOs.Requests
{
    public interface ICreateContributionDtoToContributionMapper
    {
        public Contribution Map(CreateContributionDto dto);
    }
}
