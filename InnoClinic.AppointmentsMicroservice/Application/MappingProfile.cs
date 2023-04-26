using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;
using AutoMapper;
using Domain.Entities;

namespace Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppointmentIncomingDto, Appointment>();
        CreateMap<Appointment, PatientAppointmentsOutgoingDto>();
        CreateMap<Appointment, AppointmentByReceptionistOutgoingDto>();
        CreateMap<Appointment, AppointmentScheduleByDoctorOutgoingDto>()
            .ForMember(e => e.ResultId, opt => opt.MapFrom(src => src.Result.Id));
        CreateMap<Appointment, AppointmentForResultOutgoingDto>();
        CreateMap<ResultIncomingDto, Result>();
        CreateMap<Result, ResultOutgoingDto>();
    }
}
