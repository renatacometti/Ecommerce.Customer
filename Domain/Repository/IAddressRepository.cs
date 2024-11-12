using Domain.Entities;
namespace Domain.Repository
{
    public interface IAddressRepository: ICommonRepository<Address>
    {
        void Add(Address address);
        Task<bool> SaveAllAsync();
    }
}
