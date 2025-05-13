using Server.Models.Database;
using Server.Models.Types.ProjectAndUserBinding.InputTypes;

namespace Server.Mapper.ProjectAndUserBindingTypes.InputTypes
{
    public class UpdateProjectAndUserBindingTypeToProjectAndUserBindingMapper : IUpdateProjectAndUserBindingTypeToProjectAndUserBindingMapper
    {
        public ProjectAndUserBinding Map(ProjectAndUserBinding existingProjectAndUserBinding, UpdateProjectAndUserBindingType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return new ProjectAndUserBinding
            {
                ProjectAndUserBindingID = type.ProjectAndUserBindingID,
                ProjectID = type.ProjectID ?? existingProjectAndUserBinding.ProjectID,
                UserID = type.UserID ?? existingProjectAndUserBinding.UserID,
            };
        }
    }
}
