using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Todos
{
    public interface ITodoApiClient
    {
        [Get("/todos")]
        Task<IEnumerable<Todo>> GetTodos();

        [Get("/todo/{id}")]
        Task<Todo> GetTodo(int id);

        [Post("/todos")]
        Task<Todo> CreateTodo(Todo todo);

        [Delete("/todo/{id}")]
        Task DeleteTodo(int id);

        [Put("/todo/{id}")]
        Task UpdateTodo(int id, Todo todo);
    }
}
