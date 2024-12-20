
namespace Domain.DTOs.Profile
{
    public class ProfileGetByIdDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public ICollection<ProfilePermissionDTO> ProfilePermissions { get; set; }
        
    }
}
