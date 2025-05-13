namespace Server.Models.DTOs.Contribution.Responses
{
    public class GetContributionOverviewDto
    {
        public Guid ContributionID { get; set; }
        public Guid ProjectID { get; set; }
        public Guid UserID { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public List<VoteDto> Votes { get; set; } = new();
        public List<CommentDto> Comments { get; set; } = new();
    }

    public class VoteDto
    {
        public Guid VoteID { get; set; }
        public Guid UserID { get; set; }
    }

    public class CommentDto
    {
        public Guid CommentID { get; set; }
    }
}
