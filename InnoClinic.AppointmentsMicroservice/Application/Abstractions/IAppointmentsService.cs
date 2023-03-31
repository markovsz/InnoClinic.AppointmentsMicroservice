using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;
using Domain.RequestParameters;

namespace Application.Abstractions;

public interface IAppointmentsService
{
    Task<Guid> CreateAsync(AppointmentIncomingDto incomngDto);
    Task ApproveAsync(Guid id);
    Task<IEnumerable<PatientAppointmentsOutgoingDto>> GetAsync(Guid patientId);
    Task<IEnumerable<AppointmentByReceptionistOutgoingDto>> GetByReceptionistAsync(AppointmentParameters parameters);
    Task<IEnumerable<AppointmentScheduleByDoctorOutgoingDto>> GetScheduleByDoctorAsync(ScheduleParameters parameters);
    Task RescheduleAsync(Guid id, RescheduleAppointmentIncomingDto incomingDto);
    Task DeleteByIdAsync(Guid id);
}
