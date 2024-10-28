﻿using Talabat.Core.Order_Aggregrate;

namespace Talabat.APIS.DTOs
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; }
        public Address ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }

        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
        public decimal SubTotal { get; set; } // OrderItem * Quantity
        public decimal Total { get; set; }
        public string PaymentIntenId { get; set; } = string.Empty;
    }
}
