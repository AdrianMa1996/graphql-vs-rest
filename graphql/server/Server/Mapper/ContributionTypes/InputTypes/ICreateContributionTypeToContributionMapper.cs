using Server.Models.Database;
using Server.Models.Types.Contribution.InputTypes;

namespace Server.Mapper.ContributionTypes.InputTypes
{
    public interface ICreateContributionTypeToContributionMapper
    {
        public Contribution Map(CreateContributionType type);
    }
}
