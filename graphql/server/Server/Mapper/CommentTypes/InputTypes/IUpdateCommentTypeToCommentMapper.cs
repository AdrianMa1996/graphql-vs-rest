using Server.Models.Database;
using Server.Models.Types.Comment.InputTypes;

namespace Server.Mapper.CommentTypes.InputTypes
{
    public interface IUpdateCommentTypeToCommentMapper
    {
        public Comment Map(Comment existingComment, UpdateCommentType type);
    }
}
