using Domain.Common;
namespace Domain.Entities
{
    public class ProfileEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<PermissionProfileEntity> PermissionProfiles { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; }

    }
}
