using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.DAL;
using Cart.DAL.Models;

namespace Cart.BLL.Managers
{
    public class CartManager
    {
        CartRepository _cartRepository;
        public CartManager(CartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public List<DAL.Models.CartItem> GetCartItems(long id)
        {
            return _cartRepository.GetCartItems(id);
        }

        public void AddCartItem(long cartId, CartItem cartItem)
        {
            cartItem.CartId = cartId;
            _cartRepository.AddItemToCart(cartItem);
        }

        public void DeleteItemFromCart(long cartId, long itemId)
        {
            _cartRepository.RemoveCartItem(cartId, itemId);
        }
    }
}
