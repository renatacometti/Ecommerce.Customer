using Domain.Entities;

namespace Service.Interfaces
{
    public interface IAddressService
    {
        string RetornaErros();
        Task<bool> RegisterNewAddressForUser(Address endereco);
    }
}
