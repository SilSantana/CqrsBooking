using Booking.Infrastructure.CQRS.Queries;
using Booking.Infrastructure.Storage.SqlServer;
using Booking.Queries.Application.Hotel.Processors;
using Booking.Queries.Application.Hotel.Query;
using Booking.Queries.Application.Hotel.ReadModel;
using Booking.Queries.Application.Repository;
using Booking.Queries.Application.Reservation.Processors;
using Booking.Queries.Application.Reservation.Query;
using Booking.Queries.Application.Reservation.ReadModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Booking.Queries.Application
{
    public static class RegisterBookingQueries
    {
        public static IServiceCollection RegisterQueriesApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterSqlServerInfrastructureDependencies(configuration);

            services.AddTransient<IQueryHandler<RetrieveReservationDetail, ReservationDetail>, RetrieveReservationDetailHandler>();
            services.AddTransient<IQueryHandler<HotelQuery, IEnumerable<HotelListItem>>, HotelQueryHandler>();
            services.AddTransient<IQueryHandler<AvailableRoomsQuery, IEnumerable<AvailableRooms>>, HotelQueryHandler>();
            services.AddTransient<IQueryHandler<RoomQuery, IEnumerable<RoomListItem>>, HotelQueryHandler>();
            services.AddTransient<IQueryHandler<CurrentAddressQuery, CurrentAddress>, HotelQueryHandler>();
            services.AddTransient<IQueryHandler<CurrentContactsQuery, CurrentContacts>, HotelQueryHandler>();

            services.AddSingleton<ReservationPersistence, ReservationPersistence>();
            services.AddSingleton<HotelPersistence, HotelPersistence>();

            return services;
        }

    }
}
