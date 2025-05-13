using Server.Models.Database;
using Server.Models.DTOs.Contribution.Requests;

namespace Server.Mapper.ContributionDTOs.Requests
{
    public class CreateContributionDtoToContributionMapper : ICreateContributionDtoToContributionMapper
    {
        public Contribution Map(CreateContributionDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Contribution
            {
                ContributionID = Guid.NewGuid(),
                ProjectID = dto.ProjectID,
                UserID = dto.UserID,
                Category = dto.Category,
                Title = dto.Title,
                Text = dto.Text,
                Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Status = "open",
            };
        }
    }
}
