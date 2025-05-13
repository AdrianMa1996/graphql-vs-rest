namespace Server.Models.DTOs.Comment.Requests
{
    public class CreateCommentDto
    {
        public Guid UserID { get; set; }
        public Guid ContributionID { get; set; }
        public string Text { get; set; }
    }
}
