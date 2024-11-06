using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Order_Aggregrate
{
    public class Order : BaseEntity
    {
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail; 
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }
        public Order()
        {
            
        }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; }
        public Address ShippingAddress { get; set; }
        public int? DeliveryMethodId { get; set; } // Fk
        public DeliveryMethod? DeliveryMethod { get; set; } // relation 

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; } // OrderItem * Quantity
        public decimal GetTotal()  
            => SubTotal* DeliveryMethod.Cost;
         public string? PaymentIntenId { get; set; } = string.Empty;

    }
}
