using KristaShop.Business.DTOs;
using KristaShop.Common.Models;
using KristaShop.DataReadOnly.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KristaShop.Business.Interfaces
{
    public interface INomService
    {
        List<CModelDTO> GetNomModels();
        Task<OperationResult> AddNomModel(NomModelDTO dto);
        List<NomUserDTO> GetNomUsers(UserDTO user, Guid nomId);
        Task<NomModelDTO> GetNomModel(Guid id);

        List<Dictionary<string, object>> GetProdCtlgs(Guid nomId, Guid ctlgId);
        Task AddVisProdCtlg(Guid nomId, Guid ctlgId, Guid prodId);

        List<Dictionary<string, object>> GetProdCtgrs(Guid nomId, Guid ctgrId);
        Task AddVisProdCtgr(Guid nomId, Guid ctgrId, Guid prodId);

        List<Dictionary<string, object>> GetPrices(Guid nomId);
        Task AddProdPrice(Guid nomId, Guid prodId, double price);

        List<Dictionary<string, object>> GetSHAmounts(Guid nomId);

        Task<List<PhotoDTO>> GetNomPhotos(Guid nomId);
        Task<OperationResult> AddColorPhoto(Guid photoId, Guid colorId);
        Task<OperationResult> RemovePhoto(Guid photoId);

        List<CModelDTO> GetNomModelsByCatalog(Guid id);
        Task<OperationResult> AddModelsCtlg(Guid id, List<Guid> modelIds);

        List<CModelDTO> GetNomModelsByCategory(Guid id);
        Task<OperationResult> AddModelsCtgr(Guid id, List<Guid> modelIds);

        Task<CModelDTO> GetCatalogModel(Guid id);
        List<ColorDTO> GetColorsByNomId(Guid nomId);
        Task UpdateNomCatalog(Guid id, Guid catId, int toPosition);
        Task<OperationResult> UpdateNomCatalogOrders(Guid id, Guid catId, int toPosition);
    }
}