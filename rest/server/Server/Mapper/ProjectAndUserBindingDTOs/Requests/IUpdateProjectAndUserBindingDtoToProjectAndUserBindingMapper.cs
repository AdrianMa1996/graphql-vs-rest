using Server.Models.Database;
using Server.Models.DTOs.ProjectAndUserBinding.Requests;

namespace Server.Mapper.ProjectAndUserBindingDTOs.Requests
{
    public interface IUpdateProjectAndUserBindingDtoToProjectAndUserBindingMapper
    {
        public ProjectAndUserBinding Map(UpdateProjectAndUserBindingDto dto);
    }
}
