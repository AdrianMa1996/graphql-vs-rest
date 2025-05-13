using HotChocolate.Authorization;
using Server.Mapper.CommentTypes.InputTypes;
using Server.Models.Types.Comment.InputTypes;
using Server.Repositories;

namespace Server.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class CommentMutation
    {
        [Authorize]
        public async Task<string> CreateCommentAsync(CreateCommentType input, [Service] ICommentRepository commentRepository, [Service] ICreateCommentTypeToCommentMapper createCommentTypeToCommentMapper)
        {
            var comment = createCommentTypeToCommentMapper.Map(input);
            await commentRepository.CreateCommentAsync(comment);
            return "Comment created successfully";
        }

        [Authorize]
        public async Task<string> UpdateCommentAsync(UpdateCommentType input, [Service] ICommentRepository commentRepository, [Service] IUpdateCommentTypeToCommentMapper updateCommentTypeToCommentMapper)
        {
            var existingComment = await commentRepository.GetCommentByIdAsync(input.CommentID);
            var comment = updateCommentTypeToCommentMapper.Map(existingComment, input);
            await commentRepository.UpdateCommentAsync(comment);
            return "Comment updated successfully";
        }

        [Authorize]
        public async Task<string> DeleteCommentByIdAsync(Guid commentId, [Service] ICommentRepository commentRepository)
        {
            await commentRepository.DeleteCommentByIdAsync(commentId);
            return "Comment deleted successfully";
        }
    }
}
