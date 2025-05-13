using Server.Models.Database;
using Server.Models.Types.Contribution.OutputTypes;

namespace Server.Mapper.ContributionTypes.OutputTypes
{
    public interface IContributionToGetContributionTypeMapper
    {
        public GetContributionType Map(Contribution contribution);
    }
}
