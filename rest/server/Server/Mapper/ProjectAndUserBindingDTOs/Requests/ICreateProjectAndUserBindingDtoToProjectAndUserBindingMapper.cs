using Server.Models.Database;
using Server.Models.DTOs.ProjectAndUserBinding.Requests;

namespace Server.Mapper.ProjectAndUserBindingDTOs.Requests
{
    public interface ICreateProjectAndUserBindingDtoToProjectAndUserBindingMapper
    {
        public ProjectAndUserBinding Map(CreateProjectAndUserBindingDto dto);
    }
}
