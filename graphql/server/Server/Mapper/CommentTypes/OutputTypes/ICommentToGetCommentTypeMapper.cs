using Server.Models.Database;
using Server.Models.Types.Comment.OutputTypes;

namespace Server.Mapper.CommentTypes.OutputTypes
{
    public interface ICommentToGetCommentTypeMapper
    {
        public GetCommentType Map(Comment comment);
    }
}
