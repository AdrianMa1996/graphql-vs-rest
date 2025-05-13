using Server.Models.Database;
using Server.Models.DTOs.Project.Requests;

namespace Server.Mapper.ProjectDTOs.Requests
{
    public interface IUpdateProjectDtoToProjectMapper
    {
        public Project Map(UpdateProjectDto dto);
    }
}
