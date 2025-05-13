namespace Server.Models.Types.Comment.InputTypes
{
    public class UpdateCommentType
    {
        public Guid CommentID { get; set; }
        public Guid? UserID { get; set; }
        public Guid? ContributionID { get; set; }
        public string? Text { get; set; }
    }
}
