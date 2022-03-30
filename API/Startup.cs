using API.Configurations;
using BAL.Core;
using Common.DataContext;
using DTO.Models;
using ExceptionHandling.DependencyResolver;
using ExceptionHandling.ExceptionManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        }

        public IConfiguration Configuration { get; }
        readonly string _corsPolicy = "_headless.cors";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.SwaggerConfiguration();
            services.CorsConfiguration(Configuration, _corsPolicy);
            services.ConfigureDI(Configuration);
            services.ExceptionDIResolver();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(Configuration.GetConnectionString("DefaultConnectionLocal")), ServiceLifetime.Scoped);

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".UserManagement.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(3);
                options.Cookie.IsEssential = true;
            });

            services.ConfigureApplicationCookie(sessionConfig =>
            {
                sessionConfig.ExpireTimeSpan = TimeSpan.FromHours(24);
                sessionConfig.SlidingExpiration = true;
            });
            services.ConfigureAuth(Configuration);

            services.AddControllers().AddNewtonsoftJson(options =>
                       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                       );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext, 
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(_corsPolicy);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(env.ContentRootPath, "assets")),
                RequestPath = "/assets"
            });
            //Enable directory browsing
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //                Path.Combine(env.ContentRootPath, "assets")),
            //    RequestPath = "/assets"
            //});
            app.UseSwagger();
            app.UseSwaggerUI(o =>

            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "HeadlessCMS");
            });
            
            app.ConfigureExceptionMiddleware();
            app.UseHttpsRedirection(); 
            app.UseAuthentication();

            app.UseRouting();  
            app.UseAuthorization();  
            app.UseSession();

            Task.Run(() => Seed.SeedDefaultAuthUser(serviceProvider)); // call this class to seed default values for the first time.

            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });  
        }

    }
}