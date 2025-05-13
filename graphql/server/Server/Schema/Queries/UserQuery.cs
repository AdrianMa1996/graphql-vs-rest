using Server.Mapper.UserTypes.OutputTypes;
using Server.Models.Types.User.OutputTypes;
using Server.Repositories;

namespace Server.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class UserQuery
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserToGetUserTypeMapper _userToGetUserTypeMapper;

        public UserQuery(IUserRepository userRepository, IUserToGetUserTypeMapper userToGetUserTypeMapper)
        {
            _userRepository = userRepository;
            _userToGetUserTypeMapper = userToGetUserTypeMapper;
        }

        public async Task<IEnumerable<GetUserType>> GetUsers()
        {
            try
            {
                var userList = await _userRepository.GetUsersAsync();
                var getUserList = userList.Select(_userToGetUserTypeMapper.Map).ToList();
                return getUserList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetUserType> GetUserById(Guid userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                return _userToGetUserTypeMapper.Map(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
