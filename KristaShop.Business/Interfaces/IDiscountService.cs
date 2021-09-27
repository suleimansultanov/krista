using KristaShop.Business.DTOs;
using KristaShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KristaShop.Business.Interfaces
{
    public interface IDiscountService
    {
        Task<List<DiscountDTO>> GetDiscounts(Guid discountId, int discountType);
        Task<DiscountDTO> GetDiscountDetailss(Guid discountId, int discountType);
        Task<OperationResult> AddDiscount(DiscountDTO dto);
        Task<OperationResult> UpdateDiscount(DiscountDTO dto);
        Task<OperationResult> RemoveDiscount(Guid id, int discountType);
    }
}