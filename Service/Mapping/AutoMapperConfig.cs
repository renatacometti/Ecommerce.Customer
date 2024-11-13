using Domain.DTO;
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
            CreateMap<PermissionDTO, PermissionEntity>().ReverseMap();
            CreateMap<AddressVM, AddressEntity>().ReverseMap();

        }
    }
}
