using FluentValidation;
using backend.src.Application.Service.Posts.Dto;

namespace backend.src.Application.Validators.Posts
{
    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500);

            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Body is required.")
                .MinimumLength(10);

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Slug is required.")
                .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$")
                .WithMessage("Slug must be URL-friendly (lowercase letters, numbers, hyphens).");

            RuleFor(x => x.AuthorId)
                .NotEqual(Guid.Empty)
                .WithMessage("AuthorId is required.");
        }
    }
}
