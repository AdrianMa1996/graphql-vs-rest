using Server.Models.Database;
using Server.Models.DTOs.Comment.Requests;

namespace Server.Mapper.CommentDTOs.Requests
{
    public class UpdateCommentDtoToCommentMapper : IUpdateCommentDtoToCommentMapper
    {
        public Comment Map(UpdateCommentDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Comment
            {
                CommentID = dto.CommentID,
                UserID = dto.UserID,
                ContributionID = dto.ContributionID,
                Text = dto.Text,
                Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };
        }
    }
}
