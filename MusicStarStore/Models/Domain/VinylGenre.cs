using System.ComponentModel.DataAnnotations;

namespace MusicStarStore.Models.Domain
{
    public class VinylGenre
    {
        public int Id { get; set; }

        public int VinylId { get; set; }

        public int GenreId { get; set; }

    }
}
