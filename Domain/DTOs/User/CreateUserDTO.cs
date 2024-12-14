using Domain.DTOs.Address;

namespace Domain.DTOs.User
{
    public class CreateUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public int ProfileId { get; set; }
        public ICollection<AddressDTO> Addresses { get; set; }

    }
}
