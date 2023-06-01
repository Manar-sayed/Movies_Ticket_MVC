using MovieTicketApp_MVC_project.Models;
using System.Linq.Expressions;

namespace MovieTicketApp_MVC_project.Data.Base
{
    public interface IEntityBaseRepository<T> where T:class,IEntitybase,new()//معناها لازم الكلاس يكون فيه اللى جوه الbase اللو الid  ويكون فيه ديفيلت كونستركتر
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProp);
        Task<T> GetById(int id);
        Task Add(T entity);
        Task Update(T entity, int id);
        Task Delete(int id);
    }
}
