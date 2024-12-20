using Domain.DTOs.Profile;
using FluentValidation.Results;

namespace Service.Interfaces
{
    public interface IProfileService
    {
        Task<(ValidationResult validation, int? idNovo)> Add(ProfileDTO profile);

    }
}
