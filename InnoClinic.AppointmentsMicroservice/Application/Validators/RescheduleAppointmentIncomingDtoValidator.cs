using FluentValidation;
using InnoClinic.SharedModels.DTOs.Appointments.Incoming;

namespace Application.Validators;

public class RescheduleAppointmentIncomingDtoValidator : AbstractValidator<RescheduleAppointmentIncomingDto>
{
	public RescheduleAppointmentIncomingDtoValidator()
	{
		RuleFor(e => e.DoctorFirstName).MinimumLength(1);
		RuleFor(e => e.DoctorLastName).MinimumLength(1);
		RuleFor(e => e.DoctorMiddleName).MinimumLength(1);

	}
}
