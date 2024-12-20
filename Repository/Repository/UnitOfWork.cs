using Domain.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Context;
namespace Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IAddressRepository _addressRepository;
        public IPermissionRepository _permissionRepository;
        public IProfileRepository _profileRepository;
        public IUserRepository _userRepository;
        public IProfilePermissionRepository _profilePermissionRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }


        public IAddressRepository AddressRepository
        {
            get
            {
                return _addressRepository ?? (_addressRepository = new AddressRepository(_context));
            }
        }

        public IPermissionRepository PermissionRepository
        {
            get
            {
                return _permissionRepository ?? (_permissionRepository = new PermissionRepository(_context));
            }
        }

        public IProfileRepository ProfileRepository
        {
            get
            {
                return _profileRepository ?? (_profileRepository = new ProfileRepository(_context));
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ?? (_userRepository = new UserRepository(_context));
            }
        }

        public IProfilePermissionRepository ProfilePermissionRepository
        {
            get
            {
                return _profilePermissionRepository ?? (_profilePermissionRepository = new ProfilePermissionRepository(_context));
            }
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
