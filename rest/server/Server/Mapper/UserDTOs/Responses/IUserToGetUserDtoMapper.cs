using Server.Models.Database;
using Server.Models.DTOs.User.Responses;

namespace Server.Mapper.UserDTOs.Responses
{
    public interface IUserToGetUserDtoMapper
    {
        public GetUserDto Map(User user);
    }
}
