using Server.Models.Database;
using Server.Models.Types.Vote.InputTypes;

namespace Server.Mapper.VoteTypes.InputTypes
{
    public class UpdateVoteTypeToVoteMapper : IUpdateVoteTypeToVoteMapper
    {
        public Vote Map(Vote existingVote, UpdateVoteType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return new Vote
            {
                VoteID = type.VoteID,
                UserID = type.UserID ?? existingVote.UserID,
                ContributionID = type.ContributionID ?? existingVote.ContributionID
            };
        }
    }
}
