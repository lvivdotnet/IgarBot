using GuidBot.Configurations;
using GuidBot.Helpers;
using GuidBot.Middlewares;
using GuidBot.TelegramServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace GuidBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddApplicationPart(typeof(Startup).Assembly)
                .AddNewtonsoftJson(options => options.SerializerSettings.Formatting = Formatting.Indented);

            services.AddControllers();

            services.AddHostedService<TelegramHostedService>();

            services.AddScoped<TelegramMessageHandler>();

            var config = Configuration.GetOptions<BotConfiguration>("BotConfiguration");
            services.AddSingleton(config);
            services.AddTelegram(config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<PingMiddleware>();
            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}