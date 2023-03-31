namespace Domain.Abstractions;

public interface IRepositoryManager
{
    IAppointmentsRepository Appointments { get; }
    IResultsRepository Results { get; }
    Task SaveChangesAsync();
}
