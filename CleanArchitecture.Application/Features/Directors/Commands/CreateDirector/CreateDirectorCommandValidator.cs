using FluentValidation;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotNull()
                .WithMessage("{FirstName} no puede ser nulo");

            RuleFor(p => p.LastName)
                .NotNull()
                .WithMessage("{LastName} no puede ser nulo");
        }
    }
}
