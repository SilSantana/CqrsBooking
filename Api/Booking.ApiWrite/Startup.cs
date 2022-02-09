using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Booking.Infrastructure.CQRS;
using Booking.Management.Application;
using Booking.Reservation.Application;

namespace Booking.ApiWrite
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                             new Microsoft.OpenApi.Models.OpenApiInfo
                             {
                                 Title = "EmergingBooking Api",
                                 Version = "v1",
                                 Description = "List of Apis to interact with EmergingBooking"
                             });
            });

            services
                .RegisterInfrastructureCqrsDependencies(Configuration)
                .RegisterManagementApplicationDependencies(Configuration)
                .RegisterReservationApplicationDependencies(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "CqrsBooking Api V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
