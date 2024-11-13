using Domain.DTO;
using Domain.Entities;

namespace Domain.Repository
{
    public interface IUserRepository: ICommonRepository<UserEntity>
    {
        UserEntity ValidateUser(string passaword, string password);
        bool UserExist(string cpf, string email);
        void SendEmail(UserEntity user);
        UserEntity SearchUserAndTheirRelationships(int userId);
        void Change(UserEntity user);
        Task<bool> SaveAllAsync();
        UserEntity SearchUserByCpf(string cpf);
        AddressEntity SearchAddressByUser(string postalCode, string cpf);
        UserEntity GetByIDTest(int id);
        UserEntity GetByIDTestTwo(int id);
        UserDTO SearchInfosToken(int userId);

    }
}
