using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ResultsRepository : BaseRepository<Result>, IResultsRepository
{
    public ResultsRepository(RepositoryContext context)
		: base(context)
	{
	}

    public override async Task<Result> GetByIdAsync(Guid id) =>
        await _context.Results
            .Include(e => e.Appointment)
            .Where(e => e.Id.Equals(id))
            .FirstOrDefaultAsync();
}
