﻿using Domain.Entities;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using Repository.Context;

namespace Repository.Repository
{
    public class PermissionRepository : BaseRepository<PermissionEntity>, IPermissionRepository
    {
        private IConfiguration _configuration;
        public PermissionRepository(AppDbContext appDbContext, IConfiguration configuration) : base(appDbContext)
        {
            _configuration = configuration;
        }
    }
}
