using Domain.DTOs.User;
using Domain.Entities;
using Service.ViewModel;

namespace Service.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserVM> GetAll(int page, int rows, string SortColumn, string SortDirection);
        bool ValidatePassword(string password, string validatePassword);
        bool validateEmail(string email);
        Task<bool> Created(CreateUserDTO user, string password);
        Task<bool> Update(UserEntity user);
        string RetornaErros();
        UserVM GetById(int id);
        UserVM SearchCustomerByCpf(string cpf);
        Task<bool> Delete(int id);
        AddressVM SearchUserAddress(string postalCode, string cpf);
        UserDTO SearchInfosToken(int userId);

    }
        
}
