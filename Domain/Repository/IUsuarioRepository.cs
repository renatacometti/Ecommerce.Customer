using Domain.DTO;
using Domain.Entities;

namespace Domain.Repository
{
    public interface IUsuarioRepository: ICommonRepository<Usuario>
    {
        User ValidaCliente(string email, string senha);

        bool CustomerExist(string cpf, string email);

        void EnviarEmail(Usuario cliente);
        Usuario BuscarClienteESeusRelacionamentos(int idCliente);
        void Alterar(Usuario cliente);
        Task<bool> SaveAllAsync();
        Usuario BuscarClienteporCpf(string cpf);
        Endereco BuscarEnderecoPorCliente(string cep, string cpf);
        Usuario GetByIDTest(int id);
        Usuario GetByIDTestTwo(int id);
        UsuarioDTO BuscarPermissoesUsuario(int idUsuario);


    }
}
