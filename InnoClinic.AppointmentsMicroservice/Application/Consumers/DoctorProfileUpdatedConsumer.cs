using Application.Abstractions;
using InnoClinic.SharedModels.Messages;
using MassTransit;

namespace Application.Consumers;

public class DoctorProfileUpdatedConsumer : IConsumer<DoctorProfileUpdatedMessage>
{
    private readonly IAppointmentsService _appointmentsService;

    public DoctorProfileUpdatedConsumer(IAppointmentsService appointmentsService)
    {
        _appointmentsService = appointmentsService;
    }

    public async Task Consume(ConsumeContext<DoctorProfileUpdatedMessage> context)
    {
        await _appointmentsService.UpdateDoctorProfileAsync(context.Message);
    }
}
