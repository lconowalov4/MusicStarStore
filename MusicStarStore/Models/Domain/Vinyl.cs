using System.ComponentModel.DataAnnotations;

namespace MusicStarStore.Models.Domain
{
    public class Vinyl
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Artist { get; set; }

        [Required]
        public string? VinylImage { get; set; }
    }
}
