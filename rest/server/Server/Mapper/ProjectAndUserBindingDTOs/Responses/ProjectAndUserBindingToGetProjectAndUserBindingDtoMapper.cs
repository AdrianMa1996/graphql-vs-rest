using Server.Models.Database;
using Server.Models.DTOs.ProjectAndUserBinding.Responses;

namespace Server.Mapper.ProjectAndUserBindingDTOs.Responses
{
    public class ProjectAndUserBindingToGetProjectAndUserBindingDtoMapper : IProjectAndUserBindingToGetProjectAndUserBindingDtoMapper
    {
        public GetProjectAndUserBindingDto Map(ProjectAndUserBinding projectAndUserBinding)
        {
            if (projectAndUserBinding == null)
                throw new ArgumentNullException(nameof(projectAndUserBinding));

            return new GetProjectAndUserBindingDto
            {
                ProjectAndUserBindingID = projectAndUserBinding.ProjectAndUserBindingID,
                ProjectID = projectAndUserBinding.ProjectID,
                UserID = projectAndUserBinding.UserID,
            };
        }
    }
}
