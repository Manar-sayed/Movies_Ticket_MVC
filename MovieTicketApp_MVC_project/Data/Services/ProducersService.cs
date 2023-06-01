using MovieTicketApp_MVC_project.Data.Base;
using MovieTicketApp_MVC_project.Models;

namespace MovieTicketApp_MVC_project.Data.Services
{
    public class ProducersService:EntityBaseRepository<Producer>,IProducersService
    {
        public ProducersService(AppDbContext context):base(context) { }
      
    }
}
