namespace Server.Models.Types.Vote.InputTypes
{
    public class CreateVoteType
    {
        public Guid UserID { get; set; }
        public Guid ContributionID { get; set; }
    }
}
