using Domain.Entities;
namespace Domain.Repository
{
    public interface IAddressRepository: ICommonRepository<AddressEntity>
    {
        void Add(AddressEntity address);
        Task<bool> SaveAllAsync();
    }
}
