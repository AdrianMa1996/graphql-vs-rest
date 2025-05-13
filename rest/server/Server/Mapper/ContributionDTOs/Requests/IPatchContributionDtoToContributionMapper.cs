using Server.Models.Database;
using Server.Models.DTOs.Contribution.Requests;

namespace Server.Mapper.ContributionDTOs.Requests
{
    public interface IPatchContributionDtoToContributionMapper
    {
        public Contribution Map(Contribution existingContribution, PatchContributionDto dto);
    }
}
