using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStarStore.Models.Domain;
using MusicStarStore.Repositories.Abstract;

namespace MusicStarStore.Controllers
{
    [Authorize]
    public class VinylController : Controller
    {
        private readonly IVinylService _vinylService;

        private readonly IFileService _filelService;
        public VinylController(IVinylService vinylService, IFileService filelService) 
        {
            _vinylService = vinylService;
            _filelService = filelService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Vinyl model)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);

            }
            var fileResult = this._filelService.SaveImage(model.ImageFile);
            if (fileResult.Item1 == 0) 
            {
                TempData["msg"] = "File could not saved";
            }
            var imageName = fileResult.Item2;
            model.VinylImage = imageName;
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
            var data = _vinylService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Vinyl model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
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
