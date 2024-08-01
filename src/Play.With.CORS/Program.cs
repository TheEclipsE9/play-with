namespace Play.With.CORS
{
    public class Program
    {
        public const string CORS_POLICY_ANY = "any";
        public const string CORS_POLICY_PORT = "port";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                //doesnt work
                options.AddPolicy(
                    name: CORS_POLICY_PORT,
                    policyBuilder =>
                    {
                        policyBuilder.WithOrigins("localhost");
                        policyBuilder.AllowAnyHeader();
                        policyBuilder.AllowAnyMethod();
                        policyBuilder.AllowCredentials();
                    });

                //doesnt work
                options.AddPolicy(
                    name: CORS_POLICY_ANY,
                    policyBuilder =>
                    {
                        policyBuilder.AllowAnyOrigin();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseCors();

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/weather", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .FirstOrDefault();
                return forecast;
            }).RequireCors(CORS_POLICY_PORT);

            Console.WriteLine(DateTime.Now);
            
            app.Run();
        }
    }
}
