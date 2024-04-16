using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.ToDo.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DDD.ToDo.Infrastructure.Repositories.Todo
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;

        public TodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.Todo>> GetAsync(CancellationToken cancellationToken = default)
        {
            var todos = await _context.Todos
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return todos;
        }

        public async Task<Domain.Entities.Todo> GetByIdAsync(Guid id, bool isTracked = true,
            CancellationToken cancellationToken = default)
        {
            var todo = isTracked
                ? await _context.Todos
                            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                : await _context.Todos
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return todo;
        }

        public async Task<Domain.Entities.Todo> CreateAsync(Domain.Entities.Todo todo,
            CancellationToken cancellationToken = default)
        {
            var todoResult = await _context.Todos.AddAsync(todo, cancellationToken);
            return todoResult.Entity;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<Domain.Entities.Todo> UpdateAsync(Domain.Entities.Todo entity,
            CancellationToken cancellationToken = default)
        {
            var todo = _context.Todos.Update(entity);
            return Task.FromResult(todo.Entity);
        }

        public Task DeleteAsync(Domain.Entities.Todo entity, CancellationToken cancellationToken = default)
        {
            _context.Todos.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
