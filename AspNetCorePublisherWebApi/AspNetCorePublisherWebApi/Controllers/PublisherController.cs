using AspNetCorePublisherWebApi.Models;
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

        [HttpPost]
        public IActionResult Post([FromBody] PublisherCreateDTO publisher)
        {
            if (publisher == null) return BadRequest();
            if (publisher.Established < 1534)
            {
                ModelState.AddModelError("Established", "The first publishing house was founded in 1534.");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var publisherToAdd = new PublisherDTO
            {
                Name = publisher.Name,
                Established = publisher.Established
            };
            _bookstoreRepository.AddPublisher(publisherToAdd);
            _bookstoreRepository.Save();
            return CreatedAtRoute("GetPublisher", new { id = publisherToAdd.Id }, publisherToAdd);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PublisherUpdateDTO publisher)
        {
            if (publisher == null) return BadRequest();
            if (publisher.Established < 1534)
            {
                ModelState.AddModelError("Established", "The first publishing house was founded in 1534.");
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var publisherExists = _bookstoreRepository.PublisherExist(id);
            if (!publisherExists)
            {
                return NotFound();
            }

            _bookstoreRepository.UpdatePublisher(id, publisher);
            return NoContent();
        }
    }
}