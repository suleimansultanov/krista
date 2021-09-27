using AutoMapper;
using KristaShop.Business.DTOs;
using KristaShop.Business.Interfaces;
using KristaShop.DataReadOnly.Interfaces;
using KristaShop.WebUI.Filters;
using KristaShop.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace KristaShop.WebUI.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(PermissionFilter))]
    public class UrlAclController : Controller
    {
        private readonly IUrlAclService _urlAclService;
        private readonly IDictionaryService _dictService;
        private readonly IMapper _mapper;

        public UrlAclController
            (IUrlAclService urlAclService, IDictionaryService dictService, IMapper mapper)
        {
            _urlAclService = urlAclService;
            _dictService = dictService;
            _mapper = mapper;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> LoadData() => Ok(await _urlAclService.GetUrlAcls());

        public IActionResult Create()
        {
            ViewData["AccessControls"] = new SelectList(_dictService.GetAcls(), "Id", "Name");
            ViewData["AccessGroups"] = new SelectList(_dictService.GetGroups(), "GroupId", "Name");
            ViewData["DeniedGroups"] = new SelectList(_dictService.GetGroups(), "GroupId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UrlAccessViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<UrlAccessDTO>(model);
                await _urlAclService.InsertUrlAcl(dto);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccessControls"] = new SelectList(_dictService.GetAcls(), "Id", "Name", model.Acl);
            ViewData["AccessGroups"] = new SelectList(_dictService.GetGroups(), "GroupId", "Name", model.AccessGroupsJson);
            ViewData["DeniedGroups"] = new SelectList(_dictService.GetGroups(), "GroupId", "Name", model.DeniedGroupsJson);
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var dto = await _urlAclService.GetUrlAclDetails(id.Value);
            var model = _mapper.Map<UrlAccessViewModel>(dto);
            if (model == null)
                return NotFound();

            ViewData["AccessControls"] = new SelectList(_dictService.GetAcls(), "Id", "Name", model.Acl);
            ViewData["AccessGroups"] = new SelectList(_dictService.GetGroups(), "GroupId", "Name", model.AccessGroupsJson);
            ViewData["DeniedGroups"] = new SelectList(_dictService.GetGroups(), "GroupId", "Name", model.DeniedGroupsJson);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UrlAccessViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<UrlAccessDTO>(model);
                await _urlAclService.UpdateUrlAcl(dto);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccessControls"] = new SelectList(_dictService.GetAcls(), "Id", "Name", model.Acl);
            ViewData["AccessGroups"] = new SelectList(_dictService.GetGroups(), "GroupId", "Name", model.AccessGroupsJson);
            ViewData["DeniedGroups"] = new SelectList(_dictService.GetGroups(), "GroupId", "Name", model.DeniedGroupsJson);
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var dto = await _urlAclService.GetUrlAclDetails(id.Value);
            var model = _mapper.Map<UrlAccessViewModel>(dto);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _urlAclService.DeleteUrlAcl(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
