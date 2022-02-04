using Booking.Infrastructure.CQRS.Commands;
using Booking.Infrastructure.Storage.RavenDB;
using Booking.Reservation.Application.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Booking.Reservation.Application
{
    public static class RegisterReservationApplication
    {

        public static IServiceCollection RegisterReservationApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<ReservationProducerSettings>()
                    .Bind(configuration.GetSection(nameof(ReservationProducerSettings)));

            services.RegisterRavenDBStorageInfrastructureDependencies(configuration);

            services.AddTransient<ICommandHandler<MakeRoomReservation>, ReservationHandler>();
            services.AddTransient<ICommandHandler<CancelReservation>, ReservationHandler>();

            services.AddTransient<IEventHandler<ReservationCreated>, ReservationCreatedHandler>();

            services.AddTransient<ReservationPersistence, ReservationPersistence>();
            services.AddTransient<HotelPersistence, HotelPersistence>();

            return services;
        }


    }
}
