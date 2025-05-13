using HotChocolate.Authorization;
using Server.Mapper.ProjectTypes.OutputTypes;
using Server.Mapper.UserTypes.OutputTypes;
using Server.Models.Types.Project.OutputTypes;
using Server.Models.Types.User.OutputTypes;
using Server.Repositories;

namespace Server.Models.Types.ProjectAndUserBinding.OutputTypes
{
    public class GetProjectAndUserBindingType
    {
        [Authorize]
        public Guid ProjectAndUserBindingID { get; set; }

        [Authorize]
        public Guid ProjectID { get; set; }

        [Authorize]
        public Guid UserID { get; set; }

        [Authorize]
        [GraphQLNonNullType]
        public async Task<GetProjectType> ProjectAsync([Service] IProjectRepository projectRepository, [Service] IProjectToGetProjectTypeMapper projectToGetProjectTypeMapper)
        {
            var project = await projectRepository.GetProjectByIdAsync(ProjectID);
            var getProject = projectToGetProjectTypeMapper.Map(project);
            return getProject;
        }

        [Authorize]
        [GraphQLNonNullType]
        public async Task<GetUserType> UserAsync([Service] IUserRepository userRepository, [Service] IUserToGetUserTypeMapper userToGetUserTypeMapper)
        {
            var user = await userRepository.GetUserByIdAsync(UserID);
            var getUser = userToGetUserTypeMapper.Map(user);
            return getUser;
        }
    }
}
