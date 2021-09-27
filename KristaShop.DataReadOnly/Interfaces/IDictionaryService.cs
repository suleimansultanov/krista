using KristaShop.DataReadOnly.DTOs;
using System.Collections.Generic;

namespace KristaShop.DataReadOnly.Interfaces
{
    public interface IDictionaryService
    {
        List<CityDTO> GetCities();
        List<UserGroupDTO> GetGroups();
        List<AccessControlDTO> GetAcls();
        List<SizeLineDTO> GetSizeLines();
        List<ColorDTO> GetColors();
        List<SizeDTO> GetSizes();
    }
}
