using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.ToDo.Application.Dto;

namespace DDD.ToDo.Application.Services.Todo
{
    public interface ITodoService
    {
        Task<List<TodoDto>> GetTodosAsync(CancellationToken cancellationToken = default);
        Task<TodoDto> GetTodoByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TodoDto> CreateTodoAsync(CreateTodoDto createTodoDto, CancellationToken cancellationToken = default);
        Task<TodoDto> UpdateTodoAsync(Guid id, CreateTodoDto updateTodoDto, CancellationToken cancellationToken = default);
        Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ChangeStatusAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
