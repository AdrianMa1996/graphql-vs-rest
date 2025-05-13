namespace Server.Models.DTOs.Vote.Requests
{
    public class UpdateVoteDto
    {
        public Guid VoteID { get; set; }
        public Guid UserID { get; set; }
        public Guid ContributionID { get; set; }
    }
}
