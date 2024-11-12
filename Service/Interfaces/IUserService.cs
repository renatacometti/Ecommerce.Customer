using Domain.DTO;
using Domain.Entities;
using Service.ViewModel;

namespace Service.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserVM> GetAll(int page, int rows, string colunaOrdenacao, string direcaoOrdenacao);
        bool ValidacaodeSenha(string senhaCliente, string senhaValidacao);
        bool validaEmail(string email);
        Task<bool> Created(User cliente, string senha);
        Task<bool> Update(User cliente);
        string RetornaErros();
        UserVM GetById(int id);
        UserVM SearchCustomerByCpf(string cpf);
        Task<bool> Delete(int id);
        AddressVM BuscarEnderecoUsuario(string cep, string cpf);
        UserDTO BuscarInfosToken(int idUsuario);

    }
        
}
