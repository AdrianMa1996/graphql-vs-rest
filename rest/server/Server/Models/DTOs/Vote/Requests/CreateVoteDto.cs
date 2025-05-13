namespace Server.Models.DTOs.Vote.Requests
{
    public class CreateVoteDto
    {
        public Guid UserID { get; set; }
        public Guid ContributionID { get; set; }
    }
}
