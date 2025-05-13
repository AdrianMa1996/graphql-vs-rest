using HotChocolate.Authorization;
using Server.Mapper.ProjectTypes.InputTypes;
using Server.Models.Types.Project.InputTypes;
using Server.Repositories;

namespace Server.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class ProjectMutation
    {
        [Authorize]
        public async Task<string> CreateProjectAsync(CreateProjectType input, [Service] IProjectRepository projectRepository, [Service] ICreateProjectTypeToProjectMapper createProjectTypeToProjectMapper)
        {
            var project = createProjectTypeToProjectMapper.Map(input);
            await projectRepository.CreateProjectAsync(project);
            return "Project created successfully";
        }

        [Authorize]
        public async Task<string> UpdateProjectAsync(UpdateProjectType input, [Service] IProjectRepository projectRepository, [Service] IUpdateProjectTypeToProjectMapper updateProjectTypeToProjectMapper)
        {
            var existingProject = await projectRepository.GetProjectByIdAsync(input.ProjectID);
            var project = updateProjectTypeToProjectMapper.Map(existingProject, input);
            await projectRepository.UpdateProjectAsync(project);
            return "Project updated successfully";
        }

        [Authorize]
        public async Task<string> DeleteProjectByIdAsync(Guid projectId, [Service] IProjectRepository projectRepository)
        {
            await projectRepository.DeleteProjectByIdAsync(projectId);
            return "Project deleted successfully";
        }
    }
}
