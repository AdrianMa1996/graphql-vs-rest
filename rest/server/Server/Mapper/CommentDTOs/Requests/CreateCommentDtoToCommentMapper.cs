using Server.Models.Database;
using Server.Models.DTOs.Comment.Requests;

namespace Server.Mapper.CommentDTOs.Requests
{
    public class CreateCommentDtoToCommentMapper : ICreateCommentDtoToCommentMapper
    {
        public Comment Map(CreateCommentDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Comment
            {
                CommentID = Guid.NewGuid(),
                UserID = dto.UserID,
                ContributionID = dto.ContributionID,
                Text = dto.Text,
                Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };
        }
    }
}
