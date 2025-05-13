using SQLite;

namespace Server.Models.Database
{
    public class ProjectAndUserBinding
    {
        [PrimaryKey]
        public Guid ProjectAndUserBindingID { get; set; }
        public Guid ProjectID { get; set; }
        public Guid UserID { get; set; }
    }
}
