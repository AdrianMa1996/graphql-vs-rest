using Server.Models.Database;
using Server.Models.Types.Comment.InputTypes;

namespace Server.Mapper.CommentTypes.InputTypes
{
    public class UpdateCommentTypeToCommentMapper : IUpdateCommentTypeToCommentMapper
    {
        public Comment Map(Comment existingComment, UpdateCommentType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return new Comment
            {
                CommentID = type.CommentID,
                UserID = type.UserID ?? existingComment.UserID,
                ContributionID = type.ContributionID ?? existingComment.ContributionID,
                Text = type.Text ?? existingComment.Text,
                Date = existingComment.Date,
            };
        }
    }
}
