using Server.Models.DTOs.Project.Requests;
using Server.Models.Database;

namespace Server.Mapper.ProjectDTOs.Requests
{
    public class CreateProjectDtoToProjectMapper : ICreateProjectDtoToProjectMapper
    {
        public Project Map(CreateProjectDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Project
            {
                ProjectID = Guid.NewGuid(),
                Name = dto.Name,
                Logo = dto.Logo
            };
        }
    }
}
