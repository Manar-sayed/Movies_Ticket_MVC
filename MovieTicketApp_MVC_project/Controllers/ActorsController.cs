using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTicketApp_MVC_project.Data;
using MovieTicketApp_MVC_project.Data.Services;
using MovieTicketApp_MVC_project.Models;

namespace MovieTicketApp_MVC_project.Controllers
{
    
    public class ActorsController : Controller
    {
        public IActorsService _services;

        public ActorsController(IActorsService services)
        {
            _services = services;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            var data = await _services.GetAll();
              return View(data);
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var actors = await  _services.GetById(id);
           

            if (actors == null)
            {
                return View("NotFound");
            }
            return View(actors);
        }
        //-----------------------start of image-----------------------//
        public async Task<FileContentResult>GetImage(int id)
        {
            var data = await  _services.GetAll();
            var model =  data.FirstOrDefault(x => x.Id == id);
               // .FirstOrDefaultAsync(m => m.Id == id);
            string imagePath = model.ProfilepictureURL;
            byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
          return File(imageData, "image/jpeg");
        }
        //-------------------------end of image---------------------//

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create([Bind("Id,ProfilepictureURL,FullName,Bio")] Actor actor)
        {
            if (ModelState.IsValid)
            {
               await _services.Add(actor);
               
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        //GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _services.GetById(id);
            if (id == null || result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        //// POST: Actors/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilepictureURL,FullName,Bio")] Actor actor)
        {
            if (id != actor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                 await _services.Update(actor,id);
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        ////// GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var actor=await _services.GetById(id);
            if (id == null || actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        //// POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _services.GetById(id);

            if (actor == null)
            {
                return Problem("Entity set 'AppDbContext.Actors'  is null.");
            }

            await _services.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private Task<Actor> ActorExists(int id)
        {
            var actors = _services.GetAll();
            return _services.GetById(id);
        }
    }
}
