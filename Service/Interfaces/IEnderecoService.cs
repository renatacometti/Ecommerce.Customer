using Domain.Entities;

namespace Service.Interfaces
{
    public interface IEnderecoService
    {
        string RetornaErros();
        Task<bool> CadastrarNovoEnderecoParaUsuario(Endereco endereco);
    }
}
