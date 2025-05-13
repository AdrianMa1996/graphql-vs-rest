using Server.Models.Database;
using Server.Models.DTOs.Comment.Requests;

namespace Server.Mapper.CommentDTOs.Requests
{
    public interface IUpdateCommentDtoToCommentMapper
    {
        public Comment Map(UpdateCommentDto dto);
    }
}
