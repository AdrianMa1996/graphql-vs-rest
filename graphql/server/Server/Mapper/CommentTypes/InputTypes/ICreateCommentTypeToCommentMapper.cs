using Server.Models.Database;
using Server.Models.Types.Comment.InputTypes;

namespace Server.Mapper.CommentTypes.InputTypes
{
    public interface ICreateCommentTypeToCommentMapper
    {
        public Comment Map(CreateCommentType type);
    }
}
