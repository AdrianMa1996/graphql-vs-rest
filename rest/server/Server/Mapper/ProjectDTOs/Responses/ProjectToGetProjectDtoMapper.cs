using Server.Models.Database;
using Server.Models.DTOs.Project.Responses;

namespace Server.Mapper.ProjectDTOs.Responses
{
    public class ProjectToGetProjectDtoMapper : IProjectToGetProjectDtoMapper
    {
        public GetProjectDto Map(Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            return new GetProjectDto
            {
                ProjectID = project.ProjectID,
                Name = project.Name,
                Logo = project.Logo
            };
        }
    }
}
