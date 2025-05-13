using SQLite;

namespace Server.Models.Database
{
    public class User
    {
        [PrimaryKey]
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public byte[] ProfilPicture { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
