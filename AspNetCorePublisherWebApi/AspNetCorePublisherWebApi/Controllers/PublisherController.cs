using AspNetCorePublisherWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePublisherWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/publisher")]
    public class PublisherController : Controller
    {
        private IBookStoreRepository _bookstoreRepository;

        public PublisherController(IBookStoreRepository bookstoreRepository)
        {
            this._bookstoreRepository = bookstoreRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookstoreRepository.GetPublishers());
        }

        [HttpGet("{id}", Name = "GetPublisher")]
        public IActionResult Get(int id, bool includeBooks = false)
        {
            var publisher = _bookstoreRepository.GetPublisher(id, includeBooks);
            if (publisher == null) return NotFound();
            return Ok(publisher);
        }
    }
}