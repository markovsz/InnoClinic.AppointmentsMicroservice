using Application.DTOs.Outgoing;

namespace Application.Abstractions;

public interface IPdfGenerationService
{
    byte[] Generate(ResultOutgoingDto outgoingDto);
}
