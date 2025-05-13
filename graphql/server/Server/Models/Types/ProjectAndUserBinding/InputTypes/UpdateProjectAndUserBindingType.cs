namespace Server.Models.Types.ProjectAndUserBinding.InputTypes
{
    public class UpdateProjectAndUserBindingType
    {
        public Guid ProjectAndUserBindingID { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? UserID { get; set; }
    }
}
