using System;
using System.Collections.Generic;
using System.Text;

namespace KristaShop.DataAccess.Interfaces
{
    public interface ICacheRepository<T> : IShopRepository<T>
        where T : class
    {
    }
}
