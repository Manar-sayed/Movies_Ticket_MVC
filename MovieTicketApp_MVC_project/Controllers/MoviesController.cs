using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieTicketApp_MVC_project.Data;
using MovieTicketApp_MVC_project.Data.Enum;
using MovieTicketApp_MVC_project.Data.Services;
using MovieTicketApp_MVC_project.Data.ViewModels;
using MovieTicketApp_MVC_project.Models;

namespace MovieTicketApp_MVC_project.Controllers
{

    public class MoviesController : Controller
    {
        private readonly IMoviesService _context;

        public MoviesController(IMoviesService context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var appDbContext =await _context.GetAll(m => m.Cinema,m =>m.Producer);
            
            return View( appDbContext);
        } 
        //Filter
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies =await _context.GetAll(m => m.Cinema,m =>m.Producer);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredMovie=allMovies.Where(n=>n.Name.ToUpper().Contains(searchString.ToUpper()) || n.Name.Contains(searchString.ToUpper())).ToList();
                return View("Index",filteredMovie);
            }

            return View("Index",allMovies);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.GetMovieByIdAsync(id);
            //if (movie == null)
            //{
            //    return NotFound();
            //}

            return View(movie);
        }

        // GET: Movies/Create
       
        public async Task< IActionResult> Create()
        {
            var movieDropdownsData = await _context.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas?? new List<Cinema>(), "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers ?? new List<Producer>(), "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors ?? new List<Actor>(), "Id", "FullName");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,ImageURL,StartDate,EndDate,MovieCategory,ActorIds,CinemaId,ProducerId")] NewMovieVM movie)
        {
          
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _context.GetNewMovieDropdownsValues();
                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas ?? new List<Cinema>(), "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers ?? new List<Producer>(), "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors ?? new List<Actor>(), "Id", "FullName");
                return View(movie);
              
            }
            
            await _context.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));

        }

        // GET: Movies/Edit/5
      

        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _context.GetMovieByIdAsync(id);
            if (movieDetails == null) return NotFound();

            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),
            };
            var movieDropdownsData = await _context.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas ?? new List<Cinema>(), "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers ?? new List<Producer>(), "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors ?? new List<Actor>(), "Id", "FullName");

            return View(response);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");
           

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _context.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
                return View(movie);
            }
            await _context.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Movies/Delete/5

        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.GetById(id);
            if (movie == null)
            {
                return Problem("Entity set 'AppDbContext.Movies'  is null.");
            }
            await _context.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private Task<Movie> MovieExists(int id)
        {
            var movies = _context.GetAll();
            return _context.GetById(id);
        }
        //-----------------------start of image-----------------------//
        public async Task<FileContentResult> GetImage(int id)
        {
            var data = await _context.GetAll();
            var model = data.FirstOrDefault(x => x.Id == id);
            // .FirstOrDefaultAsync(m => m.Id == id);
            string imagePath = model.ImageURL;
            byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
            return File(imageData, "image/jpeg");
        }
        //-
    }
}
