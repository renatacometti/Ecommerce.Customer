using Domain.Entities;

namespace Service.ViewModel
{
    public struct UserVM
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
        public ICollection<AddressVM> Addresses { get; set; }
        public ICollection<PermissionVM> Permissions { get; set; }
    }
}
