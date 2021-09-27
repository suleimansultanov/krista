using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KristaShop.Business.DTOs;
using KristaShop.Common.Models;

namespace KristaShop.Business.Interfaces
{
    public interface IFeedbackService
    {
        Task<FeedbackDTO> GetFeedbackDetails(Guid id);
        Task<List<FeedbackDTO>> GetFeedbacks();
        Task<OperationResult> InsertFeedback(FeedbackDTO dto);
        Task<OperationResult> UpdateFeedback(Guid id, Guid userId);
    }
}