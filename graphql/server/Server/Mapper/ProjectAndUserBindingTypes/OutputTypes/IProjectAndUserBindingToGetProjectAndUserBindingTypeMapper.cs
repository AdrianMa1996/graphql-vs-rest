using Server.Models.Database;
using Server.Models.Types.ProjectAndUserBinding.OutputTypes;

namespace Server.Mapper.ProjectAndUserBindingTypes.OutputTypes
{
    public interface IProjectAndUserBindingToGetProjectAndUserBindingTypeMapper
    {
        public GetProjectAndUserBindingType Map(ProjectAndUserBinding projectAndUserBinding);
    }
}
