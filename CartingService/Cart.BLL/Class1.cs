namespace Cart.BLL
{
    public class Class1
    {
        public void Testas()
        {
            Cart.DAL.CartRepository cartRepository = new DAL.CartRepository();

            var cartId = new Random().Next();
            var cartItem1 = new Cart.DAL.Models.CartItem()
            {
                Id = new Random().Next(),
                Name = "name1",
                CartId = cartId,
                ItemId = 1,
                Image = "image1",
                Price = 1.1M,
                Quantity = 2.3M
            };

            cartRepository.AddItemToCart(cartItem1);

            var cartItem2 = new Cart.DAL.Models.CartItem()
            {
                Id = new Random().Next(),
                Name = "name2",
                CartId = cartId,
                ItemId = 2,
                Image = "image1",
                Price = 1.1M,
                Quantity = 2.3M
            };

            cartRepository.AddItemToCart(cartItem2);

            var cartItem3 = new Cart.DAL.Models.CartItem()
            {
                Id = new Random().Next(),
                Name = "name3",
                CartId = cartId,
                ItemId = 3,
                Image = "image1",
                Price = 1.1M,
                Quantity = 2.3M
            };

            cartRepository.AddItemToCart(cartItem3);
            cartRepository.RemoveCartItem(cartItem2);
            var a = cartRepository.GetCartItems(cartId);
        }

    }
}