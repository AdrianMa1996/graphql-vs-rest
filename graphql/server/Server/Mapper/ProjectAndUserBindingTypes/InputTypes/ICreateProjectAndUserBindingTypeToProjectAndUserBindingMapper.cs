using Server.Models.Database;
using Server.Models.Types.ProjectAndUserBinding.InputTypes;

namespace Server.Mapper.ProjectAndUserBindingTypes.InputTypes
{
    public interface ICreateProjectAndUserBindingTypeToProjectAndUserBindingMapper
    {
        public ProjectAndUserBinding Map(CreateProjectAndUserBindingType type);
    }
}
