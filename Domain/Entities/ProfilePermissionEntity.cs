
using Domain.Common;

namespace Domain.Entities
{
    public class ProfilePermissionEntity : BaseEntity
    {
        public int ProfileId { get; set; }
        public virtual ProfileEntity Profiles { get; set; }

        public int PermissionId { get; set; }
        public virtual PermissionEntity Permissions { get; set; }
       
    }
}
