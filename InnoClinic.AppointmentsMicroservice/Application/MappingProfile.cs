using AutoMapper;
using Domain.Entities;
using InnoClinic.SharedModels.DTOs.Appointments.Incoming;
using InnoClinic.SharedModels.DTOs.Appointments.Outgoing;

namespace Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppointmentIncomingDto, Appointment>();
        CreateMap<Appointment, PatientAppointmentsOutgoingDto>()
            .ForMember(e => e.ResultId, opt => opt.MapFrom(src => src.Result.Id));
        CreateMap<Appointment, AppointmentByReceptionistOutgoingDto>();
        CreateMap<Appointment, AppointmentScheduleByDoctorOutgoingDto>()
            .ForMember(e => e.ResultId, opt => opt.MapFrom(src => src.Result.Id));
        CreateMap<Appointment, TimeSlotAppointmentOutgoingDto>();
        CreateMap<Appointment, AppointmentForResultOutgoingDto>();
        CreateMap<ResultIncomingDto, Result>();
        CreateMap<Result, ResultOutgoingDto>();
    }
}
