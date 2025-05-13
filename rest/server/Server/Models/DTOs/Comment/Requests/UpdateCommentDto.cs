namespace Server.Models.DTOs.Comment.Requests
{
    public class UpdateCommentDto
    {
        public Guid CommentID { get; set; }
        public Guid UserID { get; set; }
        public Guid ContributionID { get; set; }
        public string Text { get; set; }
    }
}
