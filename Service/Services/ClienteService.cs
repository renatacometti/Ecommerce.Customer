using AutoMapper;
using Domain.Entities;
using Domain.Repository;
using Service.Interfaces;
using Service.ViewModel;
using System.Text.RegularExpressions;
using System.Linq.Dynamic.Core;

namespace Service.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private string ErrosValidacao { get; set; }


        public string RetornaErros() 
        {
            return ErrosValidacao;
        }
        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
          
        }

        public ClienteVM GetById(int id) 
        {
            var cliente = _clienteRepository.GetById(id);
            var clienteVM = _mapper.Map<ClienteVM>(cliente);
            return clienteVM;
        }

        public ClienteVM BuscarClienteporCpf(string cpf)
        {
            var cliente = _clienteRepository.BuscarClienteporCpf(cpf);
            var clienteVM = _mapper.Map<ClienteVM>(cliente);
            return clienteVM;
        }

    

        public async Task<bool> Delete(int id)
        {
            var cliente = _clienteRepository.BuscarClienteESeusRelacionamentos(id);
            if (cliente == null) 
            {
                ErrosValidacao = "Cliente não encontrado";
                return false;
            }
            else
            {
                await this._clienteRepository.Delete(cliente);
                return true;

            }
   

        }

        public IEnumerable<ClienteVM> GetAll(int page, int rows, string colunaOrdenacao, string direcaoOrdenacao)
        {

         
            var cliente = _clienteRepository.GetAll().Skip((page  - 1) * rows).Take(rows).ToList();


            if (!string.IsNullOrEmpty(colunaOrdenacao) && !string.IsNullOrEmpty(direcaoOrdenacao))
            {
                //direcaoOrdenacao = "asc/desc" ou bool sortAscending = true;
                //string sortExpression = colunaOrdenacao + (sortAscending ? " ascending" : " descending");

                // Construindo a expressão de ordenação dinâmica
                cliente = cliente.AsQueryable()
                    .OrderBy($"{colunaOrdenacao} {direcaoOrdenacao}")
                    .ToList();
        
            }

            var clienteVM = _mapper.Map<IEnumerable<ClienteVM>>(cliente);
            return clienteVM;
        }


        public async Task<bool> Update(Cliente cliente)
        {

            try
            {
                if (cliente.Id == 0)
                {
                    ErrosValidacao = "por favor informe um cliente para ser Alterado";
                    return false;
                }
                if (!validaEmail(cliente.Email))
                    return false;

                this._clienteRepository.Alterar(cliente);

                if (!await _clienteRepository.SaveAllAsync())
                    return false;

                return true;

            }
            catch (Exception ex)
            {
                _clienteRepository.RollbackTransaction();
                throw new Exception(ex.Message);
            }

        }
        public async Task<bool> Created(Cliente cliente, string senha)
        {

            try
            {
                if (cliente == null) 
                {
                    ErrosValidacao = "por favor informe um cliente para ser cadastrado";
                    return false;
                }
                    

                var validarUsuario = _clienteRepository.CustomerExist(cliente.Cpf, cliente.Email);
                if (validarUsuario) 
                {
                    ErrosValidacao = "Usuario já cadastrado";
                    return false;
                }
                    

                if (!ValidacaodeSenha(cliente.Senha, senha))
                    return false;

                if (!validaEmail(cliente.Email))
                    return false;


                var clienteRetornado = _clienteRepository.Create(cliente);
                _clienteRepository.EnviarEmail(cliente);
                return true;

            }
            catch (Exception ex)
            {
                _clienteRepository.RollbackTransaction();
                throw new Exception(ex.Message);
            }

        }

        public bool ValidacaodeSenha(string senhaCliente, string senhaValidacao) 
        {
            if (senhaCliente != senhaValidacao)
            {
                this.ErrosValidacao = "As senhas não conferem";
                return false;
            }


            if (senhaCliente.Length < 8) 
            {
                this.ErrosValidacao = "A senha precisa ter no mínimo 8 caracteres";
                return false;
            }


            //verifica se existe pelo menos um número
            if (!senhaCliente.Any(c => char.IsDigit(c))) 
            {
                this.ErrosValidacao = "A senha precisa ter um caracter numerico";
                return false;
            }
               

            //verifica se existe alguma letra maiuscula
            if (!senhaCliente.Any(c => char.IsUpper(c))) 
            {
                this.ErrosValidacao = "A senha precisa ter pelo menos uma letra maiuscula";
                return false;

            }


            //verifica se existe alguma letra minuscula
            if (!senhaCliente.Any(c => char.IsLower(c))) 
            {
                this.ErrosValidacao = "A senha precisa ter uma letra minuscula";
                return false;
            }


            //verifica se existe algum caracter especial q nao seja letras(maiúscula ou minúscula) e numeros
            if (!Regex.IsMatch(senhaCliente, (@"[^a-zA-Z0-9]"))) 
            {
                this.ErrosValidacao = "A senha precisa ter um caracter especial";
                return false;
            }
  
            return true;

        }

        public bool validaEmail(string email) 
        {
            string strModelo = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, strModelo)) 
            {
                this.ErrosValidacao = "Email Invalido";
                return false;

            }
    
            return true;
      
        }

        public EnderecoVM BuscarEnderecoCLiente(string cep, string cpf)
        {
            var enderecoRetornado = _clienteRepository.BuscarEnderecoPorCliente(cep, cpf);
            if(enderecoRetornado == null)
                return new EnderecoVM();

            var endVM = _mapper.Map<EnderecoVM>(enderecoRetornado);
            return endVM;
        }

     
    }
}
