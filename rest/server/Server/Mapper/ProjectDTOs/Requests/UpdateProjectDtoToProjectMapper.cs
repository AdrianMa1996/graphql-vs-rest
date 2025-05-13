using Server.Models.Database;
using Server.Models.DTOs.Project.Requests;

namespace Server.Mapper.ProjectDTOs.Requests
{
    public class UpdateProjectDtoToProjectMapper : IUpdateProjectDtoToProjectMapper
    {
        public Project Map(UpdateProjectDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Project
            {
                ProjectID = dto.ProjectID,
                Name = dto.Name,
                Logo = dto.Logo
            };
        }
    }
}
