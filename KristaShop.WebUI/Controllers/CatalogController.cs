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
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catService;
        private readonly IMapper _mapper;

        public CatalogController(ICatalogService catService, IMapper mapper)
        {
            _catService = catService;
            _mapper = mapper;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> LoadData() => Ok(await _catService.GetCatalogs());

        [HttpPost]
        public async Task UpdateRow(Guid id, int fromPosition, int toPosition)
        {
            var dto = await _catService.GetCatalogDetailsNoTrack(id);
            dto.Order = toPosition;
            await _catService.UpdateCatalog(dto);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CatalogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<CatalogDTO>(model);
                await _catService.InsertCatalog(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var dto = await _catService.GetCatalogDetails(id.Value);
            var model = _mapper.Map<CatalogViewModel>(dto);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CatalogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<CatalogDTO>(model);
                await _catService.UpdateCatalog(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var dto = await _catService.GetCatalogDetails(id.Value);
            var model = _mapper.Map<CatalogViewModel>(dto);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _catService.DeleteCatalog(id);
            return RedirectToAction(nameof(Index));
        }
    }
}