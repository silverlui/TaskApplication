using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApplication.Data;
using TaskApplication.Data.DTOs;
using TaskApplication.Data.Models;

namespace TaskApplication.Controllers;

[Route("api/v1/todo")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly TaskApplicationDbContext _context;

    public TodoController(TaskApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/v1/Todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodos(
        DateTime lastCreatedAt, 
        int limit = 20)
    {
        var todos = await _context.Todos
            .OrderBy(t => t.CreatedAt)
            .Take(limit)
            .Where(t => t.CreatedAt > lastCreatedAt)
            .ToListAsync();
        
        return Ok(todos);
    }

    // GET: api/v1/Todo/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetTodo(Guid id)
    { 
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        return todo;
    }

    // PUT: api/v1/Todo/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodo(Guid id, UpdateTodoDto updateTodoDto)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo is null)
            return NotFound();

        _context.Entry(todo).CurrentValues.SetValues(new Todo()
        {
            Id = todo.Id,
            Description = updateTodoDto.Description,
            IsCompleted = updateTodoDto.IsCompleted ?? todo.IsCompleted,
            IsImportant = updateTodoDto.IsImportant ?? todo.IsImportant,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = DateTime.Now
        });

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoExists(id))
            {
                return NotFound();
            }
        }

        return Ok(todo);
    }

    // POST: api/v1/Todo
    [HttpPost]
    public async Task<ActionResult<Todo>> PostTodo(CreateTodoDto createTodoDto)
    {
        var todo = new Todo()
        {
            Id = Guid.NewGuid(),
            Description = createTodoDto.Description,
            IsImportant = false,
            IsCompleted = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
    }

    // DELETE: api/v1/Todo/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null)
        {
            return NotFound();
        }

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    // DELETE: api/v1/Todo
    [HttpDelete]
    public async Task<IActionResult> DeleteAllTodos()
    {
        _context.Todos.RemoveRange(_context.Todos);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool TodoExists(Guid id)
    {
        return (_context.Todos?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}

