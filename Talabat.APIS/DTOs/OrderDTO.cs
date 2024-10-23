using System.ComponentModel.DataAnnotations;
using Talabat.Core.Order_Aggregrate;


namespace Talabat.APIS.DTOs
{
    public class OrderDTO
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]  
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; } 
        public AddressDTO ShippingAddress { get; set; }
    }
}
