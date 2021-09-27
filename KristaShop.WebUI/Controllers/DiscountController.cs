using AutoMapper;
using KristaShop.Business.DTOs;
using KristaShop.Business.Interfaces;
using KristaShop.Common.Models;
using KristaShop.DataReadOnly.Interfaces;
using KristaShop.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace KristaShop.WebUI.Controllers
{
    [Authorize]
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        private readonly IDictionaryService _dictService;
        private readonly ICatalogService _catalogService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public DiscountController
            (IDiscountService discountService, IDictionaryService dictService,
            ICatalogService catalogService, ICategoryService categoryService,IMapper mapper)
        {
            _discountService = discountService;
            _dictService = dictService;
            _catalogService = catalogService;
            _categoryService = categoryService;
            _mapper = mapper;
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

        public IActionResult IndexType(Guid discountId, int discountType)
        {
            DiscountViewModel model = new DiscountViewModel
            {
                DiscountId = discountId,
                DiscountType = discountType
            };
            return View(model);
        }

        public async Task<IActionResult> LoadDataType(Guid discountId, int discountType)
        {
            var dto = await _discountService.GetDiscounts(discountId, discountType);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(DiscountViewModel model)
        {
            OperationResult result = new OperationResult();
            var dto = _mapper.Map<DiscountDTO>(model);
            if (model.Id == Guid.Empty)
                result = await _discountService.AddDiscount(dto);
            else
                result = await _discountService.UpdateDiscount(dto);

            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        public async Task<IActionResult> Details(Guid entityId, int entityType) => Ok(await _discountService.GetDiscountDetailss(entityId, entityType));

        [HttpPost]
        public async Task<IActionResult> Delete(Guid entityId, int entityType) => Ok(await _discountService.RemoveDiscount(entityId, entityType));
    }
}