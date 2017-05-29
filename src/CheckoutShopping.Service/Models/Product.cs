using System.ComponentModel.DataAnnotations;

namespace CheckoutShopping.Service.Models
{
    public class Product
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Image { get; set; }
        public decimal ShippingCost { get; set; }
        public string TrackingUrl { get; set; }
    }
}
