﻿using Domain.Abstractions;
using Domain.Entities;
using Domain.RequestParameters;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AppointmentsRepository : BaseRepository<Appointment>, IAppointmentsRepository
{
    public AppointmentsRepository(RepositoryContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Appointment>> GetAsync(Guid patientId) =>
        await _context.Appointments
        .OrderByDescending(e => e.Time)
        .OrderByDescending(e => e.Date)
        .Where(e => e.PatientId.Equals(patientId))
        .ToListAsync();

    public async Task<IEnumerable<Appointment>> GetByReceptionistAsync(AppointmentParameters parameters) =>
        await _context.Appointments
        .AppointmentsFilter(parameters)
        .OrderBy(e => e.ServiceName)
        .OrderBy(e => e.DoctorFirstName)
        .OrderBy(e => e.DoctorLastName)
        .OrderBy(e => e.Time)
        .ToListAsync();

    public async Task<IEnumerable<Appointment>> GetScheduleByDoctorAsync(ScheduleParameters parameters) =>
        await _context.Appointments
        .Include(e => e.Result)
        .ScheduleFilter(parameters)
        .OrderBy(e => e.Time)
        .ToListAsync();

    public async Task<bool> HasAnotherResult(Guid appointmentId) =>
        await _context.Results
        .Where(e => e.AppointmentId.Equals(appointmentId))
        .AnyAsync();

    public async Task UpdateServiceNameAsync(Guid serviceId, string serviceName)
    {
        var entities = await _context.Appointments
        .Where(e => e.ServiceId.Equals(serviceId))
        .ToListAsync();

        entities.ForEach(e => e.ServiceName = serviceName);
    }

    public async Task UpdateDoctorProfileAsync(Guid doctorId, string doctorFirstName, string doctorLastName, string doctorMiddleName)
    {
        var entities = await _context.Appointments
        .Where(e => e.DoctorId.Equals(doctorId))
        .ToListAsync();

        entities.ForEach(e =>
        {
            e.DoctorFirstName = doctorFirstName;
            e.DoctorLastName = doctorLastName;
            e.DoctorMiddleName = doctorMiddleName;
        });
    }
}
