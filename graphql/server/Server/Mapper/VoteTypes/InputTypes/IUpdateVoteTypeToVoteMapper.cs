using Server.Models.Database;
using Server.Models.Types.Vote.InputTypes;

namespace Server.Mapper.VoteTypes.InputTypes
{
    public interface IUpdateVoteTypeToVoteMapper
    {
        public Vote Map(Vote existingVote, UpdateVoteType type);
    }
}
