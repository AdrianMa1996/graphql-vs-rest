using Server.Models.Database;
using Server.Models.DTOs.Comment.Responses;

namespace Server.Mapper.CommentDTOs.Responses
{
    public class CommentToGetCommentDtoMapper : ICommentToGetCommentDtoMapper
    {
        public GetCommentDto Map(Comment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            return new GetCommentDto
            {
                CommentID = comment.CommentID,
                UserID = comment.UserID,
                ContributionID = comment.ContributionID,
                Text = comment.Text,
                Date = comment.Date
            };
        }
    }
}
