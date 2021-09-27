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
    public class MBodyController : Controller
    {
        private readonly IMContentService _mcontentService;
        private readonly IMapper _mapper;

        public MBodyController(IMContentService mcontentService, IMapper mapper)
        {
            _mcontentService = mcontentService;
            _mapper = mapper;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> LoadData() => Ok(await _mcontentService.GetMContents());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(MContentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<MenuContentDTO>(model);
                await _mcontentService.InsertMContent(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var dto = await _mcontentService.GetMContentDetails(id.Value);
            var model = _mapper.Map<MContentViewModel>(dto);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MContentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<MenuContentDTO>(model);
                await _mcontentService.UpdateMContent(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var dto = await _mcontentService.GetMContentDetails(id.Value);
            var model = _mapper.Map<MContentViewModel>(dto);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mcontentService.DeleteMContent(id);
            return RedirectToAction(nameof(Index));
        }
    }
}