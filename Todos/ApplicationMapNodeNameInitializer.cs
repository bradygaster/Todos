using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Todos;

namespace Todos
{
    public class ApplicationMapNodeNameInitializer : ITelemetryInitializer
    {
        public ApplicationMapNodeNameInitializer(IConfiguration configuration)
        {
            Name = configuration["ApplicationMapNodeName"];
        }

        public string Name { get; set; }

        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Cloud.RoleName = Name;
        }
    }
}

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationMapName(this IServiceCollection services, IConfiguration configuration)
        {
            if(!string.IsNullOrEmpty(configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]))
            {
                services.AddApplicationInsightsTelemetry();
            }
            else
            {
                services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions
                {
                    ConnectionString = configuration["APPINSIGHTS_CONNECTIONSTRING"]
                });
            }
            
            services.AddSingleton<ITelemetryInitializer, ApplicationMapNodeNameInitializer>();
        }
    }
}
