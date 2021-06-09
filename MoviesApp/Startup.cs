using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoviesApp.MovieModals;
using MoviesApp.Service;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using MoviesApp.JwtToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MoviesApp
{
    public class Startup
    {
        public IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../ClientApp/dist";
            });
            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
            //services.AddScoped<UserManager<IdentityUser>>();
            //services.AddScoped<RoleManager<IdentityRole>>();
            //services.AddScoped<SignInManager<IdentityUser>>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<MoviesDBContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(cfg => {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Token:Issuer"]
                };
            });
            services.AddAuthorization();
            if (!_env.IsDevelopment())
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
                    options.HttpsPort = 443;
                });
            }
            else
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
                    options.HttpsPort = 5001;
                });
            }
            services.AddDbContextPool<MoviesDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MoviesDB"));
            });
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IStillsService, StillsService>();
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
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
                app.UseHsts();
            }

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();

            }
            else
            {

            }
           
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
