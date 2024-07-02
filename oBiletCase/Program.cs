
using oBiletCase.Business.Utilities.Api.Extensions;
using oBiletCase.Core.App;
using log4net;
using oBiletCase.Business.Utilities.Middlewares;

namespace oBiletCase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            var services = builder.Services;

            // Projeye Session yapýsýný ekler
            services.AddSession();

            // AppHttpContext için kullanýlacak HttpContextAccessor yapýsýný projeye ekler
            services.AddHttpContextAccessor();

            // ObiletBusiness Api DI'a eklenir.
            services.AddOBiletApi(configuration);

            // DI servislerini ayarlar
            services.AddDIServices();

            // Add services to the container.
            // AddRazorRuntimeCompilation uyumluluk sorunlarýný gidermek için eklendi.
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            // Basic file logging için Log4Net'i projeye ekler
            builder.Logging.AddLog4Net();

            var app = builder.Build();

            // AppHttpContext'e HttpContextAccessor set eder
            AppHttpContext.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            //app.UseMiddleware<ErrorHandlingMiddleware>();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=BusSearch}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
