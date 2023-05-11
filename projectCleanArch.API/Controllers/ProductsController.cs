using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectCleanArch.Application.DTOs;
using projectCleanArch.Application.Interfaces;

namespace projectCleanArch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
        {
            var products = await _productService.GetProducts();
            if (products == null) return NotFound("Products not found");
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetByIdProduct")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null) return NotFound("Product not found");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null) return BadRequest("Invalid Data");

            await _productService.Create(productDTO);
            return new CreatedAtRouteResult("GetByIdProduct", new { id = productDTO.Id }, productDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.Id) return BadRequest();
            if (productDTO == null) return BadRequest();

            await _productService.Update(productDTO);
            return Ok(productDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null) return NotFound("Product not found");
            await _productService.Remove(id);
            return Ok(product);
        }
    }
}
