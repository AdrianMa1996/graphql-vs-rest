using Server.Models.Database;
using Server.Models.Types.ProjectAndUserBinding.InputTypes;

namespace Server.Mapper.ProjectAndUserBindingTypes.InputTypes
{
    public interface IUpdateProjectAndUserBindingTypeToProjectAndUserBindingMapper
    {
        public ProjectAndUserBinding Map(ProjectAndUserBinding existingProjectAndUserBinding, UpdateProjectAndUserBindingType type);
    }
}
