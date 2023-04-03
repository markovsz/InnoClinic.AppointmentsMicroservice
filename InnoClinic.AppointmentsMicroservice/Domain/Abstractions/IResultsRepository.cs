using Domain.Entities;

namespace Domain.Abstractions;

public interface IResultsRepository
{
    Task CreateAsync(Result entity);
    Task<Result> GetByIdAsync(Guid resultId);
    Task<bool> Exists(Guid id);
    void Update(Result result);
}
