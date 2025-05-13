using Server.Models.Database;
using Server.Models.DTOs.Vote.Responses;

namespace Server.Mapper.VoteDTOs.Responses
{
    public interface IVoteToGetVoteDtoMapper
    {
        public GetVoteDto Map(Vote vote);
    }
}
