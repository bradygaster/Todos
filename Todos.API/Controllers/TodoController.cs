using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todos.API.Controllers
{
    [ApiController]
    public class TodoController : ControllerBase
    {
        TodoDbContext dbContext;

        public TodoController(TodoDbContext db)
        {
            dbContext = db;
            dbContext.Database.EnsureCreated();
        }

        [HttpPost("todos", Name = "CreateTodo")]
        public async Task<ActionResult<Todo>> Post([FromBody] Todo todo)
        {
            dbContext.Todos.Add(todo);
            await dbContext.SaveChangesAsync();

            return Created($"/todo/{todo.Id}", todo);
        }

        [HttpGet("todos", Name = "GetAllTodos")]
        public async Task<IEnumerable<Todo>> Get()
        {
            return await dbContext.Todos.ToListAsync();
        }

        [HttpGet("todo/{id}", Name = "GetTodo")]
        public async Task<ActionResult<Todo>> Get(int id)
        {
            var todo = await dbContext.Todos.FirstOrDefaultAsync(_ => _.Id == id);
            if(todo == null) return NotFound();
            return Ok(todo);
        }

        [HttpPut("todo/{id}", Name = "UpdateTodo")]
        public async Task<ActionResult<Todo>> Put(int id, [FromBody] Todo todo)
        {
            var existing = await dbContext.Todos.FirstOrDefaultAsync(_ => _.Id == id);
            if (existing == null) return NotFound();

            existing.Title = todo.Title;
            existing.IsCompleted = todo.IsCompleted;

            dbContext.Update(existing);
            await dbContext.SaveChangesAsync();

            return Accepted($"/todo/{id}", todo);
        }

        [HttpDelete("todo/{id}", Name = "DeleteTodo")]
        public async Task Delete(int id)
        {
            var existing = await dbContext.Todos.FirstOrDefaultAsync(_ => _.Id == id);
            if (existing == null) return;
            dbContext.Todos.Remove(existing);
            await dbContext.SaveChangesAsync();
        }
    }
}
