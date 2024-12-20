using Domain.Entities;
using Domain.Repository;
using Repository.Context;


namespace Repository.Repository
{
    public class AddressRepository : RepositoryBase<AddressEntity>, IAddressRepository
    {
        
        public AddressRepository(AppDbContext appDbContext) : base(appDbContext)
        {
          
        }

        public Task Add(AddressEntity address)
        {
            throw new NotImplementedException();
        }

        //public async Task Add(AddressEntity address)
        //{
        //    address.CreateDate = DateTime.Now;
        //    address.UpdateDate = DateTime.Now;
        //    await _context.Address.Add(address);
        //}

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
