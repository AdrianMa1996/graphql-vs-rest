using Server.Models.Database;

namespace Server.Repositories
{
    public interface ICommentRepository
    {
        public Task<Comment> GetCommentByIdAsync(Guid commentId);

        public Task<List<Comment>> GetCommentsAsync();

        public Task CreateCommentAsync(Comment comment);

        public Task UpdateCommentAsync(Comment comment);

        public Task DeleteCommentByIdAsync(Guid commentId);

        public Task<List<Comment>> GetCommentsByUserIdAsync(Guid userId);

        public Task<List<Comment>> GetCommentsByContributionIdAsync(Guid contributionId);
    }
}
