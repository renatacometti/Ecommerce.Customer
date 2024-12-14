using Domain.Entities;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using Repository.Context;


namespace Repository.Repository
{
    public class AddressRepository: BaseRepository<AddressEntity>, IAddressRepository
    {
        private IConfiguration _configuration;
        public AddressRepository(AppDbContext appDbContext, IConfiguration configuration) : base(appDbContext)
        {
            _configuration = configuration;
        }

        public void Add(AddressEntity address)
        {
            address.CreateDate = DateTime.Now;
            address.UpdateDate = DateTime.Now;
            _context.Address.Add(address);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;

        }

    }
}
