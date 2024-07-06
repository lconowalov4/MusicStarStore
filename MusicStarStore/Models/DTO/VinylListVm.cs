using MusicStarStore.Models.Domain;

namespace MusicStarStore.Models.DTO
{
    public class VinylListVm
    {
        public IQueryable<Vinyl> VinylList { get; set; }

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }
}
