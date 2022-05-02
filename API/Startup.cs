using API.Dbcontext; 
using BAL.DependencyResolver;
using DTO.UserMaster;
using ExceptionHandling.DependencyResolver;
using ExceptionHandling.ExceptionManagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;

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
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseMySQL(
                     Configuration.GetConnectionString("DefaultConnection"),
                     b => b.MigrationsAssembly("DiseaseMIS.BAL")), ServiceLifetime.Scoped);

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
           .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
             
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0); not needed in 3.1
            //services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            //Dependencies resolver
            services.DIBALResolver();
            services.ExceptionDIResolver();
            services.AddSwaggerGen();
            services.AddDistributedMemoryCache();
              
            //===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["JwtIssuer"],
                    ValidAudience = Configuration["JwtIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                    ClockSkew = TimeSpan.Zero // remove delay of token when expire
                };
            });

            services.AddControllers().AddNewtonsoftJson(options =>
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                   );// for 3.1

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(o =>

                {
                    o.SwaggerEndpoint("/swagger/v1/swagger.json", "<Project name>"); // add project name
                });
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           
            app.UseCors(MyAllowSpecificOrigins);
           // app.ConfigureExceptionMiddleware();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication(); 
            app.UseAuthorization(); 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });  
        }

    }
}