using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace SubtextMatching
{
    using SubtextMatching.Source.Domain.Interfaces;
    using SubtextMatching.Source.Infrastructure;
    using SubtextMatching.Source.Utilities;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SubtextMatching", Version = "v1" });
            });
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IMatchingAlgorithmFactory, MatchingAlgorithmFactory>();
        }

        public void Configure(IApplicationBuilder app, ILoggerProvider loggerProvider)
        {
            app.ConfigureExceptionHandler(loggerProvider.CreateLogger("default"));
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SubtextMatching v1"));
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
