using Server.Models.Database;
using SQLite;

namespace Server.Repositories
{
    public class ProjectAndUserBindingRepository: IProjectAndUserBindingRepository
    {
        private SQLiteAsyncConnection _dbConnection;
        private string dbPath = ".\\Resources\\Raw\\database.sqlite";

        public ProjectAndUserBindingRepository()
        {
            if (_dbConnection == null)
            {
                _dbConnection = new SQLiteAsyncConnection(dbPath);
            }
        }

        public Task<ProjectAndUserBinding> GetProjectAndUserBindingByIdAsync(Guid projectAndUserBindingId) => _dbConnection.GetAsync<ProjectAndUserBinding>(projectAndUserBindingId);

        public Task<List<ProjectAndUserBinding>> GetProjectAndUserBindingsAsync() => _dbConnection.Table<ProjectAndUserBinding>().ToListAsync();

        public Task CreateProjectAndUserBindingAsync(ProjectAndUserBinding projectAndUserBinding) => _dbConnection.InsertAsync(projectAndUserBinding);

        public Task UpdateProjectAndUserBindingAsync(ProjectAndUserBinding projectAndUserBinding) => _dbConnection.UpdateAsync(projectAndUserBinding);

        public Task DeleteProjectAndUserBindingByIdAsync(Guid projectAndUserBindingId) => _dbConnection.DeleteAsync<ProjectAndUserBinding>(projectAndUserBindingId);

        public Task<List<ProjectAndUserBinding>> GetProjectAndUserBindingsByUserIdAsync(Guid userId) => _dbConnection.Table<ProjectAndUserBinding>().Where(x => x.UserID == userId).ToListAsync();

        public Task<List<ProjectAndUserBinding>> GetProjectAndUserBindingsByProjectIdAsync(Guid projectId) => _dbConnection.Table<ProjectAndUserBinding>().Where(x => x.ProjectID == projectId).ToListAsync();
    }
}
