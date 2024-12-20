using AutoMapper;
using Domain.DTOs.Permission;
using Domain.DTOs.Profile;
using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Repository.Repository
{
    public class ProfileRepository : RepositoryBase<ProfileEntity>, IProfileRepository
    {
        public ProfileRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ProfileGetByIdDTO> ProfileGetById(int id)
        {

            var profileDTO = await (from profile in _context.Profile
                                    join pp1 in _context.ProfilePermission on profile.Id equals pp1.ProfileId into profilePermissionGroup
                                    from pp1 in profilePermissionGroup.DefaultIfEmpty()
                                    join permission in _context.Permission on pp1.PermissionId equals permission.Id into permissionGroup
                                    from permission in permissionGroup.DefaultIfEmpty()
                                    where profile.Id == id
                                    select new ProfileGetByIdDTO
                                    {
                                        Id = profile.Id,
                                        Name = profile.Name,
                                        Description = profile.Description,
                                        Active = profile.Active,
                                        ProfilePermissions = (from pp in _context.ProfilePermission
                                                              join perm in _context.Permission on pp.PermissionId equals perm.Id into ppGroup
                                                              from perm in ppGroup.DefaultIfEmpty()
                                                              where pp.ProfileId == profile.Id || pp.ProfileId == null
                                                              select new ProfilePermissionDTO
                                                              {
                                                                  Id = pp != null ? pp.Id : 0,
                                                                  PermissionId = pp != null ? pp.PermissionId : 0,
                                                                  Permissions = perm != null
                                                                      ? new PermissionDTO
                                                                      {
                                                                          Id = perm != null ? perm.Id : 0,
                                                                          Name = perm != null ? perm.Name : "Sem Permissão",
                                                                          Description = perm != null ? perm.Description : "N/A",
                                                                          Active = perm != null && perm.Active
                                                                      }
                                                                      : null
                                                              }).ToList()
                                    }).FirstOrDefaultAsync();

            return profileDTO;

        }
    }
}
