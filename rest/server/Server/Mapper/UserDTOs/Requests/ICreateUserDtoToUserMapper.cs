using Server.Models.Database;
using Server.Models.DTOs.User.Requests;

namespace Server.Mapper.UserDTOs.Requests
{
    public interface ICreateUserDtoToUserMapper
    {
        public User Map(CreateUserDto dto);
    }
}
