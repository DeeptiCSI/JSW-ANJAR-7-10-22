using CSI_PPMS.ColdLevellerDb;
using CSI_PPMS.DownCoilerDb;
using CSI_PPMS.Entity;
using CSI_PPMS.IServices;
using CSI_PPMS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CSI_PPMS
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
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
#endif
            services.AddDbContext<PPMSContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Default Connection"))
                   .EnableSensitiveDataLogging()
                   .EnableDetailedErrors());

            services.AddDbContext<ColdLevellerContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ColdLevellerDb"))
                   .EnableSensitiveDataLogging()
                   .EnableDetailedErrors());

            services.AddDbContext<DownCoilerContext>(options =>
            options.UseOracle(Configuration.GetConnectionString("DownCoilerDb"))
                   .EnableSensitiveDataLogging()
                   .EnableDetailedErrors());

            services.AddTransient<IAccountServices, AccountServices>();
            services.AddTransient<ISAPServices, SAPServices>();
            services.AddTransient<IPlateServices, PlateServices>();
            services.AddTransient<ITCPServices, TCPServices>();
            services.AddTransient<IReportServices, ReportServices>();
            services.AddTransient<IExcellServices, ExcellServices>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHost hostBuilder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Sends mail to the developer if there is an internal server error along with api name.
            else
            {
                var _exceptionMail = ActivatorUtilities.CreateInstance<EmailServices>(hostBuilder.Services);

                app.UseExceptionHandler(options =>
                {
                    options.Run(
                        async context =>
                        {
                            var ex = context.Features.Get<IExceptionHandlerFeature>();
                            if (ex != null)
                            {
                                _exceptionMail.SendExceptionMail(ex, context);
                            }
                        });
                });
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Accounts}/{action=Login}/{id?}");
            });
        }
    }
}
