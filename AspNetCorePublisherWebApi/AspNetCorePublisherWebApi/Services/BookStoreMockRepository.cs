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

        public void AddPublisher(PublisherDTO publisher)
        {
            var id = GetPublishers().Max(x => x.Id) + 1;
            publisher.Id = id;
            MockData.Current.Publishers.Add(publisher);
        }

        public bool Save()
        {
            return true;
        }

        public void UpdatePublisher(int id, PublisherUpdateDTO publisher)
        {
            var publisherToUpdate = GetPublisher(id);
            publisherToUpdate.Name = publisher.Name;
            publisherToUpdate.Established = publisher.Established;
        }

        public bool PublisherExist(int publisherId)
        {
            return MockData.Current.Publishers.Count(x => x.Id.Equals(publisherId)).Equals(1);
        }

        public void DeleteBook(BookDTO book)
        {
            MockData.Current.Books.Remove(book);
        }

        public void DeletePublisher(PublisherDTO publisher)
        {
            MockData.Current.Books.RemoveAll(b => b.PublisherId.Equals(publisher.Id));
            MockData.Current.Publishers.Remove(publisher);
        }
    }
}
