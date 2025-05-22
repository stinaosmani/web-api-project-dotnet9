using FluentValidation;
using backend.src.Application.Service.Users.Dto;

namespace backend.src.Application.Validators.Users
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(100)
                .WithMessage("First name must be at most 100 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

            RuleFor(x => x.LastName)
                .MaximumLength(100)
                .WithMessage("Last name must be at most 100 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.LastName));

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Today)
                .WithMessage("Date of birth must be in the past.")
                .When(x => x.DateOfBirth.HasValue);

            RuleFor(x => x.SettingsJson)
                .Must(json => string.IsNullOrWhiteSpace(json) || json.Trim().StartsWith("{"))
                .WithMessage("SettingsJson must be a valid JSON string.")
                .When(x => !string.IsNullOrWhiteSpace(x.SettingsJson));
        }
    }
}
