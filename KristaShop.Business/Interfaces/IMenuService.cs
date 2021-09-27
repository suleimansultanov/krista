using KristaShop.Business.DTOs;
using KristaShop.Common.Enums;
using KristaShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristaShop.Business.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuItemDTO>> GetMenus();
        List<MenuItemDTO> GetMenusByType(MenuType menuType);
        MenuItemDTO GetMenuByNames(string controller, string action);
        Task<MenuItemDTO> GetMenuDetails(Guid id);
        Task<OperationResult> InsertMenu(MenuItemDTO dto);
        Task<OperationResult> UpdateMenu(MenuItemDTO dto);
        Task<OperationResult> DeleteMenu(Guid id);
    }
}
