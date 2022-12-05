using Cart.DAL.Models;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace Cart.DAL
{
    public class CartRepository
    {
        private readonly string _connString;

        public CartRepository(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("CartDb") ?? throw new Exception("CartDb missing");
        }

        public List<CartItem> GetCartItems(long cartId)
        {
            using var db = new LiteDatabase(_connString);

            var col = db.GetCollection<CartItem>("cart_items");

            return col.Find(x => x.CartId == cartId).ToList();
        }

        public void AddItemToCart(CartItem cartItem)
        {
            using var db = new LiteDatabase(_connString);

            var col = db.GetCollection<CartItem>("cart_items");
            col.Insert(cartItem);
        }

        public void RemoveCartItem(CartItem cartItem) => RemoveCartItem(cartItem.CartId, cartItem.ItemId);

        public void RemoveCartItem(long cartId, long itemId)
        {
            using var db = new LiteDatabase(_connString);

            var col = db.GetCollection<CartItem>("cart_items");
            col.DeleteMany(x => x.CartId == cartId && x.ItemId == itemId);
        }

        public void UpdateItems(Product product)
        {
            using var db = new LiteDatabase(_connString);

            var col = db.GetCollection<CartItem>("cart_items");
            col.UpdateMany(cartItem => new CartItem()
            {
                Id = cartItem.Id,
                CartId = cartItem.CartId,
                Image = product.Image,
                ItemId = cartItem.ItemId,
                Name = product.Name,
                Price = product.Price,
                Quantity = cartItem.Quantity
            }, cartItem => cartItem.ItemId == product.Id);
        }
    }
}