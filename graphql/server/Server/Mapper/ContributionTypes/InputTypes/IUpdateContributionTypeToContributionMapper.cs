using Server.Models.Database;
using Server.Models.Types.Contribution.InputTypes;

namespace Server.Mapper.ContributionTypes.InputTypes
{
    public interface IUpdateContributionTypeToContributionMapper
    {
        public Contribution Map(Contribution existingContribution, UpdateContributionType type);
    }
}
