using KristaShop.Business.DTOs;
using KristaShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristaShop.Business.Interfaces
{
    public interface IUrlAclService
    {
        Task<UrlAccessDTO> GetUrlAclDetails(Guid id);
        Task<List<UrlAccessDTO>> GetUrlAcls();
        List<UrlAccessDTO> GetUrlAclsByAcl(int acl);
        Task<OperationResult> InsertUrlAcl(UrlAccessDTO dto);
        Task<OperationResult> UpdateUrlAcl(UrlAccessDTO dto);
        Task<OperationResult> DeleteUrlAcl(Guid id);
    }
}