using Server.Models.Database;
using Server.Models.Types.Vote.OutputTypes;

namespace Server.Mapper.VoteTypes.OutputTypes
{
    public interface IVoteToGetVoteTypeMapper
    {
        public GetVoteType Map(Vote vote);
    }
}
