using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
{
	protected readonly RepositoryContext _context;

	public BaseRepository(RepositoryContext context)
	{
		_context = context;
	}

    public async Task CreateAsync(TEntity entity) =>
        await _context.AddAsync(entity);

    public async Task<IEnumerable<TEntity>> GetAsync() =>
        await _context.Set<TEntity>()
            .ToListAsync();

    public async Task<TEntity> GetByIdAsync(Guid id) =>
        await _context.Set<TEntity>()
            .Where(e => e.Id.Equals(id))
            .FirstOrDefaultAsync();

    public async Task<bool> Exists(Guid id) =>
        await _context.Set<TEntity>()
            .Where(e => e.Id.Equals(id))
            .AnyAsync();

    public void Update(TEntity entity) =>
        _context.Update(entity);

    public void Delete(TEntity entity) =>
        _context.Remove(entity);
}
