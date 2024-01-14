namespace OnlineBookstore.Api.Authentication
{
    internal static class AuthExtensions
    {

        public static void ConfigureAuthentication(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "MultiAuthSchemes";
                options.DefaultChallengeScheme = "MultiAuthSchemes";
            });
        }

        public static void EnableAuthentication(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
