using Server.Models.Database;
using Server.Models.Types.User.InputTypes;

namespace Server.Mapper.UserTypes.InputTypes
{
    public class CreateUserTypeToUserMapper : ICreateUserTypeToUserMapper
    {
        public User Map(CreateUserType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return new User
            {
                UserID = Guid.NewGuid(),
                Name = type.Name,
                ProfilPicture = Convert.FromBase64String(type.ProfilPicture),
                Email = type.Email,
                Password = type.Password,
                Role = type.Role,
            };
        }
    }
}
