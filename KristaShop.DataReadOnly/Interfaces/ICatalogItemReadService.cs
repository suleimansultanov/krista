using KristaShop.DataReadOnly.DTOs;
using KristaShop.DataReadOnly.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KristaShop.DataReadOnly.Interfaces
{
    public interface ICatalogItemReadService
    {
        List<CModelGridDTO> GetCModelGrid(Guid nomId, List<Guid> productIds);
        List<Dictionary<string, object>> GetColorsSizesGrid(Guid nomId, List<Guid> productIds);
        List<Dictionary<string, object>> GetSHAmountGrid(Guid nomId);
        List<CModelDTO> GetCatalogModels();
        Task<CModelDTO> GetCatalogModel(Guid id);
        List<ColorDTO> GetColorsByNomId(Guid nomId);
    }
}
