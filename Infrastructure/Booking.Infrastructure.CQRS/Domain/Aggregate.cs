using Booking.Infrastructure.CQRS.Events;
using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.CQRS.Domain
{
    public class Aggregate : Entity
    {

        private readonly List<IEvent> _events = new List<IEvent>();
        public IReadOnlyList<IEvent> Events => _events;

        public Aggregate(Guid? identifier) : base(identifier)
        {

        }

        protected void AddEvent(IEvent @event) 
        {
            _events.Add(@event);
        }

    }
}
