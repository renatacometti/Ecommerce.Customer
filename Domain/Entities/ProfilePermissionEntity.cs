
namespace Domain.Entities
{
    public class ProfilePermissionEntity
    {
        public int Id {  get; set; }

        public int ProfileId { get; set; }
        public virtual ProfileEntity Profiles { get; set; }

        public int PermissionId { get; set; }
        public virtual PermissionEntity Permissions { get; set; }
       
    }
}
