using HotChocolate.Authorization;
using Server.Mapper.ProjectAndUserBindingTypes.InputTypes;
using Server.Models.Types.ProjectAndUserBinding.InputTypes;
using Server.Repositories;

namespace Server.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class ProjectAndUserBindingMutation
    {
        [Authorize]
        public async Task<string> CreateProjectAndUserBindingAsync(CreateProjectAndUserBindingType input, [Service] IProjectAndUserBindingRepository projectAndUserBindingRepository, [Service] ICreateProjectAndUserBindingTypeToProjectAndUserBindingMapper createProjectAndUserBindingTypeToProjectAndUserBindingMapper)
        {
            var projectAndUserBinding = createProjectAndUserBindingTypeToProjectAndUserBindingMapper.Map(input);
            await projectAndUserBindingRepository.CreateProjectAndUserBindingAsync(projectAndUserBinding);
            return "ProjectAndUserBinding created successfully";
        }

        [Authorize]
        public async Task<string> UpdateProjectAndUserBindingAsync(UpdateProjectAndUserBindingType input, [Service] IProjectAndUserBindingRepository projectAndUserBindingRepository, [Service] IUpdateProjectAndUserBindingTypeToProjectAndUserBindingMapper updateProjectAndUserBindingTypeToProjectAndUserBindingMapper)
        {
            var existingProjectAndUserBinding = await projectAndUserBindingRepository.GetProjectAndUserBindingByIdAsync(input.ProjectAndUserBindingID);
            var projectAndUserBinding = updateProjectAndUserBindingTypeToProjectAndUserBindingMapper.Map(existingProjectAndUserBinding, input);
            await projectAndUserBindingRepository.UpdateProjectAndUserBindingAsync(projectAndUserBinding);
            return "ProjectAndUserBinding updated successfully";
        }

        [Authorize]
        public async Task<string> DeleteProjectAndUserBindingByIdAsync(Guid projectAndUserBindingId, [Service] IProjectAndUserBindingRepository projectAndUserBindingRepository)
        {
            await projectAndUserBindingRepository.DeleteProjectAndUserBindingByIdAsync(projectAndUserBindingId);
            return "ProjectAndUserBinding deleted successfully";
        }
    }
}
