namespace Server.Models.Types.User.InputTypes
{
    public class CreateUserType
    {
        public string Name { get; set; }
        public string ProfilPicture { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
