using Server.Models.Database;
using Server.Models.Types.User.InputTypes;

namespace Server.Mapper.UserTypes.InputTypes
{
    public interface IUpdateUserTypeToUserMapper
    {
        public User Map(User existingUser, UpdateUserType type);
    }
}
