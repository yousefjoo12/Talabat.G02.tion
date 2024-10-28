using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Order_Aggregrate;

namespace Talabat.Core.Specifications.OrderSpecifications
{
    public class OrderSpecif:BaseSpecifications<Order>
    {
        public OrderSpecif(string buyerEmail) :base(o=>o.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
            AddOrderBy(o => o.OrderDate);


        }

        public OrderSpecif(int orderId, string buyerEmail) : base(o => o.BuyerEmail == buyerEmail&& o.Id == orderId)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
          
        }
    }
}
