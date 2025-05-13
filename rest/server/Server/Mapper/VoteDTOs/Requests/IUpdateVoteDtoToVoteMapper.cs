using Server.Models.Database;
using Server.Models.DTOs.Vote.Requests;

namespace Server.Mapper.VoteDTOs.Requests
{
    public interface IUpdateVoteDtoToVoteMapper
    {
        public Vote Map(UpdateVoteDto dto);
    }
}
