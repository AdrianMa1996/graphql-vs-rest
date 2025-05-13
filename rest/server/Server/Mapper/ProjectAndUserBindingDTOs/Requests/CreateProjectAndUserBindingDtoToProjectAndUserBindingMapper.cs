using Server.Models.Database;
using Server.Models.DTOs.ProjectAndUserBinding.Requests;

namespace Server.Mapper.ProjectAndUserBindingDTOs.Requests
{
    public class CreateProjectAndUserBindingDtoToProjectAndUserBindingMapper : ICreateProjectAndUserBindingDtoToProjectAndUserBindingMapper
    {
        public ProjectAndUserBinding Map(CreateProjectAndUserBindingDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new ProjectAndUserBinding
            {
                ProjectAndUserBindingID = Guid.NewGuid(),
                ProjectID = dto.ProjectID,
                UserID = dto.UserID,
            };
        }
    }
}
