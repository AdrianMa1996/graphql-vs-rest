using Server.Models.Database;
using Server.Models.DTOs.Vote.Requests;

namespace Server.Mapper.VoteDTOs.Requests
{
    public interface ICreateVoteDtoToVoteMapper
    {
        public Vote Map(CreateVoteDto dto);
    }
}
