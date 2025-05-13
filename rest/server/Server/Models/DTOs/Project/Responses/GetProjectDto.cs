namespace Server.Models.DTOs.Project.Responses
{
    public class GetProjectDto
    {
        public Guid ProjectID { get; set; }
        public string Name { get; set; }
        public byte[] Logo { get; set; }
    }
}
