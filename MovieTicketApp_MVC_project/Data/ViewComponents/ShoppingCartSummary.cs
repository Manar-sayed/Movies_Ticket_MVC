using Microsoft.AspNetCore.Mvc;
using MovieTicketApp_MVC_project.Data.Cart;

namespace MovieTicketApp_MVC_project.Data.ViewComponents
{
    public class ShoppingCartSummary:ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }
        public IViewComponentResult Invoke()
        {
            var items=_shoppingCart.GetShoppingCartItems();
            return View(items.Count);

        }
    }
}
