using Server.Models.Database;
using Server.Models.DTOs.User.Responses;

namespace Server.Mapper.UserDTOs.Responses
{
    public class UserToGetUserWithPasswordDtoMapper : IUserToGetUserWithPasswordDtoMapper
    {
        public GetUserWithPasswordDto Map(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return new GetUserWithPasswordDto
            {
                UserID = user.UserID,
                Name = user.Name,
                ProfilPicture = user.ProfilPicture,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
            };
        }
    }
}
