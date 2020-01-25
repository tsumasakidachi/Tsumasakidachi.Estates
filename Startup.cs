using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetEnv;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tsumasakidachi.Estates.Dtos;
using Tsumasakidachi.Estates.Entities;
using Tsumasakidachi.Estates.EntityMappers;
using Tsumasakidachi.Estates.Repositories;

namespace Tsumasakidachi.Estates
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient("Tsumasakidachi", c =>
            {
                c.BaseAddress = new Uri(Env.GetString("REPOS_URL"));
            });
            services.AddSingleton<IRepository<File>, FileRepository>();
            services.AddSingleton<IEntityMapper<FileDto, File>, FileEntityMapper>();
            services.AddSingleton<IRepository<Structure>, StructureRepository>();
            services.AddSingleton<IEntityMapper<StructureDto, Structure>, StructureEntityMapper>();
            services.AddSingleton<IRepository<Market>, MarketRepository>();
            services.AddSingleton<IEntityMapper<MarketDto, Market>, MarketEntityMapper>();
            services.AddSingleton<IEntityMapper<ServiceDto, Service>, ServiceEntityMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Env.Load();

            app.UsePathBase("/estates");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
