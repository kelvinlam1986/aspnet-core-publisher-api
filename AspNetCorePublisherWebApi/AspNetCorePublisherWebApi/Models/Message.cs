using System.ComponentModel.DataAnnotations;

namespace AspNetCorePublisherWebApi.Models
{
    public class Message
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Text { get; set; }
    }
}
