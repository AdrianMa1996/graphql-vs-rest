using Server.Models.DTOs.Project.Requests;
using Server.Models.Database;

namespace Server.Mapper.ProjectDTOs.Requests
{
    public interface ICreateProjectDtoToProjectMapper
    {
        public Project Map(CreateProjectDto dto);
    }
}
