using Booking.Infrastructure.CQRS.Commands;
using Booking.Infrastructure.CQRS.Events;
using Booking.Infrastructure.CQRS.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Infrastructure.CQRS
{
    public static class RegisterCqrsInfrastructure
    {

        public static IServiceCollection RegisterInfrastructureCqrsDependencies(
           this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DependencyResolver>();

            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddSingleton<IQueryProcessor, QueryProcessor>();
            services.AddSingleton<IEventPublisher, EventPublisher>();

            return services;
        }

    }
}
