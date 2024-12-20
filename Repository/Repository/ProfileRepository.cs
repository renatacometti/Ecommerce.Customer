using Domain.Entities;
using Domain.Repository;
using Repository.Context;

namespace Repository.Repository
{
    public class ProfileRepository : RepositoryBase<ProfileEntity>, IProfileRepository
    {
        public ProfileRepository(AppDbContext context) : base(context)
        {
        }
    }
}
