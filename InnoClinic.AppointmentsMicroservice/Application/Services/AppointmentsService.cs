using Application.Abstractions;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.RequestParameters;
using InnoClinic.SharedModels.DTOs.Appointments.Incoming;
using InnoClinic.SharedModels.DTOs.Appointments.Outgoing;
using InnoClinic.SharedModels.Messages;

namespace Application.Services;

public class AppointmentsService : IAppointmentsService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public AppointmentsService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task ApproveAsync(Guid id)
    {
        var entity = await _repositoryManager.Appointments.GetByIdAsync(id);
        if (entity is null)
            throw new EntityNotFoundException();
        entity.IsApproved = true;
        _repositoryManager.Appointments.Update(entity);
        await _repositoryManager.SaveChangesAsync();
    }

    public async Task<Guid> CreateAsync(AppointmentIncomingDto incomngDto)
    {
        var entity = _mapper.Map<Appointment>(incomngDto);
        entity.Id = Guid.NewGuid();
        entity.IsApproved = false;
        await _repositoryManager.Appointments.CreateAsync(entity);
        await _repositoryManager.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entity = await _repositoryManager.Appointments.GetByIdAsync(id);
        if (entity is null)
            throw new EntityNotFoundException();
        _repositoryManager.Appointments.Delete(entity);
        await _repositoryManager.SaveChangesAsync();
    }

    public async Task<IEnumerable<PatientAppointmentsOutgoingDto>> GetAsync(Guid patientId)
    {
        var entities = await _repositoryManager.Appointments.GetAsync(patientId);
        var mappedEntities = _mapper.Map<IEnumerable<PatientAppointmentsOutgoingDto>>(entities);
        return mappedEntities;
    }

    public async Task<IEnumerable<AppointmentByReceptionistOutgoingDto>> GetByReceptionistAsync(AppointmentParameters parameters)
    {
        var entities = await _repositoryManager.Appointments.GetByReceptionistAsync(parameters);
        var mappedEntities = _mapper.Map<IEnumerable<AppointmentByReceptionistOutgoingDto>>(entities);
        return mappedEntities;
    }

    public async Task<IEnumerable<AppointmentScheduleByDoctorOutgoingDto>> GetScheduleByDoctorAsync(ScheduleParameters parameters)
    {
        var entities = await _repositoryManager.Appointments.GetScheduleByDoctorAsync(parameters);
        var mappedEntities = _mapper.Map<IEnumerable<AppointmentScheduleByDoctorOutgoingDto>>(entities);
        return mappedEntities;
    }

    public async Task RescheduleAsync(Guid id, RescheduleAppointmentIncomingDto incomingDto)
    {
        var entity = await _repositoryManager.Appointments.GetByIdAsync(id);
        if (entity is null)
            throw new EntityNotFoundException();
        entity.Date = incomingDto.Date;
        entity.Time = incomingDto.Time;
        entity.DoctorId = incomingDto.DoctorId;
        entity.DoctorFirstName = incomingDto.DoctorFirstName;
        entity.DoctorLastName = incomingDto.DoctorLastName;
        entity.DoctorMiddleName = incomingDto.DoctorMiddleName;
        _repositoryManager.Appointments.Update(entity);
        await _repositoryManager.SaveChangesAsync();
    }

    public async Task UpdateDoctorProfileAsync(DoctorProfileUpdatedMessage message)
    {
        await _repositoryManager.Appointments.UpdateDoctorProfileAsync(message.Id, message.DoctorFirstName, message.DoctorLastName, message.DoctorMiddleName);
        await _repositoryManager.SaveChangesAsync();
    }

    public async Task UpdateServiceNameAsync(ServiceUpdatedMessage message)
    {
        await _repositoryManager.Appointments.UpdateServiceNameAsync(message.Id, message.Name);
        await _repositoryManager.SaveChangesAsync();
    }
}
