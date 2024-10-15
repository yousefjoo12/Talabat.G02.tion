using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Erorrs;
using Talabat.APIS.Helpers;
using Talabat.Core.Entites;
using Talabat.Core.Reposeitories.Contract;
using Talabat.Core.Specifications.ProductSpecifications;

namespace Talabat.APIS.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IGenericRepository<ProductBrand> _productbrand;
        private readonly IGenericRepository<ProductCategory> _productcategory;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> repository,
            IGenericRepository<ProductBrand> productbrand,
            IGenericRepository<ProductCategory> productcategory,
            IMapper mapper)
        {
            _repository = repository;
            _productbrand = productbrand;
            _productcategory = productcategory;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductsDTO>>> GetProductsAsync([FromQuery] ProductsSpecParams specParams)
        {
            var spec = new ProductWithBrandAndCategotySpecifications(specParams);
            var products = await _repository.GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductsDTO>>(products);
            var countspec = new ProductWithFilterationCountSpec(specParams);

            var count = await _repository.GetCountAsync(countspec);

            return Ok(new Pagination<ProductsDTO>(specParams.PageSize,specParams.PageIndex, count, data));
        }

        [ProducesResponseType(typeof(ProductsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndCategotySpecifications(id);
            var product = await _repository.GetWithspecAsync(spec);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<Product, ProductsDTO>(product));
        }

        [HttpGet("Categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategory()
        {
            var Category = await _productcategory.GetAllAsync();
            return Ok(Category);
        }
        [HttpGet("Brands")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrand()
        {
            var Brand = await _productbrand.GetAllAsync();
            return Ok(Brand);
        }
    }
}
