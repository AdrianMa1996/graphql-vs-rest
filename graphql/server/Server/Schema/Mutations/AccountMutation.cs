using Server.Models.Types.Account.InputTypes;
using Server.Models.Types.Account.OutputTypes;
using Server.Repositories;
using Server.Services;

namespace Server.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class AccountMutation
    {
        public async Task<GetTokenType> LoginAsync(LoginType input, [Service] IUserRepository userRepository, [Service] ITokenService tokenService)
        {
            var user = await userRepository.GetUserByNameAsync(input.Name);
            if (user.Password != input.Password)
            {
                throw new GraphQLException("Invalid password");
            }
            var token = await tokenService.CreateToken(user);
            return new GetTokenType
            {
                Token = token
            };
        }
    }
}
