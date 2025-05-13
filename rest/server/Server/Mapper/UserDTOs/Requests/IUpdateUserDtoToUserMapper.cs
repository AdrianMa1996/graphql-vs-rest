using Server.Models.Database;
using Server.Models.DTOs.User.Requests;

namespace Server.Mapper.UserDTOs.Requests
{
    public interface IUpdateUserDtoToUserMapper
    {
        public User Map(UpdateUserDto dto);
    }
}
