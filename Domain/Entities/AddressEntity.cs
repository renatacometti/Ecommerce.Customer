using Domain.Common;

namespace Domain.Entities
{
    public class AddressEntity: BaseEntity 
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string AddressName { get; set; }
        public int UserId { get; set; }
        public virtual UserEntity Users { get; set; }
    }
    
}
