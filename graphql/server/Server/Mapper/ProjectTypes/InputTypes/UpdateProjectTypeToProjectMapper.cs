using Server.Models.Database;
using Server.Models.Types.Project.InputTypes;

namespace Server.Mapper.ProjectTypes.InputTypes
{
    public class UpdateProjectTypeToProjectMapper : IUpdateProjectTypeToProjectMapper
    {
        public Project Map(Project existingProject, UpdateProjectType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return new Project
            {
                ProjectID = type.ProjectID,
                Name = type.Name ?? existingProject.Name,
                Logo = type.Logo == null ? existingProject.Logo : Convert.FromBase64String(type.Logo),
            };
        }
    }
}
