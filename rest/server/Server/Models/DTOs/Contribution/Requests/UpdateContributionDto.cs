namespace Server.Models.DTOs.Contribution.Requests
{
    public class UpdateContributionDto
    {
        public Guid ContributionID { get; set; }
        public Guid ProjectID { get; set; }
        public Guid UserID { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
    }
}
