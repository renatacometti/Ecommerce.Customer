using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Service.ViewModel;


namespace Service.Mapping
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<UsuarioDTO, UsuarioVM>().ReverseMap();
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
            CreateMap<UsuarioVM, Usuario>().ReverseMap();
            CreateMap<EnderecoVM, Endereco>().ReverseMap();
          

        }
    }
}
