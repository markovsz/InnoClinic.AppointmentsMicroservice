using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;

namespace Application.Abstractions;

public interface IResultsService
{
    Task<Guid> CreateAsync(ResultIncomingDto incomngDto);
    Task ApproveAsync(Guid id);
    Task<ResultOutgoingDto> GetByIdAsync(Guid id);
    Task<byte[]> GetAsPdfAsync(Guid id);
    Task UpdateAsync(Guid id, UpdateResultIncomingDto incomingDto);
}
