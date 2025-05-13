using Server.Models.Database;

namespace Server.Repositories
{
    public interface IProjectAndUserBindingRepository
    {
        public Task<ProjectAndUserBinding> GetProjectAndUserBindingByIdAsync(Guid projectId);

        public Task<List<ProjectAndUserBinding>> GetProjectAndUserBindingsAsync();

        public Task CreateProjectAndUserBindingAsync(ProjectAndUserBinding project);

        public Task UpdateProjectAndUserBindingAsync(ProjectAndUserBinding project);

        public Task DeleteProjectAndUserBindingByIdAsync(Guid projectId);

        public Task<List<ProjectAndUserBinding>> GetProjectAndUserBindingsByUserIdAsync(Guid userId);

        public Task<List<ProjectAndUserBinding>> GetProjectAndUserBindingsByProjectIdAsync(Guid projectId);
    }
}
