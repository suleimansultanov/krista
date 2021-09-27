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
using System.Text;
using System.Threading.Tasks;

namespace KristaShop.Business.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IShopRepository<CatalogDiscount> _ctlgDiscountRepo;
        private readonly IShopRepository<NomDiscount> _nomDiscountRepo;
        private readonly IShopRepository<UserDiscount> _userDiscountRepo;

        private readonly IMapper _mapper;

        public DiscountService
            (IShopRepository<CatalogDiscount> ctlgDiscountRepo, IShopRepository<NomDiscount> nomDiscountRepo,
            IShopRepository<UserDiscount> userDiscountRepo, IMapper mapper)
        {
            _ctlgDiscountRepo = ctlgDiscountRepo;
            _nomDiscountRepo = nomDiscountRepo;
            _userDiscountRepo = userDiscountRepo;
            _mapper = mapper;
        }

        public async Task<List<DiscountDTO>> GetDiscounts(Guid discountId, int discountType)
            => discountType switch
            {
                (int)DiscountType.Catalog => await GetCtlgDiscounts(discountId),
                (int)DiscountType.Nom => await GetNomDiscounts(discountId),
                (int)DiscountType.User => await GetUserDiscounts(discountId),
                _ => null
            };

        private async Task<List<DiscountDTO>> GetCtlgDiscounts(Guid discountId)
        {
            var list = await _ctlgDiscountRepo.GetAllFindByAsync(x => x.catalog_id == discountId);
            var dto = _mapper.Map<List<DiscountDTO>>(list.ToList());
            dto.ForEach(x => x.DiscountType = (int)DiscountType.Catalog);
            return dto;
        }

        private async Task<List<DiscountDTO>> GetNomDiscounts(Guid discountId)
        {
            var list = await _nomDiscountRepo.GetAllFindByAsync(x => x.nom_id == discountId);
            var dto = _mapper.Map<List<DiscountDTO>>(list.ToList());
            dto.ForEach(x => x.DiscountType = (int)DiscountType.Nom);
            return dto;
        }

        private async Task<List<DiscountDTO>> GetUserDiscounts(Guid discountId)
        {
            var list = await _userDiscountRepo.GetAllFindByAsync(x => x.user_id == discountId);
            var dto = _mapper.Map<List<DiscountDTO>>(list.ToList());
            dto.ForEach(x => x.DiscountType = (int)DiscountType.User);
            return dto;
        }


        public async Task<DiscountDTO> GetDiscountDetailss(Guid discountId, int discountType)
            => discountType switch
            {
                (int)DiscountType.Catalog => await GetCtlgDiscountDetails(discountId),
                (int)DiscountType.Nom => await GetNomDiscountDetails(discountId),
                (int)DiscountType.User => await GetUserDiscountDetails(discountId),
                _ => null
            };

        private async Task<DiscountDTO> GetCtlgDiscountDetails(Guid discountId)
        {
            var entity = await _ctlgDiscountRepo.FindByIdAsync(discountId);
            var dto = _mapper.Map<DiscountDTO>(entity);
            return dto;
        }

        private async Task<DiscountDTO> GetNomDiscountDetails(Guid discountId)
        {
            var entity = await _nomDiscountRepo.FindByIdAsync(discountId);
            var dto = _mapper.Map<DiscountDTO>(entity);
            return dto;
        }

        private async Task<DiscountDTO> GetUserDiscountDetails(Guid discountId)
        {
            var entity = await _userDiscountRepo.FindByIdAsync(discountId);
            var dto = _mapper.Map<DiscountDTO>(entity);
            return dto;
        }


        public async Task<OperationResult> AddDiscount(DiscountDTO dto)
            => dto.DiscountType switch
            {
                (int)DiscountType.Catalog => await AddCtlgDiscount(dto),
                (int)DiscountType.Nom => await AddNomDiscount(dto),
                (int)DiscountType.User => await AddUserDiscount(dto),
                _ => null
            };

        private async Task<OperationResult> AddCtlgDiscount(DiscountDTO dto)
        {
            try
            {
                var entity = _mapper.Map<CatalogDiscount>(dto);
                entity.id = Guid.NewGuid();
                await _ctlgDiscountRepo.AddAsync(entity);
                return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
            }
            catch (Exception)
            {
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");
            }
        }

        private async Task<OperationResult> AddNomDiscount(DiscountDTO dto)
        {
            try
            {
                var entity = _mapper.Map<NomDiscount>(dto);
                entity.id = Guid.NewGuid();
                await _nomDiscountRepo.AddAsync(entity);
                return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
            }
            catch (Exception)
            {
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");
            }
        }

        private async Task<OperationResult> AddUserDiscount(DiscountDTO dto)
        {
            try
            {
                var entity = _mapper.Map<UserDiscount>(dto);
                entity.id = Guid.NewGuid();
                await _userDiscountRepo.AddAsync(entity);
                return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
            }
            catch (Exception)
            {
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");
            }
        }


        public async Task<OperationResult> UpdateDiscount(DiscountDTO dto)
            => dto.DiscountType switch
            {
                (int)DiscountType.Catalog => await UpdateCtlgDiscount(dto),
                (int)DiscountType.Nom => await UpdateNomDiscount(dto),
                (int)DiscountType.User => await UpdateUserDiscount(dto),
                _ => null
            };

        private async Task<OperationResult> UpdateCtlgDiscount(DiscountDTO dto)
        {
            try
            {
                var entity = _mapper.Map<CatalogDiscount>(dto);
                await _ctlgDiscountRepo.UpdateAsync(entity);
                return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
            }
            catch (Exception)
            {
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");
            }
        }

        private async Task<OperationResult> UpdateNomDiscount(DiscountDTO dto)
        {
            try
            {
                var entity = _mapper.Map<NomDiscount>(dto);
                await _nomDiscountRepo.UpdateAsync(entity);
                return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
            }
            catch (Exception)
            {
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");
            }
        }

        private async Task<OperationResult> UpdateUserDiscount(DiscountDTO dto)
        {
            try
            {
                var entity = _mapper.Map<UserDiscount>(dto);
                await _userDiscountRepo.UpdateAsync(entity);
                return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
            }
            catch (Exception)
            {
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");
            }
        }


        public async Task<OperationResult> RemoveDiscount(Guid id, int discountType)
            => discountType switch
            {
                (int)DiscountType.Catalog => await RemoveCtlgDiscount(id),
                (int)DiscountType.Nom => await RemoveNomDiscount(id),
                (int)DiscountType.User => await RemoveUserDiscount(id),
                _ => null
            };

        private async Task<OperationResult> RemoveCtlgDiscount(Guid id)
        {
            try
            {
                var entity = await _ctlgDiscountRepo.FindByIdAsync(id);
                await _ctlgDiscountRepo.RemoveAsync(entity);
                return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
            }
            catch (Exception)
            {
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");
            }
        }

        private async Task<OperationResult> RemoveNomDiscount(Guid id)
        {
            try
            {
                var entity = await _nomDiscountRepo.FindByIdAsync(id);
                await _nomDiscountRepo.RemoveAsync(entity);
                return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
            }
            catch (Exception)
            {
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");
            }
        }

        private async Task<OperationResult> RemoveUserDiscount(Guid id)
        {
            try
            {
                var entity = await _userDiscountRepo.FindByIdAsync(id);
                await _userDiscountRepo.RemoveAsync(entity);
                return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
            }
            catch (Exception)
            {
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");
            }
        }
    }
}
