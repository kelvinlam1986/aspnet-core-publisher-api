using System.Collections.Generic;
using System.Linq;
using AspNetCorePublisherWebApi.Entities;
using AspNetCorePublisherWebApi.Models;
using AutoMapper;

namespace AspNetCorePublisherWebApi.Services
{
    public class BookstoreSqlRepository : IBookStoreRepository
    {
        private SqlDbContext _db;

        public BookstoreSqlRepository(SqlDbContext db)
        {
            _db = db;
        }

        public void AddBook(BookDTO book)
        {
            var bookToAdd = Mapper.Map<Book>(book);
            _db.Books.Add(bookToAdd);
        }

        public void AddPublisher(PublisherDTO publisher)
        {
            var publisherToAdd = Mapper.Map<Publisher>(publisher);
            _db.Publishers.Add(publisherToAdd);
        }

        public void DeleteBook(BookDTO book)
        {
            var bookToDelete = _db.Books.FirstOrDefault(x => x.Id.Equals(book.Id));
            if (bookToDelete != null)
            {
                _db.Books.Remove(bookToDelete);
            }
        }

        public void DeletePublisher(PublisherDTO publisher)
        {
            var publisherToDelete = _db.Publishers.FirstOrDefault(x => x.Id.Equals(publisher.Id));
            if (publisherToDelete != null)
            {
                _db.Publishers.Remove(publisherToDelete);
            }
        }

        public BookDTO GetBook(int publisherId, int bookId)
        {
            var book = _db.Books.FirstOrDefault(x => x.PublisherId.Equals(publisherId)
                && x.Id.Equals(bookId));
            var bookDTO = Mapper.Map<BookDTO>(book);
            return bookDTO;
        }

        public IEnumerable<BookDTO> GetBooks(int publisherId)
        {
            var books = _db.Books.Where(x => x.PublisherId.Equals(publisherId));
            var bookDTOs = Mapper.Map<IEnumerable<BookDTO>>(books);
            return bookDTOs;
        }

        public PublisherDTO GetPublisher(int publisherId, bool includeBooks = false)
        {
            var publisher = _db.Publishers.FirstOrDefault(x => x.Id.Equals(publisherId));
            if (includeBooks && publisher != null)
            {
                _db.Entry(publisher).Collection(x => x.Books).Load();
            }

            var publisherDTO = Mapper.Map<PublisherDTO>(publisher);
            return publisherDTO;
        }

        public IEnumerable<PublisherDTO> GetPublishers()
        {
            return Mapper.Map<IEnumerable<PublisherDTO>>(_db.Publishers);
        }

        public bool PublisherExist(int publisherId)
        {
            return _db.Publishers.Count(x => x.Id.Equals(publisherId)).Equals(1);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public void UpdateBook(int publisherId, int bookId, BookUpdateDTO book)
        {
            var bookToUpdate = _db.Books.FirstOrDefault(b => b.PublisherId.Equals(publisherId) && b.Id.Equals(bookId));
            if (bookToUpdate == null) return;
            bookToUpdate.Title = book.Title;
            bookToUpdate.PublisherId = book.PublisherId;
        }

        public void UpdatePublisher(int id, PublisherUpdateDTO publisher)
        {
            var publisherUpdate = _db.Publishers.FirstOrDefault(p => p.Id.Equals(id));
            if (publisherUpdate == null) return;
            publisherUpdate.Name = publisher.Name;
            publisherUpdate.Established = publisher.Established;
        }
    }
}
