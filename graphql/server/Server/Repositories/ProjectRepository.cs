using Server.Models.Database;
using SQLite;

namespace Server.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private IProjectAndUserBindingRepository _projectAndUserBindingRepository;
        private IContributionRepository _contributionRepository;

        private SQLiteAsyncConnection _dbConnection;
        private string dbPath = ".\\Resources\\Raw\\database.sqlite";

        public ProjectRepository(IProjectAndUserBindingRepository projectAndUserBindingRepository, IContributionRepository contributionRepository)
        {
            _projectAndUserBindingRepository = projectAndUserBindingRepository;
            _contributionRepository = contributionRepository;

            if (_dbConnection == null)
            {
                _dbConnection = new SQLiteAsyncConnection(dbPath);
            }
        }

        public Task<Project> GetProjectByIdAsync(Guid projectId) => _dbConnection.GetAsync<Project>(projectId);

        public Task<List<Project>> GetProjectsByIdsAsync(List<Guid> projectIds) => _dbConnection.Table<Project>().Where(p => projectIds.Contains(p.ProjectID)).OrderBy(p => p.Name).ToListAsync();

        public Task<List<Project>> GetProjectsAsync() => _dbConnection.Table<Project>().OrderBy(p => p.Name).ToListAsync();

        public Task CreateProjectAsync(Project project) => _dbConnection.InsertAsync(project);

        public Task UpdateProjectAsync(Project project) => _dbConnection.UpdateAsync(project);

        public async Task DeleteProjectByIdAsync(Guid projectId)
        {
            var projectAndUserBindings = await _projectAndUserBindingRepository.GetProjectAndUserBindingsByProjectIdAsync(projectId);
            foreach (var projectAndUserBinding in projectAndUserBindings)
            {
                await _projectAndUserBindingRepository.DeleteProjectAndUserBindingByIdAsync(projectAndUserBinding.ProjectAndUserBindingID);
            }

            var contributions = await _contributionRepository.GetContributionsByProjectIdAsync(projectId);
            foreach (var contribution in contributions)
            {
                await _contributionRepository.DeleteContributionByIdAsync(contribution.ContributionID);
            }

            _ = _dbConnection.DeleteAsync<Project>(projectId);
        }
    }
}
