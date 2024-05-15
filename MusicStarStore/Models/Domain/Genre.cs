using System.ComponentModel.DataAnnotations;

namespace MusicStarStore.Models.Domain
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string? GenreName { get; set; }
       
    }
}
