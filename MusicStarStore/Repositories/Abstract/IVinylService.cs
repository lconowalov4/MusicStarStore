﻿
using MusicStarStore.Models.Domain;
using MusicStarStore.Models.DTO;

namespace MusicStarStore.Repositories.Abstract

{
    public interface IVinylService
    {
        bool Add(Vinyl model);
        bool Update(Vinyl model);
        Vinyl GetById(int id);
        bool Delete(int id);
        VinylListVm List(string term="", bool paging = false, int currentPage = 0);

        List<int> GetGenreByVinylId(int vinylId);

    }
}
