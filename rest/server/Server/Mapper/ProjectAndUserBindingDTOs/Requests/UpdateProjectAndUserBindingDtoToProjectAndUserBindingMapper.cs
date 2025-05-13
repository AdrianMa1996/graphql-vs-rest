using Server.Models.Database;
using Server.Models.DTOs.ProjectAndUserBinding.Requests;

namespace Server.Mapper.ProjectAndUserBindingDTOs.Requests
{
    public class UpdateProjectAndUserBindingDtoToProjectAndUserBindingMapper : IUpdateProjectAndUserBindingDtoToProjectAndUserBindingMapper
    {
        public ProjectAndUserBinding Map(UpdateProjectAndUserBindingDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new ProjectAndUserBinding
            {
                ProjectAndUserBindingID = dto.ProjectAndUserBindingID,
                ProjectID = dto.ProjectID,
                UserID = dto.UserID,
            };
        }
    }
}
