using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTicketApp_MVC_project.Data;
using MovieTicketApp_MVC_project.Data.Services;
using MovieTicketApp_MVC_project.Models;

namespace MovieTicketApp_MVC_project.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _context;

        public ProducersController(IProducersService context)
        {
            _context = context;
        }

        // GET: Producers
        public async Task<IActionResult> Index()
        {
              return View(await _context.GetAll());
        }

        // GET: Producers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var producer = await _context.GetById(id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfilepictureURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);
           
            await _context.Add(producer);
            return RedirectToAction(nameof(Index));
         
            
        }

        // GET: Producers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var producer = await _context.GetById(id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilepictureURL,FullName,Bio")] Producer producer)
        {
            if (id != producer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _context.Update(producer, id);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var producer =await _context.GetById(id);
            if (id == null ||producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.GetById(id);

            if (actor == null)
            {
                return Problem("Entity set 'AppDbContext.Producers'  is null.");
            }

            await _context.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private Task<Producer> ProducerExists(int id)
        {

            //var producers = _context.GetAll();
            return _context.GetById(id);
        }
    }
}
