using SQLite;

namespace Server.Models.Database
{
    public class Vote
    {
        [PrimaryKey]
        public Guid VoteID { get; set; }
        public Guid UserID { get; set; }
        public Guid ContributionID { get; set; }
    }
}
