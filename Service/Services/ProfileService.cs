using AutoMapper;
using Domain.DTOs.Profile;
using Domain.Entities;
using Domain.Repository;
using FluentValidation.Results;
using Repository.Repository;
using Service.Interfaces;
using Service.Services.Validator;

namespace Service.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProfileService( IMapper mapper, IUnitOfWork unitOfWork)
        {
           _mapper = mapper;
           _unitOfWork = unitOfWork;
           
        }

        public async Task<(ValidationResult validation, int? idNovo)> Add(ProfileDTO profile)
        {
            //throw new NotImplementedException();
            var profileEntity = _mapper.Map<ProfileEntity>(profile);
            var validation = new ProfileValidator().Validate(profileEntity);
            if (!validation.IsValid)
            {
                return (validation, 0);
            }
            var profileExisted = _unitOfWork.ProfileRepository.Get(w => w.Name == profile.Name).FirstOrDefault();
            if (profileExisted != null)
            {
                validation.Errors.Add(new FluentValidation.Results.ValidationFailure() { ErrorMessage = " O perfil já existe no sistema" });
                return (validation, profileExisted.Id);
            }
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var createdProfile = await _unitOfWork.ProfileRepository.Create(profileEntity);
                    if (createdProfile == null)
                    {
                        validation.Errors.Add(new FluentValidation.Results.ValidationFailure() { ErrorMessage = " Erro ao criar um perfil" });
                        return (validation, 0);
                    }

                    foreach (var permissionDTO in profile.Permissions)
                    {
                        var profilePermissionEntity = new ProfilePermissionEntity()
                        {
                            ProfileId = createdProfile.Id,
                            PermissionId = permissionDTO.Id,
                            CreateDate = DateTime.UtcNow,
                        };
                        await _unitOfWork.ProfilePermissionRepository.Create(profilePermissionEntity);
                    }
                    await transaction.CommitAsync();
                    return (validation, createdProfile.Id);


                    //foreach (var profilePermissionDto in profile.ProfilePermissions)
                    //{
                    //    var permissionEntity = new PermissionEntity
                    //    {
                    //        Name = profilePermissionDto.Permissions.Name,
                    //        Description = profilePermissionDto.Permissions.Description,
                    //        Active = profilePermissionDto.Permissions.Active
                    //    };
                    //    var createdPermission = await _unitOfWork.PermissionRepository.Create(permissionEntity);

                    //    var profilePermissionEntity = new ProfilePermissionEntity
                    //    {
                    //        ProfileId = createdProfile.Id,
                    //        PermissionId = profilePermissionDto.PermissionId,
                    //        CreateDate = DateTime.Now
                    //    };

                    //    await _unitOfWork.ProfilePermissionRepository.Create(profilePermissionEntity);

                    //}
                    //await transaction.CommitAsync();
                    //return (validation, createdProfile.Id);

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    validation.Errors.Add(new FluentValidation.Results.ValidationFailure() { ErrorMessage = $"Message: {ex.Message} | InnerException: {ex.InnerException?.Message}" });
                }

            }

            return (validation, profileEntity.Id);
        }
    }
}
