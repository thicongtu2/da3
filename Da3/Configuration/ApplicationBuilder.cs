using System;
using System.Linq;
using Da3.Core.Entities;
using Da3.Core.EventSource.Event;
using Da3.Core.EventSource.Handler;
using Da3.Core.Role;
using Da3.Infrastructure.Authentication;
using Da3.Infrastructure.Database;
using Da3.Share.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Da3.Configuration
{
    public static class ApplicationBuilder
    {
        public static IApplicationBuilder CustomizeMvc(this IApplicationBuilder app)
        {
            app.UseStatusCodePagesWithRedirects("/home/error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();
            app.UseSession();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                RequireHeaderSymmetry = false,
                ForwardLimit = null,
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            
            return app;
        }
        
        public static IServiceCollection ConfigureCoreServices(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = c => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.ConsentCookie.HttpOnly = true;
                options.ConsentCookie.Name = "COOKIE_CONSENT";
            });

            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = "X-CSRF-TOKEN";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Expiration = TimeSpan.FromHours(3);
                options.FormFieldName = "X-Anti-Forgery";
            });
            
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "AUTH_TOKEN";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.ExpireTimeSpan = TimeSpan.FromDays(4);
                options.SlidingExpiration = true;
            });
            services.Replace(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(ApplicationLogger<>)));

            services.Configure<CookieOptions>(options =>
            {
                options.MaxAge = TimeSpan.FromDays(2);
            });
            
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });

            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = 8192;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.ConsentCookie.Name = "ConsentCookie";
            });

            services.Configure<CookieTempDataProviderOptions>(options => options.Cookie.Name = "MyTempDataCookie");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "dd/MM/yyyy";
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddAuthorization(options =>
            {
                var restricted = new[] { RoleConstants.Admin };

                options.AddPolicy(PolicyRule.Restricted, policy => policy.RequireRole(restricted));
                options.AddPolicy(PolicyRule.Employer, policy =>
                {
                    policy.RequireRole(restricted.Append(RoleConstants.Employer));
                });
                options.AddPolicy(PolicyRule.Employee, policy =>
                {
                    policy.RequireRole(restricted.Append(RoleConstants.Employee));
                });
            });
            
            return services;
        }

        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services,  IConfiguration configuration)
        {
            var applicationString = configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(applicationString,
                    dbOptions =>
                    {
                        dbOptions.EnableRetryOnFailure(
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null
                        );
                    });
            });
            services.AddEventBus(builder =>
            {
                builder.AddInMemoryEventBus(subscriber =>
                {
                    subscriber.Subscribe<ApplyJobEvent, ApplyJobEventHandler>();
                    subscriber.Subscribe<UpNewJobEvent, UpNewJobEventHandler>();
                });
            });
            services.AddMemoryCache();

            services.AddIdentity<Da3User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddUserStore<ApplicationUserStore>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddUserManager<ApplicationUserManager>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}