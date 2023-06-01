using MovieTicketApp_MVC_project.Data.Base;
using MovieTicketApp_MVC_project.Models;

namespace MovieTicketApp_MVC_project.Data.Services
{
    public interface IOrderService: IEntityBaseRepository<Order>
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items,string userId,string userEmailAddress);
        Task <List<Order>> GetOrdersByUserIdAsync(string userId);
    }
}
