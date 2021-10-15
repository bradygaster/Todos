using Microsoft.EntityFrameworkCore;

namespace Todos.API
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<Todo> Todos => Set<Todo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
#if DEBUG
            modelBuilder.Entity<Todo>().HasData(
                new Todo { Id = 10, Title = "Read each of the todo items out loud. Then, mark this task complete and start completing the other items." },
                new Todo { Id = 20, Title = "Create a new todo item." },
                new Todo { Id = 30, Title = "Mark the new todo item complete." },
                new Todo { Id = 40, Title = "Delete the todo item you created." },
                new Todo { Id = 50, Title = "Right-click-publish the API project to Azure App Service (Windows). During the publishing phase, you'll need to create an Application Insights resource and an Azure SQL Database using Visual Studio Connected Services. When you create the SQL DB, make sure you change the connection string to be `TodoDb`, not the default `ConnectionStrings:TodoDb` setting. Once you've published the code to App Service and it has opened in the browser, mark this todo item complete."},
                new Todo { Id = 60, Title = "Add a new todo in the live site, mark it complete, and then, delete it. Then mark this todo complete."}
            );
#endif 
            base.OnModelCreating(modelBuilder);
        }
    }
}
