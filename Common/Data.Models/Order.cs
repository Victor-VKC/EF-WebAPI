using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        public string UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        public ShoppingCart ShoppingCart { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        public Address ShippingAddress { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        public Address BillingAddress { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        public PaymentMethod PaymentMethod { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        public ShippingMethod ShippingMethod { get; set; }
    }
}
