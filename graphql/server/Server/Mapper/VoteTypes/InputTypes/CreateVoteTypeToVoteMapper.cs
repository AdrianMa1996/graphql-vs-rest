using Server.Models.Database;
using Server.Models.Types.Vote.InputTypes;

namespace Server.Mapper.VoteTypes.InputTypes
{
    public class CreateVoteTypeToVoteMapper : ICreateVoteTypeToVoteMapper
    {
        public Vote Map(CreateVoteType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return new Vote
            {
                VoteID = Guid.NewGuid(),
                UserID = type.UserID,
                ContributionID = type.ContributionID
            };
        }
    }
}
