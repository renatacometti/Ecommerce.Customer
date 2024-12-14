using Domain.DTOs.User;
using Domain.Entities;

namespace Service.Interfaces
{
    public interface ITokenService
    {
        bool ValidateToken(string token);
        string CreateToken(UserEntity user);
        String Sign(UserDTO user);
    }
}
