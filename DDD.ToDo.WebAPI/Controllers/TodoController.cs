using DDD.ToDo.Application.Dto;
using DDD.ToDo.Application.Services.Todo;
using Microsoft.AspNetCore.Mvc;

namespace DDD.ToDo.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos(CancellationToken cancellationToken = default)
    {
        var todos = await _todoService.GetTodosAsync(cancellationToken);
        if (todos == null)
        {
            return BadRequest();
        }
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var todo = await _todoService.GetTodoByIdAsync(id, cancellationToken);
        if (todo == null)
        {
            return BadRequest();
        }
        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo([FromBody] CreateTodoDto createTodoDto, CancellationToken cancellationToken = default)
    {
        var todo = await _todoService.CreateTodoAsync(createTodoDto, cancellationToken);
        if (todo == null)
        {
            return BadRequest();
        }
        return Ok(todo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo([FromRoute] Guid id, 
        [FromBody] CreateTodoDto updateTodoDto, 
        CancellationToken cancellationToken = default)
    {
        var todo = await _todoService.UpdateTodoAsync(id, updateTodoDto, cancellationToken);
        if (todo == null)
        {
            return BadRequest();
        }
        return Ok(todo);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        await _todoService.DeleteTodoAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> ChangeStatus([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await _todoService.ChangeStatusAsync(id, cancellationToken);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
}