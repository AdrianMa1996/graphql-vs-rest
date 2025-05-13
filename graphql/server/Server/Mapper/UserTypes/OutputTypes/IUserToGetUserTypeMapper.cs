using Server.Models.Database;
using Server.Models.Types.User.OutputTypes;

namespace Server.Mapper.UserTypes.OutputTypes
{
    public interface IUserToGetUserTypeMapper
    {
        public GetUserType Map(User user);
    }
}
