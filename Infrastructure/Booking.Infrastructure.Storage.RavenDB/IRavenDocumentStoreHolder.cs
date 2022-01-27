using Raven.Client.Documents;

namespace Booking.Infrastructure.Storage.RavenDB
{
    public interface IRavenDocumentStoreHolder
    {
        IDocumentStore Store { get; }
    }
}
