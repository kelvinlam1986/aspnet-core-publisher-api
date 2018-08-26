using System.Collections;
using System.Collections.Generic;

namespace AspNetCorePublisherWebApi.Models
{
    public class PublisherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Established { get; set; }
        public ICollection<BookDTO> Books { get; set; } = new List<BookDTO>();
        public int BookCount { get { return Books.Count; } }
    }
}
