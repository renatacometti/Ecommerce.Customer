using Domain.DTOs.Address;
using Domain.DTOs.Permission;
using Domain.DTOs.Profile;
using Domain.DTOs.User;
using Domain.Entities;
using Service.ViewModel;


namespace Service.Mapping
{
    public class AutoMapperConfig: AutoMapper.Profile
    {
        public AutoMapperConfig()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;
            
            CreateMap<UserDTO, UserVM>().ReverseMap();
            CreateMap<UserDTO, UserEntity>().ReverseMap();
            CreateMap<AddressDTO, AddressEntity>().ReverseMap();
            CreateMap<ProfileDTO, ProfileEntity>().ReverseMap();
            CreateMap<PermissionDTO, PermissionEntity>().ReverseMap();
            CreateMap<ProfilePermissionDTO, ProfilePermissionEntity>().ReverseMap();
            CreateMap<AddressVM, AddressEntity>().ReverseMap();
            CreateMap<CreateUserDTO, UserEntity>().ReverseMap();

        }
    }
}
