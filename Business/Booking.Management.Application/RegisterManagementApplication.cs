using Booking.Infrastructure.CQRS.Commands;
using Booking.Infrastructure.CQRS.Events;
using Booking.Infrastructure.Storage.RavenDB;
using Booking.Management.Application.Commands;
using Booking.Management.Application.Domain.Events;
using Booking.Management.Application.Handlers;
using Booking.Management.Application.Handlers.Events;
using Booking.Management.Application.Repository;
using Booking.Management.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Management.Application
{
    public static class RegisterManagementApplication
    {
        public static IServiceCollection RegisterManagementApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<ManagementProducerSettings>()
                    .Bind(configuration.GetSection(nameof(ManagementProducerSettings)));

            services.RegisterRavenDBStorageInfrastructureDependencies(configuration);

            services.AddTransient<ICommandHandler<CreateHotel>, CreateHotelHandler>();
            services.AddTransient<ICommandHandler<UpdateHotelAddress>, UpdateHotelAddressHandler>();
            services.AddTransient<ICommandHandler<UpdateHotelContacts>, UpdateHotelContactsHandler>();
            services.AddTransient<ICommandHandler<AddRoomToHotel>, AddRoomToHotelHandler>();

            services.AddTransient<IEventHandler<HotelCreated>, HotelCreatedHandler>();
            services.AddTransient<IEventHandler<HotelAddressUpdated>, HotelAddressUpdatedHandler>();
            services.AddTransient<IEventHandler<HotelContactsUpdated>, HotelContactsUpdatedHandler>();
            services.AddTransient<IEventHandler<RoomAdded>, RoomAddedHandler>();

            services.AddTransient<HotelPersistence, HotelPersistence>();

            return services;
        }
    }
}
