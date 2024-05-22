using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Culture;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using webapp.DataContext;
using webapp.Repository.BenuterRepository;
using webapp.Repository.BusRepository;
using webapp.Repository.FahrerRepository;
using webapp.Repository.ReservationRepository;
using webapp.Repository.SitzplatzRepository;

namespace webapp
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

            services.AddDbContext<TicketReservationContext>
                (options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddNotyf(config => { config.DurationInSeconds = 3; 
                                          config.IsDismissable = true; 
                                          config.Position = NotyfPosition.TopRight; });

            services.AddDistributedMemoryCache(); // Configurez le stockage de session en mémoire (à des fins de démonstration)
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Définissez la durée de la session selon vos besoins
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            //services.AddSingleton<IBenutzerRepository, BenutzerRepository>();
            services.AddScoped<IBenutzerRepository, BenutzerRepository>();
            services.AddScoped<IFahrerRepository, FahrerRepository>();
            services.AddScoped<IBusRepository, BusRepository>();
            services.AddScoped<ISitzplatzRepository, SitzplatzRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            //services.AddDbContext<TicketReservationContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));







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
            app.UseNotyf();
            app.UseRouting();
            app.UseSession();

           //app.UseAuthorization();
           app.UseCustomAuthorization();
           



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Bus}/{action=Reservation}/{id?}");
            });
        }
    }
}
