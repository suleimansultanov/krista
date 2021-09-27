using System;
using System.Collections.Generic;

namespace KristaShop.Business.DTOs
{
    public class UrlAccessDTO
    {
        public Guid Id { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public int Acl { get; set; }
        public List<Guid> AccessGroupsJson { get; set; }
        public List<Guid> DeniedGroupsJson { get; set; }
    }
}
