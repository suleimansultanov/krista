using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KristaShop.Business.DTOs;
using KristaShop.Common.Models;

namespace KristaShop.Business.Interfaces
{
    public interface ICatalogService
    {
        Task<CatalogDTO> GetCatalogDetailsNoTrack(Guid id);
        List<CatalogDTO> GetCatalogsByNomId(Guid nomId);
        Task<OperationResult> DeleteCatalog(Guid id);
        Task<CatalogDTO> GetCatalogDetails(Guid id);
        Task<List<CatalogDTO>> GetCatalogs();
        Task<OperationResult> InsertCatalog(CatalogDTO dto);
        Task<OperationResult> UpdateCatalog(CatalogDTO dto);
    }
}