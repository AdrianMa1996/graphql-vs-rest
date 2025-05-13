using Server.Models.Database;
using Server.Models.Types.Project.OutputTypes;

namespace Server.Mapper.ProjectTypes.OutputTypes
{
    public interface IProjectToGetProjectTypeMapper
    {
        public GetProjectType Map(Project project);
    }
}
