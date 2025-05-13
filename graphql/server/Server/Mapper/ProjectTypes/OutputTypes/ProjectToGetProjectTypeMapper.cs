using Server.Models.Database;
using Server.Models.Types.Project.OutputTypes;

namespace Server.Mapper.ProjectTypes.OutputTypes
{
    public class ProjectToGetProjectTypeMapper : IProjectToGetProjectTypeMapper
    {
        public GetProjectType Map(Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            return new GetProjectType
            {
                ProjectID = project.ProjectID,
                Name = project.Name,
                Logo = Convert.ToBase64String(project.Logo)
            };
        }
    }
}
