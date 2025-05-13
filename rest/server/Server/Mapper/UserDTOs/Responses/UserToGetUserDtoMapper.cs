using Server.Models.Database;
using Server.Models.DTOs.User.Responses;

namespace Server.Mapper.UserDTOs.Responses
{
    public class UserToGetUserDtoMapper : IUserToGetUserDtoMapper
    {
        public GetUserDto Map(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return new GetUserDto
            {
                UserID = user.UserID,
                Name = user.Name,
                ProfilPicture = user.ProfilPicture,
                Email = user.Email,
                Role = user.Role,
            };
        }
    }
}
