using Domain.DTOs.Permission;


namespace Domain.DTOs.Profile
{
    public class ProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        //public ICollection<ProfilePermissionDTO> ProfilePermissions { get; set; }
        public ICollection<PermissionDTO> Permissions { get; set; }

    }

    public class ProfilePermissionDTO 
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public ProfileDTO Profiles { get; set; }
        public int PermissionId { get; set; }
        public virtual PermissionDTO Permissions { get; set; }

    }
}
