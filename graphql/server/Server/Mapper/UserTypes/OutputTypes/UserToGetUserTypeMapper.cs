using Server.Models.Database;
using Server.Models.Types.User.OutputTypes;

namespace Server.Mapper.UserTypes.OutputTypes
{
    public class UserToGetUserTypeMapper : IUserToGetUserTypeMapper
    {
        public GetUserType Map(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return new GetUserType
            {
                UserID = user.UserID,
                Name = user.Name,
                ProfilPicture = Convert.ToBase64String(user.ProfilPicture),
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
            };
        }
    }
}
