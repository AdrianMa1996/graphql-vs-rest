using Server.Models.Database;
using Server.Models.Types.Contribution.OutputTypes;

namespace Server.Mapper.ContributionTypes.OutputTypes
{
    public class ContributionToGetContributionTypeMapper : IContributionToGetContributionTypeMapper
    {
        public GetContributionType Map(Contribution contribution)
        {
            if (contribution == null)
                throw new ArgumentNullException(nameof(contribution));

            return new GetContributionType
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
