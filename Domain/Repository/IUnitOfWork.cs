using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Repository
{
    public interface IUnitOfWork
    {

        public IAddressRepository AddressRepository { get; }
        public IPermissionRepository PermissionRepository {  get; }
        public IProfileRepository ProfileRepository {  get; }
        public IUserRepository UserRepository {  get; }
        public IProfilePermissionRepository ProfilePermissionRepository {  get; }
        IDbContextTransaction BeginTransaction();
        int SaveChanges();
        Task<int> SaveChangesAsync();
       
    }
}
