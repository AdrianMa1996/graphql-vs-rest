namespace Server.Models.DTOs.User.Requests
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public byte[] ProfilPicture { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
