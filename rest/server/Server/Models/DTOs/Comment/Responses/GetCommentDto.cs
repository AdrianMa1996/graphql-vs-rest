namespace Server.Models.DTOs.Comment.Responses
{
    public class GetCommentDto
    {
        public Guid CommentID { get; set; }
        public Guid UserID { get; set; }
        public Guid ContributionID { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
    }
}
