using AutoMapper;
using Domain.Entities;
using Domain.Repository;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EnderecoService: IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;
        private string ErrosValidacao { get; set; }


        public string RetornaErros()
        {
            return ErrosValidacao;
        }
        public EnderecoService(IEnderecoRepository enderecoRepository, IMapper mapper)
        {
            _enderecoRepository = enderecoRepository;
            _mapper = mapper;

        }
        public async Task<bool> CadastrarNovoEnderecoParaUsuario(Endereco endereco, int idUsuario)
        {
            try
            {
                endereco.Id_Cliente = idUsuario;
                //var clienteRetornado = _enderecoRepository.Create(endereco);
                _enderecoRepository.Add(endereco);
                if (!await _enderecoRepository.SaveAllAsync())
                    return false;

                return true;
            }
            catch (Exception ex)
            {

                _enderecoRepository.RollbackTransaction();
                throw new Exception(ex.Message);
            }
          
        }
    }
}
