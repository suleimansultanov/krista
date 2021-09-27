using System;
using System.Collections.Generic;
using System.Text;

namespace KristaShop.Business.DTOs
{
    public class PhotoDTO
    {
        public Guid Id { get; set; }
        public string PhotoPath { get; set; }
        public Guid ColorId { get; set; }
        public string ColorName { get; set; }
    }
}
