using System;

namespace Cart.DAL.Models
{
    public class CartItem
    {
        public long Id { get; set; }
        public long CartId { get; set; }
        public long ItemId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}
