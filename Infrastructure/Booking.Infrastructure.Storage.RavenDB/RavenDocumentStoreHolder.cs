using Raven.Client.Documents;
using System;

namespace Booking.Infrastructure.Storage.RavenDB
{
    internal class RavenDocumentStoreHolder : IRavenDocumentStoreHolder
    {
        private readonly Lazy<IDocumentStore> _documentStore;

        public RavenDocumentStoreHolder(Lazy<IDocumentStore> documentStore)
        {
            _documentStore = documentStore;
        }
        public IDocumentStore Store => _documentStore.Value;
    }
}
