using SQLite;

namespace Server.Models.Database
{
    public class Project
    {
        [PrimaryKey]
        public Guid ProjectID { get; set; }
        public string Name { get; set; }
        public byte[] Logo { get; set; }
    }
}
