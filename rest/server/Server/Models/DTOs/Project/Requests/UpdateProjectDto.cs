namespace Server.Models.DTOs.Project.Requests
{
    public class UpdateProjectDto
    {
        public Guid ProjectID { get; set; }
        public string Name { get; set; }
        public byte[] Logo { get; set; }
    }
}
