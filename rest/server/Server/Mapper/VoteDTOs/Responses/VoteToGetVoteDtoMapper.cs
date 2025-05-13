using Server.Models.Database;
using Server.Models.DTOs.Vote.Responses;

namespace Server.Mapper.VoteDTOs.Responses
{
    public class VoteToGetVoteDtoMapper : IVoteToGetVoteDtoMapper
    {
        public GetVoteDto Map(Vote vote)
        {
            if (vote == null)
                throw new ArgumentNullException(nameof(vote));

            return new GetVoteDto
            {
                VoteID = vote.VoteID,
                UserID = vote.UserID,
                ContributionID = vote.ContributionID
            };
        }
    }
}
