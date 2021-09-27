using AutoMapper;
using KristaShop.Business.DTOs;
using KristaShop.Business.Interfaces;
using KristaShop.Common.Extensions;
using KristaShop.Common.Models;
using KristaShop.DataReadOnly.DTOs;
using KristaShop.DataReadOnly.Interfaces;
using KristaShop.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KristaShop.WebUI.Controllers
{
    [Authorize]
    public class CModelController : Controller
    {
        private readonly INomService _nomService;
        private readonly IDictionaryService _dictService;
        private readonly ICatalogService _catalogService;
        private readonly ICategoryService _categoryService;

        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CModelController
            (INomService nomService, IDictionaryService dictService,
            ICatalogService catalogService, ICategoryService categoryService,
            IMapper mapper, IWebHostEnvironment env)
        {
            _nomService = nomService;
            _dictService = dictService;
            _catalogService = catalogService;
            _categoryService = categoryService;

            _mapper = mapper;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Colors = new SelectList(_dictService.GetColors(), "Name", "Name");
            ViewBag.Sizes = new SelectList(_dictService.GetSizes(), "Name", "Name");
            ViewBag.SizeLines = new SelectList(_dictService.GetSizeLines(), "Name", "Name");
            ViewBag.Catalogs = new SelectList(await _catalogService.GetCatalogs(), "Name", "Name");
            ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Name", "Name");

            return View();
        }

        public IActionResult LoadData() => Ok(_nomService.GetNomModels());

        public async Task<IActionResult> IndexByCatalog(Guid? id)
        {
            if (id == null)
                return NotFound();

            var dto = await _catalogService.GetCatalogDetails(id.Value);
            var model = _mapper.Map<CatalogViewModel>(dto);
            if (model == null)
                return NotFound();

            ViewBag.Colors = new SelectList(_dictService.GetColors(), "Name", "Name");
            ViewBag.Sizes = new SelectList(_dictService.GetSizes(), "Name", "Name");
            ViewBag.SizeLines = new SelectList(_dictService.GetSizeLines(), "Name", "Name");
            ViewBag.Catalogs = new SelectList(await _catalogService.GetCatalogs(), "Name", "Name");
            ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Name", "Name");
            return View(model);
        }
        public IActionResult LoadDataByCatalog(Guid? id) => Ok(_nomService.GetNomModelsByCatalog(id.Value));
        [HttpPost]
        public async Task UpdateRow(Guid id, Guid catId, int toPosition)
        {
            await _nomService.UpdateNomCatalog(id, catId, toPosition);
        }
        [HttpPost]
        public async Task<IActionResult> ReorderModel(Guid id, Guid catId, int toPosition)
        {
            var result = await _nomService.UpdateNomCatalogOrders(id, catId, toPosition);
            return Ok(result);
        }
        public async Task<IActionResult> AddModelsCtlg(Guid id, List<Guid> modelIds)
        {
            var result = await _nomService.AddModelsCtlg(id, modelIds);
            return Ok(result);
        }

        public async Task<IActionResult> IndexByCategory(Guid? id)
        {
            if (id == null)
                return NotFound();

            var dto = await _categoryService.GetCategoryDetails(id.Value);
            var model = _mapper.Map<CategoryViewModel>(dto);
            if (model == null)
                return NotFound();

            return View(model);
        }
        public IActionResult LoadDataByCategory(Guid? id) => Ok(_nomService.GetNomModelsByCategory(id.Value));
        public async Task<IActionResult> AddModelsCtgr(Guid id, List<Guid> modelIds)
        {
            var result = await _nomService.AddModelsCtgr(id, modelIds);
            return Ok(result);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var dtoRO = await _nomService.GetCatalogModel(id.Value);
            if (dtoRO == null)
                return NotFound();

            var dto = await _nomService.GetNomModel(id.Value);
            var model = _mapper.Map<NomViewModel>(dto);
            model = model ?? new NomViewModel { IsVisible = true };
            model.NomId = dtoRO.Id;
            model.Articul = dtoRO.Articul;
            model.ItemName = dtoRO.ItemName;

            ViewBag.Colors = new SelectList(_nomService.GetColorsByNomId(id.Value), "Id", "Name");
            ViewBag.Catalogs = new SelectList(await _catalogService.GetCatalogs(), "Id", "Name");
            ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            ViewBag.NomCatalogs = new SelectList(_catalogService.GetCatalogsByNomId(id.Value), "Id", "Name");
            ViewBag.NomCategories = new SelectList(_categoryService.GetCategoriesByNomId(id.Value), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NomViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string path = "/Gallery/";
                    model.ImagePath = path + model.NomId + Path.GetExtension(model.Image.FileName);
                    if (!Directory.Exists(_env.ContentRootPath + path))
                    {
                        Directory.CreateDirectory(_env.ContentRootPath + path);
                    }
                    using (var fileStream = new FileStream(_env.ContentRootPath + model.ImagePath, FileMode.OpenOrCreate))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }
                }

                var dto = _mapper.Map<NomModelDTO>(model);
                if (model.Photos != null)
                {
                    foreach (var photo in model.Photos)
                    {
                        string path = "/Gallery/";
                        string photoPath = path + Guid.NewGuid() + photo.FileName;
                        if (!Directory.Exists(_env.ContentRootPath + path))
                        {
                            Directory.CreateDirectory(_env.ContentRootPath + path);
                        }
                        using (var fileStream = new FileStream(_env.ContentRootPath + photoPath, FileMode.OpenOrCreate))
                        {
                            await photo.CopyToAsync(fileStream);
                            dto.PhotoPaths.Add(photoPath);
                        }
                    }
                }
                await _nomService.AddNomModel(dto);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Colors = new SelectList(_nomService.GetColorsByNomId(model.NomId), "Id", "Name");
            ViewBag.Catalogs = new SelectList(await _catalogService.GetCatalogs(), "Id", "Name");
            ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            ViewBag.NomCatalogs = new SelectList(_catalogService.GetCatalogsByNomId(model.NomId), "Id", "Name");
            ViewBag.NomCategories = new SelectList(_categoryService.GetCategoriesByNomId(model.NomId), "Id", "Name");
            return View(model);
        }

        public IActionResult LoadProdCtlg(Guid nomId, Guid ctlgId) => Ok(_nomService.GetProdCtlgs(nomId, ctlgId));
        [HttpPost]
        public async Task<IActionResult> ChangeVisProdCtlg(Guid nomId, Guid ctlgId, Guid prodId)
        {
            await _nomService.AddVisProdCtlg(nomId, ctlgId, prodId);
            return Ok(true);
        }

        public IActionResult LoadProdCtgr(Guid nomId, Guid ctgrId) => Ok(_nomService.GetProdCtgrs(nomId, ctgrId));
        [HttpPost]
        public async Task<IActionResult> ChangeVisProdCtgr(Guid nomId, Guid ctgrId, Guid prodId)
        {
            await _nomService.AddVisProdCtgr(nomId, ctgrId, prodId);
            return Ok(true);
        }

        public IActionResult LoadProdPrice(Guid nomId) => Ok(_nomService.GetPrices(nomId));
        [HttpPost]
        public async Task<IActionResult> ChangeProdPrice(Guid nomId, Guid prodId, double price)
        {
            await _nomService.AddProdPrice(nomId, prodId, price);
            return Ok(true);
        }

        public IActionResult LoadSHAmount(Guid nomId) => Ok(_nomService.GetSHAmounts(nomId));

        public async Task<IActionResult> LoadPhotos(Guid? id)
        {
            var list = await _nomService.GetNomPhotos(id.Value);
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> EditPhoto(Guid photoId, Guid colorId)
        {
            var result = await _nomService.AddColorPhoto(photoId, colorId);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePhoto(Guid photoId)
        {
            var result = await _nomService.RemovePhoto(photoId);
            return Ok(result);
        }

        public IActionResult LoadUsers(Guid? id)
        {
            UserDTO user = HttpContext.Session.Get<UserDTO>(GlobalConstant.SessionKeys.User);
            var list = _nomService.GetNomUsers(user, id.Value);
            return Ok(list);
        }
    }
}