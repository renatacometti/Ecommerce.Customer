using Domain.DTOs.Address;
using Domain.DTOs.Permission;
using Domain.DTOs.Profile;

namespace Domain.DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public ProfileDTO Profile { get; set; }
        public ICollection<AddressDTO> Addresses { get; set; }
        public ICollection<PermissionDTO> Permissions { get; set; }

    }
}
