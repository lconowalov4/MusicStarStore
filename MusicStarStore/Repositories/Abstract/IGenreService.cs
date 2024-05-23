﻿
using MusicStarStore.Models.Domain;
using MusicStarStore.Models.DTO;

namespace MusicStarStore.Repositories.Abstract

{
    public interface IGenreService
    {
        bool Add(Genre model);
        bool Update(Genre model);
        Genre GetById(int id);
        bool Delete(int id);
        IQueryable<Genre> List();
    }
}
