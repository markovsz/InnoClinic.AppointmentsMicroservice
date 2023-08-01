using Domain.Entities;
using InnoClinic.SharedModels.DTOs.Appointments.RequestParameters;

namespace Infrastructure.Extensions;

public static class FiltrationExtensions
{
    public static IQueryable<Appointment> AppointmentsFilter(this IQueryable<Appointment> entities, AppointmentParameters parameters)
    {
        if(parameters.DateTime.HasValue)
            entities = entities.Where(e => e.DateTime.Equals(parameters.DateTime));

        if(parameters.DoctorFirstName is not null)
            entities = entities.Where(e => e.DoctorFirstName.Contains(parameters.DoctorFirstName));

        if(parameters.DoctorLastName is not null)
            entities = entities.Where(e => e.DoctorLastName.Contains(parameters.DoctorLastName));

        if(parameters.DoctorMiddleName is not null)
            entities = entities.Where(e => e.DoctorMiddleName.Contains(parameters.DoctorMiddleName));

        if(parameters.ServiceId is not null)
            entities = entities.Where(e => e.ServiceId.Equals(parameters.ServiceId));
        
        if(parameters.IsApproved.HasValue)
            entities = entities.Where(e => e.IsApproved.Equals(parameters.IsApproved));

        if(parameters.OfficeId.HasValue)
            entities = entities.Where(e => e.OfficeId.Equals(parameters.OfficeId));

        return entities;
    }

    public static IQueryable<Appointment> ScheduleFilter(this IQueryable<Appointment> entities, ScheduleParameters parameters)
    {
        entities = entities.Where(e => e.DateTime.Day.Equals(parameters.Day));
        entities = entities.Where(e => e.DateTime.Month.Equals(parameters.Month));
        entities = entities.Where(e => e.DateTime.Year.Equals(parameters.Year));
        entities = entities.Where(e => e.DoctorId.Equals(parameters.DoctorId));
        return entities;
    }

    public static IQueryable<Appointment> TimeSlotsFilter(this IQueryable<Appointment> entities, TimeSlotParameters parameters)
    {
        entities = entities.Where(e => e.DateTime.Day.Equals(parameters.Day));
        entities = entities.Where(e => e.DateTime.Month.Equals(parameters.Month));
        entities = entities.Where(e => e.DateTime.Year.Equals(parameters.Year));
        entities = entities.Where(e => e.DoctorId.Equals(parameters.DoctorId));
        return entities;
    }
}
