using Server.Models.Database;
using Server.Models.DTOs.Contribution.Requests;

namespace Server.Mapper.ContributionDTOs.Requests
{
    public class PatchContributionDtoToContributionMapper : IPatchContributionDtoToContributionMapper
    {
        public Contribution Map(Contribution existingContribution, PatchContributionDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (existingContribution == null)
                throw new ArgumentNullException(nameof(existingContribution));

            return new Contribution
            {
                ContributionID = dto.ContributionID,
                ProjectID = dto.ProjectID ?? existingContribution.ProjectID,
                UserID = dto.UserID ?? existingContribution.UserID,
                Category = dto.Category ?? existingContribution.Category,
                Title = dto.Title ?? existingContribution.Title,
                Text = dto.Text ?? existingContribution.Text,
                Date = dto.Date ?? existingContribution.Date,
                Status = dto.Status ?? existingContribution.Status,
            };
        }
    }
}
