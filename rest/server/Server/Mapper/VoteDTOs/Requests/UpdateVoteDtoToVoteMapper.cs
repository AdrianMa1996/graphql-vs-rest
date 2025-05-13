using Server.Models.Database;
using Server.Models.DTOs.Vote.Requests;

namespace Server.Mapper.VoteDTOs.Requests
{
    public class UpdateVoteDtoToVoteMapper : IUpdateVoteDtoToVoteMapper
    {
        public Vote Map(UpdateVoteDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Vote
            {
                VoteID = dto.VoteID,
                UserID = dto.UserID,
                ContributionID = dto.ContributionID
            };
        }
    }
}
