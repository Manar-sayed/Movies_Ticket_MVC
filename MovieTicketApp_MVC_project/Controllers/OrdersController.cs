using Microsoft.AspNetCore.Mvc;
using MovieTicketApp_MVC_project.Data.Cart;
using MovieTicketApp_MVC_project.Data.Services;
using MovieTicketApp_MVC_project.Data.ViewModels;
using MovieTicketApp_MVC_project.Models;

namespace MovieTicketApp_MVC_project.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly IOrderService _orderService;
        private readonly ShoppingCart _shoppingCart;// from main class
       
        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrderService orderService)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        public async Task< IActionResult> Index()
        {
            string userId = "";
            var orders=await _orderService.GetOrdersByUserIdAsync(userId);
            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items=_shoppingCart.GetShoppingCartItems();//بجيب كل الايتمز اللى عندى فى الرئيسى
            _shoppingCart.ShoppingCartItems=items;//بحطهم فى متغير

            var result = new ShoppingCartVM()// create obj from vm class and == it to real obj
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal= _shoppingCart.GetShoppingCartTotal(),
            };
            return View(result);
        }



        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item=await _moviesService.GetMovieByIdAsync(id);
            if (item!=null) { _shoppingCart.AddItemToCart(item); }
            return RedirectToAction(nameof(ShoppingCart));

        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item=await _moviesService.GetMovieByIdAsync(id);

            if (item!=null) { _shoppingCart.RemoveItemFromCart(item); }

            return RedirectToAction(nameof(ShoppingCart));

        }
    
        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string UserId = "";
            string UserEmailAddress = "";
            await _orderService.StoreOrderAsync(items, UserId, UserEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }
    }
}
