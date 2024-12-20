using Domain.DTOs.Profile;
using FluentValidation.Results;
using Service.ViewModel;

namespace Service.Interfaces
{
    public interface IProfileService
    {
        Task<(ValidationResult validation, int? idNovo)> Add(ProfileDTO profile);
        Task<ProfileGetByIdDTO> GetById(int id);

    }
}
