using Server.Models.Database;
using Server.Models.Types.Comment.OutputTypes;

namespace Server.Mapper.CommentTypes.OutputTypes
{
    public class CommentToGetCommentTypeMapper : ICommentToGetCommentTypeMapper
    {
        public GetCommentType Map(Comment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            return new GetCommentType
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
