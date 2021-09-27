using KristaShop.Business.DTOs;
using KristaShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KristaShop.Business.Interfaces
{
    public interface ISettingService
    {
        Task<List<SettingDTO>> GetSettings();
        Task<SettingDTO> GetSettingByKey(string key);
        Task<SettingDTO> GetSettingDetails(Guid id);
        Task<OperationResult> InsertSetting(SettingDTO dto);
        Task<OperationResult> UpdateSetting(SettingDTO dto);
        Task<OperationResult> DeleteSetting(Guid id);
    }
}
