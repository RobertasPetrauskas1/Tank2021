using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tank2021.Hubs;
using Tank2021SharedContent;
using Tank2021SharedContent.Command;
using Tank2021SharedContent.Facade;

namespace Tank2021Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            var hubContext = app.ApplicationServices.GetService<IHubContext<TankHub>>();
            FacadeSingleton.setFacade(new Facade());
            MapControllerSingleton.setMap(new MapController(hubContext));
            CommandInvokerSingleton.SetPlayer1Invoker(new Invoker());
            CommandInvokerSingleton.SetPlayer2Invoker(new Invoker());       
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<TankHub>("/tankhub");
            });
        }
    }
}
