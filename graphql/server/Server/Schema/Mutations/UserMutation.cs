using HotChocolate.Authorization;
using Server.Mapper.UserTypes.InputTypes;
using Server.Models.Types.User.InputTypes;
using Server.Repositories;
using System.Security.Claims;

namespace Server.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class UserMutation
    {
        [Authorize]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<string> CreateUserAsync(CreateUserType input, [Service] IUserRepository userRepository, [Service] ICreateUserTypeToUserMapper createUserTypeToUserMapper)
        {
            var user = createUserTypeToUserMapper.Map(input);
            await userRepository.CreateUserAsync(user);
            return "User created successfully";
        }

        [Authorize]
        public async Task<string> UpdateUserAsync(UpdateUserType input, [Service] IUserRepository userRepository, [Service] IUpdateUserTypeToUserMapper updateUserTypeToUserMapper, ClaimsPrincipal currentUser)
        {
            if (!currentUser.HasClaim("Role", "Admin") && !currentUser.HasClaim("CanEditUser", input.UserID.ToString()))
            {
                throw new GraphQLException("You are not authorized to update this user.");
            }
            if (!currentUser.HasClaim("Role", "Admin") && input.Role != null)
            {
                throw new GraphQLException("Only admins are allowed to change the role.");
            }
            var existingUser = await userRepository.GetUserByIdAsync(input.UserID);
            var user = updateUserTypeToUserMapper.Map(existingUser, input);
            await userRepository.UpdateUserAsync(user);
            return "User updated successfully";
        }

        [Authorize]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<string> DeleteUserByIdAsync(Guid userId, [Service] IUserRepository userRepository)
        {
            await userRepository.DeleteUserByIdAsync(userId);
            return "User deleted successfully";
        }
    }
}
