using Server.Models.Database;
using Server.Models.Types.Comment.InputTypes;

namespace Server.Mapper.CommentTypes.InputTypes
{
    public class CreateCommentTypeToCommentMapper : ICreateCommentTypeToCommentMapper
    {
        public Comment Map(CreateCommentType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return new Comment
            {
                CommentID = Guid.NewGuid(),
                UserID = type.UserID,
                ContributionID = type.ContributionID,
                Text = type.Text,
                Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };
        }
    }
}
