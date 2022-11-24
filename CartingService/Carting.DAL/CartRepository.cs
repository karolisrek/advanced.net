using LiteDB;
using Microsoft.Extensions.Configuration;

namespace Cart.DAL
{
    public class CartRepository
    {
        private readonly string _connString;
        private readonly IConfiguration _configuration;

        public CartRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connString = configuration.GetConnectionString("CartDb") ?? throw new Exception("CartDb missing");
        }

        public List<Models.CartItem> GetCartItems(long cartId)
        {
            using (var db = new LiteDatabase(_connString))
            {
                var col = db.GetCollection<Models.CartItem>("cart_items");

                return col.Find(x => x.CartId == cartId).ToList();
            }
        }

        public void AddItemToCart(Models.CartItem cartItem)
        {
            using (var db = new LiteDatabase(_connString))
            {
                var col = db.GetCollection<Models.CartItem>("cart_items");

                col.Insert(cartItem);
            }
        }

        public void RemoveCartItem(Models.CartItem cartItem) => RemoveCartItem(cartItem.CartId, cartItem.ItemId);

        public void RemoveCartItem(long cartId, long itemId)
        {
            using (var db = new LiteDatabase(_connString))
            {
                var col = db.GetCollection<Models.CartItem>("cart_items");

                col.DeleteMany(x => x.CartId == cartId && x.ItemId == itemId);
            }
        }
    }
}