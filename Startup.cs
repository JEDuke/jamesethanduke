using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace jamesethanduke
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
            services.AddControllersWithViews();

            services.AddAuthentication()
            .AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection =
                    Configuration.GetSection("Authentication:Google");

                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
            });

            // services.AddAuthentication()
            // .AddMicrosoftAccount(options => 
            // {
            //     IConfigurationSection msftAuthNSection = 
            //         Configuration.GetSection("Authentication:Microsoft");

            //     options.ClientId = msftAuthNSection["ClientId"];
            //     options.ClientSecret = msftAuthNSection["ClientSecret"];
            // });

                // services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
                // {
                //     microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"];
                //     microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
                // });

        // services.AddAuthentication()
        //     .AddMicrosoftAccount(microsoftOptions => { ... })
        //     .AddGoogle(googleOptions => { ... })
        //     .AddTwitter(twitterOptions => { ... })
        //     .AddFacebook(facebookOptions => { ... });

        // services.AddDbContext<ApplicationDbContext>(options =>
        //     options.UseSqlServer(
        //         Configuration.GetConnectionString("DefaultConnection")));
        // services.AddDefaultIdentity<IdentityUser>(options =>
        //     options.SignIn.RequireConfirmedAccount = true)
        //         .AddEntityFrameworkStores<ApplicationDbContext>();

                // services.AddAuthorization(options => options..AuthorizationPolicy))
        //         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        // .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => Configuration.Bind("JwtSettings", options))
        // .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => Configuration.Bind("CookieSettings", options));

        //      services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        // .AddEntityFrameworkStores<ApplicationDbContext>();

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
                app.UseExceptionHandler("/About/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            // app.UseAuthorization();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=About}/{action=Index}/{id?}");
            });
        }
    }
}
