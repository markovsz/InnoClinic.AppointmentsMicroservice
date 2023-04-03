using Domain.Entities;
using Domain.RequestParameters;

namespace Domain.Abstractions;

public interface IAppointmentsRepository
{
    Task CreateAsync(Appointment entity);
    Task<Appointment> GetByIdAsync(Guid id);
    Task<IEnumerable<Appointment>> GetAsync(Guid patientId);
    Task<IEnumerable<Appointment>> GetByReceptionistAsync(AppointmentParameters parameters);
    Task<IEnumerable<Appointment>> GetScheduleByDoctorAsync(ScheduleParameters parameters);
    Task<bool> HasAnotherResult(Guid appointmentId);
    Task<bool> Exists(Guid id);
    void Update(Appointment entity);
    void Delete(Appointment entity);
}
