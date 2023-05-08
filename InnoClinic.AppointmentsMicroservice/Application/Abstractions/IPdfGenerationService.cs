using InnoClinic.SharedModels.DTOs.Appointments.Outgoing;

namespace Application.Abstractions;

public interface IPdfGenerationService
{
    byte[] Generate(ResultOutgoingDto outgoingDto);
}
