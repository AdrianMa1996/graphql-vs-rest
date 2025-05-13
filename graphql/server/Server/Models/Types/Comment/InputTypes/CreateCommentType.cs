namespace Server.Models.Types.Comment.InputTypes
{
    public class CreateCommentType
    {
        public Guid UserID { get; set; }
        public Guid ContributionID { get; set; }
        public string Text { get; set; }
    }
}
