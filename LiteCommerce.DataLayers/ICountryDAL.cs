using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai báo các phép xử lý dữ liệu liên quán đến quốc gia
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// lấy được danh sách quốc gia
        /// </summary>
        /// <returns></returns>
        List<Country> List();
    }
}
