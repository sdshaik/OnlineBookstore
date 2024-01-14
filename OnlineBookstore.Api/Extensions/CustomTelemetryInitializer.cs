using Microsoft.ApplicationInsights.Channel;
using System.Reflection;

namespace OnlineBookstore.Api.Extensions
{
    public class CustomTelemetryInitializer
    {
        private readonly IConfiguration _configuration;
        public CustomTelemetryInitializer(IConfiguration config)
        {
            _configuration = config;
        }

        public void Initialize(ITelemetry telemetry)
        {
            var roleName = "OnlineBookStore";
            telemetry.Context.Cloud.RoleName = roleName;
            telemetry.Context.Component.Version = Assembly.GetEntryAssembly()
            ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        }
    }
}
