using System;
using System.Collections.Generic;
using System.Text;

namespace KristaShop.DataReadOnly.DTOs
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public int Status { get; set; }
        public bool IsRoot { get; set; }
        public List<UserGroupDTO> UserGroups { get; set; }
        public int AccessLevel { get; set; }
    }

    public class UserClientDTO
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string ClientFullName { get; set; }
        public string CityName { get; set; }
        public string PhoneNumber { get; set; }
        public string ShopName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
    }

    public class UserGroupDTO
    {
        public Guid GroupId { get; set; }
        public int UserType { get; set; }
        public string Name { get; set; }
        public int AccessLevel { get; set; }
    }
}
