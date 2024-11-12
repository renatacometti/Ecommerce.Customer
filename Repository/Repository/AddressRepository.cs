using Domain.Entities;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using Repository.Context;


namespace Repository.Repository
{
    public class AddressRepository: BaseRepository<Address>, IAddressRepository
    {
        private IConfiguration _configuration;
        public AddressRepository(AppDbContext appDbContext, IConfiguration configuration) : base(appDbContext)
        {
            _configuration = configuration;
        }

        public void Add(Address address)
        {
            address.Create_Date = DateTime.Now;
            address.Update_Date = DateTime.Now;
            _context.Endereco.Add(address);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;

        }

    }
}
