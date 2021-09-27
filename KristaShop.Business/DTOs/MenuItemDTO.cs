using System;
using System.Collections.Generic;
using System.Text;

namespace KristaShop.Business.DTOs
{
    public class MenuItemDTO
    {
        public Guid Id { get; set; }
        public int MenuType { get; set; }
        public string Title { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string URL { get; set; }
        public string Params { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
    }
}
