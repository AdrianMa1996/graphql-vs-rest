using Server.Models.Database;
using Server.Models.Types.ProjectAndUserBinding.InputTypes;

namespace Server.Mapper.ProjectAndUserBindingTypes.InputTypes
{
    public class CreateProjectAndUserBindingTypeToProjectAndUserBindingMapper : ICreateProjectAndUserBindingTypeToProjectAndUserBindingMapper
    {
        public ProjectAndUserBinding Map(CreateProjectAndUserBindingType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return new ProjectAndUserBinding
            {
                ProjectAndUserBindingID = Guid.NewGuid(),
                ProjectID = type.ProjectID,
                UserID = type.UserID,
            };
        }
    }
}
