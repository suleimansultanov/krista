using AutoMapper;
using KristaShop.Business.DTOs;
using KristaShop.Business.Interfaces;
using KristaShop.Common.Models;
using KristaShop.DataAccess.Entities;
using KristaShop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristaShop.Business.Services
{
    public class UrlAclService : IUrlAclService
    {
        private readonly ICacheRepository<UrlAccess> _uaRepo;
        private readonly IMapper _mapper;

        public UrlAclService
            (ICacheRepository<UrlAccess> uaRepo, IMapper mapper)
        {
            _uaRepo = uaRepo;
            _mapper = mapper;
        }

        public async Task<List<UrlAccessDTO>> GetUrlAcls()
        {
            return _mapper.Map<List<UrlAccessDTO>>(await _uaRepo.GetAllAsync());
        }

        public async Task<UrlAccessDTO> GetUrlAclDetails(Guid id)
        {
            var entity = await _uaRepo.FindByIdAsync(id);
            return _mapper.Map<UrlAccessDTO>(entity);
        }

        public List<UrlAccessDTO> GetUrlAclsByAcl(int acl)
        {
            return _mapper.Map<List<UrlAccessDTO>>(_uaRepo.QueryFindBy(x => x.acl <= acl));
        }

        public async Task<OperationResult> InsertUrlAcl(UrlAccessDTO dto)
        {
            var entity = _mapper.Map<UrlAccess>(dto);
            entity.id = Guid.NewGuid();
            if (await _uaRepo.AddAsync(entity) == null)
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");

            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }

        public async Task<OperationResult> UpdateUrlAcl(UrlAccessDTO dto)
        {
            var entity = _mapper.Map<UrlAccess>(dto);
            if (await _uaRepo.UpdateAsync(entity) == null)
                return OperationResult.AlertFailure("Ошибка: процедура не выполнилась");

            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }

        public async Task<OperationResult> DeleteUrlAcl(Guid id)
        {
            var entity = await _uaRepo.FindByIdAsync(id);
            await _uaRepo.RemoveAsync(entity);
            return OperationResult.AlertSuccess("Успешно: процедура выполнилась");
        }
    }
}
