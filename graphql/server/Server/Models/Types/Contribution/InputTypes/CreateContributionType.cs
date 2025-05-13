namespace Server.Models.Types.Contribution.InputTypes
{
    public class CreateContributionType
    {
        public Guid UserID { get; set; }
        public Guid ProjectID { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
