using AutoMapper;
using KristaShop.Business.DTOs;
using KristaShop.Business.Interfaces;
using KristaShop.Common.Enums;
using KristaShop.Common.Extensions;
using KristaShop.Common.Models;
using KristaShop.DataAccess.Entities;
using KristaShop.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristaShop.Business.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICacheRepository<Catalog> _catRepo;
        private readonly IShopRepository<NomCatalog> _nomCatRepo;
        private readonly IMapper _mapper;

        public CatalogService
            (ICacheRepository<Catalog> catRepo, IShopRepository<NomCatalog> nomCatRepo, IMapper mapper)
        {
            _catRepo = catRepo;
            _nomCatRepo = nomCatRepo;
            _mapper = mapper;
        }

        public List<CatalogDTO> GetCatalogsByNomId(Guid nomId)
        {
            var ctlgs = _nomCatRepo.QueryFindBy(x => x.nom_id == nomId).Select(x => x.Catalog).ToList();
            return _mapper.Map<List<CatalogDTO>>(ctlgs);
        }

        public async Task<List<CatalogDTO>> GetCatalogs()
        {
            var nomCtlgs = await _nomCatRepo.GetAllAsync();
            var catalogs = await _catRepo.GetAllAsync();
            var dto = catalogs
                .Select(s => new CatalogDTO
                {
                    Id = s.id,
                    Name = s.name,
                    Uri = s.uri,
                    OrderFormName = EnumExtensions<OrderFormType>.GetDisplayValue((OrderFormType)s.order_form),
                    Order = s.order,
                    IsVisible = s.is_visible,
                    NomCount = nomCtlgs.Where(x => x.catalog_id == s.id).Count()
                }).ToList();
            return dto;
        }

        public async Task<CatalogDTO> GetCatalogDetailsNoTrack(Guid id)
        {
            var entity = await _catRepo.QueryFindBy(x => x.id == id).AsNoTracking().FirstOrDefaultAsync();
            return _mapper.Map<CatalogDTO>(entity);
        }

        public async Task<CatalogDTO> GetCatalogDetails(Guid id)
        {
            var entity = await _catRepo.FindByIdAsync(id);
            return _mapper.Map<CatalogDTO>(entity);
        }

        public async Task<OperationResult> InsertCatalog(CatalogDTO dto)
        {
            var entity = _mapper.Map<Catalog>(dto);
            var lastOrder = _catRepo.Query().OrderByDescending(s => s.order)
                         .FirstOrDefault()?.order;
            if (lastOrder == null)
                entity.order = 1;
            else
                entity.order = lastOrder.Value + 1;
            entity.id = Guid.NewGuid();
            entity.is_visible = true;
            if (await _catRepo.AddAsync(entity) == null)
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");

            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }

        public async Task<OperationResult> UpdateCatalog(CatalogDTO dto)
        {
            var entity = _mapper.Map<Catalog>(dto);
            if (await _catRepo.UpdateAsync(entity) == null)
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");

            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }

        public async Task<OperationResult> DeleteCatalog(Guid id)
        {
            var entity = await _catRepo.FindByIdAsync(id);
            await _catRepo.RemoveAsync(entity);
            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }
    }
}
