using Server.Models.Database;
using Server.Models.Types.User.InputTypes;

namespace Server.Mapper.UserTypes.InputTypes
{
    public interface ICreateUserTypeToUserMapper
    {
        public User Map(CreateUserType type);
    }
}
