using Server.Models.Database;
using Server.Models.DTOs.User.Responses;

namespace Server.Mapper.UserDTOs.Responses
{
    public interface IUserToGetUserWithPasswordDtoMapper
    {
        public GetUserWithPasswordDto Map(User user);
    }
}
