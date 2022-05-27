using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BCP.Framework.Logs;
using BCP.Sap.Business;
using BCP.Sap.Models.Configuracion;
using Microsoft.Extensions.Caching.Memory;

namespace BCP.Microservicio.Aplicacion
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

            
            #region SECCION.01: CONFIGURACION DE SERVICIO
            //Se realiza la configuracion del servicio por medio de inyeccion de dependencias.
            var miConfiguracion = this.Configuration.GetSection("aplicacion").Get<ConfiguracionBase>();
            services.AddSingleton<ILogger, Logger>(objeto => new Logger(miConfiguracion.configuracionLog.rutaArchivoLog, miConfiguracion.configuracionLog.nivel));
            services.AddScoped<ISapBusiness, SapBusiness>();
            #endregion

            /*services.AddControllersWithViews().AddJsonOptions(options=> {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });*/
            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout=TimeSpan.FromHours(3);
                //options.Cookie.IsEssential = true;
            });
            services.AddMemoryCache();

            /*services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(60);
                options.Cookie.IsEssential = true;
            });*/
            services.AddControllersWithViews();
            //services.AddControllersWithViews().AddSessionStateTempDataProvider();
            //services.AddRazorPages();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseStatusCodePagesWithReExecute("/Error");

            app.UseRouting();

            app.UseAuthentication();
            //app.UseAuthorization();
                
            app.UseSession();

            app.UseEndpoints(endpoints =>
                {
                /*endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("");
                });*/
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages();
            });
            /*app.Run(async ctx=> 
            {
                try
                {
                    var user = (WindowsIdentity)ctx.User.Identity;
                    await ctx.Response.WriteAsync($"User: {user.Name}\tState: {user.ImpersonationLevel}\n");
                    WindowsIdentity.RunImpersonated(user.AccessToken,()=> 
                    {
                        var impersonatedUser = WindowsIdentity.GetCurrent();
                        var message =
                            $"User: {impersonatedUser.Name}\t" +
                            $"State: {impersonatedUser.ImpersonationLevel}";
                        var bytes = Encoding.UTF8.GetBytes(message);
                        ctx.Response.Body.Write(bytes, 0, bytes.Length);
                    });
                }
                catch(Exception e)
                {
                    await ctx.Response.WriteAsync(e.ToString());
                }
            });*/
            
        }
    }
}
