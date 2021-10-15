using Microsoft.EntityFrameworkCore;

namespace Todos.API
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<Todo> Todos => Set<Todo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().HasData(
                new Todo { Id = 10, Title = "Read all of the steps below out loud." },
                new Todo { Id = 20, Title = "Add a new todo item." },
                new Todo { Id = 30, Title = "Mark the new todo item complete." },
                new Todo { Id = 40, Title = "Delete the todo item you created." },
                new Todo { Id = 42, Title = "As you create Azure resources, use whatever tool(s) or process you prefer; the Azure Portal, Visual Studio Connected Services or VS Code, or the Azure CLI. Mark this todo complete." },
                new Todo { Id = 45, Title = "As you create Azure resources, if you're using Visual Studio Connected Services, uncheck the boxes for adding NuGet packages or reconfiguring your code, other than appsettings.json or your App Service settings. Mark this todo complete."},
                new Todo { Id = 50, Title = "Create an Application Insights resource in a new resource group in Azure." },
                new Todo { Id = 60, Title = "Create a new Azure SQL Database server and database in the same resource group in which you created the Application Insights instance." },
                new Todo { Id = 70, Title = "Publish the app to Azure App Service on Windows in a new Web App in the same resource group as the Application Insights and SQL database resources." },
                new Todo { Id = 80, Title = "Add a new todo in the live site, mark it complete, and then, delete it. Then mark this todo complete."},
                new Todo { Id = 90, Title = "Delete this step, then go back to the first step, delete it, and start completing the steps from top to bottom, marking each as complete as you go." }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
