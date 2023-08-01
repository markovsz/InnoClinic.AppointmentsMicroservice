using Domain.Entities;
using Domain.RequestParameters;

namespace Infrastructure.Extensions;

public static class FiltrationExtensions
{
    public static IQueryable<Appointment> AppointmentsFilter(this IQueryable<Appointment> entities, AppointmentParameters parameters)
    {
        if(parameters.Date.HasValue)
            entities = entities.Where(e => e.Date.Equals(parameters.Date));

        if(parameters.DoctorFirstName is not null)
            entities = entities.Where(e => e.DoctorFirstName.Contains(parameters.DoctorFirstName));

        if(parameters.DoctorLastName is not null)
            entities = entities.Where(e => e.DoctorLastName.Contains(parameters.DoctorLastName));

        if(parameters.DoctorMiddleName is not null)
            entities = entities.Where(e => e.DoctorMiddleName.Contains(parameters.DoctorMiddleName));

        if(parameters.ServiceName is not null)
            entities = entities.Where(e => e.ServiceName.Contains(parameters.ServiceName));
        
        if(parameters.IsApproved.HasValue)
            entities = entities.Where(e => e.IsApproved.Equals(parameters.IsApproved));

        if(parameters.OfficeId.HasValue)
            entities = entities.Where(e => e.OfficeId.Equals(parameters.OfficeId));

        if (parameters.IsApproved.HasValue)
            entities = entities.Where(e => e.IsApproved.Equals(parameters.IsApproved));
        return entities;
    }

    public static IQueryable<Appointment> ScheduleFilter(this IQueryable<Appointment> entities, ScheduleParameters parameters)
    {
        entities = entities.Where(e => e.PatientId.Equals(parameters.PatientId));

        if (parameters.Day.HasValue)
            entities = entities.Where(e => e.Date.Day.Equals(parameters.Day));

        if (parameters.Month.HasValue)
            entities = entities.Where(e => e.Date.Month.Equals(parameters.Month));

    public static IQueryable<Appointment> TimeSlotsFilter(this IQueryable<Appointment> entities, TimeSlotParameters parameters)
    {
        entities = entities.Where(e => e.DateTime.Day.Equals(parameters.Day));
        entities = entities.Where(e => e.DateTime.Month.Equals(parameters.Month));
        entities = entities.Where(e => e.DateTime.Year.Equals(parameters.Year));
        entities = entities.Where(e => e.DoctorId.Equals(parameters.DoctorId));
        return entities;
    }
}
