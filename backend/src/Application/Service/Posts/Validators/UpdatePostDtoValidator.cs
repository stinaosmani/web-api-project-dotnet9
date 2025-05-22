using FluentValidation;
using backend.src.Application.Service.Posts.Dto;

namespace backend.src.Application.Validators.Posts
{
    public class UpdatePostDtoValidator : AbstractValidator<UpdatePostDto>
    {
        public UpdatePostDtoValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(200)
                .WithMessage("Title cannot exceed 200 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Title));

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.Body)
                .MinimumLength(10)
                .WithMessage("Body must be at least 10 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Body));

            RuleFor(x => x.Slug)
                .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$")
                .WithMessage("Slug must be URL-friendly.")
                .When(x => !string.IsNullOrWhiteSpace(x.Slug));

            RuleFor(x => x.AuthorId)
                .Must(id => id != Guid.Empty)
                .WithMessage("AuthorId must be a valid GUID.")
                .When(x => x.AuthorId != default);
        }
    }
}
