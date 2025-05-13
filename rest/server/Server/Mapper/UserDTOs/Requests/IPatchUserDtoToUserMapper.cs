using Server.Models.Database;
using Server.Models.DTOs.User.Requests;

namespace Server.Mapper.UserDTOs.Requests
{
    public interface IPatchUserDtoToUserMapper
    {
        public User Map(User existingUser, PatchUserDto dto);
    }
}
