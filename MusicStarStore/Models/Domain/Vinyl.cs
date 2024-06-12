using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStarStore.Models.Domain
{
    public class Vinyl
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Artist { get; set; }

        public string? VinylImage { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [NotMapped]
        [Required]
        public List<int>? Genres { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? GenreList { get; set; }

        [NotMapped]
        public string ? GenreNames { get; set; }

        [NotMapped]
        public MultiSelectList ? MultiGenreList { get; set; }
    }
}
