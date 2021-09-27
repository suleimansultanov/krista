using KristaShop.Common.Extensions;
using KristaShop.DataReadOnly.DTOs;
using KristaShop.DataReadOnly.Interfaces;
using KristaShop.DataReadOnly.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace KristaShop.DataReadOnly.Services
{
    public class CatalogItemReadService : ICatalogItemReadService
    {
        private readonly IReadOnlyRepo<CatalogItem> _ciRepo;
        private readonly IReadOnlyRepo<Nomenclature> _nomRepo;
        private readonly IReadOnlyRepo<StoreHouse> _shRepo;
        private readonly IReadOnlyRepo<StoreHouseRest> _shRestRepo;

        private readonly IDictionaryService _dictionaryService;


        public CatalogItemReadService
            (IReadOnlyRepo<CatalogItem> ciRepo, IReadOnlyRepo<Nomenclature> nomRepo, IDictionaryService dictionaryService,
            IReadOnlyRepo<StoreHouse> shRepo, IReadOnlyRepo<StoreHouseRest> shRestRepo)
        {
            _ciRepo = ciRepo;
            _nomRepo = nomRepo;
            _dictionaryService = dictionaryService;
            _shRepo = shRepo;
            _shRestRepo = shRestRepo;
        }

        public List<CModelDTO> GetCatalogModels()
        {
            var noms = _nomRepo.QueryReadOnly()
                .Where(x => x.nom_type == 1 && x.visible_on_site)
                .ToList();

            var items = _ciRepo.QueryReadOnly()
                .Where(x => x.Nomenclature.nom_type == 1 && x.Nomenclature.visible_on_site)
                .Where(x => x.level == 1)
                .Include(x => x.Option)
                    .ThenInclude(x => x.NomFilterColors)
                        .ThenInclude(x => x.Color)
                .Include(x => x.Option)
                    .ThenInclude(x => x.NomFilterSizes)
                        .ThenInclude(x => x.Size)
                .Include(x => x.Option)
                    .ThenInclude(x => x.NomFilterSizeLines)
                        .ThenInclude(x => x.SizeLine); ;

            var nomList = noms
                .GroupJoin(
                    items,
                    nomencl => nomencl.id,
                    item => item.nomenclature_id,
                    (nomencl, item) => new CModelDTO
                    {
                        Id = nomencl.id,
                        ItemName = nomencl.name,
                        Articul = nomencl.articul,
                        IsVisible = nomencl.visible_on_site,
                        Colors = item
                            .SelectMany(color => color.Option.NomFilterColors
                                .Select(x => x.Color.color_name))
                            .Distinct()
                            .ToList(),
                        Sizes = nomencl.is_set ? item
                            .SelectMany(size => size.Option.NomFilterSizeLines
                                .Select(x => x.SizeLine.size_line_name))
                            .Distinct()
                            .ToList()
                            : item
                            .SelectMany(size => size.Option.NomFilterSizes
                                .Select(x => x.Size.size_name))
                            .Distinct()
                            .ToList()
                    }).ToList();

            return nomList;
        }

        public async Task<CModelDTO> GetCatalogModel(Guid id)
        {
            var cmodel = await _nomRepo.FindByIdAsync(id);
            if (cmodel == null)
                return null;

            CModelDTO dTO = new CModelDTO
            {
                Id = cmodel.id,
                Articul = cmodel.articul,
                ItemName = cmodel.name
            };
            return dTO;
        }

        public List<CModelGridDTO> GetCModelGrid(Guid nomId, List<Guid> productIds)
        {
            var nomList = _ciRepo.QueryFindBy(x =>
                x.Nomenclature.nom_type == 1 && x.Nomenclature.visible_on_site &&
                x.level == 1 && x.nomenclature_id == nomId)
                .Select(x => new CModelGridDTO
                {
                    ProductId = x.product_id,
                    Color = x.Option.NomFilterColors
                        .Select(x => x.Color.color_name)
                        .SingleOrDefault(),
                    Size = x.Nomenclature.is_set ? x.Option.NomFilterSizeLines
                        .Select(x => x.SizeLine.size_line_name)
                        .SingleOrDefault()
                        : x.Option.NomFilterSizes
                        .Select(x => x.Size.size_name)
                        .SingleOrDefault(),
                    NotVisible = productIds.Contains(x.product_id)
                }).ToList();
            return nomList;
        }

        public List<Dictionary<string, object>> GetColorsSizesGrid(Guid nomId, List<Guid> productIds)
        {
            var nomList = GetCModelGrid(nomId, productIds);

            var sizes = nomList.Select(x => x.Size).Distinct().ToList();
            var groupColors = nomList.GroupBy(x => x.Color)
                .Select(x => new
                {
                    Сolor = x.Key,
                    Options = x.Select(d2 => new { d2.Size, d2.NotVisible, d2.ProductId })
                }).ToList();

            var list = new List<Dictionary<string, object>>();
            foreach (var g in groupColors)
            {
                var dict = new Dictionary<string, object>();
                dict["Цвет"] = g.Сolor;
                foreach (var size in sizes)
                {
                    dict.Add(size, null);
                }
                foreach (var opt in g.Options)
                {
                    dict[opt.Size] = new OptAttributes(opt.ProductId, opt.NotVisible);
                }
                list.Add(dict);
            }
            return list;
        }

        public List<Dictionary<string, object>> GetSHAmountGrid(Guid nomId)
        {
            var shIds = _shRepo.QueryFindBy(x => x.is_for_online_store).Select(x => x.id).ToList();
            var shAmounts = _shRestRepo.QueryFindBy(x =>
                x.reservation_doc_id == Guid.Empty &&
                x.level == 1 && shIds.Contains(x.storehouse_id))
                .GroupBy(x => x.product_id)
                .Select(s => new
                {
                    ProductId = s.Key,
                    Amount = s.Sum(c => c.count)
                }).ToList();

            var nomList = GetCModelGrid(nomId, new List<Guid>());

            var bindGrid = nomList
                .Join(shAmounts,
                    nl => nl.ProductId,
                    sha => sha.ProductId,
                    (nomList, shAmounts) => new
                    {
                        nomList.Color,
                        nomList.Size,
                        shAmounts.Amount
                    }).ToList();
            var sizes = bindGrid.Select(x => x.Size).Distinct().ToList();
            var groupColors = bindGrid.GroupBy(x => x.Color)
                .Select(x => new
                {
                    Сolor = x.Key,
                    Options = x.Select(d2 => new { d2.Size, d2.Amount })
                }).ToList();

            var list = new List<Dictionary<string, object>>();
            foreach (var g in groupColors)
            {
                var dict = new Dictionary<string, object>();
                dict["Цвет"] = g.Сolor;
                foreach (var size in sizes)
                {
                    dict.Add(size, null);
                }
                foreach (var opt in g.Options)
                {
                    dict[opt.Size] = opt.Amount;
                }
                list.Add(dict);
            }
            return list;
        }

        public List<ColorDTO> GetColorsByNomId(Guid nomId)
        {
            var colors = _ciRepo.QueryFindBy(x =>
                x.Nomenclature.nom_type == 1 && x.Nomenclature.visible_on_site &&
                x.level == 1 && x.nomenclature_id == nomId)
                .Select(x => x.Option.NomFilterColors
                    .Select(x => x.Color)
                    .SingleOrDefault()
                ).ToList();

            var dto = colors.GroupBy(x => new { x.color_name, x.id }).Select(x => new ColorDTO
            {
                Id = x.Key.id,
                Name = x.Key.color_name
            }).ToList();
            return dto;
        }
    }
}
