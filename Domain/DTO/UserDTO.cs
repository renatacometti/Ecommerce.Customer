using Domain.Entities;
namespace Domain.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthdayDate { get; set; }
        public string Phone { get; set; }
        public string Active { get; set; }
        public ProfileEntity Profile { get; set; }
        public ICollection<AddressDTO> Addresses { get; set; }
        public ICollection<PermissionDTO> Permissions { get; set; }
    }
}
