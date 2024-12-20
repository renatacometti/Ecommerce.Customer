using Domain.Entities;
using Domain.Repository;
using Repository.Context;

namespace Repository.Repository
{
    public class PermissionRepository : RepositoryBase<PermissionEntity>, IPermissionRepository
    {
        
        public PermissionRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            
        }
    }
}
