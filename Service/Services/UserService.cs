using AutoMapper;
using Domain.Entities;
using Domain.Repository;
using Service.Interfaces;
using Service.ViewModel;
using System.Text.RegularExpressions;
using System.Linq.Dynamic.Core;
using Domain.DTO;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private string ErrosValidacao { get; set; }


        public string RetornaErros()
        {
            return ErrosValidacao;
        }
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;

        }

        public UserVM GetById(int id)
        {
            var usuario = _userRepository.GetById(id);

            var usuarioVM = _mapper.Map<UserVM>(usuario);
            return usuarioVM;
        }

        public UserVM SearchCustomerByCpf(string cpf)
        {
            var usuario = _userRepository.SearchUserByCpf(cpf);
            var usuarioVM = _mapper.Map<UserVM>(usuario);
            return usuarioVM;
        }

        public async Task<bool> Delete(int id)
        {
            var usuario = _userRepository.SearchUserAndTheirRelationships(id);
            if (usuario == null)
            {
                ErrosValidacao = "Usuário não encontrado";
                return false;
            }
            else
            {
                await this._userRepository.Delete(usuario);
                return true;

            }
        }

        public IEnumerable<UserVM> GetAll(int page, int rows, string colunaOrdenacao, string direcaoOrdenacao)
        {

            var usuario = _userRepository.GetAll().Skip((page - 1) * rows).Take(rows).ToList();

            if (!string.IsNullOrEmpty(colunaOrdenacao) && !string.IsNullOrEmpty(direcaoOrdenacao))
            {
                //direcaoOrdenacao = "asc/desc" ou bool sortAscending = true;
                //string sortExpression = colunaOrdenacao + (sortAscending ? " ascending" : " descending");

                // Construindo a expressão de ordenação dinâmica
                usuario = usuario.AsQueryable()
                    .OrderBy($"{colunaOrdenacao} {direcaoOrdenacao}")
                    .ToList();

            }

            var usuarioVM = _mapper.Map<IEnumerable<UserVM>>(usuario);
            return usuarioVM;
        }

        public async Task<bool> Update(UserEntity user)
        {

            try
            {
                if (user.Id == 0)
                {
                    ErrosValidacao = "por favor informe um usuário para ser Alterado";
                    return false;
                }
                if (!validateEmail(user.Email))
                    return false;

                this._userRepository.Change(user);

                if (!await _userRepository.SaveAllAsync())
                    return false;

                return true;

            }
            catch (Exception ex)
            {
                _userRepository.RollbackTransaction();
                throw new Exception(ex.Message);
            }

        }
        public async Task<bool> Created(UserEntity user, string password)
        {

            try
            {
                if (user == null)
                {
                    ErrosValidacao = "por favor informe um usuário para ser cadastrado";
                    return false;
                }


                var validarUsuario = _userRepository.UserExist(user.Cpf, user.Email);
                if (validarUsuario)
                {
                    ErrosValidacao = "Usuario já cadastrado";
                    return false;
                }


                if (!ValidatePassword(user.Password, password))
                    return false;

                if (!validateEmail(user.Email))
                    return false;


                var returnedUser = _userRepository.Create(user);
                _userRepository.SendEmail(user);
                return true;

            }
            catch (Exception ex)
            {
                _userRepository.RollbackTransaction();
                throw new Exception(ex.Message);
            }

        }

        public bool ValidatePassword(string userPassword, string validatePassword)
        {
            if (userPassword != validatePassword)
            {
                this.ErrosValidacao = "As senhas não conferem";
                return false;
            }


            if (userPassword.Length < 8)
            {
                this.ErrosValidacao = "A senha precisa ter no mínimo 8 caracteres";
                return false;
            }


            //verifica se existe pelo menos um número
            if (!userPassword.Any(c => char.IsDigit(c)))
            {
                this.ErrosValidacao = "A senha precisa ter um caracter numerico";
                return false;
            }


            //verifica se existe alguma letra maiuscula
            if (!userPassword.Any(c => char.IsUpper(c)))
            {
                this.ErrosValidacao = "A senha precisa ter pelo menos uma letra maiuscula";
                return false;

            }


            //verifica se existe alguma letra minuscula
            if (!userPassword.Any(c => char.IsLower(c)))
            {
                this.ErrosValidacao = "A senha precisa ter uma letra minuscula";
                return false;
            }


            //verifica se existe algum caracter especial q nao seja letras(maiúscula ou minúscula) e numeros
            if (!Regex.IsMatch(userPassword, (@"[^a-zA-Z0-9]")))
            {
                this.ErrosValidacao = "A senha precisa ter um caracter especial";
                return false;
            }

            return true;

        }

        public bool validateEmail(string email)
        {
            string strModelo = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, strModelo))
            {
                this.ErrosValidacao = "Email Invalido";
                return false;

            }

            return true;

        }

        public AddressVM SearchUserAddress(string postalCode, string cpf)
        {
            var returnedAddress = _userRepository.SearchAddressByUser(postalCode, cpf);
            if (returnedAddress == null)
                return new AddressVM();

            var addressVM = _mapper.Map<AddressVM>(returnedAddress);
            return addressVM;
        }

        public UserDTO SearchInfosToken(int userId) 
        {
            var user = _userRepository.SearchInfosToken(userId);
            return user;
        }

    }
}
