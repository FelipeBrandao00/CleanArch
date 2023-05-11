using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using projectCleanArch.Application.DTOs;
using projectCleanArch.Application.Interfaces;
using projectCleanArch.Application.Services;

namespace projectCleanArch.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment)
        {
            this._productService = productService;
            this._categoryService = categoryService;
            this._environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Products = await _productService.GetProducts();
            return View(Products);
        }

        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.Create(productDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(productDTO);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var ProductDTO = await _productService.GetById(id.Value);
            if (ProductDTO == null) return NotFound();
            var categories = await _categoryService.GetCategories();
            ViewBag.CategoryId = new SelectList(categories, "Id","Name",ProductDTO.CategoryId);
            return View(ProductDTO);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.Update(productDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(productDTO);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var productDTO = await _productService.GetById(id.Value);
            if (productDTO == null) return NotFound();
            return View(productDTO);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null) return NotFound();
            var productDTO = await _productService.GetById(id.Value);

            if (productDTO == null) return NotFound();
            var wwroot = _environment.WebRootPath;
            var image = Path.Combine(wwroot, "images\\" + productDTO.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;

            return View(productDTO);
        }



    }
}
