using FluentValidation;
using InnoClinic.SharedModels.DTOs.Appointments.RequestParameters;

namespace Application.Validators;

public class ScheduleParametersValidator : AbstractValidator<ScheduleParameters>
{
	public ScheduleParametersValidator()
	{
		RuleFor(e => e.Day).GreaterThanOrEqualTo(1).LessThanOrEqualTo(31);
		RuleFor(e => e.Month).GreaterThanOrEqualTo(1).LessThanOrEqualTo(12);
		RuleFor(e => e.Year).GreaterThanOrEqualTo(1).LessThanOrEqualTo(9999);
	}
}
