using Server.Models.Database;
using Server.Models.Types.Contribution.InputTypes;

namespace Server.Mapper.ContributionTypes.InputTypes
{
    public class UpdateContributionTypeToContributionMapper : IUpdateContributionTypeToContributionMapper
    {
        public Contribution Map(Contribution existingContribution, UpdateContributionType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return new Contribution
            {
                ContributionID = type.ContributionID,
                ProjectID = type.ProjectID ?? existingContribution.ProjectID,
                UserID = type.UserID ?? existingContribution.UserID,
                Category = type.Category ?? existingContribution.Category,
                Title = type.Title ?? existingContribution.Title,
                Text = type.Text ?? existingContribution.Text,
                Date = type.Date ?? existingContribution.Date,
                Status = type.Status ?? existingContribution.Status,
            };
        }
    }
}
