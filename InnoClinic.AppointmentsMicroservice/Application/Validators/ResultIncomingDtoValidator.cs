using FluentValidation;
using InnoClinic.SharedModels.DTOs.Appointments.Incoming;

namespace Application.Validators;

public class ResultIncomingDtoValidator : AbstractValidator<ResultIncomingDto>
{
	public ResultIncomingDtoValidator()
	{
		RuleFor(e => e.Complaints).MinimumLength(1);
		RuleFor(e => e.Conclusion).MinimumLength(1);
		RuleFor(e => e.Recomendations).MinimumLength(1);
	}
}
