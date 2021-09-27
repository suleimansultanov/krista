using KristaShop.Business.DTOs;
using KristaShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KristaShop.Business.Interfaces
{
    public interface IMContentService
    {
        Task<List<MenuContentDTO>> GetMContents();
        Task<MenuContentDTO> GetMContentByMenuTitle(string url);
        Task<MenuContentDTO> GetMContentDetails(Guid id);
        Task<OperationResult> InsertMContent(MenuContentDTO dto);
        Task<OperationResult> UpdateMContent(MenuContentDTO dto);
        Task<OperationResult> DeleteMContent(Guid id);
    }
}
