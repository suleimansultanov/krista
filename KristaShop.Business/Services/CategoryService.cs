using AutoMapper;
using KristaShop.Business.DTOs;
using KristaShop.Business.Interfaces;
using KristaShop.Common.Models;
using KristaShop.DataAccess.Entities;
using KristaShop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristaShop.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICacheRepository<Category> _catRepo;
        private readonly IShopRepository<NomCategory> _nomCatRepo;
        private readonly IMapper _mapper;

        public CategoryService
            (ICacheRepository<Category> catRepo, IShopRepository<NomCategory> nomCatRepo, IMapper mapper)
        {
            _catRepo = catRepo;
            _nomCatRepo = nomCatRepo;
            _mapper = mapper;
        }

        public List<CategoryDTO> GetCategoriesByNomId(Guid nomId)
        {
            var ctgrs = _nomCatRepo.QueryFindBy(x => x.nom_id == nomId).Select(x => x.Category).ToList();
            return _mapper.Map<List<CategoryDTO>>(ctgrs);
        }

        public async Task<List<CategoryDTO>> GetCategories()
        {
            var nomCtgrs = await _nomCatRepo.GetAllAsync();
            var categories = await _catRepo.GetAllAsync();
            var dto = categories
                .Select(s => new CategoryDTO
                {
                    Id = s.id,
                    Name = s.name,
                    IsVisible = s.is_visible,
                    NomCount = nomCtgrs.Where(x => x.category_id == s.id).Count()
                }).ToList();
            return dto;
        }

        public async Task<CategoryDTO> GetCategoryDetails(Guid id)
        {
            var entity = await _catRepo.FindByIdAsync(id);
            return _mapper.Map<CategoryDTO>(entity);
        }

        public async Task<OperationResult> InsertCategory(CategoryDTO dto)
        {
            var entity = _mapper.Map<Category>(dto);
            entity.id = Guid.NewGuid();
            entity.is_visible = true;
            if (await _catRepo.AddAsync(entity) == null)
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");

            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }

        public async Task<OperationResult> UpdateCategory(CategoryDTO dto)
        {
            var entity = _mapper.Map<Category>(dto);
            if (await _catRepo.UpdateAsync(entity) == null)
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");

            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }

        public async Task<OperationResult> DeleteCategory(Guid id)
        {
            var entity = await _catRepo.FindByIdAsync(id);
            await _catRepo.RemoveAsync(entity);
            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }
    }
}
