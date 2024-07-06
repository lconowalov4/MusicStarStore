using MusicStarStore.Models.Domain;
using MusicStarStore.Models.DTO;
using MusicStarStore.Repositories.Abstract;

namespace MusicStarStore.Repositories.Implementation
{
    public class VinylService : IVinylService
    {
        private readonly DatabaseContext ctx;
        public VinylService(DatabaseContext ctx) 
        { 
            this.ctx = ctx;
        }
        public bool Add(Vinyl model)
        {
            try
            {
               
                ctx.Vinyl.Add(model);
                ctx.SaveChanges();
                foreach (int genreId in model.Genres)
                {
                    var vinylGenre = new VinylGenre
                    {
                        VinylId = model.Id,
                        GenreId = genreId
                    };
                    ctx.VinylGenre.Add(vinylGenre);
                }
                ctx.SaveChanges();
                return true;
            }

            catch (Exception ex) 
            { 
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if(data == null) 
                { 
                    return false; 
                }
                var vinylGenres = ctx.VinylGenre.Where(a => a.VinylId == data.Id);
                foreach (var vinylGenre in vinylGenres) {
                    ctx.VinylGenre.Remove(vinylGenre);
                }
                ctx.Vinyl.Remove(data);
                ctx.SaveChanges();
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public Vinyl GetById(int id)
        {
            return ctx.Vinyl.Find(id);
        }

        public VinylListVm List(string term="", bool paging = false, int currentPage = 0)
        {
            var data = new VinylListVm();

            var list = ctx.Vinyl.ToList();
            if(!string.IsNullOrEmpty(term)) 
            {
                term = term.ToLower();
                list = list.Where(a => a.Title.ToLower().StartsWith(term)).ToList();
            }

            if (paging)
            {
                int pageSize = 5;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count /(double)pageSize);
                list = list.Skip((currentPage-1)*pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }

            foreach (var vinyl in list) 
            { 
                var genres = (from genre in ctx.Genre 
                              join vg in ctx.VinylGenre
                              on genre.Id equals vg.GenreId
                              where vg.VinylId == vinyl.Id
                              select genre.GenreName
                              ).ToList();
                var genreNames = string.Join(',', genres);
                vinyl.GenreNames = genreNames;
            }

            data.VinylList = list.AsQueryable();

            return data;
        }

    public bool Update(Vinyl model)
        {
            try
            {
                var genresToDeleted = ctx.VinylGenre.Where(a => a.VinylId == model.Id && !model.Genres.Contains(a.GenreId)).ToList(); ;
                foreach (var vGenre in genresToDeleted) 
                {
                    ctx.VinylGenre.Remove(vGenre);
                }
                
                foreach(int genId in model.Genres) 
                {
                    var vinylGenre = ctx.VinylGenre.FirstOrDefault(a => a.VinylId == model.Id && a.GenreId == genId);
                    if (vinylGenre == null) 
                    {
                        vinylGenre = new VinylGenre { GenreId = genId, VinylId = model.Id };
                        ctx.VinylGenre.Add(vinylGenre);
                    }
                }
                ctx.Vinyl.Update(model);
                ctx.SaveChanges();
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

    public List<int> GetGenreByVinylId(int vinylId)
        {
            var genreIds = ctx.VinylGenre.Where(a => a.VinylId == vinylId).Select(a => a.GenreId).ToList();
            return genreIds;
        }
    }
}
