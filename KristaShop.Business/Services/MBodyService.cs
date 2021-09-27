using AutoMapper;
using KristaShop.Business.DTOs;
using KristaShop.Business.Interfaces;
using KristaShop.Common.Models;
using KristaShop.DataAccess.Entities;
using KristaShop.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KristaShop.Business.Services
{
    public class MBodyService : IMContentService
    {
        private readonly ICacheRepository<MenuContent> _mcontentRepo;
        private readonly IMapper _mapper;

        public MBodyService(ICacheRepository<MenuContent> mcontentRepo, IMapper mapper)
        {
            _mcontentRepo = mcontentRepo;
            _mapper = mapper;
        }

        public async Task<List<MenuContentDTO>> GetMContents()
        {
            return _mapper.Map<List<MenuContentDTO>>(await _mcontentRepo.GetAllAsync());
        }

        public async Task<MenuContentDTO> GetMContentByMenuTitle(string url)
        {
            var entity = await _mcontentRepo.Query().SingleOrDefaultAsync(x => x.url == url);
            return _mapper.Map<MenuContentDTO>(entity);
        }

        public async Task<MenuContentDTO> GetMContentDetails(Guid id)
        {
            var entity = await _mcontentRepo.FindByIdAsync(id);
            return _mapper.Map<MenuContentDTO>(entity);
        }

        public async Task<OperationResult> InsertMContent(MenuContentDTO dto)
        {
            var entity = _mapper.Map<MenuContent>(dto);
            entity.id = Guid.NewGuid();
            if (await _mcontentRepo.AddAsync(entity) == null)
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");

            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }

        public async Task<OperationResult> UpdateMContent(MenuContentDTO dto)
        {
            var entity = _mapper.Map<MenuContent>(dto);
            if (await _mcontentRepo.UpdateAsync(entity) == null)
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");

            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }

        public async Task<OperationResult> DeleteMContent(Guid id)
        {
            var entity = await _mcontentRepo.FindByIdAsync(id);
            await _mcontentRepo.RemoveAsync(entity);
            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }
    }
}
