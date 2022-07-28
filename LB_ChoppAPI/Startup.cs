using LB_ChoppAPI.Repository.DAO;
using LB_ChoppAPI.Repository.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace LB_ChoppAPI
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
            services.AddTransient<IVendedor, VendedorDAO>();
            services.AddTransient<ITerminalMobile, TerminalMobileDAO>();
            services.AddTransient<IBarril, BarrilDAO>();
            services.AddTransient<IChopeira, ChopeiraDAO>();
            services.AddTransient<ICilindro, CilindroDAO>();
            services.AddTransient<IReservaChopp, ReservaChoppDAO>();
            services.AddTransient<ICliente, ClienteDAO>();
            services.AddTransient<IChopp, ChoppDAO>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LB System - Chopp", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LB System - Chopp V-1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
