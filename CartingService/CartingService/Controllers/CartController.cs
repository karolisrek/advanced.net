using Microsoft.AspNetCore.Mvc;
using Cart.BLL.Managers;
using Cart.DAL.Models;

namespace CartingService.Controllers
{
    [ApiController]
    [Route("cart")]
    public class CartController : ControllerBase
    {
        private readonly CartManager _cartManager;

        public CartController(CartManager cartManager)
        {
            _cartManager = cartManager;
        }

        [HttpGet]
        public List<CartItem> GetCartItems(long id)
        {
            return _cartManager.GetCartItems(id);
        }

        [HttpPost]
        public void AddNewItemToCart(long id, [FromBody]CartItem cartItem)
        {
            _cartManager.AddCartItem(id, cartItem);
        }

        [HttpDelete]
        public void DeleteItemFromCart(long cartId, long itemId)
        {
            _cartManager.DeleteItemFromCart(cartId, itemId);
        }
    }
}