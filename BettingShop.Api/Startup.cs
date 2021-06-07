using System;
using BettingShop.DataLayer.DB;
using BettingShop.DataLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BettingShop.Api
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
            /*services.AddDbContext<BetEventContext>(opt =>
                opt.UseInMemoryDatabase("TodoList"));
            services.AddDbContext<BetContext>(opt =>
                opt.UseInMemoryDatabase("TodoList"));
            services.AddDbContext<UserContext>(opt =>
                opt.UseInMemoryDatabase("TodoList"));*/
            services.AddControllers();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IBetRepository, BetRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDeserializer, Deserializer>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}