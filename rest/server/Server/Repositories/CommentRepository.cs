using Server.Models.Database;
using SQLite;

namespace Server.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private SQLiteAsyncConnection _dbConnection;
        private string dbPath = ".\\Resources\\Raw\\database.sqlite";

        public CommentRepository()
        {
            if (_dbConnection == null)
            {
                _dbConnection = new SQLiteAsyncConnection(dbPath);
            }
        }

        public Task<Comment> GetCommentByIdAsync(Guid commentId) => _dbConnection.GetAsync<Comment>(commentId);

        public Task<List<Comment>> GetCommentsAsync() => _dbConnection.Table<Comment>().OrderBy(p => p.Date).ToListAsync();

        public Task CreateCommentAsync(Comment comment) => _dbConnection.InsertAsync(comment);

        public Task UpdateCommentAsync(Comment comment) => _dbConnection.UpdateAsync(comment);

        public Task DeleteCommentByIdAsync(Guid commentId) => _dbConnection.DeleteAsync<Comment>(commentId);

        public Task<List<Comment>> GetCommentsByUserIdAsync(Guid userId) => _dbConnection.Table<Comment>().Where(x => x.UserID == userId).OrderBy(p => p.Date).ToListAsync();

        public Task<List<Comment>> GetCommentsByContributionIdAsync(Guid contributionId) => _dbConnection.Table<Comment>().Where(x => x.ContributionID == contributionId).OrderBy(p => p.Date).ToListAsync();
    }
}
