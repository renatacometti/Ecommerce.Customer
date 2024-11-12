using AutoMapper;
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
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<UserVM, User>().ReverseMap();
            CreateMap<AddressVM, Address>().ReverseMap();
          

        }
    }
}
