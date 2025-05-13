using Server.Models.Database;
using Server.Models.DTOs.Contribution.Responses;

namespace Server.Mapper.ContributionDTOs.Responses
{
    public class ContributionToGetContributionDtoMapper : IContributionToGetContributionDtoMapper
    {
        public GetContributionDto Map(Contribution contribution)
        {
            if (contribution == null)
                throw new ArgumentNullException(nameof(contribution));

            return new GetContributionDto
            {
                ContributionID = contribution.ContributionID,
                ProjectID = contribution.ProjectID,
                UserID = contribution.UserID,
                Category = contribution.Category,
                Title = contribution.Title,
                Text = contribution.Text,
                Date = contribution.Date,
                Status = contribution.Status,
            };
        }
    }
}
