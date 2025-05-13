namespace Server.Models.DTOs.User.Responses
{
    public class GetUserDto
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public byte[] ProfilPicture { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
