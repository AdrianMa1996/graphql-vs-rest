using Server.Models.Database;
using Server.Models.DTOs.ProjectAndUserBinding.Responses;

namespace Server.Mapper.ProjectAndUserBindingDTOs.Responses
{
    public interface IProjectAndUserBindingToGetProjectAndUserBindingDtoMapper
    {
        public GetProjectAndUserBindingDto Map(ProjectAndUserBinding projectAndUserBinding);
    }
}
