namespace Server.Models.Types.Project.InputTypes
{
    public class UpdateProjectType
    {
        public Guid ProjectID { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
    }
}
