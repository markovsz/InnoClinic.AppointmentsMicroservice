using Domain.Abstractions;
using Infrastructure.Repositories;

namespace Infrastructure;

public class RepositoryManager : IRepositoryManager
{
    private IAppointmentsRepository _appointmentsRepository;
    private IResultsRepository _resultsRepository;
    private readonly RepositoryContext _context;


    public RepositoryManager(RepositoryContext context)
    {
        _context = context;
    }

    public IAppointmentsRepository Appointments
    {
        get
        {
            if(_appointmentsRepository is null)
                _appointmentsRepository = new AppointmentsRepository(_context);
            return _appointmentsRepository;
        }
    }

    public IResultsRepository Results
    {
        get
        {
            if (_resultsRepository is null)
                _resultsRepository = new ResultsRepository(_context);
            return _resultsRepository;
        }
    }

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}
