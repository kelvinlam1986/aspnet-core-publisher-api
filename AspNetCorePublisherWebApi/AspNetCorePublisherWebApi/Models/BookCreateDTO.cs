using System.ComponentModel.DataAnnotations;

namespace AspNetCorePublisherWebApi.Models
{
    public class BookCreateDTO
    {
        [Required(ErrorMessage = "You must enter a title")]
        [MaxLength(50)]
        public string Title { get; set; }
        public int PublisherId { get; set; }
    }
}
