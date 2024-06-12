using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicStarStore.Models.Domain;
using MusicStarStore.Repositories.Abstract;

namespace MusicStarStore.Controllers
{
    [Authorize]
    public class VinylController : Controller
    {
        private readonly IVinylService _vinylService;

        private readonly IFileService _fileService;
        
        private readonly IGenreService _genService;
        public VinylController(IGenreService genService, IVinylService vinylService, IFileService fileService) 
        {
            _vinylService = vinylService;
            _fileService = fileService;
            _genService = genService;
        }

        public IActionResult Add()
        {
            var model = new Vinyl();
            model.GenreList = _genService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString() });
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Vinyl model)
        {
            model.GenreList = _genService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString() });
            if (!ModelState.IsValid) 
            {
                return View(model);
            }
            if (model.ImageFile != null) 
            { 
            var fileResult = this._fileService.SaveImage(model.ImageFile);
            if (fileResult.Item1 == 0) 
            {
                TempData["msg"] = "File could not saved";
                return View(model);
            }
            var imageName = fileResult.Item2;
            model.VinylImage = imageName;
            }
            
            var result = _vinylService.Add(model);
            if (result) 
            {
                TempData["msg"] = "Successfully Added";
                return RedirectToAction(nameof(Add));
            } else 
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
            
        }

        public IActionResult Edit(int id)
        {
            var model = _vinylService.GetById(id);
            var selectedGenres = _vinylService.GetGenreByVinylId(model.Id);
            MultiSelectList multiGenreList = new MultiSelectList(_genService.List(), "Id","GenreName", selectedGenres);
            model.MultiGenreList = multiGenreList;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Vinyl model)
        {
            var selectedGenres = _vinylService.GetGenreByVinylId(model.Id);
            MultiSelectList multiGenreList = new MultiSelectList(_genService.List(), "Id", "GenreName", selectedGenres);
            model.MultiGenreList = multiGenreList;

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.ImageFile != null)
            {
                var fileResult = this._fileService.SaveImage(model.ImageFile);
                if (fileResult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileResult.Item2;
                model.VinylImage = imageName;
            }
            var result = _vinylService.Update(model);
            if (result)
            {
                return RedirectToAction(nameof(VinylList));
            }
            else
            {
                return View(model);
            }

        }

        public IActionResult VinylList() 
        {
            var data = this._vinylService.List();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _vinylService.Delete(id);
            return RedirectToAction(nameof(VinylList));
        }
    }
}
