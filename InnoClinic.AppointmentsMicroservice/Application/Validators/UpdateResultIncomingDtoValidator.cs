using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;
using FluentValidation;

namespace Application.Validators;

public class UpdateResultIncomingDtoValidator : AbstractValidator<UpdateResultIncomingDto>
{
	public UpdateResultIncomingDtoValidator()
	{
        RuleFor(e => e.Complaints).MinimumLength(1);
        RuleFor(e => e.Conclusion).MinimumLength(1);
        RuleFor(e => e.Recomendations).MinimumLength(1);
    }
}
