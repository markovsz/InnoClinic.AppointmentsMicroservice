using Application.Abstractions;
using InnoClinic.SharedModels.Messages;
using MassTransit;

namespace Application.Consumers;

public class ServiceUpdatedConsumer : IConsumer<ServiceUpdatedMessage>
{
    private readonly IAppointmentsService _appointmentsService;

    public ServiceUpdatedConsumer(IAppointmentsService appointmentsService)
    {
        _appointmentsService = appointmentsService;
    }

    public async Task Consume(ConsumeContext<ServiceUpdatedMessage> context)
    {
        await _appointmentsService.UpdateServiceNameAsync(context.Message);
    }
}
