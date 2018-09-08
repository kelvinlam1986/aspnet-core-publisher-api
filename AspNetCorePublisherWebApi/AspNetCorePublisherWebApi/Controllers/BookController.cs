using AspNetCorePublisherWebApi.Models;
using AspNetCorePublisherWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePublisherWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/publisher")]
    public class BookController : Controller
    {
        private IBookStoreRepository _bookStoreRepository;

        public BookController(IBookStoreRepository bookStoreRepository)
        {
            _bookStoreRepository = bookStoreRepository;
        }

        [HttpGet("{publisherId}/books")]
        public IActionResult Get(int publisherId)
        {
            var books = _bookStoreRepository.GetBooks(publisherId);
            return Ok(books);
        }

        [HttpGet("{publisherId}/books/{id}", Name = "GetBook")]
        public IActionResult Get(int publisherId, int id)
        {
            var book = _bookStoreRepository.GetBook(publisherId, id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost("{publisherId}/books")]
        public IActionResult Post(int publisherId, [FromBody]BookCreateDTO book)
        {
            if (book == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var publisherExists = _bookStoreRepository.PublisherExist(publisherId);
            if (!publisherExists) return NotFound();
            var bookToAdd = new BookDTO
            {
                Title = book.Title,
                PublisherId = publisherId
            };

            _bookStoreRepository.AddBook(bookToAdd);
            return CreatedAtRoute("GetBook", new {  publisherId, id = bookToAdd.Id }, bookToAdd);
        }

        [HttpPut("{publisherId}/books/{id}")]
        public IActionResult Put(int publisherId, int id, [FromBody] BookUpdateDTO book)
        {
            if (book == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            var bookToUpdate = _bookStoreRepository.GetBook(publisherId, id);
            if (bookToUpdate == null) return NotFound();
            _bookStoreRepository.UpdateBook(publisherId, id, book);
            _bookStoreRepository.Save();
            return NoContent();
        }
    }
}