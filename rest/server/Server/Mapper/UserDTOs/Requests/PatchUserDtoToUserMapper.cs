using Server.Models.Database;
using Server.Models.DTOs.User.Requests;

namespace Server.Mapper.UserDTOs.Requests
{
    public class PatchUserDtoToUserMapper : IPatchUserDtoToUserMapper
    {
        public User Map(User existingUser, PatchUserDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (existingUser == null)
                throw new ArgumentNullException(nameof(existingUser));

            return new User
            {
                UserID = dto.UserID,
                Name = dto.Name ?? existingUser.Name,
                ProfilPicture = dto.ProfilPicture ?? existingUser.ProfilPicture,
                Email = dto.Email ?? existingUser.Email,
                Password = dto.Password ?? existingUser.Password,
                Role = dto.Role ?? existingUser.Role,
            };
        }
    }
}
