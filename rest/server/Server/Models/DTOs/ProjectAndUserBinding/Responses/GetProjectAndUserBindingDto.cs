namespace Server.Models.DTOs.ProjectAndUserBinding.Responses
{
    public class GetProjectAndUserBindingDto
    {
        public Guid ProjectAndUserBindingID { get; set; }
        public Guid ProjectID { get; set; }
        public Guid UserID { get; set; }
    }
}
