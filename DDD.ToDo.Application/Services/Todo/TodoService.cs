using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.ToDo.Application.Dto;
using DDD.ToDo.Infrastructure.Repositories.Todo;

namespace DDD.ToDo.Application.Services.Todo
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<List<TodoDto>> GetTodosAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var todos = await _todoRepository.GetAsync(cancellationToken);
                if (todos is {Count: < 1})
                {
                    return [];
                }
                return todos
                    .Select(todo => new TodoDto(todo.Id, todo.Title, todo.Description, todo.IsDone)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TodoDto> GetTodoByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var todo = await _todoRepository.GetByIdAsync(id, false, cancellationToken);
                if (todo == null)
                {
                    return null;
                }

                return new TodoDto(todo.Id, todo.Title, todo.Description, todo.IsDone);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TodoDto> CreateTodoAsync(CreateTodoDto createTodoDto, 
                                        CancellationToken cancellationToken = default)
        {
            try
            {
                var todoObject = Domain.Entities.Todo.Create(createTodoDto.Title,
                    createTodoDto.Description);
                var todo = await _todoRepository.CreateAsync(todoObject, cancellationToken);
                if (todo == null)
                {
                    return null;
                }

                await _todoRepository.SaveChangesAsync(cancellationToken);

                return new TodoDto(todo.Id, todo.Title, todo.Description, todo.IsDone);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TodoDto> UpdateTodoAsync(Guid id, 
            CreateTodoDto updateTodoDto, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await _todoRepository.GetByIdAsync(id, true, cancellationToken);
                if (entity is null)
                {
                    return null;
                }

                entity.Title = updateTodoDto.Title;
                entity.Description = updateTodoDto.Description;

                var todo = await _todoRepository.UpdateAsync(entity, cancellationToken);

                if (todo is null)
                {
                    return null;
                }
                await _todoRepository.SaveChangesAsync(cancellationToken);
                return new TodoDto(todo.Id, todo.Title, todo.Description, todo.IsDone);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task DeleteTodoAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await _todoRepository.GetByIdAsync(id, true, cancellationToken);
                if (entity is null)
                {
                    return;
                }
                await _todoRepository.DeleteAsync(entity, cancellationToken);
                await _todoRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public async Task<bool> ChangeStatusAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await _todoRepository.GetByIdAsync(id, true, cancellationToken);
                if (entity is null)
                {
                    return false;
                }

                entity.ChangeStatus();
                await _todoRepository.UpdateAsync(entity, cancellationToken);
                await _todoRepository.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
