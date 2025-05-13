using Server.Mapper.ProjectTypes.OutputTypes;
using Server.Models.Types.Project.OutputTypes;
using Server.Repositories;

namespace Server.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class ProjectQuery
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectToGetProjectTypeMapper _projectToGetProjectTypeMapper;

        public ProjectQuery(IProjectRepository projectRepository, IProjectToGetProjectTypeMapper projectToGetProjectTypeMapper)
        {
            _projectRepository = projectRepository;
            _projectToGetProjectTypeMapper = projectToGetProjectTypeMapper;
        }

        public async Task<IEnumerable<GetProjectType>> GetProjects()
        {
            try
            {
                var projectList = await _projectRepository.GetProjectsAsync();
                var getProjectList = projectList.Select(_projectToGetProjectTypeMapper.Map).ToList();
                return getProjectList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetProjectType> GetProjectById(Guid projectId)
        {
            try
            {
                var project = await _projectRepository.GetProjectByIdAsync(projectId);
                var getProject = _projectToGetProjectTypeMapper.Map(project);
                return getProject;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
