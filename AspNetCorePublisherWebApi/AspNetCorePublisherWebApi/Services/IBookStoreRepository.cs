using AspNetCorePublisherWebApi.Models;
using System.Collections.Generic;

namespace AspNetCorePublisherWebApi.Services
{
    public interface IBookStoreRepository
    {
        IEnumerable<PublisherDTO> GetPublishers();
        PublisherDTO GetPublisher(int publisherId, bool includeBooks = false);
        void AddPublisher(PublisherDTO publisher);
        bool Save();
    }
}
