using MusicStarStore.Models.Domain;

namespace MusicStarStore.Models.DTO
{
    public class VinylListVm
    {
        public IQueryable<Vinyl> VinylList { get; set; }
    }
}
