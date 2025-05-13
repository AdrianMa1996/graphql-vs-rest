using Server.Models.Database;
using Server.Models.DTOs.User.Requests;

namespace Server.Mapper.UserDTOs.Requests
{
    public class UpdateUserDtoToUserMapper : IUpdateUserDtoToUserMapper
    {
        public User Map(UpdateUserDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new User
            {
                UserID = dto.UserID,
                Name = dto.Name,
                ProfilPicture = dto.ProfilPicture,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role,
            };
        }
    }
}
