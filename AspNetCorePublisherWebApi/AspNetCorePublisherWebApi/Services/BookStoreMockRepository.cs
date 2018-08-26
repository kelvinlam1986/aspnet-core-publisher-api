using System.Collections.Generic;
using System.Linq;
using AspNetCorePublisherWebApi.Data;
using AspNetCorePublisherWebApi.Models;

namespace AspNetCorePublisherWebApi.Services
{
    public class BookStoreMockRepository : IBookStoreRepository
    {
        public IEnumerable<PublisherDTO> GetPublishers()
        {
            return MockData.Current.Publishers;
        }

        public PublisherDTO GetPublisher(int publisherId, bool includeBooks = false)
        {
            var publisher = MockData.Current.Publishers.FirstOrDefault(x => x.Id == publisherId);
            if (includeBooks && publisher != null)
            {
                publisher.Books = MockData.Current.Books.Where(x => x.PublisherId == publisherId).ToList();
            }

            return publisher;
        }
    }
}
