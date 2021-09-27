using KristaShop.DataReadOnly.DTOs;
using KristaShop.DataReadOnly.Interfaces;
using KristaShop.DataReadOnly.Models;
using System.Collections.Generic;
using System.Linq;

namespace KristaShop.DataReadOnly.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly IReadOnlyRepo<City> _cityRepo;
        private readonly IReadOnlyRepo<AccessControl> _aclRepo;
        private readonly IReadOnlyRepo<UserGroup> _ugRepo;
        private readonly IReadOnlyRepo<Color> _colorRepo;
        private readonly IReadOnlyRepo<Size> _sizeRepo;
        private readonly IReadOnlyRepo<SizeLine> _slRepo;

        public DictionaryService
            (IReadOnlyRepo<City> cityRepo, IReadOnlyRepo<AccessControl> aclRepo, IReadOnlyRepo<UserGroup> ugRepo,
            IReadOnlyRepo<Color> colorRepo, IReadOnlyRepo<Size> sizeRepo, IReadOnlyRepo<SizeLine> slRepo)
        {
            _cityRepo = cityRepo;
            _aclRepo = aclRepo;
            _ugRepo = ugRepo;
            _colorRepo = colorRepo;
            _sizeRepo = sizeRepo;
            _slRepo = slRepo;
        }

        public List<CityDTO> GetCities()
        {
            return _cityRepo.QueryReadOnly()
                .Select(x => new CityDTO
                {
                    Id = x.id,
                    Name = x.name
                }).ToList();
        }

        public List<UserGroupDTO> GetGroups()
        {
            return _ugRepo.QueryReadOnly()
                .Select(x => new UserGroupDTO
                {
                    GroupId = x.id,
                    Name = x.ugroup_name
                }).ToList();
        }

        public List<AccessControlDTO> GetAcls()
        {
            return _aclRepo.QueryReadOnly()
                .Select(x => new AccessControlDTO
                {
                    Id = x.acl,
                    Name = x.acl_name
                }).ToList();
        }

        public List<SizeDTO> GetSizes()
        {
            return _sizeRepo.QueryReadOnly()
                .Select(x => new SizeDTO
                {
                    Id = x.id,
                    Name = x.size_name,
                    Order = x.size_value
                }).ToList();
        }

        public List<SizeLineDTO> GetSizeLines()
        {
            return _slRepo.QueryReadOnly()
                .Select(x => new SizeLineDTO
                {
                    Id = x.id,
                    Name = x.size_line_name,
                    Order = x.size_line_pos
                }).ToList();
        }

        public List<ColorDTO> GetColors()
        {
            return _colorRepo.QueryReadOnly()
                .Select(x => new ColorDTO
                {
                    Id = x.id,
                    Name = x.color_name,
                    Code = x.color_code,
                    Image = x.color_image
                }).ToList();
        }
    }
}
