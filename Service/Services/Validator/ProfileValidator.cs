using Domain.Entities;
using FluentValidation;


namespace Service.Services.Validator
{
    public class ProfileValidator: AbstractValidator<ProfileEntity>
    {
        public ProfileValidator() 
        {
            RuleFor(dis => dis.Name).NotEmpty().WithMessage("O campo Nome o não pode ser vazio");
            RuleFor(dis => dis.Description).NotEmpty().WithMessage("O campo Descrição não pode ser vazio");
            RuleFor(dis => dis.Active).NotEmpty().WithMessage("O campo Ativo não pode ser vazio");
        }
    }
}
