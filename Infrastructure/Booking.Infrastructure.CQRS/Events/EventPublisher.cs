﻿using System;
using System.Threading.Tasks;

namespace Booking.Infrastructure.CQRS.Events
{
    internal class EventPublisher : IEventPublisher
    {
        private readonly DependencyResolver _dependencyResolver;

        public EventPublisher(DependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event == null)           
                throw new ArgumentNullException($"The {nameof(@event)} Must to be created");
          

            var eventHandlers = _dependencyResolver.ResolveAll<IEventHandler<TEvent>>();

            foreach (var eventHandler in eventHandlers)
            {
                await eventHandler.HandleAsync(@event);
            }
        }
    }
}
