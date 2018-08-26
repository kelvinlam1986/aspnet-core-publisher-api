using System.ComponentModel.DataAnnotations;

namespace AspNetCorePublisherWebApi.Models
{
    public class PublisherUpdateDTO
    {
        [Required(ErrorMessage = "You must enter a name")]
        [MaxLength(50)]
        public string Name { get; set; }
        public int Established { get; set; }
    }
}
