namespace Server.Models.Types.Vote.InputTypes
{
    public class UpdateVoteType
    {
        public Guid VoteID { get; set; }
        public Guid? UserID { get; set; }
        public Guid? ContributionID { get; set; }
    }
}
