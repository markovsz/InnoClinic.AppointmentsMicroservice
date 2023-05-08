using Domain.RequestParameters;
using InnoClinic.SharedModels.DTOs.Appointments.Incoming;
using InnoClinic.SharedModels.DTOs.Appointments.Outgoing;
using InnoClinic.SharedModels.Messages;

namespace Application.Abstractions;

public interface IAppointmentsService
{
    Task<Guid> CreateAsync(AppointmentIncomingDto incomngDto);
    Task ApproveAsync(Guid id);
    Task<IEnumerable<PatientAppointmentsOutgoingDto>> GetAsync(Guid patientId);
    Task<IEnumerable<AppointmentByReceptionistOutgoingDto>> GetByReceptionistAsync(AppointmentParameters parameters);
    Task<IEnumerable<AppointmentScheduleByDoctorOutgoingDto>> GetScheduleByDoctorAsync(ScheduleParameters parameters);
    Task RescheduleAsync(Guid id, RescheduleAppointmentIncomingDto incomingDto);
    Task UpdateServiceNameAsync(ServiceUpdatedMessage message);
    Task UpdateDoctorProfileAsync(DoctorProfileUpdatedMessage message);
    Task DeleteByIdAsync(Guid id);
}
