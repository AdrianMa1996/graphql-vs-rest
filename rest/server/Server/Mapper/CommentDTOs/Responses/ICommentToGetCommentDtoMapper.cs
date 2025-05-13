using Server.Models.Database;
using Server.Models.DTOs.Comment.Responses;

namespace Server.Mapper.CommentDTOs.Responses
{
    public interface ICommentToGetCommentDtoMapper
    {
        public GetCommentDto Map(Comment comment);
    }
}
