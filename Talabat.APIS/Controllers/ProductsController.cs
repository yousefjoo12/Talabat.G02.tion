using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entites;
using Talabat.Core.Reposeitories.Contract;
using Talabat.Core.Specifications.ProductSpecifications;

namespace Talabat.APIS.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _repository;

        public ProductsController(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync()
        {
            var spec = new ProductWithBrandAndCategotySpecifications();
            var products = await _repository.GetAllWithSpecAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndCategotySpecifications(id);
            var product = await _repository.GetWithspecAsync(spec);
            if (product == null)
            {
                return NotFound(new { Message = "NotFound", StatusCode = 404 });
            }
            return Ok(product);
        }
    }
}
