using Microsoft.Extensions.Configuration;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Todos
{
    public class TodoApiClient : ITodoApiClient
    {
        private readonly HttpClient _httpClient;

        private readonly IConfiguration _configuration;

        public TodoApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiUrlBase"]);
        }

        public async Task<Todo> CreateTodo(Todo todo)
        {
            return await RestService.For<ITodoApiClient>(_httpClient).CreateTodo(todo);
        }

        public async Task DeleteTodo(int id)
        {
            await RestService.For<ITodoApiClient>(_httpClient).DeleteTodo(id);
        }

        public async Task<Todo> GetTodo(int id)
        {
            return await RestService.For<ITodoApiClient>(_httpClient).GetTodo(id);
        }

        public async Task<IEnumerable<Todo>> GetTodos()
        {
            return await RestService.For<ITodoApiClient>(_httpClient).GetTodos();
        }

        public async Task UpdateTodo(int id, Todo todo)
        {
            await RestService.For<ITodoApiClient>(_httpClient).UpdateTodo(id, todo);
        }
    }
}
