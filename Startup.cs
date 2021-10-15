using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Tracker.Helpers;
using Tracker.Interfaces;
using Tracker.Models;
using Tracker.Services;

namespace Tracker
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
            services.AddCors();

            services.AddControllersWithViews().AddNewtonsoftJson(jsonOptions =>
            {
                jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddDbContext<TrackerContext>(opt =>
                        opt.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<JwtService>();

           // services.AddControllers().AddNewtonsoftJson();
            //services.AddControllersWithViews().AddNewtonsoftJson();
            //services.AddRazorPages().AddNewtonsoftJson();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseSpaStaticFiles();



            app.UseRouting();
            app.UseCors(options => options
                                //// specifying allowed FE interfaces
                                //.WithOrigins("http://localhost:3000/", // react front end
                                //             "http://localhost:8080/", // mvc
                                //             "http://localhost:4200/")  // angular
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
