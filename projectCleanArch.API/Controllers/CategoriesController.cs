using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectCleanArch.Application.DTOs;
using projectCleanArch.Application.Interfaces;

namespace projectCleanArch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var categories = await _categoryService.GetCategories();
            if (categories == null) return NotFound("Categories not found");
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetByIdCategory")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null) return NotFound("Category not found");
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null) return BadRequest("Invalid Data");

            await _categoryService.Add(categoryDTO);
            return new CreatedAtRouteResult("GetByIdCategory", new { id = categoryDTO.Id }, categoryDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id) return BadRequest();
            if (categoryDTO == null) return BadRequest();

            await _categoryService.Update(categoryDTO);
            return Ok(categoryDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null) return NotFound("Category not found");
            await _categoryService.Remove(id);
            return Ok(category);
        }
    }
}
