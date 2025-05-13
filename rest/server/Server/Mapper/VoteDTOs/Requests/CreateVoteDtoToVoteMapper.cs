using Server.Models.Database;
using Server.Models.DTOs.Vote.Requests;

namespace Server.Mapper.VoteDTOs.Requests
{
    public class CreateVoteDtoToVoteMapper : ICreateVoteDtoToVoteMapper
    {
        public Vote Map(CreateVoteDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Vote
            {
                VoteID = Guid.NewGuid(),
                UserID = dto.UserID,
                ContributionID = dto.ContributionID
            };
        }
    }
}
