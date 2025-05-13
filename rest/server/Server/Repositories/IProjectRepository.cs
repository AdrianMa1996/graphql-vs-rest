using Server.Models.Database;

namespace Server.Repositories
{
    public interface IProjectRepository
    {
        public Task<Project> GetProjectByIdAsync(Guid projectId);

        public Task<List<Project>> GetProjectsByIdsAsync(List<Guid> projectIds);

        public Task<List<Project>> GetProjectsAsync();

        public Task CreateProjectAsync(Project project);

        public Task UpdateProjectAsync(Project project);

        public Task DeleteProjectByIdAsync(Guid projectId);
    }
}
