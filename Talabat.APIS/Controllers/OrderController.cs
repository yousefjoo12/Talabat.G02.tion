using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Erorrs;
using Talabat.Core.Order_Aggregrate;
using Talabat.Core.Services.Contract;

namespace Talabat.APIS.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
        {
            var address = _mapper.Map<AddressDTO, Address>(orderDTO.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(orderDTO.BuyerEmail, orderDTO.BasketId, orderDTO.DeliveryMethodId, address);
            if (order == null) return BadRequest(new ApiResponse(400));
            return Ok(order);
        }
    }
}
