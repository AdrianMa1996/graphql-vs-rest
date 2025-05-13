using Server.Models.Database;
using Server.Models.Types.User.InputTypes;

namespace Server.Mapper.UserTypes.InputTypes
{
    public class UpdateUserTypeToUserMapper : IUpdateUserTypeToUserMapper
    {
        public User Map(User existingUser, UpdateUserType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (existingUser == null)
                throw new ArgumentNullException(nameof(existingUser));

            return new User
            {
                UserID = type.UserID,
                Name = type.Name ?? existingUser.Name,
                ProfilPicture = string.IsNullOrWhiteSpace(type.ProfilPicture) ? existingUser.ProfilPicture : Convert.FromBase64String(type.ProfilPicture),
                Email = type.Email ?? existingUser.Email,
                Password = type.Password ?? existingUser.Password,
                Role = type.Role ?? existingUser.Role,
            };
        }
    }
}
