namespace Server.Models.DTOs.ProjectAndUserBinding.Requests
{
    public class UpdateProjectAndUserBindingDto
    {
        public Guid ProjectAndUserBindingID { get; set; }
        public Guid ProjectID { get; set; }
        public Guid UserID { get; set; }
    }
}
