using Domain.DTO;
using Domain.Entities;
using Service.ViewModel;

namespace Service.Interfaces
{
    public interface IUsuarioService
    {
        IEnumerable<UsuarioVM> GetAll(int page, int rows, string colunaOrdenacao, string direcaoOrdenacao);
        bool ValidacaodeSenha(string senhaCliente, string senhaValidacao);
        bool validaEmail(string email);
        Task<bool> Created(Usuario cliente, string senha);
        Task<bool> Update(Usuario cliente);
        string RetornaErros();
        UsuarioVM GetById(int id);
        UsuarioVM BuscarClienteporCpf(string cpf);
        Task<bool> Delete(int id);
        EnderecoVM BuscarEnderecoCLiente(string cep, string cpf);
        UsuarioDTO BuscarPermissoesUsuario(int idUsuario);

    }
        
}
