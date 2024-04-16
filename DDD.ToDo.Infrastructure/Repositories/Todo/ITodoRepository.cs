namespace DDD.ToDo.Infrastructure.Repositories.Todo;

public interface ITodoRepository
{
    Task<List<Domain.Entities.Todo>> GetAsync(CancellationToken cancellationToken = default);
    Task<Domain.Entities.Todo> GetByIdAsync(Guid id, bool isTracked = true, CancellationToken cancellationToken = default);
    Task<Domain.Entities.Todo> CreateAsync(Domain.Entities.Todo todo, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<Domain.Entities.Todo> UpdateAsync(Domain.Entities.Todo entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Domain.Entities.Todo entity, CancellationToken cancellationToken = default);
}