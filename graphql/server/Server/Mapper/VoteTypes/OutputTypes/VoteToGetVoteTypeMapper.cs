using Server.Models.Database;
using Server.Models.Types.Vote.OutputTypes;

namespace Server.Mapper.VoteTypes.OutputTypes
{
    public class VoteToGetVoteTypeMapper : IVoteToGetVoteTypeMapper
    {
        public GetVoteType Map(Vote vote)
        {
            if (vote == null)
                throw new ArgumentNullException(nameof(vote));

            return new GetVoteType
            {
                VoteID = vote.VoteID,
                UserID = vote.UserID,
                ContributionID = vote.ContributionID
            };
        }
    }
}
