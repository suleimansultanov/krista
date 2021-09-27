using AutoMapper;
using KristaShop.Business.DTOs;
using KristaShop.Business.Interfaces;
using KristaShop.Common.Enums;
using KristaShop.Common.Models;
using KristaShop.DataAccess.Entities;
using KristaShop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristaShop.Business.Services
{
    public class MenuService : IMenuService
    {
        private readonly ICacheRepository<MenuItem> _menuRepo;
        private readonly IMapper _mapper;

        public MenuService
            (ICacheRepository<MenuItem> menuRepo, IMapper mapper)
        {
            _menuRepo = menuRepo;
            _mapper = mapper;
        }

        public async Task<List<MenuItemDTO>> GetMenus()
        {
            return _mapper.Map<List<MenuItemDTO>>(await _menuRepo.GetAllAsync());
        }

        public List<MenuItemDTO> GetMenusByType(MenuType menuType)
        {
            return _mapper.Map<List<MenuItemDTO>>(_menuRepo.QueryFindBy(x => x.menu_type == (int)menuType));
        }

        public MenuItemDTO GetMenuByNames(string controller, string action)
        {
            return _mapper.Map<MenuItemDTO>(_menuRepo.FindByFilter(x => x.controller_name == controller && x.action_name == action));
        } 

        public async Task<MenuItemDTO> GetMenuDetails(Guid id)
        {
            var menu = await _menuRepo.FindByIdAsync(id);
            return _mapper.Map<MenuItemDTO>(menu);
        }

        public async Task<OperationResult> InsertMenu(MenuItemDTO dto)
        {
            var menu = _mapper.Map<MenuItem>(dto);
            menu.id = Guid.NewGuid();
            if (await _menuRepo.AddAsync(menu) == null)
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");

            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }

        public async Task<OperationResult> UpdateMenu(MenuItemDTO dto)
        {
            var menu = _mapper.Map<MenuItem>(dto);
            if (await _menuRepo.UpdateAsync(menu) == null)
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");

            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }

        public async Task<OperationResult> DeleteMenu(Guid id)
        {
            var menu = await _menuRepo.FindByIdAsync(id);
            await _menuRepo.RemoveAsync(menu);
            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }
    }
}
