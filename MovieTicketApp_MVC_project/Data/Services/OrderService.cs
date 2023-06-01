using Microsoft.EntityFrameworkCore;
using MovieTicketApp_MVC_project.Data.Base;
using MovieTicketApp_MVC_project.Models;

namespace MovieTicketApp_MVC_project.Data.Services
{
    
    public class OrderService : EntityBaseRepository<Order>, IOrderService
    {private readonly AppDbContext _context;
        public OrderService(AppDbContext context) 
            : base(context) { _context = context; }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await _context.Orders
                .Include(n => n.OrderItems)
                .ThenInclude(n => n.Movie)
                .Where(n => n.UserId == userId)
                .ToListAsync();
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
