namespace Server.Models.DTOs.ProjectAndUserBinding.Requests
{
    public class CreateProjectAndUserBindingDto
    {
        public Guid ProjectID { get; set; }
        public Guid UserID { get; set; }
    }
}
