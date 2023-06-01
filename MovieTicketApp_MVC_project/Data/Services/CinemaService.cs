using MovieTicketApp_MVC_project.Data.Base;
using MovieTicketApp_MVC_project.Models;

namespace MovieTicketApp_MVC_project.Data.Services
{
    public class CinemaService: EntityBaseRepository<Cinema>, ICinemaService
    {
        public CinemaService(AppDbContext context) : base(context) { }
    }
}
