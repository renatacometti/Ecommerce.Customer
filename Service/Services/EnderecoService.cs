using AutoMapper;
using Domain.Entities;
using Domain.Repository;
using Service.Interfaces;


namespace Service.Services
{
    public class EnderecoService: IAddressService
    {
        private readonly IAddressRepository _enderecoRepository;
        private readonly IMapper _mapper;
        private string ErrosValidacao { get; set; }


        public string RetornaErros()
        {
            return ErrosValidacao;
        }
        public EnderecoService(IAddressRepository enderecoRepository, IMapper mapper)
        {
            _enderecoRepository = enderecoRepository;
            _mapper = mapper;

        }
        public async Task<bool> RegisterNewAddressForUser(AddressEntity endereco)
        {
            try
            {
                
                //var clienteRetornado = _enderecoRepository.Create(endereco);
                _enderecoRepository.Add(endereco);
                if (!await _enderecoRepository.SaveAllAsync())
                    return false;

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
          
        }
    }
}
