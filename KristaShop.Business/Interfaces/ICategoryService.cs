using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KristaShop.Business.DTOs;
using KristaShop.Common.Models;

namespace KristaShop.Business.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryDTO> GetCategoriesByNomId(Guid nomId);
        Task<OperationResult> DeleteCategory(Guid id);
        Task<List<CategoryDTO>> GetCategories();
        Task<CategoryDTO> GetCategoryDetails(Guid id);
        Task<OperationResult> InsertCategory(CategoryDTO dto);
        Task<OperationResult> UpdateCategory(CategoryDTO dto);
    }
}