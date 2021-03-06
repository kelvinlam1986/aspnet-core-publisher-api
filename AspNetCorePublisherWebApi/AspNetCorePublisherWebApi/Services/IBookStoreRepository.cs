﻿using AspNetCorePublisherWebApi.Models;
using System.Collections.Generic;

namespace AspNetCorePublisherWebApi.Services
{
    public interface IBookStoreRepository
    {
        IEnumerable<PublisherDTO> GetPublishers();
        PublisherDTO GetPublisher(int publisherId, bool includeBooks = false);
        void AddPublisher(PublisherDTO publisher);
        void UpdatePublisher(int id, PublisherUpdateDTO publisher);
        bool PublisherExist(int publisherId);
        void DeleteBook(BookDTO book);
        void DeletePublisher(PublisherDTO publisher);
        IEnumerable<BookDTO> GetBooks(int publisherId);
        BookDTO GetBook(int publisherId, int bookId);
        void AddBook(BookDTO book);
        void UpdateBook(int publisherId, int bookId, BookUpdateDTO book);
        bool Save();
    }
}
