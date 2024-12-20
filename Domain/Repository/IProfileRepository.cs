using Domain.DTOs.Profile;
using Domain.Entities;
namespace Domain.Repository
{
    public interface IProfileRepository : IRepositoryBase<ProfileEntity>
    {
        Task<ProfileGetByIdDTO> ProfileGetById(int id);
    }
}
