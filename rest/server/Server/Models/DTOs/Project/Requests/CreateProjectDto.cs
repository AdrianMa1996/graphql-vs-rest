namespace Server.Models.DTOs.Project.Requests
{
    public class CreateProjectDto
    {
        public string Name { get; set; }
        public byte[] Logo { get; set; }
    }
}
