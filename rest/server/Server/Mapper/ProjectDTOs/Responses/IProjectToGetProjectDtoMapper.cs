using Server.Models.Database;
using Server.Models.DTOs.Project.Responses;

namespace Server.Mapper.ProjectDTOs.Responses
{
    public interface IProjectToGetProjectDtoMapper
    {
        public GetProjectDto Map(Project project);
    }
}
