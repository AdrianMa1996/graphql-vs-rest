using Server.Models.Database;
using Server.Models.DTOs.User.Requests;

namespace Server.Mapper.UserDTOs.Requests
{
    public class CreateUserDtoToUserMapper : ICreateUserDtoToUserMapper
    {
        public User Map(CreateUserDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new User
            {
                UserID = Guid.NewGuid(),
                Name = dto.Name,
                ProfilPicture = dto.ProfilPicture,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role,
            };
        }
    }
}
