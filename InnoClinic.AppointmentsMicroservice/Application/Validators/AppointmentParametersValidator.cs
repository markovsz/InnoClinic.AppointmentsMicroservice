using FluentValidation;
using InnoClinic.SharedModels.DTOs.Appointments.RequestParameters;

namespace Application.Validators
{
    public class AppointmentParametersValidator : AbstractValidator<AppointmentParameters>
    {
        public AppointmentParametersValidator()
        {
            RuleFor(e => e.DoctorFirstName).MinimumLength(1);
            RuleFor(e => e.DoctorLastName).MinimumLength(1);
            RuleFor(e => e.DoctorMiddleName).MinimumLength(1);
        }
    }
}
