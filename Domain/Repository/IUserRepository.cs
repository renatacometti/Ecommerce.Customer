using Domain.DTO;
using Domain.Entities;

namespace Domain.Repository
{
    public interface IUserRepository: ICommonRepository<User>
    {
        User ValidateUser(string email, string password);
        bool CustomerExist(string cpf, string email);
        void SendEmail(User user);
        User SearchUserAndTheirRelationships(int userId);
        void Change(User user);
        Task<bool> SaveAllAsync();
        User SearchUserByCpf(string cpf);
        Address SearchAddressByUser(string postalCode, string cpf);
        User GetByIDTest(int id);
        User GetByIDTestTwo(int id);
        UserDTO SearchInfosToken(int userId);

    }
}
