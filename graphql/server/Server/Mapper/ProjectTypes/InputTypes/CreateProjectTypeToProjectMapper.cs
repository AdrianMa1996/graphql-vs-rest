using Server.Models.Database;
using Server.Models.Types.Project.InputTypes;

namespace Server.Mapper.ProjectTypes.InputTypes
{
    public class CreateProjectTypeToProjectMapper : ICreateProjectTypeToProjectMapper
    {
        public Project Map(CreateProjectType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return new Project
            {
                ProjectID = Guid.NewGuid(),
                Name = type.Name,
                Logo = Convert.FromBase64String(type.Logo)
            };
        }
    }
}
