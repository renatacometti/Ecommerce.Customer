
using Domain.Entities;
using Domain.Repository;
using Repository.Context;

namespace Repository.Repository
{
    public class ProfilePermissionRepository : RepositoryBase<ProfilePermissionEntity>, IProfilePermissionRepository
    {
        public ProfilePermissionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
