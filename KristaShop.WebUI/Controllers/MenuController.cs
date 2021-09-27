using AutoMapper;
using KristaShop.Business.DTOs;
using KristaShop.Business.Interfaces;
using KristaShop.WebUI.Filters;
using KristaShop.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KristaShop.WebUI.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(PermissionFilter))]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;

        public MenuController(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> LoadData() => Ok(await _menuService.GetMenus());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(MenuItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<MenuItemDTO>(model);
                await _menuService.InsertMenu(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var dto = await _menuService.GetMenuDetails(id.Value);
            var model = _mapper.Map<MenuItemViewModel>(dto);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MenuItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<MenuItemDTO>(model);
                await _menuService.UpdateMenu(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var dto = await _menuService.GetMenuDetails(id.Value);
            var model = _mapper.Map<MenuItemViewModel>(dto);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _menuService.DeleteMenu(id);
            return RedirectToAction(nameof(Index));
        }
    }
}