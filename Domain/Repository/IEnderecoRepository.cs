using Domain.Entities;


namespace Domain.Repository
{
    public interface IEnderecoRepository: ICommonRepository<Endereco>
    {
        void Add(Endereco endereco);
        Task<bool> SaveAllAsync();
    }
}
