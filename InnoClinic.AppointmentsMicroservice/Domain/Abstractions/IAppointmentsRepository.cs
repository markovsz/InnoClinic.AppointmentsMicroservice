using Domain.Entities;
using InnoClinic.SharedModels.DTOs.Appointments.RequestParameters;

namespace Domain.Abstractions;

public interface IAppointmentsRepository
{
    Task CreateAsync(Appointment entity);
    Task<Appointment> GetByIdAsync(Guid id);
    Task<IEnumerable<Appointment>> GetAsync(Guid patientId);
    Task<IEnumerable<Appointment>> GetByReceptionistAsync(AppointmentParameters parameters);
    Task<IEnumerable<Appointment>> GetScheduleByDoctorAsync(ScheduleParameters parameters);
    Task<IEnumerable<Appointment>> GetTimeSlotsAsync(TimeSlotParameters parameters);
    Task<bool> HasAnotherResult(Guid appointmentId);
    Task<bool> Exists(Guid id);
    void Update(Appointment entity);
    Task UpdateServiceNameAsync(Guid serviceId, string serviceName);
    Task UpdateDoctorProfileAsync(Guid doctorId, string doctorFirstName, string doctorLastName, string doctorMiddleName);
    void Delete(Appointment entity);
}
