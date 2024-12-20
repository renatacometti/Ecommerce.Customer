using Domain.Entities;
namespace Domain.Repository
{
    public interface IAddressRepository: IRepositoryBase<AddressEntity>
    {
        Task Add(AddressEntity address);
        Task<bool> SaveAllAsync();
    }
}
