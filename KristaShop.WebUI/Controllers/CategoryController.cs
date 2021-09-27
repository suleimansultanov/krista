using AutoMapper;
using KristaShop.Business.DTOs;
using KristaShop.Business.Interfaces;
using KristaShop.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KristaShop.WebUI.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _catService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService catService, IMapper mapper)
        {
            _catService = catService;
            _mapper = mapper;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> LoadData() => Ok(await _catService.GetCategories());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<CategoryDTO>(model);
                await _catService.InsertCategory(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var dto = await _catService.GetCategoryDetails(id.Value);
            var model = _mapper.Map<CategoryViewModel>(dto);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<CategoryDTO>(model);
                await _catService.UpdateCategory(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var dto = await _catService.GetCategoryDetails(id.Value);
            var model = _mapper.Map<CategoryViewModel>(dto);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _catService.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}