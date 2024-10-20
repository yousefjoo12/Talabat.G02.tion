using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Erorrs;
using Talabat.Core.Entites;
using Talabat.Core.Reposeitories.Contract;

namespace Talabat.APIS.Controllers
{

    public class BasketController : BaseApiController
    {
        private readonly IBasketReposeitory _basketReposeitory;
        private readonly IMapper _mapper;

        public BasketController(IBasketReposeitory basketReposeitory,IMapper mapper)
        {
            _basketReposeitory = basketReposeitory;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketReposeitory.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO basket)
        {
            // mapping  => from Dto[CustomerBasketDTO] to model[CustomerBasket]
            var mappedbasket = _mapper.Map<CustomerBasketDTO, CustomerBasket>(basket);
            var CreatOrUpdateBasket = await _basketReposeitory.UpdateBasketAsync(mappedbasket);
            if (CreatOrUpdateBasket is null) return BadRequest(new ApiResponse(400));
            return Ok(CreatOrUpdateBasket);
        }
        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basketReposeitory.DeleteBasketAsync(id);
        }

    }
}
