namespace Server.Models.DTOs.Vote.Responses
{
    public class GetVoteDto
    {
        public Guid VoteID { get; set; }
        public Guid UserID { get; set; }
        public Guid ContributionID { get; set; }
    }
}
