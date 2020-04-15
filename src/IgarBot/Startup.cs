using IgarBot.Configurations;
using IgarBot.Helpers;
using IgarBot.Middlewares;
using IgarBot.TelegramServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace IgarBot
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
            // Todo : Telegram.Bot library uses newtonsoft. Find out about alternative libraries with system.text.json 
            services.AddControllers()
                .AddApplicationPart(typeof(Startup).Assembly)
                .AddNewtonsoftJson(options => options.SerializerSettings.Formatting = Formatting.Indented);

            services.AddControllers();

            services.AddHostedService<TelegramHostedService>();

            services.AddScoped<TelegramMessageHandler>();

            var config = Configuration.GetOptions<BotConfiguration>("BotConfiguration");
            services.AddSingleton(config);
            services.AddSingleton<IRepository<Igar>, SqlRepository<Igar>>();
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
