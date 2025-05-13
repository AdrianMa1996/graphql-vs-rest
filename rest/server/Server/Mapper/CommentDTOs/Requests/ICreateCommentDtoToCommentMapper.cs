using Server.Models.Database;
using Server.Models.DTOs.Comment.Requests;

namespace Server.Mapper.CommentDTOs.Requests
{
    public interface ICreateCommentDtoToCommentMapper
    {
        public Comment Map(CreateCommentDto dto);
    }
}
