using Server.Models.Database;
using Server.Models.Types.Contribution.InputTypes;

namespace Server.Mapper.ContributionTypes.InputTypes
{
    public class CreateContributionTypeToContributionMapper : ICreateContributionTypeToContributionMapper
    {
        public Contribution Map(CreateContributionType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return new Contribution
            {
                ContributionID = Guid.NewGuid(),
                ProjectID = type.ProjectID,
                UserID = type.UserID,
                Category = type.Category,
                Title = type.Title,
                Text = type.Text,
                Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Status = "open",
            };
        }
    }
}
