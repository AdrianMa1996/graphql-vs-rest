using Server.Models.Database;
using Server.Models.Types.Project.InputTypes;

namespace Server.Mapper.ProjectTypes.InputTypes
{
    public interface ICreateProjectTypeToProjectMapper
    {
        public Project Map(CreateProjectType type);
    }
}
