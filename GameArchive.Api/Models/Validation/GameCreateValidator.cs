using FluentValidation;
using GameArchive.Api.Models.Dto;

namespace GameArchive.Api.Models.Validation
{
    class GameCreateValidator : AbstractValidator<GameDto>
    {
        internal GameCreateValidator()
        {
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
