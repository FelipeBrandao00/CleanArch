﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectCleanArch.Application.DTOs;
using projectCleanArch.Application.Interfaces;

namespace projectCleanArch.WebUI.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategories();
            return View(categories);
        }

        [HttpGet()]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO category)
        {
            if(ModelState.IsValid)
            {
                await _categoryService.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null) return NotFound();
            var categoryDTO = await _categoryService.GetCategoryById(id.Value);
            if(categoryDTO == null) return NotFound();
            return View(categoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    await _categoryService.Update(categoryDTO);
                }
                catch (Exception)
                {

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDTO);
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null) return NotFound();
            var categoryDTO = await _categoryService.GetCategoryById(id.Value);
            if(categoryDTO == null) return NotFound();
            return View(categoryDTO);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null) return NotFound();
            var categoryDTO = await _categoryService.GetCategoryById(id.Value);
            if(categoryDTO == null) return NotFound();
            return View(categoryDTO);
        }



    }
}
