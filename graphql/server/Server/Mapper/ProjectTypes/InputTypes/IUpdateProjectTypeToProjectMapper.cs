using Server.Models.Database;
using Server.Models.Types.Project.InputTypes;

namespace Server.Mapper.ProjectTypes.InputTypes
{
    public interface IUpdateProjectTypeToProjectMapper
    {
        public Project Map(Project existingProject, UpdateProjectType type);
    }
}
