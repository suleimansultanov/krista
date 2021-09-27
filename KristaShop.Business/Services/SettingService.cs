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
    public class SettingService : ISettingService
    {
        private readonly ICacheRepository<Setting> _settingRepo;
        private readonly IMapper _mapper;

        public SettingService
            (ICacheRepository<Setting> settingRepo, IMapper mapper)
        {
            _settingRepo = settingRepo;
            _mapper = mapper;
        }

        public async Task<List<SettingDTO>> GetSettings()
        {
            return _mapper.Map<List<SettingDTO>>(await _settingRepo.GetAllAsync());
        }

        public async Task<SettingDTO> GetSettingByKey(string key)
        {
            var entity = await _settingRepo.FindByFilterAsync(x => x.key == key);
            return _mapper.Map<SettingDTO>(entity);
        }

        public async Task<SettingDTO> GetSettingDetails(Guid id)
        {
            var entity = await _settingRepo.FindByIdAsync(id);
            return _mapper.Map<SettingDTO>(entity);
        }

        public async Task<OperationResult> InsertSetting(SettingDTO dto)
        {
            var entity = _mapper.Map<Setting>(dto);
            entity.id = Guid.NewGuid();
            if (await _settingRepo.AddAsync(entity) == null)
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");

            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }

        public async Task<OperationResult> UpdateSetting(SettingDTO dto)
        {
            var entity = _mapper.Map<Setting>(dto);
            if (await _settingRepo.UpdateAsync(entity) == null)
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");

            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }

        public async Task<OperationResult> DeleteSetting(Guid id)
        {
            var entity = await _settingRepo.FindByIdAsync(id);
            await _settingRepo.RemoveAsync(entity);
            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }
    }
}
