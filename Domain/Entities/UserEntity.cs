using Domain.Common;

namespace Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<AddressEntity> Addresses { get; set; }
        public int ProfileId { get; set; }
        public virtual ProfileEntity Profile { get; set; }
    }
}
