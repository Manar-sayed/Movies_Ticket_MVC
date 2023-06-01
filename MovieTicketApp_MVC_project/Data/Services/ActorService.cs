using Microsoft.EntityFrameworkCore;
using MovieTicketApp_MVC_project.Data.Base;
using MovieTicketApp_MVC_project.Models;

namespace MovieTicketApp_MVC_project.Data.Services
{
    public class ActorService : EntityBaseRepository<Actor>,IActorsService
    {
        
        public ActorService(AppDbContext context):base(context) { }
       

        //public async Task Add(Actor actor)
        //{
        //    await _context.Actors.AddAsync(actor);
        //    await _context.SaveChangesAsync();
        // }

        //public async Task Delete(int id)
        //{
        //    var result= await _context.Actors.FirstOrDefaultAsync(x => x.Id == id);
        //     _context.Actors.Remove(result);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<Actor>> GetAll()
        //{
        //    var result=await _context.Actors.ToListAsync();
        //    return result;
        //}

        //public async Task<Actor> GetById(int id)
        //{
        //    var result=await _context.Actors.FirstOrDefaultAsync(x=>x.Id==id);
        //    return result;
        //}

        //public async Task<Actor> Update(Actor actor, int id)
        //{
        //    _context.Update(actor);
        //    await _context.SaveChangesAsync();
        //    return actor;
        //}
    }
}
