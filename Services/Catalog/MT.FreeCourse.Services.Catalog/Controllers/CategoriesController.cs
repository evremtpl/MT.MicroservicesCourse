﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MT.FreeCourse.Services.Catalog.Dtos;
using MT.FreeCourse.Services.Catalog.Services;
using MT.FreeCourse.Shared.ControllerBases;

namespace MT.FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories= await _categoryService.GetAllAsync();   

            return CreateActionResultInstance(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            return CreateActionResultInstance(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var category = await _categoryService.CreateAsync(categoryDto);

            return CreateActionResultInstance(category);
        }
    }
}