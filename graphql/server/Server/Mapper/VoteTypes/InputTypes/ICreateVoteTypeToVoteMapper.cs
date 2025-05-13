using Server.Models.Database;
using Server.Models.Types.Vote.InputTypes;

namespace Server.Mapper.VoteTypes.InputTypes
{
    public interface ICreateVoteTypeToVoteMapper
    {
        public Vote Map(CreateVoteType type);
    }
}
