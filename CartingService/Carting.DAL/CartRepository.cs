using LiteDB;

namespace Cart.DAL
{
    public class CartRepository
    {
        const string dbPath = @"C:\Users\Karolis_Rekasius\Desktop\Advanced .net\CartingService\CartDb\Cart.db";

        public List<Models.CartItem> GetCartItems(long cartId)
        {
            using (var db = new LiteDatabase(dbPath))
            {
                var col = db.GetCollection<Models.CartItem>("cart_items");

                return col.Find(x => x.CartId == cartId).ToList();
            }
        }

        public void AddItemToCart(Models.CartItem cartItem)
        {
            using (var db = new LiteDatabase(dbPath))
            {
                var col = db.GetCollection<Models.CartItem>("cart_items");

                col.Insert(cartItem);
            }
        }

        public void RemoveCartItem(Models.CartItem cartItem)
        {
            using (var db = new LiteDatabase(dbPath))
            {
                var col = db.GetCollection<Models.CartItem>("cart_items");

                col.DeleteMany(x => x.CartId == cartItem.CartId && x.ItemId == cartItem.ItemId);
            }
        }
    }
}