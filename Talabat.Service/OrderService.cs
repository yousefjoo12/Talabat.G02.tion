using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Order_Aggregrate;
using Talabat.Core.Reposeitories.Contract;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketReposeitory _basketReposeitory;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketReposeitory basketReposeitory, IUnitOfWork unitOfWork)
        {
            _basketReposeitory = basketReposeitory;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int delveryMethodId, Address shippingAddress)
        {
            // 1 get basket from basketReposeitory
            var basket = await _basketReposeitory.GetBasketAsync(basketId);
            // 2 get selected itemsat basket from ProductRepository
            var orderItems = new List<OrderItem>();
            if (basket?.Item?.Count > 0)
            {
                foreach (var item in basket.Item)
                {
                    var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    var productItemOrder = new ProductItemOrder(item.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrder, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }

            }
            // 3 calculate Subtotal
            var subTotal = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);
            // 4 get deliver methods from  DeliveryMethodRepository
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(delveryMethodId);
            // 5 create order 
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal);
            await _unitOfWork.Repository<Order>().AddAsync(order);
            // 6 save to database

            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null; 
            return order;
        }

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
