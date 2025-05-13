namespace Server.Models.DTOs.Contribution.Requests
{
    public class CreateContributionDto
    {
        public Guid UserID { get; set; }
        public Guid ProjectID { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
