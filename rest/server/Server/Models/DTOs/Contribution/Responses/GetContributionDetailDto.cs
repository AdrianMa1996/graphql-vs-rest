namespace Server.Models.DTOs.Contribution.Responses
{
    public class GetContributionDetailDto
    {
        public Guid ProjectID { get; set; }
        public Guid UserID { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public CreatorDetailDto Creator { get; set; }
        public List<VoteDetailDto> Votes { get; set; } = new();
        public List<CommentDetailDto> Comments { get; set; } = new();
    }

    public class CreatorDetailDto
    {
        public string Name { get; set; }
        public byte[] ProfilPicture { get; set; }
    }

    public class VoteDetailDto
    {
        public Guid VoteID { get; set; }
        public Guid UserID { get; set; }
    }

    public class CommentDetailDto
    {
        public string Text { get; set; }
        public string Date { get; set; }
        public CreatorDetailDto Creator { get; set; }
    }
}
