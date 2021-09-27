using AutoMapper;
using KristaShop.Business.DTOs;
using KristaShop.Business.Interfaces;
using KristaShop.Common.Models;
using KristaShop.DataAccess.Entities;
using KristaShop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KristaShop.Business.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IShopRepository<Feedback> _fbRepo;
        private readonly IMapper _mapper;

        public FeedbackService(IShopRepository<Feedback> fbRepo, IMapper mapper)
        {
            _fbRepo = fbRepo;
            _mapper = mapper;
        }

        public async Task<List<FeedbackDTO>> GetFeedbacks()
        {
            return _mapper.Map<List<FeedbackDTO>>(await _fbRepo.GetAllAsync());
        }

        public async Task<FeedbackDTO> GetFeedbackDetails(Guid id)
        {
            var entity = await _fbRepo.FindByIdAsync(id);
            return _mapper.Map<FeedbackDTO>(entity);
        }

        public async Task<OperationResult> InsertFeedback(FeedbackDTO dto)
        {
            var entity = _mapper.Map<Feedback>(dto);
            if (await _fbRepo.AddAsync(entity) == null)
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");

            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }

        public async Task<OperationResult> UpdateFeedback(Guid id, Guid userId)
        {
            var entity = await _fbRepo.FindByIdAsync(id);
            entity.viewed = true;
            entity.view_time_stamp = DateTime.Now;
            entity.user_id = userId;
            if (await _fbRepo.UpdateAsync(entity) == null)
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");

            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }
    }
}
