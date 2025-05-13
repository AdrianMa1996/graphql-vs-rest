using Server.Models.Database;
using Server.Models.Types.ProjectAndUserBinding.OutputTypes;

namespace Server.Mapper.ProjectAndUserBindingTypes.OutputTypes
{
    public class ProjectAndUserBindingToGetProjectAndUserBindingTypeMapper : IProjectAndUserBindingToGetProjectAndUserBindingTypeMapper
    {
        public GetProjectAndUserBindingType Map(ProjectAndUserBinding projectAndUserBinding)
        {
            if (projectAndUserBinding == null)
                throw new ArgumentNullException(nameof(projectAndUserBinding));

            return new GetProjectAndUserBindingType
            {
                ProjectAndUserBindingID = projectAndUserBinding.ProjectAndUserBindingID,
                ProjectID = projectAndUserBinding.ProjectID,
                UserID = projectAndUserBinding.UserID
            };
        }
    }
}
