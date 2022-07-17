using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Onion.Infrastructure.Data;
using Onion.Core.Interfaces;
using Onion.Infrastructure.Repository;
using Onion.Core.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Onion.Core.Services;
using Onion.Core.Mappers;
using Onion.Web.Mappers;

namespace Onion.Web
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

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // check path!!
            /*services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
                            options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login"));*/

            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddScoped<IEmployee, EmployeeService>();
            services.AddScoped<IDepartment, DepartmentService>();
            services.AddSingleton(typeof(IMapper), typeof(Mapper));
            services.AddSingleton(typeof(WebMapper));
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
