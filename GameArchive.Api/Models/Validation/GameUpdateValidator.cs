using FluentValidation;
using GameArchive.Api.Models.Dto;

namespace GameArchive.Api.Models.Validation
{
    class GameUpdateValidator : AbstractValidator<GameDto>
    {
        internal GameUpdateValidator()
        {
            RuleFor(g => g.Id)
                .NotNull();

            RuleFor(g => g.Title)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();

            RuleFor(g => g.Genres)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();
        }
    }
}
