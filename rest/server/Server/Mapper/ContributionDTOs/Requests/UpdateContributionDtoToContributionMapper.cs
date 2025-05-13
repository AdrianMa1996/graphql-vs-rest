using Server.Models.Database;
using Server.Models.DTOs.Contribution.Requests;

namespace Server.Mapper.ContributionDTOs.Requests
{
    public class UpdateContributionDtoToContributionMapper : IUpdateContributionDtoToContributionMapper
    {
        public Contribution Map(UpdateContributionDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Contribution
            {
                ContributionID = dto.ContributionID,
                ProjectID = dto.ProjectID,
                UserID = dto.UserID,
                Category = dto.Category,
                Title = dto.Title,
                Text = dto.Text,
                Date = dto.Date,
                Status = dto.Status,
            };
        }
    }
}
