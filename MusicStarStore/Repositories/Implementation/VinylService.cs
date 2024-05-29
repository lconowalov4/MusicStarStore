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

        public VinylListVm List()
        {
            var list = ctx.Vinyl.AsQueryable();
            var data = new VinylListVm
            {
                VinylList = list
            };
            return data;
        }

    public bool Update(Vinyl model)
        {
            try
            {
                ctx.Vinyl.Update(model);
                ctx.SaveChanges();
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
