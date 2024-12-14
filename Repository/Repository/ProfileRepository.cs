using Domain.Entities;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using Repository.Context;

namespace Repository.Repository
{
    public class ProfileRepository : BaseRepository<ProfileEntity>, IProfileRepository
    {
        private IConfiguration _configuration;
        public ProfileRepository(AppDbContext appDbContext, IConfiguration configuration) : base(appDbContext)
        {
            _configuration = configuration;
        }
    }
}
