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
}
